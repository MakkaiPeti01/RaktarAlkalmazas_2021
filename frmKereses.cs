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
    public partial class frmKereses : Form
    {
        DB adatbazis;
        public frmKereses(DB adatbazis)
        {
            this.adatbazis = adatbazis;
            InitializeComponent();
        }

        private void btnKeres_Click_1(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();

            string cim = tbKereses.Text;

            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, bruttoAr FROM termekek where termek_nev LIKE '%" + cim + "%'";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();                   
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHus_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Húsok";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 1";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnZoldseg_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Zöldség és gyümölcs";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 2";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPek_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Pék áru";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 3";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTej_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Tejtermékek";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 4";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSzeszes_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Szeszes Italok";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 5";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTeszta_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Tészták";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 6";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPapir_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Papír és írószer";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 7";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDrog_Click(object sender, EventArgs e)
        {
            lbEredmenyek.Items.Clear();
            tbKereses.Text = "Drogéria";
            string cim = tbKereses.Text;
            if (cim != "")
            {
                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT termek_nev, kivitel, termektipusok.tipus as tip, bruttoAr FROM termekek " +
                        "inner join termektipusok on termekek.tipus_id=termektipusok.id where kategoria_id = 8";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string termekNev = sorok["termek_nev"].ToString();
                            string kivitel = sorok["kivitel"].ToString();
                            string tipus = sorok["tip"].ToString();
                            string bruttoAr = sorok["bruttoAr"].ToString();
                            lbEredmenyek.Items.Add($"{termekNev} {kivitel} {tipus} {bruttoAr} Ft");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tbKereses.Focus();
                    tbKereses.SelectAll();
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
