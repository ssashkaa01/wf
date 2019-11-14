using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloknot
{
    public partial class Form1 : Form
    {
        private void AddTab(string nameFile = "No name", string text = "")
        {
            TabPage tabPage = new TabPage();
            RichTextBox MyTextBox = new RichTextBox();

            MyTextBox.Location = new System.Drawing.Point(11, 6);
            MyTextBox.Multiline = true;
            MyTextBox.Name = "textBox";
            MyTextBox.ScrollBars = RichTextBoxScrollBars.Both;
            MyTextBox.Size = new System.Drawing.Size(752, 344);
            MyTextBox.TabIndex = 1;
            MyTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            MyTextBox.Text = text;
            MyTextBox.ContextMenuStrip = contextMenuStrip1;

            tabPage.ResumeLayout(false);
            tabPage.PerformLayout();
            tabPage.Controls.Add(MyTextBox);
            tabPage.Location = new System.Drawing.Point(4, 25);
            tabPage.Name = nameFile;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(769, 356);
            tabPage.TabIndex = 0;
            tabPage.Text = nameFile;
            tabPage.UseVisualStyleBackColor = true;
            tabControl.Controls.Add(tabPage);
        }

        private RichTextBox GetTextBox()
        {
            return tabControl.SelectedTab.Controls[0] as RichTextBox;
        }

        private string GetTabName()
        {
            return tabControl.SelectedTab.Text;
        }

        private void ChangeTabName(string name)
        {
            tabControl.SelectedTab.Text = name;
        }

        public Form1()
        {
            
            InitializeComponent();
            AddTab();
            UpdateMaxLengthTextBox();
            
        }

        private void UpdateMaxLengthTextBox()
        {
            GetTextBox().MaxLength = Convert.ToInt32(maxSymbols.Value);
        }

        private int GetCountWords()
        {
            return Regex.Matches(GetTextBox().Text, @"[^\s\d]+").Count;
        }

        private int GetCountSymbols()
        {
            return Regex.Matches(GetTextBox().Text, @"\p{C}|\p{L}").Count;
        }

        private int GetCountNumbers()
        {
            return Regex.Matches(GetTextBox().Text, @"\d").Count;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = $"{GetTextBox().Text.Count()}/{maxSymbols.Value} | Digits: {GetCountNumbers()} | Letters: {GetCountSymbols()} | Words: {GetCountWords()}";  
        }

        private void maxSymbols_ValueChanged(object sender, EventArgs e)
        {
            UpdateMaxLengthTextBox();
        }


        // Відкриття файлу
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool rewrite = true;

            if (GetTextBox().TextLength != 0)
            {
                DialogResult dialogRewrite = MessageBox.Show("Rewrite this text file?", "Ops!!!", MessageBoxButtons.YesNo);
                if (dialogRewrite == DialogResult.Yes)
                {
                    rewrite = true;
                }
                else if (dialogRewrite == DialogResult.No)
                {
                    rewrite = false;
                }
            }

            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "All files(*.*)|*.*|Text Files(*.txt)|*.txt||";
            open.FilterIndex = 1;

            if (open.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = File.OpenText(open.FileName);

                string text = reader.ReadToEnd();

                if (!rewrite)
                {
                    AddTab(open.FileName, text);
                }
                else
                {
                    GetTextBox().Text = text;
                    maxSymbols.Value = text.Length;
                    ChangeTabName(open.FileName); 
                }
                reader.Close();
            }
        }

        // Збереження файлу
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (GetTabName() == "No name" || !File.Exists(GetTabName()))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.DefaultExt = ".txt";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    
                    StreamWriter writer = new StreamWriter(save.FileName);

                    ChangeTabName(save.FileName);

                    writer.Write(GetTextBox().Text);
                    writer.Close();
                }
            }
            else
            {
                StreamWriter writer = new StreamWriter(GetTabName());
                writer.Write(GetTextBox().Text);
                writer.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";

            if (save.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(save.FileName);

                ChangeTabName(save.FileName);

                writer.Write(GetTextBox().Text);
                writer.Close();
            }
        }

        // Відкриття вкладки
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        // Закриття вкладки
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Remove(tabControl.SelectedTab);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTextBox().Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTextBox().Paste();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().SelectAll();
        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().DeselectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().Clear();
        }

        private void deselectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTextBox().DeselectAll();
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void ChangeFont()
        {
            fontDialog1.ShowColor = true;
            //Связываем свойства SelectionFont и SelectionColor элемента RichTextBox 
            //с соответствующими свойствами диалога
            fontDialog1.Font = GetTextBox().SelectionFont;
            fontDialog1.Color = GetTextBox().SelectionColor;
            //Если выбран диалог открытия файла, выполняем условие
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                GetTextBox().SelectionFont = fontDialog1.Font;
                GetTextBox().SelectionColor = fontDialog1.Color;
            }
        }

        private void ChangeColor()
        {
            colorDialog1.Color = GetTextBox().SelectionColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GetTextBox().SelectionColor = colorDialog1.Color;
            }
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTextBox().Redo();
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTextBox().SelectAll();
        }
    }
}
