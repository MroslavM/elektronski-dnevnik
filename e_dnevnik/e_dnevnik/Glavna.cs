using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_dnevnik
{
    public partial class Glavna : Form
    {
        public Glavna()
        {
            InitializeComponent();
        }

        private void osobaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Osoba Osoba_frm = new Osoba();
            Osoba_frm.Show();
        }

        private void Glavna_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Glavna_Load(object sender, EventArgs e)
        {
            string user = Program.user_ime + " " + Program.user_prezime + " " + Program.user_uloga;
            lbl_user.Text = user;
        }

        private void smeroviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik frm_sifarnik = new Sifarnik("smer");
            frm_sifarnik.Show();

        }

        private void skolskeGodineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik frm_sifarnik = new Sifarnik("skolska_godina");
            frm_sifarnik.Show();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raspodela frm_raspodela = new Raspodela();
            frm_raspodela.Show();
        }

        private void predmetiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik frm_sifarnik = new Sifarnik("predmet");
            frm_sifarnik.Show();
        }

        private void osobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik frm_sifarniik = new Sifarnik("osoba");
            frm_sifarniik.Show();
        }
    }
}
