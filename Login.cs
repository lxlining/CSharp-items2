using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace minesweeping
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷\扫雷\MineSweep.mdf;Integrated Security=True";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                //MessageBox.Show("数据库连接成功！");
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                string strSql = "select * from [User]";
                comm.CommandText = strSql;
                comm.CommandType = CommandType.Text;
                SqlDataReader dr;
                dr = comm.ExecuteReader();
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    while (dr.Read())
                    {
                        if (textBox1.Text.Equals(dr.GetValue(1).ToString()) == true && textBox2.Text.Equals(dr.GetValue(2).ToString()) == true)
                        {
                            MyClass.name = dr.GetValue(1).ToString();
                            Form1 form1 = new Form1();
                            form1.Show();
                            PublicValue.username = dr.GetValue(1).ToString();
                            break;
                        }
                    }
                    //MessageBox.Show("输入的用户名或密码有误！");
                }
                conn.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码不能为空!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //图标载入
            Bitmap myBitmap = new Bitmap(Application.StartupPath + "\\images\\icon.jpg");
            // Get an Hicon for myBitmap.
            IntPtr Hicon = myBitmap.GetHicon();
            // Create a new icon from the handle. 
            Icon newIcon = Icon.FromHandle(Hicon);
            // Set the form Icon attribute to the new icon.
            this.Icon = newIcon;
            this.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
        }
    }
}
