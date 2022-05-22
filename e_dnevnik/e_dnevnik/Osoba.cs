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
    public partial class Osoba : Form
    {
        int broj_slogaa = 0;
        DataTable tabela;
        public Osoba()
        {
            InitializeComponent();
        }
        private void Load_Data()
        {
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Osoba", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }
        private void Txt_load()
        {  
            if (tabela.Rows.Count == 0)
            {
                txt_id.Text = "";
                txt_ime.Text = "";
                txt_prezime.Text = "";
                txt_adresa.Text = "";
                txt_email.Text = "";
                txt_jmbg.Text = "";
                txt_password.Text = "";
                txt_uloga.Text = "";
                btn_delete.Enabled = false;
            }
            else
            {
                txt_id.Text = tabela.Rows[broj_slogaa]["id"].ToString();
                txt_ime.Text = tabela.Rows[broj_slogaa]["Ime"].ToString();
                txt_prezime.Text = tabela.Rows[broj_slogaa]["Prezime"].ToString();
                txt_adresa.Text = tabela.Rows[broj_slogaa]["Adresa"].ToString();
                txt_email.Text = tabela.Rows[broj_slogaa]["email"].ToString();
                txt_jmbg.Text = tabela.Rows[broj_slogaa]["jmbg"].ToString();
                txt_password.Text = tabela.Rows[broj_slogaa]["pass"].ToString();
                txt_uloga.Text = tabela.Rows[broj_slogaa]["uloga"].ToString();
                btn_delete.Enabled = true;
            }
            if (broj_slogaa == 0)
            {
                btn_first.Enabled= false;
                btn_previous.Enabled = false;
            }
            else
            {
                btn_first.Enabled = true;
                btn_previous.Enabled = true;
            }
            if (broj_slogaa == tabela.Rows.Count -1)
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

        private void Osoba_Load(object sender, EventArgs e)
        {
            Load_Data();
            Txt_load();
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            broj_slogaa = 0;
            Txt_load();
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            broj_slogaa = broj_slogaa - 1;
            Txt_load();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            broj_slogaa = broj_slogaa + 1;
            Txt_load();
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            broj_slogaa = tabela.Rows.Count - 1;
            Txt_load();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder Naredba = new StringBuilder("INSERT INTO Osoba (ime, prezime, adresa, jmbg, email, pass, uloga) VALUES ('");
            Naredba.Append(txt_ime.Text + "', '" + txt_prezime.Text + "', '" + txt_adresa.Text + "', '" + txt_jmbg.Text + "', '");
            Naredba.Append(txt_email.Text + "', '" + txt_password.Text + "', '" + txt_uloga.Text + "')");
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
            broj_slogaa = tabela.Rows.Count - 1;
            Txt_load();

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            StringBuilder Naredba = new StringBuilder("UPDATE Osoba SET ");
            Naredba.Append("ime = '" + txt_ime.Text +"', ");
            Naredba.Append("prezime = '" + txt_prezime.Text + "', ");
            Naredba.Append("adresa = '" + txt_adresa.Text + "', ");
            Naredba.Append("email = '" + txt_email.Text + "', ");
            Naredba.Append("jmbg = '" + txt_jmbg.Text + "', ");
            Naredba.Append("pass = '" + txt_password.Text + "', ");
            Naredba.Append("uloga = '" + txt_uloga.Text + "'");
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
            Txt_load();

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
           string Naredba = "DELETE FROM Osoba where id = " + txt_id.Text;
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand KomandaDelete = new SqlCommand(Naredba, veza);
            bool brisanje = false;
            try
            {
                veza.Open();
                KomandaDelete.ExecuteNonQuery();
                veza.Close();
                brisanje = true;
            }
            catch (Exception Greska)
            {

                MessageBox.Show(Greska.Message);
            }
            if (brisanje)
            {
                Load_Data();
                if (broj_slogaa > 0) broj_slogaa = broj_slogaa--;                
            }

        }
    }
}
