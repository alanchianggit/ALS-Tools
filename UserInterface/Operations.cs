using System;
using System.Drawing;
using System.Windows.Forms;
using DAL.Factory;
using Auth;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace ALSTools
{
    public partial class Operations : Form
    {

        private static Operations inst;
        public static Operations Instance
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new Operations();
                return inst;
            }
        }
        public Operations()
        {
            InitializeComponent();
            DataLayer.Instance.Reset();
            AuthEntity.Instance.Reset();
            newSigninToolStripMenuItem.PerformClick();
            Size newsize = new Size(1800, 1000);
            this.Size = newsize;
        }

        private void fileAccessFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileAccessForm.GetForm.Show();
        }

        private void archiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archiver.ArchiverForm.GetForm.Show();
        }

        private void analysisManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;

            //ShowForm(strType);

            if (!strType.Contains("Analysis_Management") || frm == null || frm.IsDisposed)
            {
                //frm = new Analysis_Management();
                frm = Analysis_Management.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(frm);

        }

        private void ShowForm(string frmName)
        {
            if (frm == null || frm.IsDisposed) { return; }

            if (!frmName.Contains("Analysis_Management"))
            { frm = Analysis_Management.GetForm; }
            else
            {

            }

            if (!frmName.Contains("Analysis_Management") || frm == null || frm.IsDisposed)
            {
                //frm = new Analysis_Management();
                frm = Analysis_Management.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
        }

        public void ClickEvent()
        {
            eventsWindowsToolStripMenuItem.PerformClick();
        }

        private void ShowChildForm(Form fm)
        {

            fm.MdiParent = this;
            fm.Show();
            fm.BringToFront();
        }

        Form frm;
        private void eventsWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AuthEntity.Authenticated)
            {
                string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
                if (!strType.Contains("Event") || (frm == null || frm.IsDisposed))
                {
                    //frm = new frm_Event();
                    frm = frm_Event.GetForm;
                }
                else
                {
                    frm.WindowState = FormWindowState.Normal;
                }
                ShowChildForm(frm);
            }
            else
            {
                MessageBox.Show("Need to sign-in first.");
            }
        }

        private void productionManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AuthEntity.Authenticated)
            {
                string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
                if (!strType.Contains("Production") || (frm == null || frm.IsDisposed))
                {
                    //frm = new Production();
                    frm = ALSTools.Production.GetForm;
                }
                else
                {
                    frm.WindowState = FormWindowState.Normal;
                }
                ShowChildForm(frm);
            }
            else
            {
                MessageBox.Show("Need to sign-in first.");
            }
        }

        private void newSigninToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
            if (!strType.Contains("Auth") || (frm == null || frm.IsDisposed))
            {
                //frm = new frmAuth();
                frm = frmAuth.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(frm);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xMLControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
            if (!strType.Contains("XMLControl") || (frm == null || frm.IsDisposed))
            {
                //frm = new XMLControl ();
                frm = XMLControl.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(frm);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
            if (!strType.Contains("SettingForm") || (frm == null || frm.IsDisposed))
            {
                //frm = new RunLoader.SettingForm();
                frm = RunLoader.SettingForm.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(frm);
        }
    }
}

namespace ALSTools
{
    public partial class BaseOperationForm : Form
    {
        public Point mouseDownPoint = Point.Empty;
        public BaseOperationForm()
        {

        }
        protected void FormMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = new Point(e.X, e.Y);
            }

        }


        protected void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }

        protected void FormMouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }


        protected void CloseForm(object sender, EventArgs e)
        {
            Close();
        }

        protected void CreateComboColumn(DataGridView dgv, string colName, DataTable dt)
        {
            string comboColName = string.Format("combo{0}", colName);
            //ADD COMBOBOX
            if (!dgv.Columns.Contains(comboColName))
            {
                DataGridViewComboBoxColumn combocol = new DataGridViewComboBoxColumn();

                //Set combo-column to original col name so it can replace
                combocol.HeaderText = colName;
                //set name to appropriate column name but not replace original
                combocol.Name = comboColName;
                //set datasource
                combocol.DataSource = dt;
                //set value member to appropriate column name
                combocol.ValueMember = colName;
                //set display member to same column name
                combocol.DisplayMember = combocol.ValueMember;
                //Key property to set in order to display values, set to original column name
                combocol.DataPropertyName = combocol.ValueMember;
                //dEFINES value type
                combocol.ValueType = dgv.Columns[colName].ValueType;
                //Add combo column to current text column
                dgv.Columns.Insert(dgv.Columns[colName].Index, combocol);
                //Hide original field
                dgv.Columns[dgv.Columns[colName].Index].Visible = false;


                foreach (DataGridViewRow dr in dgv.Rows)
                {
                    {
                        try
                        {
                            if (dr.Cells[colName].Value.ToString() != string.Empty && dr.Cells[colName].Value != null)
                            { dr.Cells[comboColName].Value = dr.Cells[colName].Value; }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                }

                dgv.EndEdit();

            }
        }


        protected void DataGridViewEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)

        {
            //DataGridView dgv = sender as DataGridView;
            ////string header = dgv.CurrentCell.OwningColumn.HeaderText;
            //Type columnType = dgv.CurrentCell.OwningColumn.GetType();
            //List<string> obj = new List<string>();
            //if (columnType == typeof(DataGridViewComboBoxColumn))
            //{
            //    ComboBox txtCell = e.Control as ComboBox;
            //    SetAutoComplete(txtCell, obj);
            //}
            //if (dgv.Parent.ToString().Contains("Event"))
            //{
            //switch (header)
            //{
            //    case "LogName":
            //        obj = BusinessLayer.Events.EventLogic.GetLogIDList();
            //        break;
            //    case "ProductionName":
            //        obj = BusinessLayer.BaseLogLogic.GetProductionList();
            //        break;
            //    case "User":
            //        obj.Add(Auth.AuthEntity.Username);
            //        obj.Add(Environment.UserName);
            //        break;
            //    case "Terminal":
            //        obj.Add(Environment.GetEnvironmentVariable("ComputerName"));
            //        break;
            //    default:
            //        break;
            //}
            //}
            //else if (dgv.Parent.ToString().Contains("Production"))
            //{
            //switch (header)
            //{
            //    case "EqpName":
            //        obj = BusinessLayer.BaseLogLogic.GetLogIDList();
            //        break;
            //    case "Method":
            //        obj = BusinessLayer.BaseLogLogic.GetMethodList();
            //        break;
            //    default:
            //        break;
            //}
            //}


        }

        private static void SetAutoComplete(ComboBox txtCell, List<string> list)
        {
            txtCell.DropDownStyle = ComboBoxStyle.DropDown;


            //if (txtCell != null)
            //{
            //    txtCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //    txtCell.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    AutoCompleteStringCollection data = new AutoCompleteStringCollection();

            //    data.AddRange(list.ToArray());
            //    txtCell.AutoCompleteCustomSource = data;
            //}
        }
    }
}
