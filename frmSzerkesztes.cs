using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RaktarAlkalmazas
{
    public partial class frmSzerkesztes : Form
    {
        DB adatbazis;
        List<Kategoria> kategoriak = new List<Kategoria>();
        List<TermekTipusok> termekTipus = new List<TermekTipusok>();
        public frmSzerkesztes(DB adatbazis)
        {
            InitializeComponent();
            this.adatbazis = adatbazis;
            KategoriakFeltoltese();
            cbKategoriak.DisplayMember = "Kategoriak";
            cbKategoriak.ValueMember = "id";
            cbKategoriak.DataSource = kategoriak;
            cbKategoriak.SelectedIndex = 0;
            TermekTipusokFeltoltese();
            cbTipusok.DisplayMember = "TipusokAdatai";
            cbTipusok.ValueMember = "id";
            cbTipusok.DataSource = termekTipus;
            cbTipusok.SelectedIndex = 0;
        }
        private void btnAdatokBetolt_Click(object sender, EventArgs e)
        {
            AdatokFeltoltese();
        }

        private void AdatokFeltoltese()
        {
            try
            {
                //string lekerdezes = "SELECT termek_nev, kivitel, kategoria_id, tipus_id, nettoAr, bruttoAr FROM termekek";
                string lekerdezes = "select termek_nev, kivitel, k.kategoria_nev as 'kategoria_id', te.tipus as 'tipus_id', nettoAr, bruttoAr from termekek as t " +
                    "inner join termektipusok as te on t.tipus_id=te.id "
                    + "inner join kategoriak as k on t.kategoria_id=k.id";
                adatbazis.MysqlKapcsolat.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(lekerdezes, adatbazis.MysqlKapcsolat);
                DataTable kolcsonzesekTabla = new DataTable();
                da.Fill(kolcsonzesekTabla);
                dgvAdatok.DataSource = kolcsonzesekTabla;
                adatbazis.MysqlKapcsolat.Close();
                dgvAdatok.Rows[0].Selected = true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSzerkesztes_Load(object sender, EventArgs e)
        {
            AdatokFeltoltese();
        }

        private void KategoriakFeltoltese()
        {
            try
            {
                string lekerdezes = "SELECT * FROM kategoriak;";
                adatbazis.MysqlKapcsolat.Open();
                MySqlDataReader sorok;
                MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                sorok = parancs.ExecuteReader();

                if (sorok.HasRows)
                {
                    while (sorok.Read())
                    {
                        int id = sorok.GetInt32(0);
                        string kategoria_nev = sorok.GetString(1);
                        var kategoria = new Kategoria
                        {
                            Id = id,
                            KategoriaNev = kategoria_nev,
                        };
                        kategoriak.Add(kategoria);
                    }
                }
                adatbazis.MysqlKapcsolat.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TermekTipusokFeltoltese()
        {
            try
            {
                string lekerdezes = "SELECT * FROM termektipusok;";
                adatbazis.MysqlKapcsolat.Open();
                MySqlDataReader sorok;
                MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                sorok = parancs.ExecuteReader();

                if (sorok.HasRows)
                {
                    while (sorok.Read())
                    {
                        int id = sorok.GetInt32(0);
                        string tipus_nev = sorok.GetString(1);
                        var tipus = new TermekTipusok
                        {
                            Id = id,
                            TipusNev = tipus_nev,
                        };
                        termekTipus.Add(tipus);
                    }
                }
                adatbazis.MysqlKapcsolat.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdatokFelvetele_Click(object sender, EventArgs e)
        {
            string termeknev = tbTermeknev.Text;
            string kivitel = tbCsomagolas.Text;
            string kategoria_id = cbKategoriak.SelectedValue.ToString();
            string tipus_id = cbTipusok.SelectedValue.ToString();
            string brutto = tbBruttoAr.Text;
            if (tbCsomagolas.Text == "" || tbNettoAr.Text == "" || tbTermeknev.Text == "" || tbBruttoAr.Text == "")
            {
                MessageBox.Show("Nem adtál meg minden adatot", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(tbNettoAr.Text, out int nettoAr))
            {
                MessageBox.Show("Nem összeget adtál meg árnak!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Ezeket az adatokat szeretnéd felvenni?" +
                    $"{termeknev}', '{kivitel}', '{kategoria_id}', '{tipus_id}', '{nettoAr}', '{brutto}'", "Ellenörzés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string lekerdez = $"insert into termekek (termek_nev, kivitel, kategoria_id, tipus_id, nettoAr, bruttoAr) " +
                            $"values ('{termeknev}', '{kivitel}', '{kategoria_id}', '{tipus_id}', '{nettoAr}', '{brutto}');";

                        adatbazis.MysqlKapcsolat.Open();
                        

                        MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                        cmd.ExecuteNonQuery();

                        adatbazis.MysqlKapcsolat.Close();

                        AdatokFeltoltese();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    adatokVisszaallitasa();
                }
                else
                {
                    adatokVisszaallitasa();
                }
            }
        }

        private void adatokVisszaallitasa()
        {
            tbTermeknev.Text = "";
            tbCsomagolas.Text = "";
            tbNettoAr.Text = "";
            cbKategoriak.SelectedIndex = 0;
            cbTipusok.SelectedIndex = 0;
        }

        private void tbBruttoAr_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNettoAr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double brutto = 0;
                double netto = int.Parse(tbNettoAr.Text);
                double seged = netto * 0.27;
                brutto = (int)netto + (int)seged;
                tbBruttoAr.Text = brutto.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Nem számot adtál meg!");
            }
        }

        private void btnTöröl_Click(object sender, EventArgs e)
        {
            frmTorles formTorles = new frmTorles(adatbazis);
            formTorles.ShowDialog();
        }

        private void btnSugo_Click(object sender, EventArgs e)
        {
            Process.Start("RaktarSugo.pdf");
        }
    }
}
