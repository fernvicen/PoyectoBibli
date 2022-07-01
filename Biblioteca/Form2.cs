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
    public partial class Form2 : Form
    {
        private DataSet data;
        public Form2()
        {
            InitializeComponent();
        }
        private void inicios()
        {
            String user = txtUsuario.Text, contra = txtContraseña.Text;
            data = Conexions.Procedimiento("logi",
                new SqlParameter("@usu", user),
                new SqlParameter("@contra", contra));
            if (user == data.Tables[0].Rows[0][0].ToString())
            {
                MessageBox.Show("Bienvenidos: " + data.Tables[0].Rows[0][0].ToString(), "Sistema BIBLIOTECA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Principal p = new Principal();
                p.Show();
            }
            else
            {
                MessageBox.Show(data.Tables[0].Rows[0][0].ToString() + "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inicios();
        }
    }
}
