using System.Diagnostics;
using System.Xml;

namespace Ratr
{
    public partial class Main : Form
    {
        private readonly string tempFolder;
        private readonly string guid;

        public Main()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.tempFolder = Path.GetTempPath();
            this.guid = Guid.NewGuid().ToString() + "-";
        }

        private void FormLoad(object sender, EventArgs e) { }

        private void UploadClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin"
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
                    string binFile = Path.Combine(tempFolder, fileName);

                    // Ensure the temp folder exists
                    if (!Directory.Exists(tempFolder))
                    {
                        Directory.CreateDirectory(tempFolder);
                    }

                    // Copy the file to the temp folder
                    File.Copy(filePath, binFile, overwrite: true); // Overwrite if file already exists

                    // Decode init
                    string decoder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "decode.exe");
                    string xmlFile = Path.Combine(tempFolder, guid + "config.xml");
                    decoder = Path.GetFullPath(decoder);

                    // Check if the decoder executable exists
                    if (!File.Exists(decoder))
                    {
                        MessageBox.Show("Decoder is missing. Please ensure 'decode.exe' is placed in the root folder.", "Decoder Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string args = $"/c {decoder} {binFile} {xmlFile} --try-all-known-keys";

                    // Decode process
                    Process process = new Process
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
                        MessageBox.Show("Failed to decode the .bin file. Please ensure the file is valid and try again.", "Decoding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Parse the decoded XML file
                    ParseXML(xmlFile);
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
                    this.upload.Text = "Upload .bin";
                }
            }
        }

        private void ParseXML(string xmlFile)
        {
            try
            {
                // Get file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);

                // Get nodes
                XmlNode dbNode = xmlDoc.SelectSingleNode("//DB");
                if (dbNode == null)
                {
                    MessageBox.Show("The XML file does not contain a valid 'DB' node.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlNode tblNode = dbNode.SelectSingleNode("//Tbl[@name='PPPIF']");
                if (tblNode == null)
                {
                    MessageBox.Show("The 'PPPIF' table was not found in the XML file.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XmlNode uNode = tblNode.SelectSingleNode(".//DM[@name='Username']");
                XmlNode pNode = tblNode.SelectSingleNode(".//DM[@name='Password']");

                // Set data
                if (uNode != null)
                {
                    this.username.Text = uNode.Attributes["val"].Value;
                }
                else
                {
                    this.username.Text = "Error!";
                }

                if (pNode != null)
                {
                    this.password.Text = pNode.Attributes["val"].Value;
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
    }
}