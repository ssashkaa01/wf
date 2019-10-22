using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ми і не сумнівалися, що ви так думаєте!");
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Старайтесь краще)");
        }

        private void buttonNo_MouseMove(object sender, MouseEventArgs e)
        {

            Random r = new Random();
            buttonNo.Left = r.Next(0, this.Size.Width - buttonNo.Width);
            buttonNo.Top = r.Next(0, this.Size.Height - buttonNo.Height);
        }
    }
}
