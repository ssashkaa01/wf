using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloknot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateMaxLengthTextBox();
        }

        private void UpdateMaxLengthTextBox()
        {
            textBox.MaxLength = Convert.ToInt32(maxSymbols.Value);
        }

        private int GetCountWords()
        {
            return Regex.Matches(textBox.Text, @"[^\s\d]+").Count;
        }

        private int GetCountSymbols()
        {
            return Regex.Matches(textBox.Text, @"\p{C}|\p{L}").Count;
        }

        private int GetCountNumbers()
        {
            return Regex.Matches(textBox.Text, @"\d").Count;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = $"{textBox.Text.Count()}/{maxSymbols.Value} | Digits: {GetCountNumbers()} | Letters: {GetCountSymbols()} | Words: {GetCountWords()}";
              
        }

        private void maxSymbols_ValueChanged(object sender, EventArgs e)
        {
            UpdateMaxLengthTextBox();
        }
    }
}
