using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace _2doProyectoProgramación
{
    public partial class Menú : Form
    {
        public Menú()
        {
            InitializeComponent();

        }


        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Agregar nfrm2 = new Agregar();
            this.Close();
            nfrm2.Show();
        }

        private void Menú_Load(object sender, EventArgs e)
        {
            if (this.GetType() == typeof(Menú))
            {
                panel1.Visible = true;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar nfrm2 = new Eliminar();
            this.Close();
            nfrm2.Show();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buscar nfrm2 = new Buscar();
            this.Close();
            nfrm2.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cargarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clientes nfrm2 = new Clientes();
            this.Close();
            nfrm2.Show();
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cargarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Proveedores nfrm2 = new Proveedores();
            this.Close();
            nfrm2.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 nfrm2 = new Form1();
            this.Close();
            nfrm2.panel1.Visible = false;
            nfrm2.Show();
            
        }

        private void gestionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos nfrm2 = new Productos();
            this.Close();
            nfrm2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



    }
}
