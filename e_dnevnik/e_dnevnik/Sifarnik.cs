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
    public partial class Sifarnik : Form
    {
        SqlDataAdapter Adapter;
        DataTable tabela;
        string tabela_ime;
        public Sifarnik(string tabela)
        {
            tabela_ime = tabela;
            InitializeComponent();
        }

        private void Sifarnik_Load(object sender, EventArgs e)
        {
            Adapter = new SqlDataAdapter("SELECT * FROM " + tabela_ime, Koncekcija.Connect());
            tabela = new DataTable();
            Adapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataGridView1.Columns["id"].ReadOnly = true;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            DataTable menjao = tabela.GetChanges();
            Adapter.UpdateCommand = new SqlCommandBuilder(Adapter).GetUpdateCommand();
            if (menjao != null)
            {
                Adapter.Update(menjao);
                this.Close();
            }
        }
    }
}
