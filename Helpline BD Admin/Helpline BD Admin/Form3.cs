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
    public partial class Form3 : Form
    {
        private int i;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string cat;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
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


            comboBox1.Items.Clear();

            string sqlSelectAll = "SELECT * FROM category";
            MySqlCommand cmd = new MySqlCommand(sqlSelectAll, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            int i = 0;

            while (dataReader.Read())
            {
                string s = dataReader["id"].ToString() + ". " + dataReader["title"].ToString();
                comboBox1.Items.Add(s);
            }
            dataReader.Close();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                String[] cat1 = comboBox1.SelectedItem.ToString().Split('.');
                long id=insert("INSERT INTO item VALUES('','" + textBox1.Text + "', '"+cat1[0]+"', '" + textBox2.Text + "')");
                insert("INSERT INTO item_details VALUES('','" + id.ToString() + "', 'Phone', '" + textBox2.Text + "')");
                insert("INSERT INTO item_details VALUES('','" + id.ToString() + "', 'Email', '" + textBox3.Text + "')");
                insert("INSERT INTO item_details VALUES('','" + id.ToString() + "', 'Details', '" + textBox4.Text + "')");
                insert("INSERT INTO item_details VALUES('','" + id.ToString() + "', 'Address', '" + textBox5.Text + "')");
                MessageBox.Show("Inserted Successfully");
            }
        }

        private long insert(string query1)
        {
            MySqlCommand dbcmd = connection.CreateCommand();
            dbcmd.CommandText = query1;
            dbcmd.ExecuteNonQuery();
            long Id = dbcmd.LastInsertedId;
            return Id;
        }
    }
}
