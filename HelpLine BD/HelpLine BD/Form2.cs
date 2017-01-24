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
    public partial class Form2 : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string item_id;
        private string title;
        private string hn;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string id, string t, string h)
        {
            InitializeComponent();
            item_id = id;
            title = t;
            hn = h;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Text = title;
            label4.Text = hn;
            this.Text = title;
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
            string sqlSelectAll = "SELECT * FROM item_details WHERE item_id='" + item_id + "'";
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, connection);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dataGridView1.DataSource = bSource;



            dataGridView1.Columns["id"].Visible = false;
            connection.Close();
        }
    }
}
