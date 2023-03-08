using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RaktarAlkalmazas
{
    public partial class frmFo : Form
    {
        DB adatbazis;
        List<string> uzik = new List<string>();
        public frmFo(DB adatbazis, User felhasznalo)
        {
            InitializeComponent();
            this.Text = "Főmenü - " + felhasznalo.SzemelyNeve + " Jogköre: " + felhasznalo.Jogkor;
            //StringBuilder udv = new StringBuilder($"Üdvözöllek " + felhasznalo.SzemelyNeve);
            lblUdvozlo.Text = $"Üdvözöllek " + felhasznalo.SzemelyNeve +" Legyen szép napod!";
            //NapiUzenet();
            //lblUdvozlo.Text = udv.ToString();
            this.adatbazis = adatbazis;
            if (felhasznalo.Jogkor=="admin")
            {
                btnTermekSzerk.Enabled = true;
                btnTermekSzerk.Image=global::RaktarAlkalmazas.Properties.Resources.btnraktar;
            }
        }

        private void btnKereses_Click(object sender, EventArgs e)
        {
            frmKereses formKeres = new frmKereses(adatbazis);
            formKeres.ShowDialog();
        }

        private void frmFo_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnTermekSzerk_Click(object sender, EventArgs e)
        {
            frmSzerkesztes formSzerk = new frmSzerkesztes(adatbazis);
            formSzerk.ShowDialog();
        }

        private void btnVasarlas_Click(object sender, EventArgs e)
        {
            frmVasarlas formVasarlas = new frmVasarlas(adatbazis);
            formVasarlas.ShowDialog();
        }
        //public void NapiUzenet()
        //{
        //    uzik.Add(" Legyen szép napod!");
        //    uzik.Add(" szaiazs");
        //    uzik.Add(" asdasdasds");
        //    Random r = new Random();
        //    int random = r.Next(0, 4);
        //    switch (random)
        //    {
        //        case 1:
        //            udv.Append(uzik[0]);
        //            break;

        //        case 2:
        //            udv.Text.Append = uzik[1];
        //            break;

        //        case 3:
        //            lblUdvozlo.Text = uzik[2];
        //            break;

        //        default: this.Text = "";
        //            break;
        //    }
        //}
    }
}
