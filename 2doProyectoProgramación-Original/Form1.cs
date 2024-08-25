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
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        private SqlDataReader SqlLecturaDatos;
        private String LocalConexion;

        public Form1()
        {

            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();
        }



        private void Form1_Load(object sender, EventArgs e)
        {



            textBox3.Focus();
           panel1.Location = new Point(panel1.Location.X, 3);

           panel1.Location = new Point(panel1.Location.Y, 3);

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            string SqlBusqueda = "SELECT * FROM Usuarios WHERE NomUsuarios ='"+textBox1.Text+"' and Contrasena ='"+textBox2.Text+"'";
            SqlCommand comando = new SqlCommand(SqlBusqueda,conn);
            SqlLecturaDatos = comando.ExecuteReader();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos","Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (SqlLecturaDatos.HasRows == true)
                {
                    Menú nfrm2 = new Menú();
                    this.Hide();
                    nfrm2.Show();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrectos", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }  
            }    
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
                conn.Close();

    }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
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
                button2.Focus();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            textBox3.Focus();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                panel1.Visible = false;
                textBox1.Focus();
            } 
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
   }
 }
