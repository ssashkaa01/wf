using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameButton
{
    public partial class Form1 : Form
    {

        private int maxTime { get; set; }

        public Form1()
        {
            InitializeComponent();
            maxTime = 10000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.progressBar1.Value == maxTime && GetLastCheckedNumber() + 1 != GetCount() && buttonStart.Enabled == false)
            {
                buttonStart.Enabled = true;
                MessageBox.Show("YOU LOSE!");
                Clear();
            }
            this.progressBar1.Increment(100);
        }

        private int GetCount()
        {
            int count = 0;

            foreach (Control item in this.panel1.Controls)
            {
                if (item is Button)
                {
                    count++;
                }
            }

            return count;
        }

        private bool CheckIssetButton(int numb)
        {
            foreach (Control item in this.panel1.Controls)
            {
                if (item is Button && item.Text == Convert.ToString(numb))
                {
                    return true;
                }
            }

            return false;
        }

        private int GenerateNumber()
        {
            int max = GetCount();
            int number = 0;

            Random rnd = new Random();

            while (number == 0)
            {
                number = rnd.Next(1, max + 1);
                
                if (CheckIssetButton(number))
                {
                    number = 0;
                }
            }

            return number;
        }

        private void Clear()
        {
            foreach (Control item in this.panel1.Controls)
            {
                if (item is Button)
                {
                    item.BackColor = Color.White;
                    item.Text = "";
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            foreach (Control item in this.panel1.Controls)
            {
                if (item is Button)
                {
                    item.Text = Convert.ToString(GenerateNumber());
                }
            }

            maxTime = 10000 - trackBar1.Value*1000;
            buttonStart.Enabled = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = maxTime;
            this.timer1.Start();
        }

        private int GetLastCheckedNumber()
        {
            int number = 0;

            foreach (Control item in this.panel1.Controls)
            {
                if (item is Button)
                {
                    if(item.BackColor == Color.Black && number < Convert.ToInt32(item.Text))
                    {
                        number = Convert.ToInt32(item.Text);
                    }
                }
            }

            return number;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonClick_Click(object sender, EventArgs e)
        {
            if(buttonStart.Enabled == false)
            {
                if(GetLastCheckedNumber() + 1 == Convert.ToInt32(((Button)sender).Text))
                {
                    ((Button)sender).BackColor = Color.Black;
                }

                if (GetLastCheckedNumber() + 1 == GetCount())
                {
                    buttonStart.Enabled = true;
                    MessageBox.Show("YOU WIN!");
                    Clear();
                }
            }
        }
    }
}