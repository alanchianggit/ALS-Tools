using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALSTools
{
    public partial class XMLControl : Form
    {
        private static XMLControl inst;
        public static XMLControl GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new XMLControl();
                return inst;
            }
        }
        public XMLControl()
        {
            InitializeComponent();

            
            
        }
    }
}
