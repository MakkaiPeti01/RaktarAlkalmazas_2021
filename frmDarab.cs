using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaktarAlkalmazas
{
    public partial class frmDarab : Form
    {
        public frmDarab()
        {
            InitializeComponent();
        }

        private void tbDarab_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void tbOk_Click(object sender, EventArgs e)
        {
            int DarabSzam = 0;
            try
            {
                DarabSzam = int.Parse(tbDarab.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Nem számot adtál meg", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
