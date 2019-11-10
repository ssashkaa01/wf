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
    public partial class Form1 : Form
    {
        private List<Product> products { get; set; }
            
        public Form1()
        {
            InitializeComponent();
            products = new List<Product>();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

            using (ProductForm form = new ProductForm())
            {
                form.SetButtonText("Створити");
                form.ShowDialog(this);
                
                if(form.DialogResult == DialogResult.OK)
                {
                    Product product = new Product()
                    {
                        name = form.GetName(),
                        count = form.GetCount(),
                        sale = form.GetSale(),
                        price = form.GetPrice(),
                        country = form.GetCountry()
                    };

                }


            }

                



        }
    }
}
