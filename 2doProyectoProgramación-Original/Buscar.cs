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
    public partial class Buscar : Menú
    {            
        private SqlConnection conn;
        private SqlCommand BuscarParametro;
        private String LocalConexion;
        private SqlDataReader SqlLecturaDatos;
        public Buscar()
        {
            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();

            SqlBuscar = "select * from Usuarios where DNI= '"+textBox1.Text+"'";
            if (textBox1.Text == "")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    BuscarParametro = new SqlCommand(SqlBuscar, conn);
                    SqlLecturaDatos = BuscarParametro.ExecuteReader();

                    dataGridView1.Columns.Clear();

                    dataGridView1.ColumnCount = 3;
                    dataGridView1.Columns[0].Name = "DNI";
                    dataGridView1.Columns[1].Name = "Usuario";
                    dataGridView1.Columns[2].Name = "Contraseña";

                    if (SqlLecturaDatos.Read())
                    {
                        dataGridView1.Rows.Add();

                        dataGridView1.Rows[0].Cells[0].Value = SqlLecturaDatos["DNI"].ToString();
                        dataGridView1.Rows[0].Cells[1].Value = SqlLecturaDatos["NomUsuarios"].ToString();
                        dataGridView1.Rows[0].Cells[2].Value = SqlLecturaDatos["Contrasena"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron resultados para el DNI proporcionado.", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       Buscar_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    throw;
                }
            }
            
                conn.Close();
                textBox1.Text="";
                textBox1.Focus();


            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Buscar_Load(object sender, EventArgs e)
        {
            string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();

            SqlBuscar = "select * from Usuarios";

            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "DNI";
            dataGridView1.Columns[1].Name = "Usuario";
            dataGridView1.Columns[2].Name = "Contraseña";

            BuscarParametro = new SqlCommand(SqlBuscar, conn);
            SqlLecturaDatos = BuscarParametro.ExecuteReader();

            for (int i = 0; i < dataGridView1.Rows.Count && SqlLecturaDatos.Read(); i++)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["DNI"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NomUsuarios"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Contrasena"].ToString();

             
            }
            textBox1.Text = "Ingrese DNI";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar_Load(sender, e);
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
