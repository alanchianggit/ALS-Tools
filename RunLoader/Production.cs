using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunLoader
{
    public partial class Production : Form
    {
        public Production()
        {
            InitializeComponent();
        }

        private static Production inst;
        public static Production GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new Production();
                return inst;
            }
        }
    }
}
