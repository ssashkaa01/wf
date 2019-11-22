using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyBoardGame
{
    public partial class Form1 : Form
    {
        int countErrors { get; set; }
        int countSeconds { get; set; }

        public Form1()
        {
            InitializeComponent();
            ResetAll();
        }

        private void UpdateStatusBar()
        {
            toolStripStatusErrors.Text = $"Errors: {countErrors} :: Timer {countSeconds} sec";
        }

        private void ShowMessage()
        {
            MessageBox.Show($"Errors: {countErrors} :: Time {countSeconds} sec", "Result");
        }

        private void ResetAll()
        {
            countErrors = 0;
            countSeconds = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            buttonStart.Enabled = true;
            timer1.Stop();
            UpdateStatusBar();
            progressBar.Value = 0;

        }

        private string GetTrainingText()
        {
            return "But I must explain to you how all.";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ResetAll();
            buttonStart.Enabled = false;
            timer1.Start();
            textBox1.Text = GetTrainingText();
            progressBar.Maximum = textBox1.Text.Length;

        }

        private bool CheckKeyInText(char key)
        {
            if(textBox1.Text != "")
            {
                return key == textBox1.Text[0];
            }
            else
            {
                ShowMessage();
                ResetAll();
                return false;
            }
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckKeyInText(e.KeyChar))
            {
                textBox2.Text += e.KeyChar;
                textBox1.Text = textBox1.Text.Substring(1);
                progressBar.Value = textBox2.Text.Length;

            }
            else
            {
                countErrors++;
                UpdateStatusBar();
            }
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            countSeconds++;
            UpdateStatusBar();
        }
    }
}
