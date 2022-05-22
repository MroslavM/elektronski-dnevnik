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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            

            
            if (txt_email.Text == "" || txt_pass == null)
            {
                MessageBox.Show("Morate uneti i email i sifru kako bi ste pristupili e-dnevniku");
                return;
            }
            else
            {
                try
                {
                    SqlConnection veza = Koncekcija.Connect();
                    SqlCommand komanda = new SqlCommand("SELECT * FROM Osoba WHERE email = @username", veza);
                    komanda.Parameters.AddWithValue("@username", txt_email.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(komanda);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    int brojac = tabela.Rows.Count;
                    if (brojac == 1)
                    {
                        if (String.Compare(tabela.Rows[0]["pass"].ToString(), txt_pass.Text) == 0)
                        {
                            MessageBox.Show("Login uspesan!");
                            Program.user_ime = tabela.Rows[0]["ime"].ToString();
                            Program.user_prezime = tabela.Rows[0]["prezime"].ToString();
                            Program.user_uloga = (int)tabela.Rows[0]["uloga"];
                            this.Hide();
                            Glavna2 glavna = new Glavna2();
                            glavna.Show(); 
                        }
                        else
                        {
                            MessageBox.Show("Uneli ste pogresnu lozinku.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uneli ste nepostojeci email");
                    }

                }
                catch (Exception greska)
                {
                    MessageBox.Show(greska.Message);
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
