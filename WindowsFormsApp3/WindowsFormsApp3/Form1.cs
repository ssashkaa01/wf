using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "";

            text += $"Name - { textName.Text}\n";
            text += $"Surname - { textSurname.Text}\n";
            text += $"Country - { textCountry.Text}\n";
            text += $"City - { textCity.Text}\n";
            text += $"Sex - { (radioMale.Checked ? "male" : "female") }\n";
            text += $"Birthday - { dateBirthday.Value }\n";
            text += $"Language - ";
         
            foreach (Control c in groupTechnologies.Controls)
            {
                if (((CheckBox)c).Checked)
                {
                    text += $" {c.Text}";
                }
            }

            MessageBox.Show(text, "Info");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Student st = new Student()
            {
                Name = textName.Text,
                Surname = textSurname.Text,
                Country = textCountry.Text,
                City = textCity.Text,
                IsMale = radioMale.Checked,
                Birthday = dateBirthday.Value,
                Techlogies = new List<string>()
            };

            foreach (Control c in groupTechnologies.Controls)
            {
                if (((CheckBox)c).Checked)
                {
                    st.Techlogies.Add(c.Text);
                }
            }
           
            BinaryFormatter formatter = new BinaryFormatter();
   
            using (FileStream fs = new FileStream("student.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, st);

                MessageBox.Show("Data saved");
            }

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("student.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    Student st = (Student)formatter.Deserialize(fs);

                    textName.Text = st.Name;
                    textSurname.Text = st.Surname;
                    textCountry.Text = st.Country;
                    textCity.Text = st.City;
                    dateBirthday.Value = st.Birthday;
                    radioMale.Checked = st.IsMale;
                    radioFemale.Checked = !st.IsMale;

                    foreach (var c in groupTechnologies.Controls)
                    {
                        //string str = new string();
                        if (st.Techlogies.IndexOf((c as CheckBox).Text) != -1)
                        {
                            (c as CheckBox).Checked = true;
                        }
                        else
                        {
                            (c as CheckBox).Checked = false;
                        }
                    }
                }
                catch(System.Runtime.Serialization.SerializationException ex)
                {
                    MessageBox.Show("File not load!");
                }      
            }
        }
    }
}
