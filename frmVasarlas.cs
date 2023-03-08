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
using System.IO;

namespace RaktarAlkalmazas
{
    public partial class frmVasarlas : Form
    {
        DB adatbazis;
        List<Kategoria> kategoriak = new List<Kategoria>();
        List<TermekTipusok> termekTipus = new List<TermekTipusok>();
        //Dictionary<int, int> TermekekAraiDict = new Dictionary<int, int>();
        List<int> TermekArai = new List<int>();
        List<int> TermekDb = new List<int>();
        List<string> KosarbanCuccok = new List<string>();
        string idKi = "";
        string termekek = "";
        int DARAB = 0;
        int SumOsszeg = 0;
        int osszeg;
        int DarabSzam = 0;
        public frmVasarlas(DB adatbazis)
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
            btnKosarhozAd.Enabled = false;
            btnMegse.Enabled = false;
            dtpDatum.MaxDate = DateTime.Today;
            dtpDatum.MinDate = DateTime.Today;
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
            tbTermeknev.Text = dgvAdatok.Rows[sor].Cells[0].Value.ToString();
            tbCsomagolas.Text = dgvAdatok.Rows[sor].Cells[1].Value.ToString();
            cbKategoriak.SelectedIndex = cbKategoriak.FindString(dgvAdatok.Rows[sor].Cells[2].Value.ToString());
            cbTipusok.SelectedIndex = cbTipusok.FindString(dgvAdatok.Rows[sor].Cells[3].Value.ToString());
            tbNettoAr.Text = dgvAdatok.Rows[sor].Cells[4].Value.ToString();
            tbBruttoAr.Text = dgvAdatok.Rows[sor].Cells[5].Value.ToString();
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

        private void Engedelyez()
        {
            tbTermeknev.Enabled = true;
            tbCsomagolas.Enabled = true;
            tbNettoAr.Enabled = true;
            tbBruttoAr.Enabled = false;
            cbKategoriak.Enabled = true;
            cbTipusok.Enabled = true;
            dgvAdatok.Enabled = false;
            btnKosarhozAd.Enabled = true;
        }

        private void NemEngedelyez()
        {
            tbTermeknev.Enabled = false;
            tbCsomagolas.Enabled = false;
            tbNettoAr.Enabled = false;
            tbBruttoAr.Enabled = false;
            cbKategoriak.Enabled = false;
            cbTipusok.Enabled = false;
            dgvAdatok.Enabled = true;
            btnKosarhozAd.Enabled = true;
        }

        private void frmVasarlas_Load(object sender, EventArgs e)
        {
            AdatokFeltoltese();           
        }

        private void dgvAdatok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int kivalasztottSor = dgvAdatok.CurrentCell.RowIndex;
            AdatokatBeir(kivalasztottSor);
        }

        private void dgvAdatok_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Engedelyez();
            btnMegse.Enabled = true;
        }

        private void btnKosarhozAd_Click(object sender, EventArgs e)
        {
            //frmDarab formDarab = new frmDarab();
            string termeknev = tbTermeknev.Text;
            string kivitel = tbCsomagolas.Text;
            string kategoria_id = cbKategoriak.SelectedIndex.ToString();
            string tipus_id = cbTipusok.SelectedIndex.ToString();
            int brutto = int.Parse(tbBruttoAr.Text);
            string netto = tbNettoAr.Text;
            int kivalasztottSor = dgvAdatok.CurrentCell.RowIndex;
            string id = dgvAdatok.Rows[kivalasztottSor].Cells[0].Value.ToString();
            string datum = dtpDatum.Value.ToString("yyyy.MM.dd");

            //List<int> TermekekAraiLista = new List<int>();         
            //formDarab.ShowDialog();

            //tbCsomagolas.Text != "" || tbDarab.Text != "" || tbNettoAr.Text != "" || tbTermeknev.Text != "" || tbBruttoAr.Text != ""
            if (tbDarabszam.Text != "")
            {
                #region lekerd
                //try
                //{
                //    DarabSzam = int.Parse(tbDarabszam.Text);
                //}
                //catch (Exception)
                //{
                //    MessageBox.Show("Nem számot adtál meg", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //try
                //{
                //    string lekerdez = $"insert into vasarlas (termekek, darab, osszeg, datum)" +
                //        $"values ('{termeknev}', '{DarabSzam}', '{brutto}', '{datum}');";

                //    adatbazis.MysqlKapcsolat.Open();

                //    MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                //    cmd.ExecuteNonQuery();
                //    adatbazis.MysqlKapcsolat.Close();
                //    AdatokatBeir(0);
                //}
                //catch (MySqlException ex)
                //{
                //    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //} 
                #endregion

                try
                {
                    DarabSzam = int.Parse(tbDarabszam.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nem számot adtál meg", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lbKosarTartalma.Items.Add($"{termeknev} {DarabSzam} darab {brutto} Ft. Ezen a napon: {datum}");
                KosarbanCuccok.Add($"{termeknev} {DarabSzam} db {brutto} Ft");
                //TermekekAraiLista.Add(int.Parse(brutto));
                //TermekekAraiDict.Add(DarabSzam, int.Parse(brutto));
                TermekArai.Add(brutto);
                TermekDb.Add(DarabSzam);
                AdatokatBeir(0);
            }
            else
            {
                MessageBox.Show("Nem számot adtál meg", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            NemEngedelyez();
            btnKosarhozAd.Enabled = false;
            foreach (var i in TermekArai)
            {
                foreach (var t in TermekDb)
                {
                    osszeg = i * t;
                }
            }
            SumSegedTablaFeltoltese(termeknev);
            OsszegSzamolas();
        }

        private void SumSegedTablaFeltoltese(string termeknev)
        {
            try
            {
                string lekerdez = $"insert into sumseged (nev, osszeg, db)" +
                    $"values ('{termeknev}', '{osszeg}','{DarabSzam}');";

                adatbazis.MysqlKapcsolat.Open();

                MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                cmd.ExecuteNonQuery();
                adatbazis.MysqlKapcsolat.Close();
                AdatokatBeir(0);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OsszegSzamolas()
        {
            try
            {
                adatbazis.MysqlKapcsolat.Open();

                string lekerdezes = "select sum(osszeg) as 'sum' from sumseged";

                MySqlDataReader sorok;
                MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                sorok = parancs.ExecuteReader();

                if (sorok.HasRows)
                {
                    while (sorok.Read())
                    {
                        string sum = sorok["sum"].ToString();
                        lblOsszeg.Text = sum;
                    }
                }
                adatbazis.MysqlKapcsolat.Close();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //#region ListabanLekerdezes
        //private void KosartartalmaListaban()
        //{
        //    try
        //    {
        //        adatbazis.MysqlKapcsolat.Open();

        //        string lekerdezes = "SELECT termekek, osszeg, darab from vasarlas;";

        //        MySqlDataReader sorok;
        //        MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
        //        sorok = parancs.ExecuteReader();

        //        if (sorok.HasRows)
        //        {
        //            while (sorok.Read())
        //            {
        //                string termekNev = sorok["termekek"].ToString();
        //                string osszeg = sorok.GetString("osszeg");
        //                string darab = sorok.GetString("darab");
        //                lbKosarTartalma.Items.Add($"{termekNev} {osszeg} Ft/db {darab}");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        adatbazis.MysqlKapcsolat.Close();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    adatbazis.MysqlKapcsolat.Open();

        //    string lekerdezesKetto = "SELECT sum(darab*osszeg) as 'sum' FROM vasarlas";

        //    MySqlDataReader sorokOsszeg;
        //    MySqlCommand parancsOsszeg = new MySqlCommand(lekerdezesKetto, adatbazis.MysqlKapcsolat);
        //    sorokOsszeg = parancsOsszeg.ExecuteReader();

        //    if (sorokOsszeg.HasRows)
        //    {
        //        while (sorokOsszeg.Read())
        //        {
        //            string osszeg = sorokOsszeg["sum"].ToString();
        //            //lblSum.Text = $"Összeg: {osszeg}";
        //            lblOsszeg.Text = osszeg;
        //        }
        //    }

        //    adatbazis.MysqlKapcsolat.Close();
        //} 
        //#endregion

        private void VasarlasIdTarolo()
        {
            //StreamWriter iro = new StreamWriter("idtarol.txt");
            //iro.WriteLine(vasarlasid);
            //iro.Close();
            //vasarlasid++;
        }

        private void btnSzamol_Click_1(object sender, EventArgs e)
        {           
            string datum = dtpDatum.Value.ToString("yyyy.MM.dd");
            DialogResult dialogResult = MessageBox.Show("Biztos, hogy szeretnéd véglegesíteni a vásárlást kiszámolni? \n A vásárlás adatai ezután nem módosíthatóak!", "Információ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                SegedLekerdezes();
                try
                {
                    string lekerdez = $"insert into vasarlas (termekek, osszeg, datum)" +
                        $"values ('{termekek}', '{SumOsszeg}', '{datum}');";

                    adatbazis.MysqlKapcsolat.Open();

                    MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                    cmd.ExecuteNonQuery();
                    adatbazis.MysqlKapcsolat.Close();
                    AdatokatBeir(0);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Sumolas();

                try
                {
                    adatbazis.MysqlKapcsolat.Open();

                    string lekerdezes = "SELECT id from vasarlas;";

                    MySqlDataReader sorok;
                    MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                    sorok = parancs.ExecuteReader();

                    if (sorok.HasRows)
                    {
                        while (sorok.Read())
                        {
                            string id = sorok.GetString("id");
                            idKi = id;
                        }
                    }
                    else
                    {
                        MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    adatbazis.MysqlKapcsolat.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                StreamWriter ir = new StreamWriter($"{idKi}VasarlasNyugta.txt");
                ir.WriteLine($"A vásárlás azonosítója: {idKi}");
                foreach (var i in KosarbanCuccok)
                {
                    ir.WriteLine(i);
                }
                ir.WriteLine($"Összesen: {SumOsszeg} Ft");
                ir.WriteLine($"\tKöszönjük a vásárlást!\nA vásárlás dátuma: {datum}\n \t Panasz esetén keresse az üzletvezetőt, a pénztár elhagyása után reklamációt nem fogadunk el.");
                ir.Close();
                TablaTisztitas();
                KosarbanCuccok.Clear();
                MessageBox.Show("A nyugta elkészült!");
            }
        }

        private void Sumolas()
        {
            try
            {
                adatbazis.MysqlKapcsolat.Open();

                string lekerdezes = "SELECT SUM(osszeg) as 'sum' FROM vasarlas";

                MySqlDataReader sorok;
                MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                sorok = parancs.ExecuteReader();

                if (sorok.HasRows)
                {
                    while (sorok.Read())
                    {
                        string osszeg = sorok["sum"].ToString();
                        lblSum.Text = $"Összeg: {osszeg} Ft";
                    }
                }
                else
                {
                    MessageBox.Show("A lekérdezésnek nincs eredménye", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                adatbazis.MysqlKapcsolat.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SegedLekerdezes()
        {
            try
            {
                adatbazis.MysqlKapcsolat.Open();

                string lekerdezes = "select nev, sum(osszeg) as 'sum', db from sumseged";
                MySqlDataReader sorok;
                MySqlCommand parancs = new MySqlCommand(lekerdezes, adatbazis.MysqlKapcsolat);
                sorok = parancs.ExecuteReader();
                if (sorok.HasRows)
                {
                    while (sorok.Read())
                    {
                        string sum = sorok["sum"].ToString();
                        int db = sorok.GetInt32("db");
                        string nev = sorok.GetString("nev");
                        SumOsszeg += int.Parse(sum);
                        DARAB += db;
                        termekek += nev;
                        //Console.WriteLine();
                    }
                }
                adatbazis.MysqlKapcsolat.Close();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMegse_Click(object sender, EventArgs e)
        {
            NemEngedelyez();
            btnMegse.Enabled = false;
        }

        private void TablaTisztitas()
        {
            try
            {
                string lekerdez = $"TRUNCATE TABLE sumseged;";

                adatbazis.MysqlKapcsolat.Open();

                MySqlCommand cmd = new MySqlCommand(lekerdez, adatbazis.MysqlKapcsolat);
                cmd.ExecuteNonQuery();
                adatbazis.MysqlKapcsolat.Close();
                AdatokatBeir(0);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number + ":" + ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
