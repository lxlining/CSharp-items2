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
    public partial class Shop : Form
    {
        public static Shop s = new Shop();
        public Shop()
        {
            InitializeComponent();
            s = this;
        }
        int[] Pid;
        PictureBox[] picture;
        TextBox[] text;
        Button[] button1;
        Button[] button2;
        Button[] button3;
        TextBox[] text1;
        DataSet ds = new DataSet();
        private void Shop_Load(object sender, EventArgs e)
        {
            //图标载入
            Bitmap myBitmap = new Bitmap(Application.StartupPath + "\\images\\icon.jpg");
            // Get an Hicon for myBitmap.
            IntPtr Hicon = myBitmap.GetHicon();
            // Create a new icon from the handle. 
            Icon newIcon = Icon.FromHandle(Hicon);
            // Set the form Icon attribute to the new icon.
            this.Icon = newIcon;
            int i, j = 0;
            string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string str = "select * from Property";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            da.Fill(ds, "Property");
            da.Dispose();
            conn.Close();
            Pid = new int[ds.Tables["Property"].Rows.Count];
            picture = new PictureBox[ds.Tables["Property"].Rows.Count];
            text= new TextBox[ds.Tables["Property"].Rows.Count];
            button1 = new Button[ds.Tables["Property"].Rows.Count];
            button2 = new Button[ds.Tables["Property"].Rows.Count];
            button3 = new Button[ds.Tables["Property"].Rows.Count];
            text1 = new TextBox[ds.Tables["Property"].Rows.Count];
            for (i = 0; i < ds.Tables["Property"].Rows.Count; i++)
            {
                Pid[i] = Convert.ToInt32(ds.Tables["Property"].Rows[i].ItemArray[0]);
                //图片显示
                picture[i] = new PictureBox();
                picture[i].Location = new Point(50, j * 55 + 20);
                picture[i].Size = new Size(40, 40);
                picture[i].Tag = i.ToString();
                string pictureform = ds.Tables["Property"].Rows[i].ItemArray[4].ToString();
                picture[i].Image = Image.FromFile(Application.StartupPath + "\\images\\"+pictureform);
                picture[i].SizeMode = PictureBoxSizeMode.StretchImage;
                toolTip1.SetToolTip(picture[i], ds.Tables["Property"].Rows[i].ItemArray[1].ToString());
                groupBox1.Controls.Add(picture[i]);
                //减号按钮
                button1[i] = new Button();
                button1[i].Tag = i.ToString();
                button1[i].Location = new Point(120, j*55+25);
                button1[i].Size = new Size(25, 25);
                button1[i].Text = "－";
                button1[i].Font = new Font("黑体", 10, FontStyle.Bold);
                button1[i].Click += new EventHandler(jian);
                groupBox1.Controls.Add(button1[i]);
                //数量显示
                text[i] = new TextBox();
                text[i].Location = new Point(150, j * 55 + 26);
                text[i].Size = new Size(25, 25);
                text[i].Text = "1";
                text[i].Font = new Font("黑体", 10, FontStyle.Bold);
                text[i].TextAlign = HorizontalAlignment.Center;
                groupBox1.Controls.Add(text[i]);
                //加号按钮
                button2[i] = new Button();
                button2[i].Tag = i.ToString();
                button2[i].Location = new Point(180, j * 55 + 25);
                button2[i].Size = new Size(25, 25);
                button2[i].Text = "＋";
                button2[i].Font = new Font("黑体", 10, FontStyle.Bold);
                button2[i].Click += new EventHandler(jia);
                groupBox1.Controls.Add(button2[i]);
                //积分
                Label l = new Label();
                l.Location = new Point(245, j * 55 + 28);
                l.Size = new Size(40, 25);
                l.Text = "积分";
                l.Font = new Font("黑体", 10, FontStyle.Bold);
                l.ForeColor = Color.DodgerBlue;
                groupBox1.Controls.Add(l);
                //积分显示
                text1[i] = new TextBox();
                text1[i].Location = new Point(285, j * 55 + 26);
                text1[i].Size = new Size(50, 25);
                text1[i].Text = ds.Tables["Property"].Rows[i].ItemArray[2].ToString();
                text1[i].Font = new Font("黑体", 10, FontStyle.Bold);
                text1[i].TextAlign = HorizontalAlignment.Center;
                groupBox1.Controls.Add(text1[i]);
                //购买按钮
                button3[i] = new Button();
                button3[i].Tag = i.ToString();
                button3[i].Location = new Point(340, j * 55 + 25);
                button3[i].Size = new Size(60, 25);
                button3[i].Text = "购买";
                button3[i].Font = new Font("黑体", 10, FontStyle.Bold);
                button3[i].Click += new EventHandler(pay);
                groupBox1.Controls.Add(button3[i]);
                j++;
            }
            ds.Dispose();
            str = "select * from [User] where Uname=N'" + PublicValue.username + "'";
            SqlConnection conn1 = new SqlConnection(strconn);
            conn1.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn1;
            comm.CommandText = str;
            comm.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = comm.ExecuteReader();
            dr.Read();
            label2.Text= dr.GetValue(4).ToString();
            dr.Close();
            comm.Dispose();
            conn1.Close();
        }
        private void jian(object B, EventArgs e)
        {

            Button b = (Button)B;
            int i = Convert.ToInt32(b.Tag);
            int v = Convert.ToInt32(ds.Tables["Property"].Rows[i].ItemArray[2]);
            int a = Convert.ToInt32(text[i].Text);
            a--;
            v = v * a;
            if (a <= 0)
            {
                text[i].Text = "0";
                text1[i].Text = "0";
            }
            else
            {
                text[i].Text = a.ToString();
                text1[i].Text = v.ToString();
                
            }
        }
        private void jia(object B, EventArgs e)
        {
            Button b = (Button)B;
            int i = Convert.ToInt32(b.Tag);
            int v = Convert.ToInt32(ds.Tables["Property"].Rows[i].ItemArray[2]);
            int a = Convert.ToInt32(text[i].Text);
            a++;
            v = v * a;
            text[i].Text = a.ToString();
            text1[i].Text = v.ToString();
        }
        private void pay(object B, EventArgs e)
        {
            Button b = (Button)B;
            int i = Convert.ToInt32(b.Tag);
            string str = "select * from [User] where Uname=N'" + PublicValue.username + "'";
            string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = str;
            comm.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = comm.ExecuteReader();
            dr.Read();
            int Uid=Convert.ToInt32(dr.GetValue(0));
            int grade1 = Convert.ToInt32(dr.GetValue(4));
            dr.Close();
            comm.Dispose();
            int grade = Convert.ToInt32(text1[i].Text);
            int number = Convert.ToInt32(text[i].Text);
            int Pd = Pid[i];
            if (grade1 < grade)
            {
                MessageBox.Show("积分不够");
            }
            else
            {
                str = "insert into Conn(Uid,Pid,Cnumber,Cdate,Cgrade) values(" + Uid + "," + Pd + "," + number + ",'" + DateTime.Now.ToString() + "'," + grade + ")";
                SqlCommand comm1 = new SqlCommand();
                comm1.Connection = conn;
                comm1.CommandText = str;
                comm1.CommandType = CommandType.Text;
                comm1.ExecuteNonQuery();
                grade1 = grade1 - grade;
                comm1.Dispose();
                str = "update [User] set Ugrade=" + grade1 + "where Uname='"+PublicValue.username+"'";
                SqlCommand comm2 = new SqlCommand();
                comm2.Connection = conn;
                comm2.CommandText = str;
                comm2.CommandType = CommandType.Text;
                comm2.ExecuteNonQuery();
                comm2.Dispose();
                str = "select * from Own where Uid=" + Uid + "and Pid=" + Pd + "";
                SqlCommand comm3 = new SqlCommand();
                comm3.Connection = conn;
                comm3.CommandText = str;
                comm3.CommandType = CommandType.Text;
                SqlDataReader dr1;
                dr1 = comm3.ExecuteReader();
                dr1.Read();
                int Onumber = Convert.ToInt32(dr1.GetValue(3));
                dr1.Close();
                comm3.Dispose();
                Onumber += number;
                str="update Own set Onumber="+Onumber+ "where Uid=" + Uid + "and Pid=" + Pd + "";
                SqlCommand comm4 = new SqlCommand();
                comm4.Connection = conn;
                comm4.CommandText = str;
                comm4.CommandType = CommandType.Text;
                comm4.ExecuteNonQuery();
                comm4.Dispose();
                conn.Close();
                MessageBox.Show("购买成功");
                //刷新
                this.Hide(); //先隐藏主窗体
                Shop form1 = new Shop(); //重新实例化此窗体
                form1.ShowDialog();//已模式窗体的方法重新打开
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();
            x.Show();
            this.Close();
        }
    }
}
