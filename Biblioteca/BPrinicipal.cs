using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace Biblioteca
{
    public partial class BPrinicipal : Form
    {
        public BPrinicipal()
        {
            InitializeComponent();
            DataSet datos = Conexions.Procedimiento("todos");
            dataGridView1.DataSource = datos.Tables[0];
            dataGridView1.Refresh();
            groupBox1.Text = "En Biblioteca Libro";
        }

        private void BPrinicipal_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet datos = Conexions.Procedimiento("administrador");
            dataGridView1.DataSource = datos.Tables[0];
            dataGridView1.Refresh();
            groupBox1.Text = "Datos de Administrador";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet datos = Conexions.Procedimiento("topprestamos");
            dataGridView1.DataSource = datos.Tables[0];
            dataGridView1.Refresh();
            groupBox1.Text = "Prestamos Realizados";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataSet datos = Conexions.Procedimiento("todos");
            dataGridView1.DataSource = datos.Tables[0];
            dataGridView1.Refresh();
            groupBox1.Text = "En Biblioteca Libro";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet datos = Conexions.Procedimiento("cliente");
            dataGridView1.DataSource = datos.Tables[0];
            dataGridView1.Refresh();
            groupBox1.Text = "Datos de Usuario";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = doc };
            ((Form)ppd).WindowState = FormWindowState.Maximized;

            doc.PrintPage += delegate (object ev, PrintPageEventArgs ep)
            {
                const int DGV_ALTO = 35;
                int left = ep.MarginBounds.Left, top = ep.MarginBounds.Top;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    ep.Graphics.DrawString(col.HeaderText, new Font("Segoe UI", 12, FontStyle.Bold), Brushes.DeepSkyBlue, left, top);
                    left += col.Width;

                    if (col.Index < dataGridView1.ColumnCount - 1)
                        ep.Graphics.DrawLine(Pens.Gray, left - 5, top, left - 5, top + 43 + (dataGridView1.RowCount - 1) * DGV_ALTO);
                }
                left = ep.MarginBounds.Left;
                ep.Graphics.FillRectangle(Brushes.Black, left, top + 40, ep.MarginBounds.Right - left, 3);
                top += 43;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index == dataGridView1.RowCount - 1) break;
                    left = ep.MarginBounds.Left;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        ep.Graphics.DrawString(Convert.ToString(cell.Value), new Font("Segoe UI", 11), Brushes.Black, left, top + 4);
                        left += cell.OwningColumn.Width;
                    }
                    top += DGV_ALTO;
                    ep.Graphics.DrawLine(Pens.Gray, ep.MarginBounds.Left, top, ep.MarginBounds.Right, top);
                }
            };
            ppd.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string datos = txtBuscar.Text.Trim();
            DataSet data = Conexions.Procedimiento("BUSCAR_LIBRO2",
                new SqlParameter("@ISBN", datos));
            dataGridView1.DataSource = data.Tables[0];
            dataGridView1.Refresh();
            groupBox1.Text = "LIBRO BUSCADO";
        }
    }
}
