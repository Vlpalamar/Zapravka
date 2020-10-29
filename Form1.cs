using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        List<Gas> gas;
        List<Product> products;

        List<CheckBox> checkBoxes;
        List<TextBox> textBoxes;
        List<NumericUpDown> numericUpDowns;



        double FuelPrice;
        double FullFuelPrice;
        decimal FullCafePrice = 0;
        public Form1()
        {
            InitializeComponent();

            gas = new List<Gas>
            {
                new Gas{Name="A-95",Price =22.50f },
                new Gas{Name="A-98",Price =28.30f },
                new Gas{Name="Disel",Price =18.10f },
                new Gas{Name="A-98.Premiums",Price =15.80f },
                new Gas{Name="Гас",Price =10.00f }
            };
            products = new List<Product>
            {
                new Product{Name="Хот-дог",Price =5.50f },
                new Product{Name="Гамбургер",Price =7.20f },
                new Product{Name="Картошка Фри",Price =3.10f },
                new Product{Name="Кока-Кола",Price =10.00f }

            };
            foreach (var item in gas)
            {
                FuelTypes.Items.Add(item.Name);
            }
            FuelTypes.SelectedIndex = 0;
            radioButton1.Checked = true;


            checkBoxes = new List<CheckBox>(products.Count);
            textBoxes = new List<TextBox>(products.Count);
            numericUpDowns = new List<NumericUpDown>(products.Count);
           

            for (int i = 0; i < products.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Location = new System.Drawing.Point(16, 14 + 30 * i);
                checkBox.Size = new System.Drawing.Size(80, 17);
                checkBox.CheckedChanged += checkBox_checkedChanged;
                checkBox.Text = products[i].Name;
                panel4.Controls.Add(checkBox);
                checkBoxes.Add(checkBox);

                TextBox textBox = new TextBox();
                textBox.Size = new System.Drawing.Size(42, 20);
                textBox.Location = new System.Drawing.Point(118, 11 + 30 * i);
                textBox.Text = products[i].Price.ToString();
                textBox.Enabled = false;
                panel4.Controls.Add(textBox);
                textBoxes.Add(textBox);

                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.Size = new System.Drawing.Size(55, 20);
                numericUpDown.Location = new System.Drawing.Point(181, 11 + 30 * i);
                numericUpDown.Enabled = false;
                numericUpDown.ValueChanged += numericUpDown_ValueChanged;
                panel4.Controls.Add(numericUpDown);
                numericUpDowns.Add(numericUpDown);

                decimal Price = new decimal();
               
            }
        }
        class Gas
        {
            public string Name { get; set; }
            public float Price { get; set; }
            public Gas() { }
        }
        class Product
        {
            public string Name { get; set; }
            public float Price { get; set; }
            public Product() { }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void checkBox_checkedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked==true)
            {
                numericUpDowns[checkBoxes.IndexOf(checkBox)].Enabled = true;
            }
            else
            {
                numericUpDowns[checkBoxes.IndexOf(checkBox)].Enabled = false;
                numericUpDowns[checkBoxes.IndexOf(checkBox)].Value = 0;
            }
           

        }
      
        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            FullCafePrice = 0;

            NumericUpDown numericUpDown = sender as NumericUpDown;
            for (int i = 0; i < products.Count; i++)
            {
                FullCafePrice  += numericUpDowns[i].Value * decimal.Parse(textBoxes[i].Text);
            }
           
            GetCafeTotalSumm();
         
        }
        private void FuelTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PriceForThisFuel.Text = gas[FuelTypes.SelectedIndex].Price.ToString();
            FuelPrice =Convert.ToDouble(PriceForThisFuel.Text);
            radioButton1.Checked = true;
            PriceForLiter.Text = "";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                this.PriceForLiter.TextChanged += new System.EventHandler(this.PriceForLiter_TextChanged);
                this.LitterForPrice.TextChanged -= new System.EventHandler(this.LitterForPrice_TextChanged);
            }
            else
            {
                this.PriceForLiter.TextChanged -= new System.EventHandler(this.PriceForLiter_TextChanged);
                this.LitterForPrice.TextChanged += new System.EventHandler(this.LitterForPrice_TextChanged);
            }
            if (radioButton1.Enabled)
            {
                PriceForLiter.Enabled=true;
                LitterForPrice.Enabled = false;
                LitterForPrice.Text="";
                PriceForLiter.Text = "";
            }
           


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                this.PriceForLiter.TextChanged -= new System.EventHandler(this.PriceForLiter_TextChanged);
                this.LitterForPrice.TextChanged += new System.EventHandler(this.LitterForPrice_TextChanged);
            }
            else
            {
                this.PriceForLiter.TextChanged += new System.EventHandler(this.PriceForLiter_TextChanged);
                this.LitterForPrice.TextChanged -= new System.EventHandler(this.LitterForPrice_TextChanged);
            }
            if (radioButton2.Enabled)
            {
                PriceForLiter.Enabled = false;
                LitterForPrice.Enabled = true;
                LitterForPrice.Text = "";
                PriceForLiter.Text = "";
            }
        


        }

       

        private void PriceForThisFuel_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void PriceForLiter_TextChanged(object sender, EventArgs e)
        {
            if(PriceForLiter.Text!="")
            {
               FullFuelPrice = Convert.ToDouble(FuelPrice * Convert.ToDouble(PriceForLiter.Text));
            }
            else
            {
                FullFuelPrice = 00.00d;
            }
            LitterForPrice.Text = Convert.ToString(FullFuelPrice);
            GetFuelTotalSumm();


        }

        private void LitterForPrice_TextChanged(object sender, EventArgs e)
        {
            if (LitterForPrice.Text != "")
            {
                FullFuelPrice = Convert.ToDouble(LitterForPrice.Text);
            }
            else
            {
                FullFuelPrice = 00.00d;
            }
            if (LitterForPrice.Text!="0"&&LitterForPrice.Text!="")
            {
                PriceForLiter.Text = Convert.ToString(FullFuelPrice / gas[FuelTypes.SelectedIndex].Price);
            }
            GetFuelTotalSumm();


        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

       
  

       
       
        void GetFuelTotalSumm(float discond = 1f)
        {
            FullFuelPrice = FullFuelPrice * discond;
            FullPriceLabel.Text = Convert.ToString(FullFuelPrice);
        }
        void GetCafeTotalSumm(decimal discond = 1M)
        {
            FullCafePrice = FullCafePrice * discond;
            FullCafeLabel.Text = Convert.ToString(FullCafePrice);
        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
