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
    public partial class Proveedores : Menú
    {
        private SqlConnection conn;
        private SqlCommand BuscarParametro;
        private SqlCommand BorrarParametro;
        private SqlCommand ActualizaParametro;
        private String LocalConexion;
        private SqlDataReader SqlLecturaDatos;
        private SqlCommand InsertaParametro;
        public Proveedores()
        {
            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();
            
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {

            textBox2.Focus();
            string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();

            SqlBuscar = "select * from Proveedores";


            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Nombre y Apellido";
            dataGridView1.Columns[2].Name = "Empresa";
            dataGridView1.Columns[3].Name = "Dirección";
            dataGridView1.Columns[4].Name = "Teléfono";

            BuscarParametro = new SqlCommand(SqlBuscar, conn);
            SqlLecturaDatos = BuscarParametro.ExecuteReader();

            for (int i = 0; i < dataGridView1.Rows.Count && SqlLecturaDatos.Read(); i++)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Empresa"].ToString();
                dataGridView1.Rows[i].Cells[3].Value = SqlLecturaDatos["Dirección"].ToString();
                dataGridView1.Rows[i].Cells[4].Value = "3444-" +SqlLecturaDatos["Telefono"].ToString();

            }
            conn.Close();
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            if (textBox6.Location.Y == 170)
            {
                textBox6.Text = " Ingrese ID";
            }
            textBox6.Location = new Point(textBox6.Location.X, 633);

            textBox6.Location = new Point(textBox6.Location.Y, 221);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (textBox6.Location.Y == 221)
            {
                textBox6.Text = " Ingrese ID";
            }
            textBox6.Location = new Point(textBox6.Location.X, 633);

            textBox6.Location = new Point(textBox6.Location.Y, 170);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string InsertaCliente;
                    InsertaCliente = "INSERT INTO Proveedores(NombreyApellido,Empresa,Dirección,Telefono)";
                    InsertaCliente += "VALUES(@NombreyApellido,@Empresa,@Dirección,@Telefono)";
                    InsertaParametro = new SqlCommand(InsertaCliente, conn);
                    InsertaParametro.Parameters.Add(new SqlParameter("@NombreyApellido", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@NombreyApellido"].Value = textBox2.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@Empresa"].Value = textBox3.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Dirección", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@Dirección"].Value = textBox4.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@Telefono"].Value = textBox5.Text;

                    InsertaParametro.ExecuteNonQuery();

                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    MessageBox.Show("Registro Agregado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }

            }
            textBox2.Focus();
            conn.Close();
            Proveedores_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Proveedores_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

                if (textBox6.Text == "" || textBox6.Text == " Ingrese ID")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
            string SqlBorrar;

            conn = new SqlConnection(LocalConexion);

            conn.Open();
            SqlBorrar = "select * from Proveedores where ID= '" + textBox6.Text + "'";
                BorrarParametro = new SqlCommand(SqlBorrar, conn);
                SqlLecturaDatos = BorrarParametro.ExecuteReader();

                if (SqlLecturaDatos.Read())
                {
                    conn.Close();
                    conn.Open();

                    DialogResult resultado = DialogResult.Yes;
                    resultado = MessageBox.Show("¿Seguro que desea eliminar el registro permanentemente de la base de datos?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.No)
                    {
                        textBox6.Focus();
                        textBox6.Text = "";
                    }
                    else
                    {
                        SqlBorrar = "delete Proveedores where ID= " + "'" + textBox6.Text + "'";
                        BorrarParametro = new SqlCommand(SqlBorrar, conn);
                        BorrarParametro.ExecuteNonQuery();


                    }
                }
                else
                {
                    MessageBox.Show("El Proveedor que desea eliminar no se encuentra ingrsado", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            textBox6.Text = "";
            textBox6.Focus();
            conn.Close();
            Proveedores_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (textBox6.Text == "" || textBox6.Text == " Ingrese ID")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {         
                string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();

            SqlBuscar = "select * from Proveedores where ID= '" + textBox6.Text + "'";
                try
                {
                    BuscarParametro = new SqlCommand(SqlBuscar, conn);
                    SqlLecturaDatos = BuscarParametro.ExecuteReader();

                    dataGridView1.Columns.Clear();

                    dataGridView1.ColumnCount = 5;
                    dataGridView1.Columns[0].Name = "ID";
                    dataGridView1.Columns[1].Name = "Nombre y Apellido";
                    dataGridView1.Columns[2].Name = "Empresa";
                    dataGridView1.Columns[3].Name = "Dirección";
                    dataGridView1.Columns[4].Name = "Teléfono";

                    if (SqlLecturaDatos.Read())
                    {
                        dataGridView1.Rows.Add();

                        dataGridView1.Rows[0].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                        dataGridView1.Rows[0].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                        dataGridView1.Rows[0].Cells[2].Value = SqlLecturaDatos["Empresa"].ToString();
                        dataGridView1.Rows[0].Cells[3].Value = SqlLecturaDatos["Dirección"].ToString();
                        dataGridView1.Rows[0].Cells[4].Value = "3444-" + SqlLecturaDatos["Telefono"].ToString();
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


                textBox6.Text = "Ingrese ID";
            }
            textBox6.Focus();
            conn.Close();
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
                textBox3.Focus();

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space) && (e.KeyChar != (char)Keys.Enter))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 13)
            {
                textBox4.Focus();

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox5.Focus();

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
                button4.Focus();

            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
                if (textBox6.Location.Y == 221)
                {
                    button3.Focus();
                }
                else
                {
                    button1.Focus();
                }

            }
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = string.Empty;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox7.Text == " Ingrese ID")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string SqlBuscar;
                conn = new SqlConnection(LocalConexion);
                conn.Open();
                SqlBuscar = "select * from Proveedores where ID ='"+textBox7.Text+"'";
                try
                {
                    BuscarParametro = new SqlCommand(SqlBuscar, conn);
                    SqlLecturaDatos = BuscarParametro.ExecuteReader();

                    if (SqlLecturaDatos.Read())
                    {

                        textBox2.Text = SqlLecturaDatos["NombreyApellido"].ToString();
                        textBox3.Text = SqlLecturaDatos["Empresa"].ToString(); 
                        textBox4.Text = SqlLecturaDatos["Dirección"].ToString();
                        textBox5.Text = SqlLecturaDatos["Telefono"].ToString(); 

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
            if (textBox7.Text == "" || textBox7.Text == " Ingrese ID")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||textBox5.Text == "")
                {
                    MessageBox.Show("No debe dejar campos sin información", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                string SqlActualizar;

                SqlActualizar = "update Proveedores set ";
                SqlActualizar += "NombreyApellido = " + "'" + textBox2.Text + "',";
                SqlActualizar += "Empresa = " + "'" + textBox3.Text + "',";
                SqlActualizar += "Dirección = " + "'" + textBox4.Text + "',";
                SqlActualizar += "Telefono = " + "'" + textBox5.Text + "'";
                SqlActualizar += "where ID = '"+textBox7.Text+"'";

                ActualizaParametro = new SqlCommand(SqlActualizar, conn);
                conn.Open();
                ActualizaParametro.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Registro Modificado ok...");

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox7.Text = "";
                textBox7.Focus();
            }

            Proveedores_Load(sender, e);

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length > 6)
            {
                MessageBox.Show("Igresar Teléfono de 6 digitos");
            }
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


    }
}
