using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuzinLines
{
    public partial class Form1 : Form
    {
        Board board;
        Ruzin ruzin;
        Graphics gr;
        Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(450, 450);
            pictureBox1.Image = bitmap;
            gr = Graphics.FromImage(bitmap);
            board = new Board();
            ruzin = new Ruzin(board);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ruzin.DrawFirst(gr);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs newE = e as MouseEventArgs;
            board.checkClick(new Point(newE.X, newE.Y));
            ruzin.Draw(gr);
            pictureBox1.Refresh();
        }
    }
}
