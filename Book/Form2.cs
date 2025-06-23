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

namespace Books
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        private void favoriteBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.favoriteBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.booksDataSet);

        }

        private void LoadData()
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mynig\Desktop\macka_vp\SQL\Books\Books\Books.mdf;Integrated Security=True";

            string query = "SELECT * FROM Favorite";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    favoriteDataGridView.DataSource = dt;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.Close(); // Show the original form when Form2 is closed
        }

        private void favoriteDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
