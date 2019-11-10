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

        private void ReloadList()
        {
            listProducts.Items.Clear();

            foreach(Product product in products)
            {
                listProducts.Items.Add(product.GetID());
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

            using (ProductForm form = new ProductForm())
            {
                form.SetButtonText("Створити");
                form.ShowDialog(this);

                if (form.DialogResult == DialogResult.OK)
                {
                    Product product = new Product()
                    {
                        name = form.GetName(),
                        count = form.GetCount(),
                        sale = form.GetSale(),
                        price = form.GetPrice(),
                        country = form.GetCountry()
                    };

                    products.Add(product);
                    ReloadList();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listProducts.SelectedIndex != -1)
            {

                Product product = (Product)products.Where(prod => prod.GetID() == listProducts.Text).FirstOrDefault();

                using (ProductForm form = new ProductForm())
                {
                    form.SetButtonText("Редагувати");
                    form.SetName(product.name);
                    form.SetPrice(product.price);
                    form.SetSale(product.sale);
                    form.SetCountry(product.country);
                    form.SetCount(product.count);

                    form.ShowDialog(this);

                    if (form.DialogResult == DialogResult.OK)
                    {
                        int index = products.IndexOf(product);

                        products.RemoveAt(index);

                        Product edit = new Product()
                        {
                            name = form.GetName(),
                            count = form.GetCount(),
                            sale = form.GetSale(),
                            price = form.GetPrice(),
                            country = form.GetCountry()
                        };

                        products.Insert(index, edit);
                        ReloadList();
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listProducts.SelectedIndex != -1)
            {
                Product product = (Product)products.Where(prod => prod.GetID() == listProducts.Text).FirstOrDefault();

                int index = products.IndexOf(product);

                products.RemoveAt(index);

                ReloadList();
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            if (listProducts.SelectedIndex != -1)
            {

                Product product = (Product)products.Where(prod => prod.GetID() == listProducts.Text).FirstOrDefault();

                using (ProductForm form = new ProductForm())
                {
                    form.SetButtonText("Закрити");
                    form.DisableForm();
                    form.SetName(product.name);
                    form.SetPrice(product.price);
                    form.SetSale(product.sale);
                    form.SetCountry(product.country);
                    form.SetCount(product.count);

                    form.ShowDialog(this);

                }
            }
        }
    }
}
