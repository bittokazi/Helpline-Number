using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Helpline_BD_Admin
{
    public partial class Form2 : Form
    {
        private int i;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string cat;
        private static Form3 add_info;
        public Form2()
        {
            i = 0;
            InitializeComponent();
        }
        public Form2(int i)
        {
            this.i = i;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (i == 0) this.Close();
            add_info = new Form3();
            server = "localhost";
            database = "bd_helpline";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * FROM category";
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dataGridView1.DataSource = bSource;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                insert("INSERT INTO category VALUES('','" + textBox1.Text + "', '" + textBox2.Text + "')");

                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "SELECT * FROM category";
                MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);

                DataTable table = new DataTable();
                MyDA.Fill(table);

                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;


                dataGridView1.DataSource = bSource;
                MessageBox.Show("Added Category");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void insert(string query1)
        {


            //create command and assign the query and connection from the constructor
            MySqlCommand cmd1 = new MySqlCommand(query1, connection);

            //Execute command
            cmd1.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            add_info.Close();
            add_info = new Form3();
            add_info.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());

            cat = dataGridView1.SelectedCells[0].Value.ToString();

            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * FROM item WHERE categoryid='" + cat + "' ORDER BY title";
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dataGridView2.DataSource = bSource;



            dataGridView2.Columns["id"].Visible = false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
