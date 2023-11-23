using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FreezerTempMonitoringSystem
{
    public partial class LogForm : Form
    {
        string slogFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\Log\"));

        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {            
            Width = 1200;
            Height = 850;
            Top = 130;
            Left = 715;                        

            LoadDirectory(slogFilePath);            
        }

        private void LogForm_Activated(object sender, EventArgs e)
        {
            SetDoubleBuffered(treeView);
            SetDoubleBuffered(richTextBox);            
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            Dispose();
        }

        private void SetDoubleBuffered(Control control, bool doubleBuffered = true)
        {
            PropertyInfo propertyInfo = typeof(Control).GetProperty
            (
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            propertyInfo.SetValue(control, doubleBuffered, null);
        }

        private void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            TreeNode tn = treeView.Nodes.Add(di.Name);
            tn.Tag = di.FullName;
            LoadFiles(Dir, tn);
            LoadSubDirectories(Dir, tn);
            //treeView.ExpandAll();
            if (di.Name == "Log")
                tn.Expand();
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            string[] Files = Directory.GetFiles(dir, "*.*");

            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tn = td.Nodes.Add(fi.Name);
                tn.Tag = fi.FullName;
                tn.ImageIndex = 1;
                tn.SelectedImageIndex = 1;
            }
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tn = td.Nodes.Add(di.Name);
                tn.ImageIndex = 0;
                tn.SelectedImageIndex = 0;
                tn.Tag = di.FullName;
                LoadFiles(subdirectory, tn);
                LoadSubDirectories(subdirectory, tn);
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            richTextBox.Clear();

            if (e.Node.Text.Contains(".txt"))
            {
                string sBasePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\"));
                string sNodePath = e.Node.FullPath;
                string sFullPath = sBasePath + sNodePath;

                richTextBox.LoadFile(sFullPath, RichTextBoxStreamType.PlainText);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();

            LoadDirectory(slogFilePath);
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox.Font = fd.Font;
            }
        }        
    }
}
