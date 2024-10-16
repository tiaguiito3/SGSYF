﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SGSYF_conexion;

namespace SGSYF
{
    public partial class Categorias : Form
    {
        public Categorias()
        {
            InitializeComponent();


            CategoriaDevolver();
        }
        public void CategoriaDevolver()
        {
            Conexion conexion = new Conexion();
            MySqlConnection mySqlConnection = conexion.Establecer_Conexion();
            if (mySqlConnection == null)
            {
                MessageBox.Show("No se pudo establecer la conexión a la base de datos.");
            }
            string query = "select nombre from categorias;";

            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> Nombres = new List<string>();

                while (reader.Read())
                {
                    Nombres.Add(reader.GetString("nombre")); // Lee el nombre de cada fila y lo agrega a la lista
                }
                cmb_categoria_asociada.Items.AddRange(Nombres.Cast<object>().ToArray());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btn_agregarcategoria_Click(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            MySqlConnection mySqlConnection = conexion.Establecer_Conexion();

            if (mySqlConnection == null)
            {
                MessageBox.Show("No se pudo establecer la conexión a la base de datos.");
                return;
            }


            if (txt_nombrecategoria.Text == "")
            {
                MessageBox.Show("No puede estar vacio el nombre");
            }
            else
            {
                string nombre_cat = txt_nombrecategoria.Text;
                string descripcion = txt_descripcioncategoria.Text;


                string query = "INSERT INTO categorias (nombre, descripcion) VALUES ('" + nombre_cat + "', '" + descripcion + "');";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Categoria agregado exitosamente.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void btn_agregarsub_Click(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            MySqlConnection mySqlConnection = conexion.Establecer_Conexion();

            if (mySqlConnection == null)
            {
                MessageBox.Show("No se pudo establecer la conexión a la base de datos.");
                return;
            }
            string a = cmb_categoria_asociada.SelectedItem.ToString();
            int id_cat = QueCategoria(a);

            if (txt_nombresub.Text == "")
            {
                MessageBox.Show("No puede estar vacio el nombre");
            }
            else
            {
                string nombre_subcat = txt_nombresub.Text;
                string subdescripcion = txt_subdescripcion.Text;



                //intento n°1 para obtener id_categoria

                string query = "INSERT INTO subcategorias (nombre, descripcion, id_categoria) VALUES ('" + nombre_subcat + "', '" + subdescripcion + "', '" + id_cat + "');";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);


                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sub Categoria agregada exitosamente.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txt_nombrecategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void Categorias_Load(object sender, EventArgs e)
        {

        }

        public int QueCategoria(string nombre_cat)
        {
            int a = 0;
            Conexion conexion = new Conexion();
            MySqlConnection mySqlConnection = conexion.Establecer_Conexion();

            if (mySqlConnection == null)
            {
                MessageBox.Show("No se pudo establecer la conexión a la base de datos.");
            }
            string query1 = "select id_categoria from categorias where nombre = '" + nombre_cat + "';";
            MySqlCommand cmd1 = new MySqlCommand(query1, mySqlConnection);

            try
            {
                cmd1.ExecuteNonQuery();
                object result = cmd1.ExecuteScalar();
                a = Convert.ToInt32(result);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return a;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
