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
    public partial class Prestamo : Form
    {
        public Prestamo()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        DataSet datas;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = txtLibro.Text.Trim(),
                    isbn = txtIsbn.Text.Trim(),
                    usuario = txtUsuario.Text.Trim(),
                    contra = txtContraseña.Text.Trim(),
                    nombre = txtNombre.Text.Trim(),
                    apellido1 = txtPaterno.Text.Trim(),
                    apellido2 = txtMaterno.Text.Trim(),
                    fecha = txtEntrega.Value.ToShortDateString();
                datas = Conexions.Procedimiento("PROCE_PRESTAMO",
                new SqlParameter("@title", titulo),
                new SqlParameter("@isbn", isbn),
                new SqlParameter("@usuario", usuario),
                new SqlParameter("@contra", contra),
                new SqlParameter("@nom_u", nombre),
                new SqlParameter("@ape_u", apellido1),
                new SqlParameter("@ape2_u", apellido2),
                new SqlParameter("@fec_dev", fecha)); 
                MessageBox.Show("Registro" + datas.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Registro" , "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
