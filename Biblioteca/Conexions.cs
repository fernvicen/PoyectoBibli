using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Biblioteca
{
    class Conexions
    {
        static SqlConnection con = new SqlConnection(@"Data Source=FERNANDO\FERNANDO;Initial Catalog=biblioteca;Integrated Security=True");
        public static void Conectar()
        {
            try
            {
                con.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("servidor no disponible");
            }


        }
        public static void Desconectar()
        {
            try
            {
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("servidor no disponible");
            }


        }
        public static SqlConnection Conexion()
        {
            return con;
        }
        public static DataSet Procedimiento(String procedimiento, params SqlParameter[] datos)
        {
            Conectar();
            SqlCommand comando = new SqlCommand(procedimiento, Conexion());
            comando.CommandType = CommandType.StoredProcedure;
            foreach (var p in datos)
            {
                comando.Parameters.Add(p);
            }
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataSet data = new DataSet();
            adaptador.Fill(data);
            Desconectar();
            return data;
        }
        public static void Funcion(String procedimiento,params SqlParameter[] datos)
        {
            SqlCommand comando = new SqlCommand(procedimiento, Conexion());
            comando.CommandType = CommandType.StoredProcedure;
            foreach (var p in datos)
            {
                comando.Parameters.Add(p);
            }
            Conectar();
            comando.ExecuteNonQuery();
            Desconectar();
        }
    }
}
