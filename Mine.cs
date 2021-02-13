using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace minesweeping
{
    public class Mine:Button
    {
        //按钮在二维矩阵中的坐标
        private int x;
        private int y;
        //在该按钮下是否有雷，false代表无雷，true代表有雷
        private Boolean ismine;
        private Boolean isSaomao;//按钮是否有扫描
        public Mine()
        {
            Tag = 0;//0代表未翻开，1代表翻开
            Size = new System.Drawing.Size(20, 20);
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {

            get { return y; }
            set { y = value; }
        }
        public Boolean Ismine
        {
            get { return ismine; }
            set { ismine = value; }
        }
        public Boolean IsSaomao
        {
            get { return isSaomao; }
            set { isSaomao = value; }
        }
    }
}
