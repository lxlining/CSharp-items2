using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;


namespace minesweeping
{
    public partial class Form1 : Form
    {
        public static Form1 my= new Form1();
        public static int o;
        public Form1()
        {
            InitializeComponent();
            my = this;
            o++;
        }
        int iii = 0;//解决加载窗体时间间隔 
        Loding l = new Loding();//加载窗体
        int cstatus, zstatus, gstatus, lstatus;//解决难度加载问题

       
       
        private void Form1_Load(object sender, EventArgs e)
        {
            if (o > 4)
            {
                MessageBox.Show("bbbbb");
            }
           
            //定义一个全局变量，当菜单某难度加载一次后++，load事件中，加载菜单难度方法
            cstatus = zstatus = gstatus = lstatus = 0;
            if (cstatus > 0)
                show1();
            else if (zstatus > 0)
                show2();
            else if (gstatus > 0)
                show3();
            else if (lstatus > 0)
                show4();
            else
            {
                this.panel1.Controls.Clear();
                PublicValue.lie = 13;
                PublicValue.line = 10;
                PublicValue.Minenumber = 15;
                Minebound s1 = new Minebound();
                s1.bound();
                s1.setMine();
                s1.setSaoMao();

            }
            //图标载入
            Bitmap myBitmap = new Bitmap(Application.StartupPath + "\\images\\icon.jpg");
            // Get an Hicon for myBitmap.
            IntPtr Hicon = myBitmap.GetHicon();
            // Create a new icon from the handle. 
            Icon newIcon = Icon.FromHandle(Hicon);
            // Set the form Icon attribute to the new icon.
            this.Icon = newIcon;
            //中间图片居中
            int x = (int)(0.5 * (this.Width - pictureBox1.Width));
            int y = pictureBox1.Location.Y;
            pictureBox1.Location = new System.Drawing.Point(x, y);
            //雷记数器
            x = (int)((this.Width - pictureBox1.Width) - 64);
            y = pictureBox1.Location.Y;
            textBox2.Location = new System.Drawing.Point(x, y);
            textBox2.Text = PublicValue.Minenumber.ToString();
        }

        //初级难度显示
        public void show1()
        {
            this.Hide();
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            my.Show();
            //在panel中展示扫雷区
            this.panel1.Controls.Clear();
            PublicValue.lie = 16;
            PublicValue.line = 11;
            PublicValue.Minenumber = 25;
            Minebound s1 = new Minebound();
            s1.bound();
            s1.setMine();
            s1.setSaoMao();

        }
        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //设置初级难度雷阵
            cstatus = 0;
            cstatus++;
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            show1();
            //图片位置
            int x = (int)(0.5 * (this.Width - pictureBox1.Width));
            int y = pictureBox1.Location.Y;
            pictureBox1.Location = new System.Drawing.Point(x, y);
            //雷记数器
            x = (int)((this.Width - pictureBox1.Width) - 64);
            y = pictureBox1.Location.Y;
            textBox2.Location = new System.Drawing.Point(x, y);
            textBox2.Text = PublicValue.Minenumber.ToString();
        }

        public void show2()
        {
            this.Hide();
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            //在panel中展示扫雷区
            this.panel1.Controls.Clear();
            PublicValue.lie = 18;
            PublicValue.line = 18;
            PublicValue.Minenumber = 40;
            Minebound s1 = new Minebound();
            s1.bound();
            s1.setMine();
            s1.setSaoMao();
        }
        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zstatus = 0;
            zstatus++;
            //加载窗体显示
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            show2();
            int x = (int)(0.5 * (this.Width - pictureBox1.Width));
            int y = pictureBox1.Location.Y;
            pictureBox1.Location = new System.Drawing.Point(x, y);
            //雷记数器
            x = (int)((this.Width - pictureBox1.Width) - 64);
            y = pictureBox1.Location.Y;
            textBox2.Location = new System.Drawing.Point(x, y);
            textBox2.Text = PublicValue.Minenumber.ToString();
        }
        public void show3()
        {
            this.Hide();
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            //在panel中展示扫雷区
            this.panel1.Controls.Clear();
            PublicValue.lie = 25;
            PublicValue.line = 20;
            PublicValue.Minenumber = 80;
            Minebound s1 = new Minebound();
            s1.bound();
            s1.setMine();
            s1.setSaoMao();
        }
        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //加载窗体显示
            gstatus = 0;
            gstatus++;
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            show3();
            int x = (int)(0.5 * (this.Width - pictureBox1.Width));
            int y = pictureBox1.Location.Y;
            pictureBox1.Location = new System.Drawing.Point(x, y);
            //雷记数器
            x = (int)((this.Width - pictureBox1.Width) - 64);
            y = pictureBox1.Location.Y;
            textBox2.Location = new System.Drawing.Point(x, y);
            textBox2.Text = PublicValue.Minenumber.ToString();
        }
        //自定义
        public void show4()
        {
            this.Hide();
            string xm;
            string[] a;
            int[] b = new int[3];
            xm = Interaction.InputBox("请以逗号分隔输入的行数、列数、雷数：", "自定义");
            a = xm.Split(',');
            for (int i = 0; i < a.Length; i++)
            {
                b[i] = Convert.ToInt32(a[i]);
            }
            //加载窗体显示
            timer2.Enabled = true;
            l.Show();
            my.Hide();
            //在panel中展示扫雷区
            this.panel1.Controls.Clear();
            PublicValue.lie = b[0];
            PublicValue.line = b[1];
            PublicValue.Minenumber = b[2];
            Minebound s1 = new Minebound();
            s1.bound();
            s1.setMine();
            s1.setSaoMao();
        }

        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //自定义输入
            lstatus = 0;
            lstatus++;
            timer2.Enabled = true;
            my.Hide();
            show4();
            //图片位置
            int x = (int)(0.5 * (this.Width - pictureBox1.Width));
            int y = pictureBox1.Location.Y;
            pictureBox1.Location = new System.Drawing.Point(x, y);
            //雷记数器
            x = (int)((this.Width - pictureBox1.Width) - 64);
            y = pictureBox1.Location.Y;
            textBox2.Location = new System.Drawing.Point(x, y);
            textBox2.Text = PublicValue.Minenumber.ToString();
        }
        public int m = 0;//分钟
        public int s = 0;//秒
        private void timer1_Tick(object sender, EventArgs e)//扫雷用时
        {
            if (timer1.Enabled == true)
            {
                if (s == 60)
                {
                    m += 1;
                    s = 0;
                }
                s++;
                if (s < 10 && m < 10)
                    textBox1.Text = "0" + m.ToString() + ":0" + s.ToString();
                else if (s >= 10 && m < 10)
                    textBox1.Text = "0" + m.ToString() + ":" + s.ToString();
                else if (s < 10 && m >= 10)
                    textBox1.Text = m.ToString() + ":0" + s.ToString();
                else
                    textBox1.Text = m.ToString() + ":" + s.ToString();
            }
        }
        
        private void timer2_Tick(object sender, EventArgs e)//窗体异步加载
        {
            
            iii++;
            if (iii >= 2)
            {
                l.Hide();
                my.Show();
                timer2.Enabled = false;
            }
        }
        
        private void 初级ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MyClass.hard = "初级";
            RankList rankList = new RankList();
            rankList.Show();
        }

        private void 中级ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MyClass.hard = "中级";
            RankList rankList = new RankList();
            rankList.Show();
        }

        private void 高级ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MyClass.hard = "高级";
            RankList rankList = new RankList();
            rankList.Show();
        }
        string hard="";
        
        int time;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void 排行榜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RankList r = new RankList();
            r.Show();
            
        }
        //扫描器持续时间
        int a = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (a < 10)
                a++;
            else
            {
                PublicValue.timeend = true;
            }
        }

        public void insert()
        {
            //登录的当前用户名
            string name = MyClass.name;
            //string strSql="";
            string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            //MessageBox.Show("数据库连接成功！");
            //SqlCommand comm = new SqlCommand();
            //comm.Connection = conn;
            string strSql1 = "select count(*) from Rank where Uname = '" + name + "' and Rhard = '" + hard + "' ";
            SqlCommand comm1 = new SqlCommand(strSql1,conn);
            int n = (int)comm1.ExecuteScalar();
            if (n != 0)
            {
                SqlDataReader dr;
                dr = comm1.ExecuteReader();
                
                if (time< Convert.ToInt32(dr.GetValue(1)))
                {
                    string strSql2 = "update Rank set Rtime = '" + time + "' where Uname = '" + name + "' and Rhard = '" + hard + "'";
                    SqlCommand comm2 = new SqlCommand(strSql2, conn);
                    int n2 = comm2.ExecuteNonQuery();
                    if (n2 > 0)
                    {
                        MessageBox.Show("更新成功！");
                    }
                    comm2.Dispose();

                }
                else
                {
                    string strSql3 = "insert into Rank(Rtime,Rhard,Rdate,Uname) values('" + time + "','" + hard + "','" + DateTime.Now.Date.ToString() + "','" + name + "')";
                    SqlCommand comm3 = new SqlCommand(strSql3, conn);
                    int n3 = comm3.ExecuteNonQuery();
                    if (n3 > 0)
                    {
                        MessageBox.Show("插入成功！");
                    }
                    comm3.Dispose();
                }
            }
            /*string s1 = DateTime.Now.Date.ToString();
            MessageBox.Show(s1);*/
            //string strSql = "insert into [User] (Uname,Password,Udate) values ('" + textBox1.Text + "','" + textBox3.Text + "','" + DateTime.Now.Date.ToString() + "')";
            /*strSql = "insert into Rank(Rtime,Rhard,Rdate,Uname) values('"+timer1.ToString()+"','"+hard+"','"+ DateTime.Now.Date.ToString()+"','"+name+"')";
            comm.CommandText = strSql;
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();*/
            comm1.Dispose();
            conn.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 商店ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shop s = new Shop();
            s.Show();
            
        }
    }
}
