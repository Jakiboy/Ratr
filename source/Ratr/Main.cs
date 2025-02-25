using System.Diagnostics;
using System.Xml;

namespace Ratr
{
    public partial class Main : Form
    {
        private readonly string tempFolder;
        private readonly string guid;
        private string modelName;

        public Main()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.tempFolder = Path.GetTempPath();
            this.guid = Guid.NewGuid().ToString() + "-";
            this.modelName = "";
        }

        private void FormLoad(object sender, EventArgs e)
        {
            model.SelectedIndex = 0;
        }

        private void UploadClick(object sender, EventArgs e)
        {
            string filter = "Binary files (*.bin)|*.bin";
            if (this.modelName == "huawei-dg8245v-10")
            {
                filter = "XML files (*.xml)|*.xml";
            }

            OpenFileDialog openFileDialog = new()
            {
                Filter = filter
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Disable upload button and update text
                    this.upload.Enabled = false;
                    this.upload.Text = "Decoding...";

                    // Upload
                    string filePath = openFileDialog.FileName;
                    string fileName = guid + Path.GetFileName(filePath);
                    string configFile = Path.Combine(tempFolder, fileName);

                    // Ensure the temp folder exists
                    if (!Directory.Exists(tempFolder))
                    {
                        Directory.CreateDirectory(tempFolder);
                    }

                    // Copy the file to the temp folder
                    File.Copy(filePath, configFile, overwrite: true); // Overwrite if file already exists

                    // Decode init
                    string decoder = "decoder/zte.exe";
                    if (this.modelName == "huawei-dg8245v-10")
                    {
                        decoder = "decoder/huawei.exe";
                    }

                    decoder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, decoder);
                    string xmlFile = Path.Combine(tempFolder, guid + "config.xml");
                    decoder = Path.GetFullPath(decoder);

                    // Check if the decoder executable exists
                    if (!File.Exists(decoder))
                    {
                        MessageBox.Show($"Decoder is missing. Please ensure '{decoder}' exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string args = "";
                    if (this.modelName == "zte-zxhn-h267n")
                    {
                        args = $"/c {decoder} {configFile} {xmlFile} --try-all-known-keys";
                    }
                    else if (this.modelName == "huawei-dg8245v-10")
                    {
                        args = $"/c {decoder} --file {configFile} --output {xmlFile}";
                    }

                    // Decode process
                    Process process = new()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = args,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    process.WaitForExit();

                    // Check if the decoding process succeeded
                    if (process.ExitCode != 0)
                    {
                        MessageBox.Show("Failed to decode config file. Please ensure the file is valid and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Parse decoded XML file
                    if (this.modelName == "zte-zxhn-h267n")
                    {
                        ParseZteXML(xmlFile);
                    }
                    else if (this.modelName == "huawei-dg8245v-10")
                    {
                        ParseHuaweiXML(xmlFile);
                    }
                    
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Re-enable the upload button and reset its text
                    this.upload.Enabled = true;
                    this.upload.Text = "Choose file";
                }
            }
        }

        private void ParseZteXML(string xmlFile)
        {
            try
            {
                // Get file
                XmlDocument xmlDoc = new();
                xmlDoc.Load(xmlFile);

                // Get nodes
                XmlNode? dbNode = xmlDoc.SelectSingleNode("//DB");
                if (dbNode == null)
                {
                    MessageBox.Show("The XML file does not contain a valid 'DB' node.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlNode? tblNode = dbNode.SelectSingleNode("//Tbl[@name='PPPIF']");
                if (tblNode == null)
                {
                    MessageBox.Show("The 'PPPIF' table was not found in the XML file.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlNode? uNode = tblNode.SelectSingleNode(".//DM[@name='Username']");
                XmlNode? pNode = tblNode.SelectSingleNode(".//DM[@name='Password']");

                // Set data
                if (uNode != null)
                {
                    this.username.Text = uNode.Attributes?["val"]?.Value ?? "Undefined";
                    this.username.Enabled = true;
                }
                else
                {
                    this.username.Text = "Error!";
                }

                if (pNode != null)
                {
                    this.password.Text = pNode.Attributes?["val"]?.Value ?? "Undefined";
                    this.password.Enabled = true;
                }
                else
                {
                    this.password.Text = "Error!";
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("The specified XML file was not found. Please check the file path and try again.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Xml.XmlException)
            {
                MessageBox.Show("The XML file is not valid or is corrupted. Please check the file and try again.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ParseHuaweiXML(string xmlFile)
        {
            try
            {
                // Load the XML document
                XmlDocument xmlDoc = new();
                xmlDoc.Load(xmlFile);

                // Navigate to the first <LineInstance> node
                XmlNode? lineInstanceNode = xmlDoc.SelectSingleNode("//LineInstance");
                if (lineInstanceNode == null)
                {
                    MessageBox.Show("The XML file does not contain a valid 'LineInstance' node.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Navigate to the <SIP> node within the <LineInstance>
                XmlNode? sipNode = lineInstanceNode.SelectSingleNode(".//SIP");
                if (sipNode == null)
                {
                    MessageBox.Show("The 'SIP' node was not found in the XML file.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extract AuthUserName and AuthPassword
                XmlNode? authUserNameNode = sipNode.SelectSingleNode(".//@AuthUserName");
                XmlNode? authPasswordNode = sipNode.SelectSingleNode(".//@AuthPassword");

                // Set data
                if (authUserNameNode != null)
                {
                    this.username.Text = authUserNameNode.Value;
                    this.username.Enabled = true;
                }
                else
                {
                    this.username.Text = "Error!";
                }

                if (authPasswordNode != null)
                {
                    this.password.Text = authPasswordNode.Value;
                    this.password.Enabled = true;
                }
                else
                {
                    this.password.Text = "Error!";
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("The specified XML file was not found. Please check the file path and try again.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Xml.XmlException)
            {
                MessageBox.Show("The XML file is not valid or is corrupted. Please check the file and try again.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AuthorClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = new ProcessStartInfo("https://github.com/Jakiboy")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(url);
        }

        private void HowClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = new ProcessStartInfo("https://github.com/Jakiboy/Ratr/blob/main/HOW.md")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(url);
        }

        private void FormClose(object sender, FormClosedEventArgs e)
        {
            // CLean temp
            string[] files = Directory.GetFiles(tempFolder, $"{guid}*");
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        private void ModelSelectedIndexChanged(object sender, EventArgs e)
        {
            string? value = model.SelectedItem.ToString();
            ResetForm();
            switch (value)
            {
                case "ZTE (ZXHN H267N)":
                    this.modelName = "zte-zxhn-h267n";
                    upload.Enabled = true;
                    break;
                case "Huawei (DG8245V-10)":
                    this.modelName = "huawei-dg8245v-10";
                    upload.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void AboutMenuItemClick(object sender, EventArgs e)
        {
            string aboutText = "Ratr v0.2.0 (By Jakiboy).\n\nhttps://github.com/Jakiboy/Ratr";
            MessageBox.Show(aboutText, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HowToExportMenuItemClick(object sender, EventArgs e)
        {
            var url = new ProcessStartInfo("https://github.com/Jakiboy/Ratr/blob/main/HOW.md")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(url);
        }

        private void ResetForm()
        {
            this.upload.Enabled = this.username.Enabled = this.password.Enabled = false;
            this.username.Text = this.password.Text = "";
        }
    }
}