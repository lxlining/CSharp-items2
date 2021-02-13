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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\C#\扫雷\扫雷\MineSweep.mdf;Integrated Security=True";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                MessageBox.Show("数据库连接成功！");
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                string strSql = "select * from Admin";
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
                            MessageBox.Show("欢迎进入管理系统！");
                            /*MyClass.name = dr.GetValue(1).ToString();
                            MessageBox.Show(MyClass.name);
                            Form1 form1 = new Form1();
                            form1.Show();*/
                            Shop shop = new Shop();
                            shop.Show();
                            break;
                        }
                    }
                    //MessageBox.Show("输入的用户名或密码有误！");
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("管理员名或密码不能为空!");
            }
        }
    }
}
