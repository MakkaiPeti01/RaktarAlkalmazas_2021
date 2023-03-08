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
    public partial class frmBelepes : Form
    {
        DB adatbazis;
        User felhasznalo;
        public frmBelepes()
        {
            InitializeComponent();
            tbNev.Focus();
            adatbazis = new DB("localhost", "aruhaz", "root");
        }

        private void btnBelepes_Click(object sender, EventArgs e)
        {
            string nev = tbNev.Text;
            string jelszo = tbJelszo.Text;

            if (nev != "" && jelszo != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT felhasznalonev, jelszo, jogkor, szemelyNeve from felhasznalok " +
                      "WHERE felhasznalonev = '" + nev + "' and jelszo = '" + jelszo + "';";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();
                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string felhasznaloNev = sorok["felhasznalonev"].ToString();
                            string felhasznaloJelszo = sorok["jelszo"].ToString();
                            string jogosultsag = sorok["jogkor"].ToString();
                            string rendesnev = sorok["szemelyNeve"].ToString();
                            felhasznalo = new User(felhasznaloNev, felhasznaloJelszo, jogosultsag, rendesnev);
                        }
                        this.Hide();
                        adatbazis.MysqlKapcsolat.Close();
                        frmFo formFo = new frmFo(adatbazis, felhasznalo);
                        formFo.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Felhasználó név vagy jelszó nem jó!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        adatbazis.MysqlKapcsolat.Close();
                    }

                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    adatbazis.MysqlKapcsolat.Close();
                }
            }
            else
            {
                MessageBox.Show("Felhasználó név vagy jelszó nem lehet üres!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
