using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsEditor
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        public string GetName()
        {
            return textBoxName.Text;
        }

        public string GetCountry()
        {
            return comboBoxCountry.SelectedItem.ToString();
        }

        public int GetPrice()
        {
            return Convert.ToInt32(numericUpDownPrice.Value);
        }

        public int GetSale()
        {
            return Convert.ToInt32(numericUpDownSale.Value);
        }

        public int GetCount()
        {
            return Convert.ToInt32(numericUpDownCount.Value);
        }

        public void SetName(string name)
        {
            textBoxName.Text = name;
        }

        public void SetCountry(string country)
        {
            comboBoxCountry.Text = country;
        }

        public void SetPrice(int price)
        {
            numericUpDownPrice.Value = price;
        }

        public void SetCount(int count)
        {
            numericUpDownCount.Value = count;
        }

        public void SetSale(int sale)
        {
            numericUpDownSale.Value = sale;
        }

        public void SetButtonText(string text)
        {
            buttonAction.Text = text;
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            switch((sender as Button).Text)
            {
                case "Створити":
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

            }
            
        }
    }
}
