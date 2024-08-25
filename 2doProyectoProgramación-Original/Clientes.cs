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
    public partial class Clientes : Agregar
    {

        private SqlConnection conn;
        private SqlCommand BuscarParametro;
        private SqlCommand BorrarParametro;
        private SqlCommand ActualizaParametro;
        private String LocalConexion;
        private SqlDataReader SqlLecturaDatos;
        private SqlCommand InsertaParametro;
        public Clientes()
        {
            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();

            SqlBuscar = "select * from Clientes";

            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "DNI";
            dataGridView1.Columns[1].Name = "Nombre y Apellido";
            dataGridView1.Columns[2].Name = "Dirección";
            dataGridView1.Columns[3].Name = "Teléfono";

            BuscarParametro = new SqlCommand(SqlBuscar, conn);
            SqlLecturaDatos = BuscarParametro.ExecuteReader();

            for (int i = 0; i < dataGridView1.Rows.Count && SqlLecturaDatos.Read(); i++)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["DNI"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Dirección"].ToString();
                dataGridView1.Rows[i].Cells[3].Value = "3444-" + SqlLecturaDatos["Telefono"].ToString();

            }
            conn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {   
            if (textBox5.Location.Y == 170)
            {
                textBox5.Text = " Ingrese DNI";
            }
            textBox5.Location = new Point(textBox5.Location.X, 633);

            textBox5.Location = new Point(textBox5.Location.Y, 221);

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        { 
            if(textBox5.Location.Y == 221){
                textBox5.Text = " Ingrese DNI";
            }
            textBox5.Location = new Point(textBox5.Location.X, 633);

            textBox5.Location = new Point(textBox5.Location.Y, 170);

            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (textBox1.Text == "" || textBox2.Text == "" || textBox6.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string InsertaCliente;
                    InsertaCliente = "INSERT INTO Clientes(DNI,NombreyApellido,Dirección,Telefono)";
                    InsertaCliente += "VALUES(@DNI,@NombreyApellido,@Dirección,@Telefono)";
                    InsertaParametro = new SqlCommand(InsertaCliente, conn);
                    InsertaParametro.Parameters.Add(new SqlParameter("@DNI", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@DNI"].Value = textBox1.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@NombreyApellido", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@NombreyApellido"].Value = textBox2.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Dirección", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@Dirección"].Value = textBox6.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@Telefono"].Value = textBox4.Text;

                    InsertaParametro.ExecuteNonQuery();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox6.Text = "";
                    textBox4.Text = "";
                    MessageBox.Show("Registro Agregado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    
                }
               
            } 
            textBox1.Focus();
            conn.Close();
            Clientes_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clientes_Load(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {  
            string SqlBorrar;

            conn = new SqlConnection(LocalConexion);

            conn.Open();
            SqlBorrar = "select * from Clientes where DNI= '" + textBox5.Text + "'";
             BorrarParametro = new SqlCommand(SqlBorrar, conn);
             SqlLecturaDatos = BorrarParametro.ExecuteReader();
             if (textBox5.Text == "" || textBox5.Text == " Ingrese DNI")
                       {
                           MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                   textBox5.Focus();
                                   textBox5.Text = "";
                               }
                               else
                               {
                                   SqlBorrar = "delete Clientes where DNI= '" + textBox5.Text + "'";
                                   BorrarParametro = new SqlCommand(SqlBorrar, conn);
                                   BorrarParametro.ExecuteNonQuery();
                                   MessageBox.Show("Eliminación Exitosa", "Usuario Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   textBox5.Text = " Ingrese DNI";

                               }
                           }
                           else
                           {
                               MessageBox.Show("El Usuario que desea eliminar no se encuentra ingrsado", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                           }
                       }   
            textBox5.Focus();
            conn.Close();
            Clientes_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();

            SqlBuscar = "select * from Clientes where DNI= '" + textBox5.Text + "'";

            if (textBox5.Text == "" || textBox5.Text == " Ingrese DNI")
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

                               dataGridView1.ColumnCount = 4;
                               dataGridView1.Columns[0].Name = "DNI";
                               dataGridView1.Columns[1].Name = "Nombre y Apellido";
                               dataGridView1.Columns[2].Name = "Dirección";
                               dataGridView1.Columns[3].Name = "Teléfono";

                               if (SqlLecturaDatos.Read())
                               {
                                   dataGridView1.Rows.Add();

                                   dataGridView1.Rows[0].Cells[0].Value = SqlLecturaDatos["DNI"].ToString();
                                   dataGridView1.Rows[0].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                                   dataGridView1.Rows[0].Cells[2].Value = SqlLecturaDatos["Dirección"].ToString();
                                   dataGridView1.Rows[0].Cells[3].Value = "3444-" + SqlLecturaDatos["Telefono"].ToString();
                               }
                               else
                               {
                                   MessageBox.Show("No se encontraron resultados para el DNI proporcionado.", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                   
                               }
                           }
                           catch (Exception ex)
                           {
                               MessageBox.Show("Error: " + ex.Message);
                           }


                           textBox5.Text = " Ingrese DNI";
                       } 
            textBox5.Focus();
            conn.Close();
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
                textBox2.Focus();

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Enter))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 13)
            {
                textBox6.Focus();

            }
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
                button4.Focus();

            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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
                if (textBox5.Location.Y == 221)
                {
                    button3.Focus();
                }
                else {
                    button1.Focus();
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox7.Text == " Ingrese DNI")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
              {
                string SqlBuscar;
                conn = new SqlConnection(LocalConexion);
                conn.Open();
                SqlBuscar = "select * from Clientes where DNI ='" + textBox7.Text + "'";
                try
                {
                    BuscarParametro = new SqlCommand(SqlBuscar, conn);
                    SqlLecturaDatos = BuscarParametro.ExecuteReader();

                    if (SqlLecturaDatos.Read())
                    {

                        textBox1.Text = SqlLecturaDatos["DNI"].ToString();
                        textBox2.Text = SqlLecturaDatos["NombreyApellido"].ToString();
                        textBox6.Text = SqlLecturaDatos["Dirección"].ToString();
                        textBox4.Text = SqlLecturaDatos["Telefono"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("No se encontraron resultados para el Código proporcionado.", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            textBox7.Focus();
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                if (textBox1.Text == "" || textBox2.Text == "" || textBox6.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("No debe dejar campos sin información", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string SqlActualizar;

                    SqlActualizar = "update Clientes set ";
                    SqlActualizar += "DNI = " + "'" + textBox1.Text + "',";
                    SqlActualizar += "NombreyApellido = " + "'" + textBox2.Text + "',";
                    SqlActualizar += "Dirección = " + "'" + textBox6.Text + "',";
                    SqlActualizar += "Telefono = " + "'" + textBox4.Text + "'";
                    SqlActualizar += "where DNI = '" + textBox7.Text + "'";

                    ActualizaParametro = new SqlCommand(SqlActualizar, conn);
                    conn.Open();
                    ActualizaParametro.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registro Modificado ok...");

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox6.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                    textBox7.Focus();
                }

            Clientes_Load(sender, e);
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
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
                button7.Focus();

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 6)
            {
                MessageBox.Show("Igresar Teléfono de 6 digitos");
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox4.Focus();

            }
        }


    }
}
