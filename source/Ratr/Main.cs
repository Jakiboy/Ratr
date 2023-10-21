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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Binary files (*.bin)|*.bin";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.upload.Enabled = false;
                this.upload.Text = "Decoding...";

                // Upload
                string filePath = openFileDialog1.FileName;
                string fileName = guid + Path.GetFileName(filePath);
                string binFile = Path.Combine(tempFolder, fileName);
                File.Copy(filePath, binFile);

                // Decode init
                string decoder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".\\decode.exe");
                string xmlFile = Path.Combine(tempFolder, guid + "config.xml");
                decoder = Path.GetFullPath(decoder);
                string args = $"/c {decoder} {binFile} {xmlFile} --try-all-known-keys";

                // Decode process
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                // Parse
                ParseXML(xmlFile);
            }

            this.upload.Enabled = true;
            this.upload.Text = "Upload .bin";
        }

        private void ParseXML(string xmlFile)
        {
            // Get file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            // Get nodes
            XmlNode dbNode = xmlDoc.SelectSingleNode("//DB");
            XmlNode tblNode = dbNode.SelectSingleNode("//Tbl[@name='PPPIF']");
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