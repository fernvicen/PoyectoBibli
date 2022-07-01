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
    public partial class Admin : Form
    {
        DataSet data;
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
        private void bntRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(txtCodigo.Text.Trim());
                string direccion = txtDireccion.Text.Trim(),
                    colonia = txtColonia.Text.Trim(),
                    ciudad = txtCiudad.Text.Trim(),
                    pais = txtPais.Text.Trim(),
                    telefono = txtTelefono.Text.Trim();
                data = Conexions.Procedimiento("PROCE_DIRECCION",
                   new SqlParameter("@direccion", direccion),
                   new SqlParameter("@colonia", colonia),
                   new SqlParameter("@ciudad", ciudad),
                   new SqlParameter("@pais", pais),
                   new SqlParameter("@codigo", codigo),
                   new SqlParameter("@tele", telefono));
                string nombre = txtNombre.Text.Trim(),
                    apellido = txtPaterno.Text.Trim(),
                    apellido2 = txtMaterno.Text.Trim(),
                    email = txtCorreo.Text.Trim(),
                    usuario = txtUsuario.Text.Trim(),
                    contrase = txtContra.Text.Trim();
                int activo = txtEstado.SelectedIndex;
                data = Conexions.Procedimiento("PROCE_BIBLIOTECARIO",
                       new SqlParameter("@nom", nombre),
                       new SqlParameter("@ape1", apellido),
                       new SqlParameter("@ape2", apellido2),
                       new SqlParameter("@email", email),
                       new SqlParameter("@act", activo),
                       new SqlParameter("@user", usuario),
                       new SqlParameter("@contra", contrase),
                       new SqlParameter("@codigo", codigo),
                       new SqlParameter("@direccion", direccion));
                MessageBox.Show("Agregar: " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("Registro " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet data;
                string nombre = txtNombre.Text.Trim(),
                usuario = txtUsuario.Text.Trim(),
                contrase = txtContra.Text.Trim();
                data = Conexions.Procedimiento("ELIMINAR_BIBLIOTECARIO",
                    new SqlParameter("@nombre", nombre),
                    new SqlParameter("@usuario", usuario),
                    new SqlParameter("@contrasena", contrase));
                MessageBox.Show("Eliminar " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Registro", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
            try
            {
                string nombre = txtNombre.Text.Trim(),
                    apellido = txtPaterno.Text.Trim(),
                    apellido2 = txtMaterno.Text.Trim(),
                    email = txtCorreo.Text.Trim(),
                    usuario = txtUsuario.Text.Trim(),
                    contrase = txtContra.Text.Trim();
                DataSet data3 = Conexions.Procedimiento("PROCE_BIBLIOTECARIO",
                       new SqlParameter("@nom", nombre),
                       new SqlParameter("@ape1", apellido),
                       new SqlParameter("@ape2", apellido2),
                       new SqlParameter("@user", usuario),
                       new SqlParameter("@contra", contrase));
                MessageBox.Show("Agregar: " + data3.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("Registro Actualizado", "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
