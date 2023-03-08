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
    public partial class frmTorles : Form
    {
        DB adatbazis;
        List<Kategoria> kategoriak = new List<Kategoria>();
        List<TermekTipusok> termekTipus = new List<TermekTipusok>();
        public frmTorles(DB adatbazis)
        {
            InitializeComponent();
            this.adatbazis = adatbazis;
            AdatokFeltoltese();
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
        private void AdatokFeltoltese()
        {
            try
            {
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
                AdatokatBeir(0);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void AdatokatBeir(int sor)
        {
            //nev csomag kategoria, tipus, netto, brutto, keszlet
            tbTermeknev.Text = dgvAdatok.Rows[sor].Cells[1].Value.ToString();
            tbCsomagolas.Text = dgvAdatok.Rows[sor].Cells[2].Value.ToString();
            cbKategoriak.SelectedIndex = cbKategoriak.FindString(dgvAdatok.Rows[sor].Cells[3].Value.ToString());
            cbTipusok.SelectedIndex = cbTipusok.FindString(dgvAdatok.Rows[sor].Cells[4].Value.ToString());
            tbNettoAr.Text = dgvAdatok.Rows[sor].Cells[5].Value.ToString();
            tbBruttoAr.Text = dgvAdatok.Rows[sor].Cells[6].Value.ToString();
        }
      
        private void Engedelyez()
        {
            tbTermeknev.Enabled = true;
            tbCsomagolas.Enabled = true;
            tbNettoAr.Enabled = true;
            tbBruttoAr.Enabled = false;
            cbKategoriak.Enabled = true;
            cbTipusok.Enabled = true;
            btnVisszaAllit.Enabled = true;
            btnModosit.Enabled = true;
            btnTorles.Enabled = true;
            dgvAdatok.Enabled = false;
        }

        private void NemEngedelyez()
        {
            tbTermeknev.Enabled = false;
            tbCsomagolas.Enabled = false;
            tbNettoAr.Enabled = false;
            tbBruttoAr.Enabled = true;
            cbKategoriak.Enabled = false;
            cbTipusok.Enabled = false;
            btnVisszaAllit.Enabled = false;
            btnModosit.Enabled = false;
            btnTorles.Enabled = true;
            dgvAdatok.Enabled = true;
        }

        private void dgvAdatok_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int kivalasztottSor = dgvAdatok.CurrentCell.RowIndex;
            AdatokatBeir(kivalasztottSor);
        }

        private void dgvAdatok_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Engedelyez();
        }

        private void btnTorles_Click(object sender, EventArgs e)
        {
            //nev csomag kategoria, tipus, netto, brutto, keszlet
            string termeknev = tbTermeknev.Text;
            if (tbCsomagolas.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show($"Biztosan törlöd" +
                    $" {termeknev} minden adatát?", "Ellenörzés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult==DialogResult.Yes)
                {
                    try
                    {
                        string lekerdez = $"delete from termekek where termek_nev='{termeknev}';";

                        adatbazis.MysqlKapcsolat.Open();

                        MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                        cmd.ExecuteNonQuery();

                        adatbazis.MysqlKapcsolat.Close();

                        TermekTipusokFeltoltese();
                        KategoriakFeltoltese();
                        AdatokatBeir(0);

                        NemEngedelyez();
                        MessageBox.Show("Sikeres törlés!");
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    AdatokFeltoltese();
                }
            }
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
                MessageBox.Show("Nem számot adtál meg!");
            }
        }

        private void btnModosit_Click(object sender, EventArgs e)
        {
            string termeknev = tbTermeknev.Text;
            string kivitel = tbCsomagolas.Text;
            string kategoria_id = cbKategoriak.SelectedValue.ToString();
            string tipus_id = cbTipusok.SelectedValue.ToString();
            string brutto = tbBruttoAr.Text;
            string netto = tbNettoAr.Text;
            int kivalasztottSor = dgvAdatok.CurrentCell.RowIndex;
            string id = dgvAdatok.Rows[kivalasztottSor].Cells[0].Value.ToString();
            if (tbCsomagolas.Text != "" || tbNettoAr.Text != "" || tbTermeknev.Text != "" || tbBruttoAr.Text != "")
            {
                try
                {
                    string lekerdez = $"update termekek set termek_nev='{termeknev}', kivitel='{kivitel}', kategoria_id='{kategoria_id}', tipus_id='{tipus_id}', nettoAr='{netto}'," +
                        $"bruttoAr='{brutto}' where id = {id};";

                    adatbazis.MysqlKapcsolat.Open();

                    MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                    cmd.ExecuteNonQuery();

                    adatbazis.MysqlKapcsolat.Close();

                    TermekTipusokFeltoltese();
                    KategoriakFeltoltese();
                    AdatokatBeir(0);

                    NemEngedelyez();
                    MessageBox.Show("Sikeres módosítás!");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Adat nem lehet üres!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnVisszaAllit_Click(object sender, EventArgs e)
        {
            adatokVisszaallitasa();
        }
     
    }
}
