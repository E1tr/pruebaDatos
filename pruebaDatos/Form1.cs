using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pruebaDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            refrescarGrid();
        }
        private void refrescarGrid()
        {
            string cadenaConexion = "Server=localhost\\SQLEXPRESS;Database=instituto;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string consultaAlumnos = "SELECT * FROM alumnos";
                SqlDataAdapter adapter = new SqlDataAdapter(consultaAlumnos, conn);
                DataTable tablaAlumnos = new DataTable();
                adapter.Fill(tablaAlumnos);
                dataGridView1.DataSource = tablaAlumnos;
            }
        }


        private void ConectarInsertar()
        {
            string cadenaConexion = "Server=localhost\\SQLEXPRESS;Database=instituto;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string insertarAlumno = "INSERT INTO alumnos (nombre, apellidos, edad) VALUES (@nombre, @apellidos, @edad)";

                SqlCommand cmd = new SqlCommand(insertarAlumno, conn);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@apellidos", txtApellido.Text);

                int edad;
                if (!int.TryParse(txtEdad.Text, out edad))
                {
                    MessageBox.Show("La edad debe ser un número entero válido.");
                    return;
                }
                cmd.Parameters.AddWithValue("@edad", edad);
                    
                int filasAfectadas = cmd.ExecuteNonQuery();
                MessageBox.Show($"Número de filas insertadas: {filasAfectadas}");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ConectarInsertar();
            refrescarGrid();
        }
    }
}
