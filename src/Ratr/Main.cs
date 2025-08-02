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
                    string decoder = "";
                    switch (this.modelName)
                    {
                        case "zte-zxhn-f600w":
                        case "zte-zxhn-h108n-2-5":
                        case "zte-zxhn-h168n-3-1":
                        case "zte-zxhn-h168n-3-5":
                        case "zte-zxhn-h188a":
                        case "zte-zxhn-h267a":
                        case "zte-zxhn-h267n":
                        case "zte-zxhn-h268n":
                        case "zte-zxhn-h268q":
                        case "zte-zxhn-h288a":
                        case "zte-zxhn-h298a":
                        case "zte-zxhn-h298n":
                        case "zte-zxhn-h298q":
                        case "zte-zxv10-h201l-2-0":
                            decoder = "decoder/zte.exe";
                            break;
                        case "huawei-dg8245v-10":
                            decoder = "decoder/huawei.exe";
                            break;
                        default:
                            MessageBox.Show("Unsupported model selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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
                    switch (this.modelName)
                    {
                        case "zte-zxhn-f600w":
                        case "zte-zxhn-h108n-2-5":
                        case "zte-zxhn-h168n-3-1":
                        case "zte-zxhn-h168n-3-5":
                        case "zte-zxhn-h188a":
                        case "zte-zxhn-h267a":
                        case "zte-zxhn-h267n":
                        case "zte-zxhn-h268n":
                        case "zte-zxhn-h268q":
                        case "zte-zxhn-h288a":
                        case "zte-zxhn-h298a":
                        case "zte-zxhn-h298n":
                        case "zte-zxhn-h298q":
                        case "zte-zxv10-h201l-2-0":
                            args = $"/c {decoder} {configFile} {xmlFile} --try-all-known-keys";
                            break;
                        case "huawei-dg8245v-10":
                            args = $"/c {decoder} --silent --file {configFile} --output {xmlFile}";
                            break;
                        default:
                            MessageBox.Show("Unsupported model selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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
                    switch (this.modelName)
                    {
                        case "zte-zxhn-f600w":
                            ParseZteZxhnF600wXml(xmlFile);
                            break;
                        case "zte-zxhn-h108n-2-5":
                            ParseZteZxhnH108n25Xml(xmlFile);
                            break;
                        case "zte-zxhn-h168n-3-1":
                            ParseZteZxhnH108n31Xml(xmlFile);
                            break;
                        case "zte-zxhn-h168n-3-5":
                            ParseZteZxhnH168n35Xml(xmlFile);
                            break;
                        case "zte-zxhn-h188a":
                            ParseZteZxhnH188aXml(xmlFile);
                            break;
                        case "zte-zxhn-h267a":
                            ParseZteZxhnH267aXml(xmlFile);
                            break;
                        case "zte-zxhn-h267n":
                            ParseZteZxhnH267nXml(xmlFile);
                            break;
                        case "zte-zxhn-h268n":
                            ParseZteZxhnH268nXml(xmlFile);
                            break;
                        case "zte-zxhn-h268q":
                            ParseZteZxhnH268qXml(xmlFile);
                            break;
                        case "zte-zxhn-h288a":
                            ParseZteZxhnH288aXml(xmlFile);
                            break;
                        case "zte-zxhn-h298a":
                            ParseZteZxhnH298aXml(xmlFile);
                            break;
                        case "zte-zxhn-h298n":
                            ParseZteZxhnH298nXml(xmlFile);
                            break;
                        case "zte-zxhn-h298q":
                            ParseZteZxhnH298qXml(xmlFile);
                            break;
                        case "zte-zxv10-h201l-2-0":
                            ParseZteZxv10H201l20Xml(xmlFile);
                            break;
                        case "huawei-dg8245v-10":
                            ParseHuaweiDg8245v10Xml(xmlFile);
                            break;
                        default:
                            MessageBox.Show("Unsupported model selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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

        private void ParseZteZxhnH267aXml(string xmlFile)
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
                    MessageBox.Show("The 'PPP' configuration was not found.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string? usernameValue = null;
                string? passwordValue = null;

                // Get all rows from the PPPIF table
                XmlNodeList? rows = tblNode.SelectNodes(".//Row");
                if (rows == null || rows.Count == 0)
                {
                    MessageBox.Show("No rows found in the PPPIF table.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Iterate through rows to find non-empty username and password
                for (int i = 0; i < rows.Count; i++)
                {
                    XmlNode? uNode = rows[i]?.SelectSingleNode("./DM[@name='Username']");
                    XmlNode? pNode = rows[i]?.SelectSingleNode("./DM[@name='Password']");

                    string? tempUsername = uNode?.Attributes?["val"]?.Value;
                    string? tempPassword = pNode?.Attributes?["val"]?.Value;

                    // If we found both username and password that are not empty, use them
                    if (!string.IsNullOrEmpty(tempUsername) && !string.IsNullOrEmpty(tempPassword))
                    {
                        usernameValue = tempUsername;
                        passwordValue = tempPassword;
                        break; // Found valid credentials, stop searching
                    }

                    // If we only found username or password (but not both), keep them as fallback
                    if (string.IsNullOrEmpty(usernameValue) && !string.IsNullOrEmpty(tempUsername))
                    {
                        usernameValue = tempUsername;
                    }

                    if (string.IsNullOrEmpty(passwordValue) && !string.IsNullOrEmpty(tempPassword))
                    {
                        passwordValue = tempPassword;
                    }
                }

                // Set data
                if (!string.IsNullOrEmpty(usernameValue))
                {
                    this.username.Text = usernameValue;
                    this.username.Enabled = true;
                }
                else
                {
                    this.username.Text = "Empty!";
                }

                if (!string.IsNullOrEmpty(passwordValue))
                {
                    this.password.Text = passwordValue;
                    this.password.Enabled = true;
                }
                else
                {
                    this.password.Text = "Empty!";
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

        private void ParseZteZxhnH188aXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN H267A
            ParseZteZxhnH267aXml(xmlFile);
        }

        private void ParseZteZxhnH267nXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN H267A
            ParseZteZxhnH267aXml(xmlFile);
        }

        private void ParseZteZxhnH268nXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH268qXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH288aXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH298aXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH298nXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH298qXml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxv10H201l20Xml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnF600wXml(string xmlFile)
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

                XmlNode? tblNode = dbNode.SelectSingleNode("//Tbl[@name='WANCPPP']");
                if (tblNode == null)
                {
                    MessageBox.Show("The 'PPP' configuration was not found.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string? usernameValue = null;
                string? passwordValue = null;

                // Get all rows from the WANCPPP table
                XmlNodeList? rows = tblNode.SelectNodes(".//Row");
                if (rows == null || rows.Count == 0)
                {
                    MessageBox.Show("No rows found in the WANCPPP table.", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Iterate through rows to find non-empty username and password
                for (int i = 0; i < rows.Count; i++)
                {
                    XmlNode? uNode = rows[i]?.SelectSingleNode("./DM[@name='UserName']");
                    XmlNode? pNode = rows[i]?.SelectSingleNode("./DM[@name='Password']");

                    string? tempUsername = uNode?.Attributes?["val"]?.Value;
                    string? tempPassword = pNode?.Attributes?["val"]?.Value;

                    // If we found both username and password that are not empty, use them
                    if (!string.IsNullOrEmpty(tempUsername) && !string.IsNullOrEmpty(tempPassword))
                    {
                        usernameValue = tempUsername;
                        passwordValue = tempPassword;
                        break; // Found valid credentials, stop searching
                    }

                    // If we only found username or password (but not both), keep them as fallback
                    if (string.IsNullOrEmpty(usernameValue) && !string.IsNullOrEmpty(tempUsername))
                    {
                        usernameValue = tempUsername;
                    }

                    if (string.IsNullOrEmpty(passwordValue) && !string.IsNullOrEmpty(tempPassword))
                    {
                        passwordValue = tempPassword;
                    }
                }

                // Set data
                if (!string.IsNullOrEmpty(usernameValue))
                {
                    this.username.Text = usernameValue;
                    this.username.Enabled = true;
                }
                else
                {
                    this.username.Text = "Empty!";
                }

                if (!string.IsNullOrEmpty(passwordValue))
                {
                    this.password.Text = passwordValue;
                    this.password.Enabled = true;
                }
                else
                {
                    this.password.Text = "Empty!";
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

        private void ParseZteZxhnH108n25Xml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH108n31Xml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN F600W
            ParseZteZxhnF600wXml(xmlFile);
        }

        private void ParseZteZxhnH168n35Xml(string xmlFile)
        {
            // Same parsing logic as for ZTE ZXHN H267A
            ParseZteZxhnH267aXml(xmlFile);
        }

        private void ParseHuaweiDg8245v10Xml(string xmlFile)
        {
            try
            {
                // Load the XML document
                XmlDocument xmlDoc = new();
                xmlDoc.Load(xmlFile);

                // Get all WANPPPConnectionInstance nodes (PPP internet connections)
                XmlNodeList? pppConnectionNodes = xmlDoc.SelectNodes("//WANPPPConnectionInstance");
                if (pppConnectionNodes == null || pppConnectionNodes.Count == 0)
                {
                    MessageBox.Show("The 'PPP' configuration was not found", "Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string? usernameValue = null;
                string? passwordValue = null;

                // Iterate through all WANPPPConnectionInstance nodes to find non-empty credentials
                for (int i = 0; i < pppConnectionNodes.Count; i++)
                {
                    XmlNode? pppConnection = pppConnectionNodes[i];

                    if (pppConnection != null)
                    {
                        string? tempUsername = pppConnection.Attributes?["Username"]?.Value;
                        string? tempPassword = pppConnection.Attributes?["Password"]?.Value;

                        // If we found both username and password that are not empty, use them
                        if (!string.IsNullOrEmpty(tempUsername) && !string.IsNullOrEmpty(tempPassword))
                        {
                            usernameValue = tempUsername;
                            passwordValue = tempPassword;
                            break; // Found valid credentials, stop searching
                        }

                        // If we only found username or password (but not both), keep them as fallback
                        if (string.IsNullOrEmpty(usernameValue) && !string.IsNullOrEmpty(tempUsername))
                        {
                            usernameValue = tempUsername;
                        }

                        if (string.IsNullOrEmpty(passwordValue) && !string.IsNullOrEmpty(tempPassword))
                        {
                            passwordValue = tempPassword;
                        }
                    }
                }

                // Set data
                if (!string.IsNullOrEmpty(usernameValue))
                {
                    this.username.Text = usernameValue;
                    this.username.Enabled = true;
                }
                else
                {
                    this.username.Text = "Empty!";
                }

                if (!string.IsNullOrEmpty(passwordValue))
                {
                    this.password.Text = passwordValue;
                    this.password.Enabled = true;
                }
                else
                {
                    this.password.Text = "Empty!";
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
                case "Huawei (DG8245V-10)":
                    this.modelName = "huawei-dg8245v-10";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN F600W)":
                    this.modelName = "zte-zxhn-f600w";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H108N-2.5)":
                    this.modelName = "zte-zxhn-h108n-2-5";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H168N-3.1)":
                    this.modelName = "zte-zxhn-h168n-3-1";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H168N-3.5)":
                    this.modelName = "zte-zxhn-h168n-3-5";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H188A)":
                    this.modelName = "zte-zxhn-h188a";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H267A)":
                    this.modelName = "zte-zxhn-h267a";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H267N)":
                    this.modelName = "zte-zxhn-h267n";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H268N)":
                    this.modelName = "zte-zxhn-h268n";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H268Q)":
                    this.modelName = "zte-zxhn-h268q";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H288A)":
                    this.modelName = "zte-zxhn-h288a";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H298A)":
                    this.modelName = "zte-zxhn-h298a";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H298N)":
                    this.modelName = "zte-zxhn-h298n";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXHN H298Q)":
                    this.modelName = "zte-zxhn-h298q";
                    upload.Enabled = true;
                    break;
                case "ZTE (ZXV10 H201L-2.0)":
                    this.modelName = "zte-zxv10-h201l-2-0";
                    upload.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void AboutMenuItemClick(object sender, EventArgs e)
        {
            string aboutText = "Ratr v0.3.0 (By Jakiboy).\n\nhttps://github.com/Jakiboy/Ratr";
            MessageBox.Show(aboutText, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HowToExportMenuItemClick(object sender, EventArgs e)
        {
            var url = new ProcessStartInfo("https://github.com/Jakiboy/Ratr/blob/main/How.md")
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