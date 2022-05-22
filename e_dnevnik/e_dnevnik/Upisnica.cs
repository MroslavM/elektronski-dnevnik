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
    public partial class Upisnica : Form
    {
        DataTable dt_upisnica = new DataTable();
        public Upisnica()
        {
            InitializeComponent();
        }

        private void cmb_godina_populate()
        {
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM skolska_godina", veza);
            DataTable dt_godina = new DataTable();
            Adapter.Fill(dt_godina);
            cmb_godina.DataSource = dt_godina;
            cmb_godina.ValueMember = "id";
            cmb_godina.DisplayMember = "naziv";
            cmb_godina.SelectedValue = 2;
        }
        private void cmb_odeljenje_populate()
        {
            string godina = cmb_godina.SelectedValue.ToString();
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT id, str(razred) + '/' + indeks as naziv FROM odeljenje WHERE godina_id = " + godina, veza);
            DataTable dt_odeljenje = new DataTable();
            Adapter.Fill(dt_odeljenje);
            cmb_odeljenje.DataSource = dt_odeljenje;
            cmb_odeljenje.ValueMember = "id";
            cmb_odeljenje.DisplayMember = "naziv";
            cmb_odeljenje.SelectedValue = 1;

        }
        private void cmb_godina_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_godina.IsHandleCreated && cmb_godina.Focused)
            {
                cmb_odeljenje_populate();
                cmb_odeljenje.SelectedIndex = -1;
                while (grid_upisnica.Rows.Count >0)
                {
                    grid_upisnica.Rows.Remove(grid_upisnica.Rows[0]); 
                }
                cmb_ucenik.Enabled = false;
                cmb_ucenik.SelectedIndex = -1;
                txt_id.Text = "";
            }
        }
        private void cmb_odeljenje_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_odeljenje.IsHandleCreated && cmb_odeljenje.Focused)
            {
                cmb_ucenik_populate();
                cmb_ucenik.SelectedIndex = -1;
                cmb_ucenik.Enabled = true;
                Grid_populate();
            }
        }
        private void cmb_ucenik_populate()
        {
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT id, ime + prezime as naziv FROM osoba WHERE uloga = 1", veza);
            DataTable dt_ucenik = new DataTable();
            Adapter.Fill(dt_ucenik);
            cmb_ucenik.DataSource = dt_ucenik;
            cmb_ucenik.ValueMember = "id";
            cmb_ucenik.DisplayMember = "naziv";

        }
        private void Grid_populate()
        { 
            SqlConnection veza = Koncekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT upisnica.id as id, ime + prezime as naziv, osoba.id as ucenik FROM upisnica JOIN osoba ON osoba_id = osoba.id WHERE odeljenje_id = " + cmb_odeljenje.SelectedValue.ToString(), veza);
            adapter.Fill(dt_upisnica);
            grid_upisnica.DataSource = dt_upisnica;
            grid_upisnica.Columns["ucenik"].Visible = false;
            grid_upisnica.AllowUserToAddRows = false;
        }
        private void Upisnica_Load(object sender, EventArgs e)
        {
            txt_id.Enabled = false;
            cmb_godina_populate();
            cmb_odeljenje_populate();
            cmb_odeljenje.SelectedIndex = -1;
            
        }

        private void grid_upisnica_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grid_upisnica.CurrentRow !=null)
            {
                int broj_sloga = grid_upisnica.CurrentRow.Index;
                if (dt_upisnica.Rows.Count > 0 && broj_sloga >= 0)
                {
                    cmb_ucenik.SelectedValue = grid_upisnica.Rows[broj_sloga].Cells["ucenik"].Value.ToString();
                    txt_id.Text = grid_upisnica.Rows[broj_sloga].Cells["id"].Value.ToString();
                }
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("INSERT INTO upisnica (odeljenje_id, osoba_id) VALUES ('");
            naredba.Append(cmb_odeljenje.SelectedValue.ToString() + "', '");
            naredba.Append(cmb_ucenik.SelectedValue.ToString() + "')");
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                Grid_populate();
            }
            catch (Exception greska)
            {          
                MessageBox.Show(greska.Message);
            }
            
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("UPDATE upisnica SET ");
            naredba.Append("osoba_id = '" + cmb_ucenik.SelectedValue.ToString() + "'");
            naredba.Append(" WHERE id =" + txt_id.Text);
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                Grid_populate();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string naredba = "DELETE FROM upisnica WHERE id = " + txt_id.Text;
            SqlConnection veza = Koncekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba, veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                Grid_populate();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }
    }
}
