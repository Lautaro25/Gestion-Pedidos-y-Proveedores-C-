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
    public partial class Eliminar : Menú
    {
        private SqlConnection conn;
        private SqlCommand BorrarParametro;
        private String LocalConexion;
        private SqlDataReader SqlLecturaDatos;

        public Eliminar()
        {
            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();
        }

        private void Eliminar_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SqlBorrar;

            conn = new SqlConnection(LocalConexion);

            conn.Open();
            SqlBorrar = "select * from Usuarios where DNI= '" + textBox1.Text + "'";

            BorrarParametro = new SqlCommand(SqlBorrar, conn);
            SqlLecturaDatos = BorrarParametro.ExecuteReader();
            if (textBox1.Text == "")
            {
               DialogResult resultado = MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (SqlLecturaDatos.Read())
                {
                    conn.Close();
                    conn.Open();
                    DialogResult resultado = DialogResult.Yes;
                    resultado = MessageBox.Show("¿Seguro que desea eliminar el registro permanentemente de la base de datos?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.No)
                    {
                        textBox1.Focus();
                        textBox1.Text = "";
                    }
                    else
                    {

                        SqlBorrar = "delete Usuarios where DNI= '" + textBox1.Text + "'";
                        BorrarParametro = new SqlCommand(SqlBorrar, conn);
                        BorrarParametro.ExecuteNonQuery();
                        MessageBox.Show("Eliminación Exitosa", "Usuario Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "Ingrese DNI";
                    }
                }
                else
                {
                    MessageBox.Show("El Usuario que desea eliminar no se encuentra ingrsado", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }       
                   textBox1.Focus();
                    conn.Close();
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }
    }
}
