using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using Entity;
using BusinessLayer;
using System.Collections.Generic;
using System.IO;

namespace RunLoader
{
    public partial class FileAccessForm : Form
    {
        private List<string> InputCheckedNodes = new List<string>();
        private List<string> OutputCheckedNodes = new List<string>();
        private bool FirstConnect = true;

        private Dictionary<string, string> ListofFileNames = new Dictionary<string, string>();

        public FileAccessForm()
        {
            InitializeComponent();
        }

        private enum OrderBy
        {
            FileName = 1,
            Size = 2,
            Type = 3,
            DateUploaded = 4
        }



        private const string FileDialogFilter = "Microsoft Access (*.accdb , *.mdb)|*.accdb;*.mdb|SQLite (*.db)|*.db|All Files (*.*)|*.*";
        private const string DATA_FILE_FILTER = "All Files (*.*)|*.*";

        private void btn_BrowseAccess_Click(object sender, EventArgs e)
        {
            //Instantiate dialog
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                //Enable upgrade for new Vista framework
                dlg.AutoUpgradeEnabled = true;
                //Initial directory is personal documents
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //Filter using database extensions
                dlg.Filter = FileDialogFilter;
                //dlg.ShowDialog();
                //If textbox is empty, then fil
                if (dlg.ShowDialog() == DialogResult.OK && txt_FileLocation.Text.Length == 0)
                {
                    txt_FileLocation.Text = dlg.FileName;
                }
                else
                {
                    return;
                }

                //PopulateListView();
            }

        }

        private void UpdateStatusConsole(string message)
        {
            this.BeginInvoke
                        (new Action(() =>
                        {
                            //add message to status console
                            this.txt_status.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + " - " + message);
                            //add new line to status console
                            this.txt_status.AppendText(Environment.NewLine);
                        }
                        ));

        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstConnect)
                {
                    try
                    {
                        //Test connection and store in datafactory
                        IDbConnection conn = DataFactory.CreateConnection(this.txt_FileLocation.Text);
                        UpdateStatusConsole(string.Format("Connection established with {0}", this.txt_FileLocation.Text));
                        FirstConnect = false;

                    }
                    catch (Exception ex)
                    {
                        UpdateStatusConsole(ex.Message);
                    }

                }
                else
                {
                    try
                    {
                        DataFactory.ActiveConn.Open();
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusConsole(ex.Message);
                    }
                }

                if (DataFactory.ActiveConn.State == ConnectionState.Open)
                {

                    this.btn_ConnectDB.Text = "Connected";
                    this.btn_ConnectDB.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UpdateStatusConsole(string.Format("Connection Failed due to error '{0}'", ex.Message));
            }
            //conn = DataFactory.CreateConnection(DatabaseType.Oracle,"192.168.1.252:1521/ORCL");
        }


        private void btn_SelectOutput_Click(object sender, EventArgs e)
        {
            //Instantiate folder browser
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                //Show dialog
                fbd.ShowDialog();
                //If selectedpath is not 0
                if (fbd.SelectedPath.Length > 0)
                {
                    this.txt_Output.Text = fbd.SelectedPath;
                }
            }

        }

        private void btn_LoadFileTable_Click(object sender, EventArgs e)
        {
            if (DataFactory.ActiveConn != null && DataFactory.ActiveConn.State != ConnectionState.Open)
            {
                //UpdateStatusConsole("Connection is not open. Attempting to re-open Connection now.");
                btn_Connect_Click(sender, e);
            }

            try
            {
                this.btn_LoadFileTable.Enabled = false;
                //create new files
                using (Files obj = new Files())
                {
                    //Get list of file names
                    ListofFileNames = obj.getFileNameList();
                    //Populate list of file names
                    if (ListofFileNames.Count != 0)
                    {
                        PopulateTreeView();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                UpdateStatusConsole(string.Format("Load table failed due to '{0}'.", ex.Message));
            }
            finally
            {
                this.btn_LoadFileTable.Enabled = true;
            }


        }
        private void ListFiles(TreeView treeView, object[] objectIDs)
        {
            if (objectIDs.Length == 0)
            {
                UpdateStatusConsole(string.Format("No Files selected"));
            }
            else
            {
                foreach (string id in objectIDs)
                {
                    TreeNode fileNode = new TreeNode(id);

                    fileNode.Name = id;
                    treeView.Nodes.Add(fileNode);
                    fileNode.Checked = true;
                }
            }
        }

        private void PopulateTreeView()
        {
            //Create top node if doesn't exist
            if (tv_OutputFiles.Nodes.ContainsKey("Select All") != true)
            { tv_OutputFiles.Nodes.Add(new TreeNode("Select All") { Name = "Select All" }); }

            //Populate child nodes
            try
            {
                TreeNode topNode = tv_OutputFiles.Nodes["Select All"];
                TreeNodeCollection childnodes = topNode.Nodes;
                foreach (string filename in ListofFileNames.Values)
                {
                    if (childnodes.ContainsKey(filename) == false)
                    {
                        topNode.Nodes.Add(filename, filename);
                    }

                }
                GC.Collect();

            }
            catch (Exception ex)
            {
                UpdateStatusConsole(string.Format("Update table error - {0}.", ex.Message));
            }
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (TreeNode node in tv_OutputFiles.Nodes["Select All"].Nodes)
                {
                    if (node.Checked == true)
                    {
                        using (Files fs = new Files())
                        {
                            //Need to find list of checked output files
                            byte[] bindata = fs[node.Text].FileContent;
                            //byte[] bindata = ListofFileEntity[node.Name].FileContent;
                            File.WriteAllBytes(string.Format(@"{0}\{1}", this.txt_Output.Text, fs[node.Text].FileName), bindata);
                            UpdateStatusConsole(string.Format("{0} Downloaded.", fs[node.Text].FileName));
                        }
                        
                    }

                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                UpdateStatusConsole(ex.Message);
            }

        }

        private void btn_SelectInputFiles_Click(object sender, EventArgs e)
        {
            ListFiles(tv_InputFiles, SelectFiles());
        }

        private string[] SelectFiles()
        {
            //Instantiate file dialog
            using (OpenFileDialog opd = new OpenFileDialog())
            {


                opd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                //Allow multiple files
                opd.Multiselect = true;
                opd.ShowDialog();
                return opd.FileNames;
            }
        }



        private void btn_Upload_Click(object sender, EventArgs e)
        {
            foreach (string checkednode in InputCheckedNodes)
            {
                byte[] data;
                using (FileStream fs = new System.IO.FileStream(checkednode, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    BinaryReader reader = new System.IO.BinaryReader(fs);
                    data = reader.ReadBytes((int)fs.Length);
                }
                //Instantiate new file
                using (Files FE = new Files())
                {
                    FileInfo fi = new FileInfo(tv_InputFiles.Nodes[checkednode].Text);

                    //Populate object
                    FE.FileName = fi.Name;
                    FE.FileContent = data;
                    FE.DateUploaded = GetDateWithoutMilliseconds(DateTime.Now);
                    FE.Type = fi.Extension;
                    FE.Size = data.Length;
                    //Add to DB
                    FE.Add();
                }
                GC.Collect();

            }
        }

        private DateTime GetDateWithoutMilliseconds(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }

        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
                if (node.Nodes.Count == 0)
                {
                    if (node.Checked == true)
                    {
                        //CheckedNodes.Add(node.Text);

                    }
                    else if (node.Checked == false)
                    {
                        //CheckedNodes.Remove(node.Text);

                    }
                }
            }
        }


        private void tv_InputFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            //if (e.Action != TreeViewAction.Unknown)
            //{
            if (e.Node.Nodes.Count > 0)
            {
                /* Calls the CheckAllChildNodes method, passing in the current 
                Checked value of the TreeNode whose checked state changed. */
                //this.CheckAllChildNodes(e.Node, e.Node.Checked);
            }
            else if (e.Node.Nodes.Count == 0)
            {
                if (e.Node.Checked == true)
                {
                    InputCheckedNodes.Add(e.Node.Text);
                }
                else if (e.Node.Checked == false)
                {
                    InputCheckedNodes.Remove(e.Node.Text);
                }
            }

        }

        private void txt_FileLocation_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_FileLocation.TextLength > 0)
            {
                this.btn_ConnectDB.Enabled = true;
            }
            else
            {
                this.btn_ConnectDB.Enabled = false;
            }
        }

        private void txt_Output_TextChanged(object sender, EventArgs e)
        {
            if (txt_Output.TextLength > 0 && OutputCheckedNodes.Count > 0)
            {
                this.btn_Download.Enabled = true;
            }
            else
            {
                this.btn_Download.Enabled = false;
            }
        }

        private void tv_OutputFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Node.Nodes.Count > 0)
            {
                /* Calls the CheckAllChildNodes method, passing in the current 
                Checked value of the TreeNode whose checked state changed. */
                this.CheckAllChildNodes(e.Node, e.Node.Checked);
            }
            else if (e.Node.Nodes.Count == 0)
            {
                if (e.Node.Checked == true)
                {
                    OutputCheckedNodes.Add(e.Node.Text);
                    this.txt_Output_TextChanged(sender, e);
                }
                else if (e.Node.Checked == false)
                {
                    OutputCheckedNodes.Remove(e.Node.Text);
                    this.txt_Output_TextChanged(sender, e);
                }

            }
        }



        //private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        //{
        //    TreeNode directoryNode = new TreeNode(directoryInfo.FullName);
        //    directoryNode.Checked = true;
        //    foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        //    {
        //        directoryNode.Nodes.Add(directory.FullName, directory.FullName);
        //        directoryNode.Nodes[directory.FullName].Checked = true;
        //        CheckedNodes.Add(directoryNode.Nodes[directory.FullName].Text.ToString());
        //    }
        //    //recursive sub-directories
        //    //foreach (var directory in directoryInfo.GetDirectories())
        //    //    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
        //    //files 
        //    //foreach (var file in directoryInfo.GetFiles())
        //    //    directoryNode.Nodes.Add(new TreeNode(file.Name));
        //    return directoryNode;
        //}
    }
}
