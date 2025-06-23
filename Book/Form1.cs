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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mynig\Desktop\macka_vp\SQL\Books\Books\Books.mdf;Integrated Security=True";

            string query = "SELECT * FROM Books";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    booksDataGridView.DataSource = dt;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mynig\Desktop\macka_vp\SQL\Books\Books\Books.mdf;Integrated Security=True";
            string query = "INSERT INTO [dbo].[Books] ([BookID], [Title], [Author], [PublicationDate], [Description], [Pages], [Genre], [Price], [Stock], [Sold]) VALUES (@BookID, @Title, @Author, @PublicationDate, @Description, @Pages, @Genre, @Price, @Stock, @Sold)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@BookID", textBox1.Text);
                command.Parameters.AddWithValue("@Title", textBox2.Text);
                command.Parameters.AddWithValue("@Author", textBox3.Text);
                command.Parameters.AddWithValue("@Genre", textBox4.Text);
                command.Parameters.AddWithValue("@Price", textBox5.Text);
                command.Parameters.AddWithValue("@Stock", textBox6.Text);
                command.Parameters.AddWithValue("@Sold", textBox7.Text);
                command.Parameters.AddWithValue("@Pages", textBox8.Text);
                command.Parameters.AddWithValue("@PublicationDate", textBox9.Text);
                command.Parameters.AddWithValue("@Description", textBox10.Text);

                try
                {
                    connection.Open();
                    int rezultat = command.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        MessageBox.Show("Data inserted!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Data not inserted.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might have occurred
                    MessageBox.Show("Error: " + ex.Message);
                }

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mynig\Desktop\macka_vp\SQL\Books\Books\Books.mdf;Integrated Security=True";

            // Query to check if the book exists in the Books table
            string checkQuery = "SELECT COUNT(*) FROM [dbo].[Books] WHERE [Author] = @Author and [Title] = @Title";

            // Query to insert into the Favorite table
            string insertQuery = "INSERT INTO[dbo].[Favorite] ([BookID], [Title], [Author], [PublicationDate], [Description], [Pages], [Genre], [Price], [Stock], [Sold]) VALUES(@BookID, @Title, @Author, @PublicationDate, @Description, @Pages, @Genre, @Price, @Stock, @Sold)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Author", textBox3.Text);
                checkCommand.Parameters.AddWithValue("@Title", textBox2.Text);

                try
                {
                    connection.Open();
                    int bookCount = (int)checkCommand.ExecuteScalar();

                    if (bookCount > 0)
                    {
                        // Book exists in the Books table, proceed with the insert
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@BookID", textBox1.Text);
                        insertCommand.Parameters.AddWithValue("@Title", textBox2.Text);
                        insertCommand.Parameters.AddWithValue("@Author", textBox3.Text);
                        insertCommand.Parameters.AddWithValue("@Genre", textBox4.Text);
                        insertCommand.Parameters.AddWithValue("@Price", textBox5.Text);
                        insertCommand.Parameters.AddWithValue("@Stock", textBox6.Text);
                        insertCommand.Parameters.AddWithValue("@Sold", textBox7.Text);
                        insertCommand.Parameters.AddWithValue("@Pages", textBox8.Text);
                        insertCommand.Parameters.AddWithValue("@PublicationDate", textBox9.Text);
                        insertCommand.Parameters.AddWithValue("@Description", textBox10.Text);

                        int rezultat = insertCommand.ExecuteNonQuery();
                        if (rezultat > 0)
                        {
                            MessageBox.Show("Book successfully added to favorites!");

                        }
                        else
                        {
                            MessageBox.Show("Data not inserted.");
                        }
                    }
                    else
                    {
                        // Book does not exist in the Books table
                        MessageBox.Show("The book does not exist in the Books table.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might have occurred
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the original form when Form2 is closed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.FormClosed += new FormClosedEventHandler(Form2_FormClosed); // Attach the event handler
            f2.Show();
            this.Hide(); // Hide the current form instead of closing it

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mynig\Desktop\macka_vp\SQL\Books\Books\Books.mdf;Integrated Security=True";

            // Query to check if the book exists in the Favorite table
            string checkQuery = "SELECT COUNT(*) FROM [dbo].[Favorite] WHERE [Author] = @Author and [Title] = @Title";

            // Query to delete into the Favorite table
            string deleteQuery = "DELETE FROM [dbo].[Favorite] WHERE [BookID] = @BookID and [Author] = @Author and [Title] = @Title";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Author", textBox3.Text);
                checkCommand.Parameters.AddWithValue("@Title", textBox2.Text);

                try
                {
                    connection.Open();
                    int bookCount = (int)checkCommand.ExecuteScalar();

                    if (bookCount > 0)
                    {
                        // Book exists in the Books table, proceed with the insert
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);

                        deleteCommand.Parameters.AddWithValue("@BookID", textBox1.Text);
                        deleteCommand.Parameters.AddWithValue("@Title", textBox2.Text);
                        deleteCommand.Parameters.AddWithValue("@Author", textBox3.Text);
                       

                        int rezultat = deleteCommand.ExecuteNonQuery();
                        if (rezultat > 0)
                        {
                            LoadData();
                            MessageBox.Show("Book successfully delete from favorites!");
                        }
                        else
                        {
                            MessageBox.Show("Book dose not exist!");
                        }
                    }
                    else
                    {
                        // Book does not exist in the Books table
                        MessageBox.Show("The book does not exist in the Favorite table.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might have occurred
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mynig\Desktop\macka_vp\SQL\Books\Books\Books.mdf;Integrated Security=True";

            // Query to check if the book exists in the Favorite table
            string checkQuery = "SELECT COUNT(*) FROM [dbo].[Books] WHERE [Author] = @Author and [Title] = @Title";

            // Query to delete into the Favorite table
            string deleteQuery = "DELETE FROM [dbo].[Books] WHERE [BookID] = @BookID and [Author] = @Author and [Title] = @Title";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Author", textBox3.Text);
                checkCommand.Parameters.AddWithValue("@Title", textBox2.Text);

                try
                {
                    connection.Open();
                    int bookCount = (int)checkCommand.ExecuteScalar();

                    if (bookCount > 0)
                    {
                        // Book exists in the Books table, proceed with the insert
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);

                        deleteCommand.Parameters.AddWithValue("@BookID", textBox1.Text);
                        deleteCommand.Parameters.AddWithValue("@Author", textBox3.Text);
                        deleteCommand.Parameters.AddWithValue("@Title", textBox2.Text);


                        int rezultat = deleteCommand.ExecuteNonQuery();
                        if (rezultat > 0)
                        {
                            LoadData();
                            MessageBox.Show("Book successfully delete from Books!");
                        }
                        else
                        {
                            MessageBox.Show("Book dose not exist!");
                        }
                    }
                    else
                    {
                        // Book does not exist in the Books table
                        MessageBox.Show("The book does not exist in the Books table.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might have occurred
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the original form when Form2 is closed
        }

        private void button5_Click(object sender, EventArgs e)
        {
                Form3 f3 = new Form3();
                f3.FormClosed += new FormClosedEventHandler(Form3_FormClosed); // Attach the event handler
                f3.Show();
                this.Hide(); // Hide the current form instead of closing it
                
        }
    }
}
