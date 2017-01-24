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

namespace HelpLine_BD
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string cat;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

            dataGridView1.Columns["id"].Visible = false;

            comboBox1.Items.Clear();

            MySqlCommand cmd = new MySqlCommand(sqlSelectAll, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            int i = 0;
            comboBox1.Items.Add("All");
            
            while (dataReader.Read())
            {
                string s = dataReader["id"].ToString()+". "+dataReader["title"].ToString();
                comboBox1.Items.Add(s);
            }
            dataReader.Close();
            comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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

            //dataGridView2.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView2_DefaultValuesNeeded);
            
        }

        private void dataGridView2_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["categoryid"].Value = cat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s = textBox1.Text;
            String category = comboBox1.SelectedItem.ToString();
            string sqlSelectAll = "";

            if (category=="All")
            {
                sqlSelectAll = "SELECT * FROM item WHERE title LIKE '%" + s + "%' ORDER BY title";
            }
            else
            {
                String[] cat1=category.Split('.');
                sqlSelectAll = "SELECT * FROM item WHERE categoryid='" + cat1[0] + "' AND title LIKE '%" + s + "%' ORDER BY title";
            }

            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dataGridView2.DataSource = bSource;



            dataGridView2.Columns["id"].Visible = false;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            Form2 form = new Form2(dataGridView2.SelectedCells[0].Value.ToString(), dataGridView2.SelectedCells[1].Value.ToString(), dataGridView2.SelectedCells[3].Value.ToString());
            form.Show();

        }
    }
}
