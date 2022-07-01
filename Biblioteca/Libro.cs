using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class Libro : Form
    {
        private DataSet data;
        public Libro()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = txtTitulo.Text,
                editorial = txtEditorial.Text,
                isbn = txtIsbn.Text, categoria = txtCategoria.Text,
                idioma = txtIdioma.Text, Resumen = txtResumen.Text,
                nombre = txtNombre.Text, apellido = txtApellido.Text;
                int paginas = Convert.ToInt32(txtPagina.Text),
                    año = Convert.ToInt32(txtAño.Text);
                data = Conexions.Procedimiento("PROCE_AUTOR",
                     new SqlParameter("@nom", nombre),
                     new SqlParameter("@ape", apellido));
                data = Conexions.Procedimiento("PROCE_LIBRO",
                     new SqlParameter("@titulo", titulo),
                     new SqlParameter("@res", Resumen),
                     new SqlParameter("@num", paginas),
                     new SqlParameter("@edit", editorial),
                     new SqlParameter("@isb", isbn),
                     new SqlParameter("@año", año),
                     new SqlParameter("@categ", categoria),
                     new SqlParameter("@idio", idioma),
                     new SqlParameter("@nom", nombre),
                     new SqlParameter("@ape", apellido)
                     );
                MessageBox.Show("Agregar: " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception)
            {

                MessageBox.Show("Registro", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = txtTitulo.Text,
                isbn = txtIsbn.Text;
               data = Conexions.Procedimiento("ELIMINAR_LIBRO",
                    new SqlParameter("@titulo", titulo),
                    new SqlParameter("@isnb", isbn));
                MessageBox.Show("Eliminar: " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception)
            {
                MessageBox.Show("Registro", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
