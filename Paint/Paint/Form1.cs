using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public struct Figure
    {
        public string name { get; set; }
        public Point firstPosition { get; set; }
        public Point endPosition { get; set; }
        public int width { get; set; }
        public int heigth { get; set; }
        public int weight { get; set; }
        public Color color { get; set; }
    }

    public partial class Form1 : Form
    {
        private Point firstPosition;
        private Point endPosition;
        private List<Figure> figures;
        private Color color { get; set; }

        public Form1()
        {
            InitializeComponent();
            figures = new List<Figure>();

            color = Color.Black;
            ChangeBackgroundButtonColor();

        }

        // Отримати вибрану фігуру
        private string GetActiveNameFigure()
        {
            foreach (var item in toolStrip1.Items)
            {
                if (item is ToolStripButton)
                {
                    if ((item as ToolStripButton).Checked)
                    {
                        return (item as ToolStripButton).Text;
                    }
                }
            }

            return "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        // Змінити активну кнопку
        private void ChangeActiveButton(object sender, EventArgs e)
        {
            foreach (var item in toolStrip1.Items)
            {
                if(item is ToolStripButton)
                {
                    if((ToolStripButton)sender != item)
                    {
                        (item as ToolStripButton).Checked = false;
                    }
                }
            }
        }

        // Отримати ширину об'єкта
        private int GetWeight()
        {
            if(toolStripComboBoxWeight.Text != "")
            {
                return Convert.ToInt32(toolStripComboBoxWeight.Text);
            }
            else
            {
                return 5;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush;
            Pen pen;

            foreach (Figure figure in figures)
            {
                brush = new SolidBrush(figure.color);
                pen = new Pen(figure.color, figure.weight);

                switch (figure.name)
                {
                    // Точка
                    case "Point":
                        g.FillEllipse(brush, figure.endPosition.X, figure.endPosition.Y, figure.weight, figure.weight);
                        break;

                    // Заповнений квадрат
                    case "SolidRectangle":
                        
                        // Create rectangle.
                        Rectangle rect = new Rectangle(figure.firstPosition.X, figure.firstPosition.Y, figure.width, figure.heigth);

                        // Fill rectangle to screen.
                        e.Graphics.FillRectangle(brush, rect);
                        break;

                    // Круг
                    case "Ellipse":

                        // Fill Ellipse to screen.
                        e.Graphics.FillEllipse(brush, figure.firstPosition.X, figure.firstPosition.Y, figure.width, figure.heigth);
                        break;

                    case "RectangleFrame":

                        e.Graphics.DrawRectangle(pen, figure.firstPosition.X, figure.firstPosition.Y, figure.width, figure.heigth);
                        break;
                }
            }
        }

        private void toolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        // Зберегти фігуру
        private void SaveFigure()
        {
            int newWidth = (this.firstPosition.X - this.endPosition.X);

            int newHeight = (this.firstPosition.Y - this.endPosition.Y);


            if (newWidth < 0)
            {
                newWidth *= -1;
            }

            if (newHeight < 0)
            {
                newHeight *= -1;
            }

            if(this.firstPosition.X > this.endPosition.X)
            {
                this.firstPosition.X = this.endPosition.X;
            }

            if (this.firstPosition.Y > this.endPosition.Y)
            {
                this.firstPosition.Y = this.endPosition.Y;
            }

            Figure newFigure = new Figure()
            {
                name = this.GetActiveNameFigure(),
                firstPosition = this.firstPosition,
                endPosition = this.endPosition,
                color = this.color,
                width = newWidth,
                heigth = newHeight,
                weight = GetWeight()
            };

            figures.Add(newFigure);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            firstPosition.X = e.X;
            firstPosition.Y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            endPosition.X = e.X;
            endPosition.Y = e.Y;

            // Зберігаємо фігуру
            SaveFigure();
            
            // Перемальовуємо
            Invalidate();
        }

        private void ChangeBackgroundButtonColor()
        {
            toolStripButtonColor.BackColor = color;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                color = colorDialog1.Color;
                ChangeBackgroundButtonColor();
            }
        }

        // Зберегти картинку
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(this.Width, this.Height);

            Graphics g = Graphics.FromImage(b);

            Size size = new Size(this.Width, this.Height);

            g.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, size);
            b.Save(@"C:/temp/test.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            if(figures.Count > 0)
            {
                figures.RemoveAt(figures.Count - 1);
                
                // Перемальовуємо
                Invalidate();
            }
        }
    }
}
