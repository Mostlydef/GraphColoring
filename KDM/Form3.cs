using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDM
{
    public partial class Form3 : Form
    {
        Form1 form;
        public Form3(Form1 form1)
        {
            InitializeComponent();
            form = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.listingString.Append("Form3 Срабатывание ивента button2_Click;");
            form.listingString.Append("\n");
            int RowsCount=0;
            int ColumnsCount=0;
           try
            {
                RowsCount = Convert.ToInt32(textBox1.Text);
                ColumnsCount = Convert.ToInt32(textBox1.Text);
                form.listingString.Append("Form3 RowsCount = Convert.ToInt32(textBox1.Text);");
                form.listingString.Append("\n");
                form.listingString.Append("Form3 ColumnsCount = Convert.ToInt32(textBox1.Text);");
                form.listingString.Append("\n");
                form.listingString.Append("if (RowsCount <= 0);");
                form.listingString.Append("\n");
                if (RowsCount <= 0)
                {
                    form.listingString.Append("True;");
                    form.listingString.Append("\n");
                    form.listingString.Append("RowsCount = 0; ColumnsCount = 0; textBox1.Clear();");
                    form.listingString.Append("\n");
                    RowsCount = 0;
                    ColumnsCount = 0;
                    textBox1.Clear();
                    form.listingString.Append("Вывод сообщения об ошибке;");
                    form.listingString.Append("\n");
                    MessageBox.Show("Допустимы только положительные значения.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                form.listingString.Append("if (RowsCount>200);");
                form.listingString.Append("\n");
                if (RowsCount>200)
                {
                    form.listingString.Append("True;");
                    form.listingString.Append("\n");
                    form.listingString.Append("RowsCount = 0; ColumnsCount = 0; textBox1.Clear();");
                    form.listingString.Append("\n");
                    RowsCount = 0;
                    ColumnsCount = 0;
                    textBox1.Clear();
                    form.listingString.Append("Вывод сообщения об ошибке;");
                    form.listingString.Append("\n");
                    MessageBox.Show("Введите число в диапазоне 1 до 200.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch(OverflowException ex)
            {
                form.listingString.Append("Ошибка OverflowException;");
                form.listingString.Append("\n");
                form.listingString.Append("Вывод сообщения об ошибке;");
                form.listingString.Append("\n");
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }
            catch(Exception ex)
            {
                form.listingString.Append("Ошибка Exception;");
                form.listingString.Append("\n");
                form.listingString.Append("Вывод сообщения об ошибке;");
                form.listingString.Append("\n");
                MessageBox.Show("Требуется ввести число!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }
            form.listingString.Append(" Form4 form4 = new Form4(form, ColumnsCount);");
            form.listingString.Append("\n");
            Form4 form4 = new Form4(form, ColumnsCount);
            form.listingString.Append(" form4.Show();");
            form.listingString.Append("\n");
            form4.Show();
            this.Close();
        }
    }
}
