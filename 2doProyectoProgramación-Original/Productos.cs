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
    public partial class Productos : Menú
    {
        private SqlConnection conn;
        private SqlCommand BuscarParametro;
        private SqlCommand BorrarParametro;
        private String LocalConexion;
        private SqlDataReader SqlLecturaDatos;
        private SqlCommand InsertaParametro;
        private int cont = 0;
        public Productos()
        {
            InitializeComponent();
            Conexion nuevaConexion = new Conexion();
            nuevaConexion.Fconexion();
            LocalConexion = nuevaConexion.cadena;
            conn = new SqlConnection(LocalConexion);
            conn.Close();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            string SqlBuscar;
            conn = new SqlConnection(LocalConexion);
            conn.Open();
            cont = cont + 1;
            if (cont == 1)
            {
                comboBox2.Items.Add("Productos");
                comboBox2.Items.Add("Proveedores");
            }
            SqlBuscar = "select Proveedores.ID, Proveedores.NombreyApellido, Productos.Código, Productos.NomProducto,Productos.Cantidad, Productos.Precio, Productos.FechaIngreso from Productos inner join Proveedores on Productos.IDProveedor=Proveedores.ID";


            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "ID del Proveedor";
            dataGridView1.Columns[1].Name = "Proveedor";
            dataGridView1.Columns[2].Name = "Código";
            dataGridView1.Columns[3].Name = "Producto";
            dataGridView1.Columns[4].Name = "Cantidad";
            dataGridView1.Columns[5].Name = "Precio";
            dataGridView1.Columns[6].Name = "Fecha de Compra";

            BuscarParametro = new SqlCommand(SqlBuscar, conn);
            SqlLecturaDatos = BuscarParametro.ExecuteReader();

            for (int i = 0; i < dataGridView1.Rows.Count && SqlLecturaDatos.Read(); i++)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Código"].ToString();
                dataGridView1.Rows[i].Cells[3].Value = SqlLecturaDatos["NomProducto"].ToString();
                if (Convert.ToInt32(SqlLecturaDatos["Cantidad"]) < 10)
                {
                    dataGridView1.Rows[i].Cells[4].Value = " "+SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                }
                else
                {
                    dataGridView1.Rows[i].Cells[4].Value = SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                }
                dataGridView1.Rows[i].Cells[5].Value = "$"+SqlLecturaDatos["Precio"].ToString();

                dataGridView1.Rows[i].Cells[6].Value = SqlLecturaDatos["FechaIngreso"].ToString();

            }
            conn.Close();
            if (comboBox2.SelectedIndex == 0)
            {
                textBox6.Text = " Ingrese Producto";
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                textBox6.Text = " Ingrese ID";
            }
            else {
                textBox6.Text = "Elija una opción";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string InsertaCliente;
                    InsertaCliente = "INSERT INTO Productos(Código,IDProveedor,NomProducto,Cantidad,Precio,FechaIngreso)";
                    InsertaCliente += "VALUES(@Código,@IDProveedor,@NomProducto,@Cantidad,@Precio,@FechaIngreso)";
                    InsertaParametro = new SqlCommand(InsertaCliente, conn);
                    InsertaParametro.Parameters.Add(new SqlParameter("@Código", SqlDbType.Int));
                    InsertaParametro.Parameters["@Código"].Value = textBox1.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@IDProveedor", SqlDbType.Int));
                    InsertaParametro.Parameters["@IDProveedor"].Value = textBox2.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@NomProducto", SqlDbType.VarChar));
                    InsertaParametro.Parameters["@NomProducto"].Value = textBox3.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int));
                    InsertaParametro.Parameters["@Cantidad"].Value = textBox4.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@Precio", SqlDbType.Int));
                    InsertaParametro.Parameters["@Precio"].Value = textBox5.Text;
                    InsertaParametro.Parameters.Add(new SqlParameter("@FechaIngreso", SqlDbType.Date));
                    InsertaParametro.Parameters["@FechaIngreso"].Value = dateTimePicker1.Text;

                    InsertaParametro.ExecuteNonQuery();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    dateTimePicker1.Text = "";

                    MessageBox.Show("Registro Agregado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }

            }
            textBox2.Focus();
            conn.Close();
            Productos_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Productos_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox6.Text == " Ingrese Código")
            {
                MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string SqlBorrar;

                conn = new SqlConnection(LocalConexion);

                conn.Open();
                SqlBorrar = "select * from Productos where Código= '" + textBox6.Text + "'";
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
                        SqlBorrar = "delete Productos where Código= " + "'" + textBox6.Text + "'";
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
            Productos_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
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
                    SqlBuscar = "select Proveedores.ID, Proveedores.NombreyApellido, Productos.Código, Productos.NomProducto,Productos.Cantidad, Productos.Precio, Productos.FechaIngreso from Productos inner join Proveedores on Productos.IDProveedor=Proveedores.ID where NomProducto= '" + textBox6.Text + "'";
                    try
                    {
                        BuscarParametro = new SqlCommand(SqlBuscar, conn);
                        SqlLecturaDatos = BuscarParametro.ExecuteReader();

                        dataGridView1.Columns.Clear();

                        dataGridView1.ColumnCount = 7;
                        dataGridView1.Columns[0].Name = "ID del Proveedor";
                        dataGridView1.Columns[1].Name = "Proveedor";
                        dataGridView1.Columns[2].Name = "Código";
                        dataGridView1.Columns[3].Name = "Producto";
                        dataGridView1.Columns[4].Name = "Cantidad";
                        dataGridView1.Columns[5].Name = "Precio";
                        dataGridView1.Columns[6].Name = "Fecha de Compra";

                        if (SqlLecturaDatos.Read())
                        {
                            int i = 0;
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                            dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                            dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Código"].ToString();
                            dataGridView1.Rows[i].Cells[3].Value = SqlLecturaDatos["NomProducto"].ToString();
                            if (Convert.ToInt32(SqlLecturaDatos["Cantidad"]) < 10)
                            {
                                dataGridView1.Rows[i].Cells[4].Value = " " + SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[4].Value = SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                            }
                            dataGridView1.Rows[i].Cells[5].Value = "$" + SqlLecturaDatos["Precio"].ToString();
                            dataGridView1.Rows[i].Cells[6].Value = SqlLecturaDatos["FechaIngreso"].ToString();

                            for (i = 1; i < dataGridView1.Rows.Count && SqlLecturaDatos.Read(); i++)
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                                dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                                dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Código"].ToString();
                                dataGridView1.Rows[i].Cells[3].Value = SqlLecturaDatos["NomProducto"].ToString();
                                if (Convert.ToInt32(SqlLecturaDatos["Cantidad"]) < 10)
                                {
                                    dataGridView1.Rows[i].Cells[4].Value = " " + SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[4].Value = SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                                }
                                dataGridView1.Rows[i].Cells[5].Value = "$" + SqlLecturaDatos["Precio"].ToString();
                                dataGridView1.Rows[i].Cells[6].Value = SqlLecturaDatos["FechaIngreso"].ToString();
                            }
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


                    textBox6.Text = "";
                }
                textBox6.Focus();
                conn.Close();
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                if (textBox6.Text == "" || textBox6.Text == " Ingrese Código")
                {
                    MessageBox.Show("Debe completar el campo", "Falta de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string SqlBuscar;
                    conn = new SqlConnection(LocalConexion);
                    conn.Open();
                    SqlBuscar = "select Proveedores.ID, Proveedores.NombreyApellido, Productos.Código, Productos.NomProducto,Productos.Cantidad,Productos.Precio, Productos.FechaIngreso from Productos inner join Proveedores on Productos.IDProveedor=Proveedores.ID where ID= " + textBox6.Text + "";
                    try
                    {
                        BuscarParametro = new SqlCommand(SqlBuscar, conn);
                        SqlLecturaDatos = BuscarParametro.ExecuteReader();

                        dataGridView1.Columns.Clear();

                        dataGridView1.ColumnCount = 7;
                        dataGridView1.Columns[0].Name = "ID del Proveedor";
                        dataGridView1.Columns[1].Name = "Proveedor";
                        dataGridView1.Columns[2].Name = "Código";
                        dataGridView1.Columns[3].Name = "Producto";
                        dataGridView1.Columns[4].Name = "Cantidad";
                        dataGridView1.Columns[5].Name = "Precio";
                        dataGridView1.Columns[6].Name = "Fecha de Compra";

                        if (SqlLecturaDatos.Read())
                        {
                            int i = 0;
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                            dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                            dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Código"].ToString();
                            dataGridView1.Rows[i].Cells[3].Value = SqlLecturaDatos["NomProducto"].ToString();
                            if (Convert.ToInt32(SqlLecturaDatos["Cantidad"]) < 10)
                            {
                                dataGridView1.Rows[i].Cells[4].Value = " " + SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[4].Value = SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                            }
                            dataGridView1.Rows[i].Cells[5].Value = "$" + SqlLecturaDatos["Precio"].ToString();
                            dataGridView1.Rows[i].Cells[6].Value = SqlLecturaDatos["FechaIngreso"].ToString();
                            for (i = 1; i < dataGridView1.Rows.Count && SqlLecturaDatos.Read(); i++)
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[i].Cells[0].Value = SqlLecturaDatos["ID"].ToString();
                                dataGridView1.Rows[i].Cells[1].Value = SqlLecturaDatos["NombreyApellido"].ToString();
                                dataGridView1.Rows[i].Cells[2].Value = SqlLecturaDatos["Código"].ToString();
                                dataGridView1.Rows[i].Cells[3].Value = SqlLecturaDatos["NomProducto"].ToString();
                                if (Convert.ToInt32(SqlLecturaDatos["Cantidad"]) < 10)
                                {
                                    dataGridView1.Rows[i].Cells[4].Value = " " + SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[4].Value = SqlLecturaDatos["Cantidad"].ToString() + " Unidades";
                                }
                                dataGridView1.Rows[i].Cells[5].Value = "$" + SqlLecturaDatos["Precio"].ToString();
                                dataGridView1.Rows[i].Cells[6].Value = SqlLecturaDatos["FechaIngreso"].ToString();
                            }
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


                    textBox6.Text = "";
                }
                textBox6.Focus();
                conn.Close();
            }
            else {
                MessageBox.Show("Debe elegir un opción antes de realizar la busqueda","Falta de Datos",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
 
            if (textBox6.Location.Y == 221)
            {
                textBox6.Text = " Ingrese Código";
            }
            textBox6.Location = new Point(textBox6.Location.X, 140);
            this.Size = new System.Drawing.Size(876, 525);

        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {

            if (textBox6.Location.Y == 140)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    textBox6.Text = " Ingrese Producto";
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                textBox6.Text = " Ingrese ID";
                }
                else
                {
                    textBox6.Text = " Elija una opción";
                }
            }
            textBox6.Location = new Point(textBox6.Location.X, 221);
            this.Size = new System.Drawing.Size(876, 525);


        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = string.Empty;
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

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
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
                textBox3.Focus();

            }
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
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
                textBox5.Focus();

            }
        }

        private void textBox5_KeyPress_1(object sender, KeyPressEventArgs e)
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
                dateTimePicker1.Focus();

            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button4.Focus();

            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
          
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

        private void comboBox2_MouseEnter(object sender, EventArgs e)
        {

        }



    }
}
