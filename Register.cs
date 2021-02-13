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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
        public int Uname(SqlDataReader dr,string name)
        {
            int n =0 ;
            while (dr.Read())
            {
                if (name.Equals(dr.GetValue(1).ToString()) == true)
                {

                    n = 0;
                    break;
                }
                else
                    n = 1;
            }
            return n;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("用户名不能为空！");
            }
            else
            {
                if (textBox2.Text.Equals(textBox3.Text) == false)
                {
                    MessageBox.Show("输入密码不对！");
                }
                else
                {
                    string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
                    SqlConnection conn = new SqlConnection(strconn);
                    conn.Open();
                    //MessageBox.Show("数据库连接成功！");
                    string strSql1 = "select * from [User] ";
                    SqlCommand comm1 = new SqlCommand(strSql1, conn);
                    SqlDataReader dr;
                    dr = comm1.ExecuteReader();
                    string name = textBox1.Text;
                    int n1 = Uname(dr, name);
                    if (n1 == 0)
                    {
                        MessageBox.Show("该用户名已经被注册！");
                    }
                    else if (n1 == 1)
                    {
                        comm1.Dispose();
                        dr.Close();
                        string strSql2 = "insert into [User] (Uname,Password,Udate,Ugrade) values ('" + textBox1.Text + "','" + textBox3.Text + "','" + DateTime.Now.ToString() + "',100)";
                        SqlCommand comm2 = new SqlCommand(strSql2, conn);
                        int n = comm2.ExecuteNonQuery();
                        if (n > 0)
                        {
                            string strSql3 = "select * from [User] where Uname='" + textBox1.Text + "'";
                            SqlCommand comm3 = new SqlCommand(strSql3, conn);
                            SqlDataReader dr1;
                            dr1 = comm3.ExecuteReader();
                            dr1.Read();
                            int id = Convert.ToInt32(dr1.GetValue(0));
                            dr1.Close();
                            comm3.Dispose();
                            string strSql4 = "select * from Property";
                            SqlDataAdapter da = new SqlDataAdapter(strSql4, conn);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "Pro");
                            da.Dispose();
                            int[] Pid = new int[ds.Tables["Pro"].Rows.Count];
                            for (int i = 0; i < ds.Tables["Pro"].Rows.Count; i++)
                                Pid[i] = Convert.ToInt32(ds.Tables["Pro"].Rows[i].ItemArray[0]);
                            ds.Dispose();
                            for(int i = 0; i < Pid.Length; i++)
                            {
                                string strSql5 = "insert into Own(Uid,Pid,Onumber) values("+id+","+Pid[i]+",0)";
                                SqlCommand comm5 = new SqlCommand(strSql5, conn);
                                comm5.ExecuteNonQuery();
                                comm5.Dispose();
                            }
                            MessageBox.Show("注册成功！返回登录界面！");
                            Login login = new Login();
                            login.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("注册失败！");
                        comm2.Dispose();
                    }
                    conn.Close();
                }
            }
        }

        private void Register_Load_1(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Close();
        }
    }
}
