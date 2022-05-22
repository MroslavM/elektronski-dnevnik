using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace e_dnevnik
{
    public partial class Raspodela : Form
    {
        DataTable raspodela;
        int broj_sloga = 0;
        public Raspodela()
        {
            InitializeComponent();
        }
        private void Load_Data()
        { 
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM raspodela", veza);
            raspodela = new DataTable();
            Adapter.Fill(raspodela);
        }

        private void Raspodela_Load(object sender, EventArgs e)
        {
            Load_Data();
            ComboFill();
        }
        private void ComboFill()
        {
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter Adapter;
            DataTable dt_godina, dt_nastavnik, dt_predmet, dt_odeljenje;
            Adapter = new SqlDataAdapter("SELECT * FROM skolska_godina", veza);
            dt_godina = new DataTable();
            Adapter.Fill(dt_godina);
            Adapter = new SqlDataAdapter("SELECT * FROM skolska_godina", veza);
            dt_godina = new DataTable();
            Adapter.Fill(dt_godina);
            Adapter = new SqlDataAdapter("SELECT id, ime + prezime AS naziv FROM osoba WHERE uloga = 2", veza);
            dt_nastavnik = new DataTable();
            Adapter.Fill(dt_nastavnik);
            Adapter = new SqlDataAdapter("SELECT id, naziv FROM predmet", veza);
            dt_predmet = new DataTable();
            Adapter.Fill(dt_predmet);
            Adapter = new SqlDataAdapter("SELECT id, STR(razred) + '/' + indeks as naziv FROM odeljenje", veza);
            dt_odeljenje = new DataTable();
            Adapter.Fill(dt_odeljenje);

            cmb_godina.DataSource = dt_godina;
            cmb_godina.ValueMember = "id";
            cmb_godina.DisplayMember = "naziv";
            cmb_godina.SelectedValue = raspodela.Rows[broj_sloga]["godina_id"];

            cmb_nastavnik.DataSource = dt_nastavnik;
            cmb_nastavnik.ValueMember = "id";
            cmb_nastavnik.DisplayMember = "naziv";
            cmb_nastavnik.SelectedValue = raspodela.Rows[broj_sloga]["nastavnik_id"];

            cmb_predmet.DataSource = dt_predmet;
            cmb_predmet.ValueMember = "id";
            cmb_predmet.DisplayMember = "naziv";
            cmb_predmet.SelectedValue = raspodela.Rows[broj_sloga]["predmet_id"];

            cmb_odeljenje.DataSource = dt_odeljenje;
            cmb_odeljenje.ValueMember = "id";
            cmb_odeljenje.DisplayMember = "naziv";
            cmb_odeljenje.SelectedValue = raspodela.Rows[broj_sloga]["odeljenje_id"];

            txt_id.Text = raspodela.Rows[broj_sloga]["id"].ToString();

            if (broj_sloga == 0)
            {
                btn_first.Enabled = false;
                btn_previous.Enabled = false;
            }
            else
            {
                btn_first.Enabled = true;
                btn_previous.Enabled = true;
            }
            if (broj_sloga == raspodela.Rows.Count - 1)
            {
                btn_last.Enabled = false;
                btn_next.Enabled = false;
            }
            else
            {
                btn_last.Enabled = true;
                btn_next.Enabled = true;
            }

        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            broj_sloga = 0;
            Load_Data();
            ComboFill();
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            broj_sloga--;
            Load_Data();
            ComboFill();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            broj_sloga++;
            Load_Data();
            ComboFill();
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            broj_sloga = raspodela.Rows.Count - 1;
            Load_Data();
            ComboFill();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string naredba = "DELETE  FROM raspodela WHERE id = " + txt_id.Text;
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba, veza);
            bool brisanje = false;
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                brisanje = true;
            }
            catch (Exception Greska)
            {

                MessageBox.Show(Greska.Message);
            }
            broj_sloga--;
            if (brisanje)
            {
                Load_Data();
                if (broj_sloga > 0) broj_sloga = broj_sloga--;
                ComboFill();
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder Naredba = new StringBuilder("INSERT INTO raspodela (godina_id, nastavnik_id, predmet_id, odeljenje_id ) VALUES ('");
            Naredba.Append(cmb_godina.SelectedValue + "', '" + cmb_nastavnik.SelectedValue + "', '" + cmb_predmet.SelectedValue + "', '" + cmb_odeljenje.SelectedValue + "')");
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand KomandaInsert = new SqlCommand(Naredba.ToString(), veza);
            try
            {
                veza.Open();
                KomandaInsert.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }

            Load_Data();
            broj_sloga = raspodela.Rows.Count - 1;
            ComboFill();

            
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            StringBuilder Naredba = new StringBuilder("UPDATE raspodela SET ");
            Naredba.Append("godina_id = '" + cmb_godina.SelectedValue + "', ");
            Naredba.Append("nastavnik_id = '" + cmb_nastavnik.SelectedValue + "', ");
            Naredba.Append("predmet_id = '" + cmb_predmet.SelectedValue + "', ");
            Naredba.Append("odeljenje_id = '" + cmb_odeljenje.SelectedValue + "' ");
            Naredba.Append("WHERE id = " + txt_id.Text);
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand KomandaUpdate = new SqlCommand(Naredba.ToString(), veza);
            try
            {
                veza.Open();
                KomandaUpdate.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {

                MessageBox.Show(Greska.Message);
            }
            Load_Data();
            ComboFill();
        }
    }
}
