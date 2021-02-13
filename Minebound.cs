using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Threading;

namespace minesweeping
{
    class Minebound
    {
        Mine[,] buttons;
        Boolean isover = false;//游戏是否结束
        int seekmine = 0;
        int Uid;
        public Minebound(){//布区域
            buttons = new Mine[PublicValue.line, PublicValue.lie];
        }
        
        public void bound()
        {
            int i, j;
            for (i = 0; i < PublicValue.line; i++)
            {
                for (j = 0; j < PublicValue.lie; j++)
                {
                    
                    buttons[i, j] = new Mine();
                    buttons[i, j].Location = new Point(j * 30, i * 30);
                    buttons[i, j].X = i;
                    buttons[i, j].Y = j;
                    buttons[i, j].Ismine = false;
                    buttons[i,j].Size= new Size(30, 30);
                    buttons[i, j].Font = new Font("宋体", 15,FontStyle.Bold);
                    buttons[i, j].BackgroundImageLayout =ImageLayout.Zoom;
                    buttons[i, j].MouseUp += new MouseEventHandler(Click);
                    buttons[i, j].MouseDown += new MouseEventHandler(ClickDown);
                    buttons[i, j].MouseUp += new MouseEventHandler(ClickUp);
                    Form1.my.panel1.Controls.Add(buttons[i, j]);
                }
            }
        }
        public void setMine()//随机生成雷
        {
            int location_x, location_y;
            Random re = new Random();
            for(int i = 0; i < PublicValue.Minenumber; i++)
            {
                location_x = re.Next(0, PublicValue.line);
                location_y = re.Next(0, PublicValue.lie);
                if (buttons[location_x, location_y].Ismine == false)
                {
                    buttons[location_x, location_y].Ismine =true;
                }
                else
                {
                    i--;
                }
            }
        }
        public void setSaoMao()
        {
            int location_x, location_y;
            Random re = new Random();
            string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\课设\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string str = "select * from [User] where Uname=N'"+PublicValue.username+"'";
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = str;
            comm.CommandType = System.Data.CommandType.Text;
            SqlDataReader dr;
            dr = comm.ExecuteReader();
            dr.Read();
            Uid = Convert.ToInt32(dr.GetValue(0));
            dr.Close();
            comm.Dispose();
            str = "select * from Own where Uid="+Uid+" and Pid=6";
            SqlCommand comm1 = new SqlCommand();
            comm1.Connection = conn;
            comm1.CommandText = str;
            comm1.CommandType = System.Data.CommandType.Text;
            SqlDataReader dr1;
            dr1 = comm1.ExecuteReader();
            dr1.Read();
            int all = Convert.ToInt32(dr1.GetValue(3));
            dr1.Close();
            comm1.Dispose();
            conn.Close();
            for (int i = 0; i < all; i++)
            {
                location_x = re.Next(0, PublicValue.line);
                location_y = re.Next(0, PublicValue.lie);
                if (buttons[location_x, location_y].Ismine == false&& buttons[location_x, location_y].IsSaomao ==false)
                {
                    buttons[location_x, location_y].IsSaomao = true;
                    buttons[location_x, location_y].Image = Image.FromFile(Application.StartupPath + "\\images\\2.jpg");
                }
                else
                {
                    i--;
                }
            }
        }
        public int getminenu(int a,int b)//获得该方格的附近有多少雷的数量
        {
            int i, j;
            int around=0;
            int minline=0, maxline=a+2, minlie=0, maxlie=b+2;
            if (a == 0)
            {
                minline = 0;
            }
            else
            {
                minline = a - 1;
            }
            if (b == 0)
            {
                minlie = 0;
            }
            else
            {
                minlie = b - 1;
            }
            for (i = minline; i < maxline; i++)
            {
                for (j = minlie; j < maxlie; j++)
                {
                    if (!(i >= 0 && i < PublicValue.line && j >= 0 && j < PublicValue.lie))
                        continue;
                    if (buttons[i, j].Ismine == true)
                        around++;
                }
            } 
            return around;
        }
        public void sweep(int a, int b)
        {
            int minline = 0, maxline = a + 2, minlie = 0, maxlie = b + 2;
            if (a == 0)
            {
                minline = 0;
            }
            else
            {
                minline = a - 1;
            }
            if (b == 0)
            {
                minlie = 0;
            }
            else
            {
                minlie = b - 1;
            }
            int minenumber = getminenu(a, b);
            if (minenumber == 0)
            {
                buttons[a, b].Enabled = false;//该按钮不能被执行
                for (int i = minline; i < maxline; i++)
                {
                    for (int j = minlie; j < maxlie; j++)
                    {
                        if (!(i >= 0 && i < PublicValue.line && j >= 0 && j < PublicValue.lie))//超出范围的
                            continue;
                        if (buttons[i, j].Enabled == true && Convert.ToInt32(buttons[i, j].Tag) == 0)
                            sweep(i, j);
                        if (Convert.ToInt32(buttons[i, j].Tag) == 0)//如果按钮未被标记过
                        {
                            buttons[i, j].Enabled = false;
                        }
                        buttons[i, j].Text = getminenu(i, j).ToString();
                        if (buttons[i, j].Text == "0")
                        {
                            buttons[i, j].Text = "";
                            buttons[i, j].BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }
        //点击时出现的图标
        private void ClickDown(object B, EventArgs e)
        {
            Form1.my.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\images\\h.jpg");
        }
        private void ClickUp(object B, EventArgs e)
        {
            Form1.my.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\images\\usually.jpg");
        }
        private void Click(object B, EventArgs e)
        {
            Form1.my.timer1.Interval = 1000;
            Form1.my.timer1.Enabled = true;
            

            Mine mouser = (Mine)B;
            MouseEventArgs Mouse_e = (MouseEventArgs)e;
            int line, lie;
            line = mouser.X;
            lie = mouser.Y;
            if (Mouse_e.Button == MouseButtons.Left)
            {
                if (buttons[line,lie].Ismine==false&& Convert.ToInt32(buttons[line, lie].Tag) == 0&&buttons[line, lie].IsSaomao == false)
                {
                    buttons[line, lie].Enabled = false;
                    buttons[line, lie].Text = Convert.ToString(getminenu(line, lie));
                    sweep(line, lie);
                    if (win())
                    {
                        showmine();
                        Form1.my.timer1.Enabled = false;
                        isover = true;
                        MessageBox.Show("SUCCESS");
                        Form1.my.timer1.Enabled = false;
                        /*Form1.my.m = 0;
                        Form1.my.s = 0;*/
                        Form1.my.textBox1.Text = "";
                    }
                }
                else if(Convert.ToInt32(buttons[line, lie].Tag) == 0 && buttons[line, lie].IsSaomao == true)
                {
                    PublicValue.timeend = false;
                    string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
                    SqlConnection conn = new SqlConnection(strconn);
                    conn.Open();
                    int xmin, xmax, ymin, ymax;
                    if (line - 2 < 0)
                        ymin = 0;
                    else
                        ymin = line - 2;
                    if (line + 2 < PublicValue.line)
                        ymax = line + 2;
                    else
                        ymax = PublicValue.line;
                    if (lie - 2 < 0)
                        xmin = 0;
                    else
                        xmin = lie - 2;
                    if (lie + 2 < PublicValue.lie)
                        xmax = lie + 2;
                    else
                        xmax = PublicValue.lie;
                    showmine(ymax, xmax, ymin, xmin);
                    Thread.Sleep(2 * 1000);
                    buttons[line, lie].IsSaomao = false;
                    buttons[line, lie].Image = null;
                    hidemine(ymax, xmax, ymin, xmin);
                    string str = "update Own set Onumber=Onumber-1 where Uid=" + Uid + " and Pid=6";
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = str;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.ExecuteNonQuery();
                    comm.Dispose();
                    conn.Close();
                }
                else if (Convert.ToInt32(buttons[line, lie].Tag) == 1)
                {

                }
                else if (Convert.ToInt32(buttons[line, lie].Tag) == 2)
                {

                }
                else
                {
                    Form1.my.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\images\\d.jpg");
                    Form1.my.timer1.Enabled = false;
                    isover = true;
                    showmine();
                    Form1.my.timer1.Enabled = false;
                    MessageBox.Show("Game Over");   
                    Form1.my.textBox1.Text = "";
                }
            }
            else 
            {
                if (Convert.ToInt32(buttons[line, lie].Tag) == 1)
                {
                    buttons[line, lie].Tag = 2;
                    buttons[line, lie].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\center.jpg");
                    seekmine--;
                }
                else if (Convert.ToInt32(buttons[line, lie].Tag) == 2)
                {
                    buttons[line, lie].Tag = 0;
                    buttons[line, lie].BackgroundImage = null;
                }
                else
                {
                    buttons[line, lie].Tag = 1;
                    buttons[line, lie].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\flag.png");
                    seekmine++;
                }
                int remain = PublicValue.Minenumber - seekmine;
                Form1.my.textBox2.Text = remain.ToString();
                if (win())
                {
                    showmine();
                    Form1.my.timer1.Enabled = false;
                    isover = true;
                    DialogResult r= MessageBox.Show("SUCCESS");
                    Form1.my.textBox1.Text = "";
                    if (r == DialogResult.OK)
                    {
                       
                    }
                }
            }
        }
        private Boolean win()
        {
            int seekmine = 0;
            for(int i = 0; i < PublicValue.line; i++)
            {
                for(int j = 0; j < PublicValue.lie; j++)
                {
                    if (buttons[i, j].Ismine == true && Convert.ToInt32(buttons[i, j].Tag) == 1)
                        seekmine++;
                }
            }
            if (seekmine == PublicValue.Minenumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //全部显示
        public void showmine()
        {
            for(int i = 0; i < PublicValue.line; i++)
            {
                for(int j = 0; j < PublicValue.lie; j++)
                {
                    if (buttons[i, j].Ismine == true)
                    {
                        buttons[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\mine.png");
                        buttons[i, j].Enabled = false;
                    }
                }
            }
        }
        //25格以内显示雷
        public void showmine(int linemax,int liemax,int linemin,int liemin)
        {
            for (int i = linemin; i < linemax; i++)
            {
                for (int j = liemin; j <liemax; j++)
                {
                    if (buttons[i, j].Ismine == true)
                    {
                        buttons[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\mine.png");
                        buttons[i, j].Enabled = false;
                    }
                }
            }
        }
        public void hidemine(int linemax, int liemax, int linemin, int liemin)
        {
            for (int i = linemin; i <linemax; i++)
            {
                for (int j = liemin; j <liemax; j++)
                {
                    if (buttons[i, j].Ismine == true)
                    {
                        buttons[i, j].BackgroundImage = null;
                        buttons[i, j].Enabled = true;
                    }
                }
            }
        }
    }
}
