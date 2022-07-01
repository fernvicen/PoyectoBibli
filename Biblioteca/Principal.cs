using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class Principal : Form
    {
        BPrinicipal bPrinicipal = new BPrinicipal();
        Prestamo prestamo = new Prestamo();
        Usuario usuario = new Usuario();
        Libro libro = new Libro();
        Admin admin = new Admin();
        Buscador buscar = new Buscador();
        public Principal()
        {
            InitializeComponent();
            bPrinicipal.MdiParent = this;
            contenedor.Panel2.Controls.Add(bPrinicipal);
            bPrinicipal.Show();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult opcion = MessageBox.Show("Realmente desea Salir", "Salir del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion==DialogResult.Yes)
            {
                Form2 form1 = new Form2();
                form1.Show();
                this.Hide();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            libro.MdiParent = this;
            contenedor.Panel2.Controls.Add(libro);
            libro.Show();
            bPrinicipal.Hide();
            prestamo.Hide();
            usuario.Hide();
            admin.Hide();
            buscar.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuario.MdiParent = this;
            contenedor.Panel2.Controls.Add(usuario);
            usuario.Show();
            bPrinicipal.Hide();
            prestamo.Hide();
            libro.Hide();
            admin.Hide();
            buscar.Hide();
        }

        private void btnPrestano_Click(object sender, EventArgs e)
        {
            prestamo.MdiParent = this;
            contenedor.Panel2.Controls.Add(prestamo);
            prestamo.Show();
            bPrinicipal.Hide();
            usuario.Hide();
            libro.Hide();
            admin.Hide();
            buscar.Hide();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            bPrinicipal.MdiParent = this;
            contenedor.Panel2.Controls.Add(bPrinicipal);
            bPrinicipal.Show();
            prestamo.Hide();
            usuario.Hide();
            libro.Hide();
            admin.Hide();
            buscar.Hide();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            admin.MdiParent = this;
            contenedor.Panel2.Controls.Add(admin);
            admin.Show();
            prestamo.Hide();
            usuario.Hide();
            libro.Hide();
            bPrinicipal.Hide();
            buscar.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.facebook.com/holasoyferbs1.0/");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.instagram.com/holasoyferbs/");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar.MdiParent = this;
            contenedor.Panel2.Controls.Add(buscar);
            buscar.Show();
            admin.Hide();
            prestamo.Hide();
            usuario.Hide();
            libro.Hide();
            bPrinicipal.Hide();
        }
    }
}
