﻿using System;
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
    public partial class Glavna2 : Form
    {
        public Glavna2()
        {
            InitializeComponent();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raspodela frm_raspodela = new Raspodela();
            frm_raspodela.Show();
        }

        private void osobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Osoba Osoba_frm = new Osoba();
            Osoba_frm.Show();
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

        private void predmetiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik frm_sifarnik = new Sifarnik("predmet");
            frm_sifarnik.Show();
        }

        private void oceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ocena frm_ocena = new Ocena();
            frm_ocena.Show();
        }

        private void odeljenjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Upisnica frm_upisnica = new Upisnica();
            frm_upisnica.Show();
        }

        private void Glavna2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
