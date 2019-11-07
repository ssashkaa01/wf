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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace _03._11._2019
{

    public partial class Form1 : Form
    {
        private List<Order> orders { get; set; }

        private int GetPrice(string order)
        {
            Regex rgx = new Regex(@"\(([0-9]+)");

            return Convert.ToInt32(rgx.Match(order).Value.Substring(1));
        }

        private void ReloadListOrder()
        {
            listOrder.Items.Clear();

            foreach(Order order in orders)
            {
                listOrder.Items.Add(order.GetDescription());
            }
        }

        public Form1()
        {
            InitializeComponent();
            orders = new List<Order>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order(orders.Count + 1);

            // Drinks
            foreach(RadioButton selected in groupDrinks.Controls)
            {
                if(selected.Checked)
                {
                    order.Add(selected.Text, GetPrice(selected.Text));
                }
            }

            // Main course
            foreach (RadioButton selected in groupMainCourse.Controls)
            {
                if (selected.Checked)
                {
                    order.Add(selected.Text, GetPrice(selected.Text));
                }
            }

            // Deserts
            foreach (RadioButton selected in groupDeserts.Controls)
            {
                if (selected.Checked)
                {
                    order.Add(selected.Text, GetPrice(selected.Text));
                }
            }

            orders.Add(order);
            ReloadListOrder();
            MessageBox.Show(order.GetOrder() + $"\n {order.GetTime().ToLocalTime()}\n Sum: {order.GetSum()} UAH", "#" + Convert.ToString(order.GetID()));
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            foreach(Order order in orders)
            {
                if(order.GetDescription() == Convert.ToString(listOrder.SelectedItem))
                {
                    MessageBox.Show(order.GetOrder() + $"\n {order.GetTime().ToLocalTime()}\n Sum: {order.GetSum()} UAH", "#" + Convert.ToString(order.GetID()));
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(listOrder.SelectedIndex != -1)
            {
                orders.RemoveAt(listOrder.SelectedIndex);
                ReloadListOrder();
            }   
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("orders.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, orders);

                MessageBox.Show("Orders saved");
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("orders.dat", FileMode.OpenOrCreate))
            {
                if(fs.CanRead)
                {
                    orders = (List<Order>)formatter.Deserialize(fs);
                    ReloadListOrder();

                    MessageBox.Show("Data loaded!");
                }
                else
                {
                    MessageBox.Show("Data not loaded");
                }          
            }
        }

        private void toolTipDrinks_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
