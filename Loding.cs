using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeping
{
    public partial class Loding : Form
    {
        public Loding()
        {
            InitializeComponent();
        }

        private void Loding_Load(object sender, EventArgs e)
        {
            string fileName = Application.StartupPath + "\\images\\loding.gif";
            pictureBox1.Image = Image.FromFile(fileName);
        }
    }
}
