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
    public partial class RankList : Form
    {
        public RankList()
        {
            InitializeComponent();
        }

        private void RankList_Load(object sender, EventArgs e)
        {
            //图标载入
            Bitmap myBitmap = new Bitmap(Application.StartupPath + "\\images\\icon.jpg");
            // Get an Hicon for myBitmap.
            IntPtr Hicon = myBitmap.GetHicon();
            // Create a new icon from the handle. 
            Icon newIcon = Icon.FromHandle(Hicon);
            // Set the form Icon attribute to the new icon.
            this.Icon = newIcon;
            comboBox1.Text = "初级";
            Label z = new Label();
            z.Location = new Point(35,28);
            z.Size = new Size(100, 25);
            z.Text = "名次";
            z.Font = new Font("黑体", 15, FontStyle.Bold);
            z.ForeColor = Color.DodgerBlue;
            groupBox1.Controls.Add(z);
            Label n = new Label();
            n.Location = new Point(135, 28);
            n.Size = new Size(100, 25);
            n.Text = "账号";
            n.Font = new Font("黑体", 15, FontStyle.Bold);
            n.ForeColor = Color.DodgerBlue;
            groupBox1.Controls.Add(n);
            Label s = new Label();
            s.Location = new Point(235, 28);
            s.Size = new Size(140, 25);
            s.Text = "通过时间(S)";
            s.Font = new Font("黑体", 15, FontStyle.Bold);
            s.ForeColor = Color.DodgerBlue;
            groupBox1.Controls.Add(s);
            int i,j = 0;
            string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string str = "select * from Rank where Rhard=N'初级' ORder BY Rtime ASC";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Rank");
            da.Dispose();
            conn.Close();
            for(i = 0; i < ds.Tables["Rank"].Rows.Count;i++)
            {
                Label x = new Label();
                x.Location = new Point(45, j * 40+55);
                x.Size = new Size(40, 30);
                x.Text = (i+1).ToString();
                x.Font = new Font("黑体", 20, FontStyle.Bold);
                x.ForeColor = Color.DodgerBlue;
                groupBox1.Controls.Add(x);
                Label a = new Label();
                a.Location = new Point(132, j * 40 + 55);
                a.Size = new Size(100, 30);
                a.Text = ds.Tables["Rank"].Rows[i].ItemArray[4].ToString();
                a.Font = new Font("黑体", 20, FontStyle.Bold);
                a.ForeColor = Color.DodgerBlue;
                groupBox1.Controls.Add(a);
                Label D = new Label();
                D.Location = new Point(255, j * 40 + 55);
                D.Size = new Size(100, 30);
                D.Text = ds.Tables["Rank"].Rows[i].ItemArray[1].ToString();
                D.Font = new Font("黑体", 20, FontStyle.Bold);
                D.ForeColor = Color.DodgerBlue;
                groupBox1.Controls.Add(D);
                if (i == 3)
                    break;
                j++;
            }
            ds.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            string texts = comboBox1.Text;
            Label z = new Label();
            z.Location = new Point(35, 28);
            z.Size = new Size(100, 25);
            z.Text = "名次";
            z.Font = new Font("黑体", 15, FontStyle.Bold);
            z.ForeColor = Color.DodgerBlue;
            groupBox1.Controls.Add(z);
            Label n = new Label();
            n.Location = new Point(135, 28);
            n.Size = new Size(100, 25);
            n.Text = "账号";
            n.Font = new Font("黑体", 15, FontStyle.Bold);
            n.ForeColor = Color.DodgerBlue;
            groupBox1.Controls.Add(n);
            Label s = new Label();
            s.Location = new Point(235, 28);
            s.Size = new Size(140, 25);
            s.Text = "通过时间(S)";
            s.Font = new Font("黑体", 15, FontStyle.Bold);
            s.ForeColor = Color.DodgerBlue;
            groupBox1.Controls.Add(s);
            int j = 0;
            string strconn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\c#\扫雷4\扫雷\MineSweep.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string str = "select * from Rank where Rhard=N'"+texts+ "' ORder BY Rtime ASC";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Rank");
            da.Dispose();
            conn.Close();
            if (ds.Tables["Rank"].Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < ds.Tables["Rank"].Rows.Count;i++)
                {
                    Label x = new Label();
                    x.Location = new Point(45, j * 40 + 55);
                    x.Size = new Size(40, 30);
                    x.Text = (i + 1).ToString();
                    x.Font = new Font("黑体", 20, FontStyle.Bold);
                    x.ForeColor = Color.DodgerBlue;
                    groupBox1.Controls.Add(x);
                    Label a = new Label();
                    a.Location = new Point(132, j * 40 + 55);
                    a.Size = new Size(100, 30);
                    a.Text = ds.Tables["Rank"].Rows[i].ItemArray[4].ToString();
                    a.Font = new Font("黑体", 20, FontStyle.Bold);
                    a.ForeColor = Color.DodgerBlue;
                    groupBox1.Controls.Add(a);
                    Label D = new Label();
                    D.Location = new Point(255, j * 40 + 55);
                    D.Size = new Size(100, 30);
                    D.Text = ds.Tables["Rank"].Rows[i].ItemArray[1].ToString();
                    D.Font = new Font("黑体", 20, FontStyle.Bold);
                    D.ForeColor = Color.DodgerBlue;
                    groupBox1.Controls.Add(D);
                    if (i == 3)
                    {
                        break;
                    }
                    j++;
                }
            }
            ds.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();
            x.Show();
            this.Close();
        }
    }
}
