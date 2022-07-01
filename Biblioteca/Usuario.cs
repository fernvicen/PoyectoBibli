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
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            DataSet data;
            string nombre = txtNombre.Text.Trim(),
                apellido1 = txtPaterno.Text.Trim(),
                apellido2 = txtMaterno.Text.Trim(),
                email = txtCorreo.Text.Trim();
            int codigo = Convert.ToInt32(txtCodigo.Text.Trim());
            string direccion = txtDireccion.Text.Trim(),
                colonia = txtColonia.Text.Trim(),
                ciudad = txtCiudad.Text.Trim(),
                pais = txtPais.Text.Trim(),
                telefono = txtTelefono.Text.Trim();
            try
            {
                data = Conexions.Procedimiento("PROCE_DIRECCION",
                    new SqlParameter("@direccion", direccion),
                    new SqlParameter("@colonia", colonia),
                    new SqlParameter("@ciudad", ciudad),
                    new SqlParameter("@pais", pais),
                    new SqlParameter("@codigo", codigo),
                    new SqlParameter("@tele", telefono));
                data = Conexions.Procedimiento("PROCE_CLIENTE",
                    new SqlParameter("@nombre", nombre),
                    new SqlParameter("@apellido1", apellido1),
                    new SqlParameter("@apellido2", apellido2),
                    new SqlParameter("@email", email),
                    new SqlParameter("@codigo", codigo),
                    new SqlParameter("@direccion", direccion));
                MessageBox.Show("Agregar: " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Registro", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataSet data;
            string nombre = txtNombre.Text.Trim(),
                apellido1 = txtPaterno.Text.Trim(),
                email = txtCorreo.Text.Trim();
            try
            {
                data = Conexions.Procedimiento("ELIMINAR_CLIENTE",
                    new SqlParameter("@nombre",nombre),
                    new SqlParameter("@apellido",apellido1),
                    new SqlParameter("@email",email));
                MessageBox.Show("Eliminar " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {

                MessageBox.Show("Registro", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            DataSet data;
            string nombre = txtNombre.Text.Trim(),
                apellido1 = txtPaterno.Text.Trim(),
                apellido2 = txtMaterno.Text.Trim(),
                email = txtCorreo.Text.Trim();
            try
            {
                data = Conexions.Procedimiento("ACTUALIZAR_USUARIO",
                new SqlParameter("@nom", nombre),
                new SqlParameter("@ape1", apellido1),
                new SqlParameter("@ape2", apellido2),
                new SqlParameter("@email", email));
                MessageBox.Show("Actualizar " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Registro", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
