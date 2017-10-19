using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RunLoader
{
    public partial class Analysis_Management : Form
    {
        public Analysis_Management()
        {
            InitializeComponent();
        }

        private void cmd_LoadRun_Click(object sender, EventArgs e)
        {
            string xxx = string.Format(@"{0}{1}",this.txt_Directory.Text,this.cmb_RunNum.Text);
            using (StreamReader sr = new StreamReader(xxx))
            {
                string FullText = sr.ReadToEnd().ToString();
                string[] textRows = FullText.Split('\n');

                foreach (string row in textRows)
                    {
                        string[] SampleArray = row.Split(",")
                        
                            Sample currSample = new Sample();
                            
                            currSample.Skip = SampleArray[0];
                            currSample.Type = SampleArray[1];
                            currSample.Vial = SampleArray[2];
                            currSample.FileName = SampleArray[3];
                            currSample.SampleName = SampleArray[4];
                            currSample.Level = SampleArray[5];
                            currSample.Dilution = SampleArray[6];
                        
                        
                    }
                
                MessageBox.Show(textRows.Length.ToString());
            }
        }
        
    }


    public class Sample 
    {
        private string _Skip;
        private string _SampleName;
        private string _Comment;
        private string _Vial;
        private string _FileName;
        private string _Dilution;
        private string _Level;
        private string _Type;

        public string Skip 
        {
            get
            {
                return Skip = _Skip;
            }
            set
            {
                _Skip = value;
            }
        }

        public string Type
        {
            get
            {
                return Type = _Type;
            }

            set
            {
                _Type = value;
            }
        } 


        public string SampleName 
        {
            get
            {
                return SampleName = _SampleName;
            }
            set
            {
                _SampleName = value;
            }
        }

        public string Vial
        {
            get
            {
                return Vial = _Vial;
            }
            set
            {
                _Vial = value;
            }
        }

        public string FileName 
        {
            get
            {
                return FileName = _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        public string Dilution
        {
            get
            {
                return Dilution = _Dilution;
            }
            set
            {
                _Dilution = value;
            }
        }

        public string Level
        {
            get
            {
                return Level = _Level;
            }
            set
            {
                _Level = value;
            }
        }
    }
}
