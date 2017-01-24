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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlSelectAll = "SELECT * FROM user WHERE username='"+textBox1.Text+"' AND password='"+textBox2.Text+"'";
            MySqlCommand cmd = new MySqlCommand(sqlSelectAll, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            int i = 0;

            while (dataReader.Read())
            {
                i++;
            }
            dataReader.Close();
            connection.Close();
            if (i > 0)
            {
                this.Hide();
                Form2 form = new Form2(1);
                form.Show();
                //this.Close();
            }
            else
            {
                label3.Text = "Username Or Password do not match";
            }
        }
    }
}
