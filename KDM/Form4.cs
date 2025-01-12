using System;
using System.Windows.Forms;

namespace KDM
{
    public partial class Form4 : Form
    {
        Form1 form;
        public Form4(Form1 form1, int ColumsCount)
        {
            InitializeComponent();
            form = form1;
            form.listingString.Append("for (int i = 0; i < ColumsCount; i++), инициализация dataGridView1;");
            for (int i = 0; i < ColumsCount; i++)
            {
                dataGridView1.Columns.Add("column1", (i + 1).ToString());
                dataGridView1.Columns[i].Width = 25;
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            form.listingString.Append("Срабатывание ивента dataGridView1_CellValueChanged;");
            form.listingString.Append("\n");
            try
            {
                form.listingString.Append("if (dataGridView1.CurrentCell.Value != null);");
                if (dataGridView1.CurrentCell.Value != null)
                {
                    form.listingString.Append("True;");
                    form.listingString.Append("\n");
                    form.listingString.Append("if (dataGridView1.CurrentCell.Value.ToString().Length <= 1);");
                    form.listingString.Append("\n");
                    if (dataGridView1.CurrentCell.Value.ToString().Length <= 1)
                    {
                        form.listingString.Append("True;");
                        form.listingString.Append("\n");
                        int value = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                        form.listingString.Append("if (value != 0 && value != 1);");
                        form.listingString.Append("\n");
                        if (value != 0 && value != 1)
                        {
                            form.listingString.Append("Вывод сообщения об ошибке;");
                            form.listingString.Append("\n");
                            form.listingString.Append("dataGridView1.CurrentCell.Value = null;");
                            form.listingString.Append("\n");
                            dataGridView1.CurrentCell.Value = null;
                            MessageBox.Show("Допустимы только значения 1 и 0.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        form.listingString.Append("Вывод сообщения об ошибке;");
                        form.listingString.Append("\n");
                        form.listingString.Append("dataGridView1.CurrentCell.Value = null;");
                        form.listingString.Append("\n");
                        dataGridView1.CurrentCell.Value = null;
                        MessageBox.Show("Допустимы только значения 1 и 0.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch(NullReferenceException ex)
            {
                form.listingString.Append("Ошибка NullReferenceException;");
                form.listingString.Append("\n");
                form.listingString.Append("Вывод сообщения об ошибке;");
                form.listingString.Append("\n");
                return;
            }
            catch(FormatException ex)
            {
                form.listingString.Append("Ошибка FormatException;");
                form.listingString.Append("\n");
                form.listingString.Append("Вывод сообщения об ошибке;");
                form.listingString.Append("\n");
                MessageBox.Show("Требуется ввести число!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form.listingString.Append("dataGridView1.CurrentCell.Value = null;");
                dataGridView1.CurrentCell.Value = null;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.listingString.Append("Срабатывание ивента button2_Click;");
            form.listingString.Append("\n");
            form.listingString.Append("this.Close;");
            form.listingString.Append("\n");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.listingString.Append("Срабатывание ивента button1_Click;");
            form.listingString.Append("\n");
            form.listingString.Append("Работа циклов for(int i=0; i < dataGridView1.RowCount;i++) и for (int j = 0; j < dataGridView1.RowCount; j++);");
            form.listingString.Append("\n");
            int[,] Matrix = new int[dataGridView1.RowCount, dataGridView1.RowCount];

            for(int i=0; i < dataGridView1.RowCount;i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    form.listingString.Append("if (dataGridView1.Rows[i].Cells[j].Value==null);");
                    form.listingString.Append("\n");
                    if (dataGridView1.Rows[i].Cells[j].Value==null)
                    {
                        form.listingString.Append("True;");
                        form.listingString.Append("\n");
                        MessageBox.Show("Заполните все поля матрицы!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Matrix[i, j] =Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            form.CreateMatrix(Matrix , dataGridView1.RowCount);
            this.Close();
        }
    }
}
