using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _2doProyectoProgramación
{
    public partial class Agregar : Menú
    {    
            private SqlConnection conn;
            private SqlCommand InsertaParametro;
            private String LocalConexion;
        public Agregar()
        {
            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();
        }

        private void Agragar_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            conn.Open();
             if (textBox1.Text=="" || textBox2.Text=="" || textBox3.Text=="")
	{
        MessageBox.Show("Debe completar todos los campos", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}
             else
             {
            try
            {


                string InsertaUsuario;
                InsertaUsuario = "INSERT INTO Usuarios(DNI,NomUsuarios,Contrasena)";
                InsertaUsuario += "VALUES(@DNI,@NomUsuarios,@Contrasena)";
                InsertaParametro = new SqlCommand(InsertaUsuario,conn);
                InsertaParametro.Parameters.Add(new SqlParameter("@NomUsuarios", SqlDbType.VarChar));
                InsertaParametro.Parameters["@NomUsuarios"].Value = textBox1.Text;
                InsertaParametro.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.VarChar));
                InsertaParametro.Parameters["@Contrasena"].Value = textBox2.Text;
                InsertaParametro.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar));
                InsertaParametro.Parameters["@DNI"].Value = textBox3.Text;

                InsertaParametro.ExecuteNonQuery();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                MessageBox.Show("Registro Agregado");

    }

            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ ex.Message);
            }

                  }  
            conn.Close();
            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox3.Focus();

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (e.KeyChar == 13)
            {

            }
            else
            {

                e.Handled = true;
                MessageBox.Show("Solo se permiten Números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (e.KeyChar == 13)
            {
                button2.Focus();

            }
        }
    }
}







