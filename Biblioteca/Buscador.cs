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
    public partial class Buscador : Form
    {
        public Buscador()
        {
            InitializeComponent();
        }

        private void btnBus_Click(object sender, EventArgs e)
        {
            string datos = textBox1.Text.Trim();
            DataSet data = Conexions.Procedimiento("BUSCAR_LIBRO",
                new SqlParameter("@ISBN", datos));
            da.DataSource = data.Tables[0];
            da.Refresh();
            groupBox1.Text = "LIBRO BUSCADO";
            textBox1.Text = "";
        }
    }
}
