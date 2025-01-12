using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace KDM
{

    public partial class Form1 : Form
    {
        GraphDrawing graphDrawing;
        public StringBuilder listingString;
        List<Vertex> vertexList;
        List<Edge> edgeList;
        List<string> files;
        int[,] Amatrix;
        int NumberOfVertices;
        int currentCell;
        int selectVertex_1;
        int selectVertex_2;
        bool OrGraph;

        public Form1()
        {
            InitializeComponent();
            listingString = new StringBuilder("");
            listingString.Append("Вызов конструктора form1;");
            listingString.Append('\n');
            listingString.Append("Инициализация формы :" + this + ";");
            listingString.Append('\n');
            vertexList = new List<Vertex>();
            listingString.Append("Инициализация списка vertexList;");
            listingString.Append('\n');
            edgeList = new List<Edge>();
            listingString.Append("Инициализация списка edgeList;");
            listingString.Append('\n');
            graphDrawing = new GraphDrawing(pictureBox1.Width, pictureBox1.Height, listingString);
            listingString.Append("Создание объекта класса GraphDrawing с параметрами pictureBox1.Width=" + pictureBox1.Width + ", pictureBox1.Height=" + pictureBox1.Height +";");
            listingString.Append('\n');
            pictureBox1.Image = graphDrawing.GetBitmap();
            listingString.Append("Установка типа Image в pictureBox1;");
            listingString.Append('\n');
            fileDataGridView.TopLeftHeaderCell.Value = "№";
            files = new List<string>();
            listingString.Append("Инициализация списка files;");
            listingString.Append('\n');
            UploadingFiles();
        }

        public void CalculationOfCoordinates()
        {
            listingString.Append("Вызов функции GraphCheck;");
            listingString.Append('\n');
            OrGraph = GraphCheck();
            Point center = new Point((pictureBox1.Width) >> 1, (pictureBox1.Height) >>1);
            double gradus = 360.0 / (double)NumberOfVertices;
            double fi= 360.0 / (double)NumberOfVertices;
            double radians = 0;
            double radius = Math.Min((pictureBox1.Height-80) >> 1, (pictureBox1.Width-80) >> 1);
            listingString.Append("Работа цикла for (int i = 0; i < NumberOfVertices; i++), вычисление координат вершин и добавление их в vertexList;");
            listingString.Append('\n');
            for (int i = 0; i < NumberOfVertices; i++)
            {
                vertexList.Add(new Vertex(center.X+(int)(radius * Math.Cos(radians)), center.Y+(int)(radius * Math.Sin(radians))));
                radians = gradus * (Math.PI / 180);
                gradus += fi;
            }
            listingString.Append("if (!OrGraph);");
            listingString.Append('\n');
            if (!OrGraph)
            {
                listingString.Append("True;");
                listingString.Append('\n');
                listingString.Append("Работа циклов for (int i = 0; i < NumberOfVertices; i++) и for (int j = i; j < NumberOfVertices; j++), создание рёбер и добавление их в edgeList;");
                listingString.Append('\n');
                for (int i = 0; i < NumberOfVertices; i++)
                {
                    for (int j = i; j < NumberOfVertices; j++)
                    {
                        if (Amatrix[i, j] == 1)
                        {
                            edgeList.Add(new Edge(i, j));
                        }
                    }
                }
            }
            else
            {
                listingString.Append("False;");
                listingString.Append('\n');
                listingString.Append("Работа циклов for (int i = 0; i < NumberOfVertices; i++) и for (int j = i; j < NumberOfVertices; j++), создание рёбер и добавление их в edgeList;");
                listingString.Append('\n');
                for (int i = 0; i < NumberOfVertices; i++)
                {
                    for (int j = 0; j < NumberOfVertices; j++)
                    {
                        if (Amatrix[i, j] == 1)
                        {
                            edgeList.Add(new Edge(i, j));
                        }
                    }
                }
            }
            listingString.Append("Вызов функции graphDrawing.DrawGraph с параметрами vertexList, edgeList,  OrGraph;");
            listingString.Append('\n');
            graphDrawing.DrawGraph(vertexList, edgeList, OrGraph, listingString);
            listingString.Append("Вызов функции pictureBox1.Invalidate;");
            listingString.Append('\n');
            pictureBox1.Invalidate();
        }

        public void UploadingFiles()
        {
            listingString.Append("Вызов функции UploadingFiles;");
            listingString.Append('\n');
            try
            {
                int i = 0;
                string[] file;
                string directory = "KDM_course";
                listingString.Append("if(Directory.Exists(directory));");
                listingString.Append('\n');
                if (Directory.Exists(directory))
                {
                    listingString.Append("True;");
                    file =Directory.GetFiles(directory);
                    listingString.Append("file =Directory.GetFiles(directory) получение массива абсолютных путей к файлам;");
                    listingString.Append('\n');
                    listingString.Append("Начало работы цикла: for (int j=0;j<file.Length;j++), добавление абсолютных путей фалов в лист строк;");
                    listingString.Append('\n');
                    for (int j=0;j<file.Length;j++)
                    {
                        files.Add(file[j]);
                    }
                    listingString.Append("Начало работы цикла foreach (string str in files), получение названия фалов и вывод в таблицу fileDataGridView;");
                    listingString.Append('\n');
                    foreach (string str in files)
                    {
                        file = str.Split('\\');
                        fileDataGridView.Rows.Add(file[file.Length-1]);
                        fileDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    currentCell = 0;
                }
                else
                {
                    listingString.Append("False;");
                    MessageBox.Show("Не удается найти указанную папку.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    listingString.Append('\n');
                    listingString.Append("Вывод сообщения об ошибке;");
                }
            }
            catch(IOException ex)
            {
                listingString.Append("Ошибка IOException;");
                listingString.Append('\n');
                return;
            }
            catch (System.UnauthorizedAccessException e)
            {
                listingString.Append("Ошибка UnauthorizedAccessException;");
                listingString.Append('\n');
                listingString.Append("Вывод сообщения об ошибке;");
                listingString.Append('\n');
                MessageBox.Show("Допустимы только значения 1 и 0.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        public void ReadOnFile(int i)
        {
            try
            {
                string line;
                int k = 0;
                string[] lines;
                listingString.Append("Cоздание файлового потока и связывание его с файлом " + files[i] + ";");
                listingString.Append('\n');
                StreamReader reader = new StreamReader(files[i]);
                listingString.Append(" while ((line = reader.ReadLine()) != null) ;");
                listingString.Append('\n');
                while ((line = reader.ReadLine()) != null)
                {
                    lines = line.Split(' ');
                    if (k == 0)
                    {
                        listingString.Append("True;");
                        listingString.Append('\n');
                        listingString.Append("Инициализация двумерного массива Amatrix;");
                        listingString.Append('\n');
                        Amatrix = new int[lines.Length, lines.Length];
                        NumberOfVertices = lines.Length;
                        listingString.Append("Присваивание NumberOfVertices= "+NumberOfVertices);
                        listingString.Append('\n');
                        listingString.Append("Начало работы цикла for (int j = 0; j < lines.Length; j++), создание матрицы смежности;");
                        listingString.Append('\n');
                    }
                    for (int j = 0; j < lines.Length; j++)
                    {

                        if (lines[j].Length > 1)
                        {
                            throw new IOException();
                        }
                        else
                        {
                            Amatrix[k, j] = Convert.ToInt32(lines[j]);
                        }
                    }
                    k++;
                }
                if(k!=NumberOfVertices)
                {
                    throw new IOException();
                }
                listingString.Append(" Закрытие файлового потока, который свзяан с файлом"+files[i]+ ";");
                listingString.Append('\n');
                reader.Close();
            }
            catch(IOException e)
            {
                listingString.Append("Ошибка IOException;");
                listingString.Append('\n');
                listingString.Append("Вывод сообщения об ошибке: Некорректный файл.;");
                listingString.Append('\n');
                MessageBox.Show("Некорректный файл.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    fileDataGridView.Rows[i].Selected = false;
                    fileDataGridView.Rows.RemoveAt(i);
                    files.RemoveAt(i);
                    fileDataGridView.Rows[0].Selected = true;
                }));
                return;
            }
            catch(FormatException ex)
            {
                listingString.Append("Ошибка FormatException;");
                listingString.Append('\n');
                listingString.Append("Вывод сообщения об ошибке: Некорректный файл.;");
                listingString.Append('\n');
                MessageBox.Show("Некорректный файл.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    fileDataGridView.Rows[i].Selected = false;
                    fileDataGridView.Rows.RemoveAt(i);
                    files.RemoveAt(i);
                    fileDataGridView.Rows[0].Selected = true;
                }));
                return;
            }
            listingString.Append("Вызов функции dataGridView1.Rows.Clear;");
            listingString.Append('\n');
            textBox1.Clear();
            dataGridView1.Rows.Clear();
            listingString.Append("Вызов функции  dataGridView1.Columns.Clear;");
            listingString.Append('\n');
            dataGridView1.Columns.Clear();
            listingString.Append("Вызов функции  vertexList.Clear;");
            listingString.Append('\n');
            vertexList.Clear();
            listingString.Append("Вызов функции  edgeList.Clear;");
            listingString.Append('\n');
            edgeList.Clear();
            listingString.Append("Вызов функции  graphDrawing.ClearPicture;");
            listingString.Append('\n');
            graphDrawing.ClearPicture();
            listingString.Append("Вызов функции   CalculationOfCoordinates;");
            listingString.Append('\n');
            CalculationOfCoordinates();
        }

        private void fileDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                listingString.Append("Срабатывание ивента fileDataGridView_CurrentCellChanged;");
                listingString.Append('\n');
                listingString.Append("Вызов функции ReadOnFile с параметром fileDataGridView.CurrentCell.RowIndex =" + fileDataGridView.CurrentCell.RowIndex + ";");
                listingString.Append('\n');
                ReadOnFile(fileDataGridView.CurrentCell.RowIndex);
            }
            catch(NullReferenceException ex)
            {
                return;
            }
        }

        private void смежностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vertexList.Count != 0)
            {
                listingString.Append("Срабатывание ивента смежностиToolStripMenuItem_Click;");
                listingString.Append('\n');
                listingString.Append("if (NumberOfVertices != 0 && dataGridView1.RowCount==0 && dataGridView1.ColumnCount==0 );");
                listingString.Append('\n');
                if (NumberOfVertices != 0 && dataGridView1.RowCount == 0 && dataGridView1.ColumnCount == 0)
                {
                    listingString.Append("Работа цикла for (int i = 0; i < NumberOfVertices; i++), инициализация dataGridView1;");
                    listingString.Append('\n');
                    for (int i = 0; i < NumberOfVertices; i++)
                    {
                        dataGridView1.Columns.Add("column1", (i + 1).ToString());
                        dataGridView1.Columns[i].Width = 25;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }
                    listingString.Append("Работа циклов for (int i = 0; i < NumberOfVertices; i++) и for (int j = 0; j < NumberOfVertices; j++), заполнение dataGridView1 матрицой смежности Amatrix;");
                    listingString.Append('\n');
                    for (int i = 0; i < NumberOfVertices; i++)
                    {
                        for (int j = 0; j < NumberOfVertices; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = Amatrix[i, j];
                        }
                    }
                    listingString.Append("Работа циклов for (int i = 0; i < NumberOfVertices; i++) и for (int j = 0; j < NumberOfVertices; j++) завершена;");
                    listingString.Append('\n');
                }
            }
            else
            {
                MessageBox.Show("Список вершин и рёбер пуст.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private bool GraphCheck()
        {
            try
            {
                listingString.Append("Начало работы циклов for (int i = 0; i < NumberOfVertices; i++) и for (int j = 0; j < NumberOfVertices; j++), проверка графа на принадлежность к орграфам;");
                listingString.Append('\n');
                for (int i = 0; i < NumberOfVertices; i++)
                {
                    for (int j = 0; j < NumberOfVertices; j++)
                    {
                        listingString.Append("if (Amatrix[i, j] != Amatrix[j, i]);");
                        listingString.Append('\n');
                        if (Amatrix[i, j] != Amatrix[j, i])
                        {
                            listingString.Append("True;");
                            listingString.Append('\n');
                            listingString.Append("throw new Exception();;");
                            listingString.Append('\n');
                            throw new Exception();
                        }
                        listingString.Append("False;");
                        listingString.Append('\n');
                    }
                }
                listingString.Append("Работа циклов for (int i = 0; i < NumberOfVertices; i++) и for (int j = 0; j < NumberOfVertices; j++) завершена;");
                listingString.Append('\n');
                listingString.Append("Возвращения значения false - граф неоренторванный;");
                listingString.Append('\n');
                return false;
            }
            catch(Exception ex)
            {
                listingString.Append("Возвращения значения true - граф является орграфом;");
                listingString.Append('\n');
                return true;
            }
        }

        private void поМатрицеСмежностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listingString.Append("Срабатывание ивента поМатрицеСмежностиToolStripMenuItem_Click;");
            listingString.Append('\n');
            listingString.Append("Присваивание переменной dataGridView1.RowCount значения 0;");
            listingString.Append('\n');
            dataGridView1.RowCount = 0;
            listingString.Append("Присваивание переменной dataGridView1.ColumnCount значения 0;");
            listingString.Append('\n');
            dataGridView1.ColumnCount = 0;
            listingString.Append("Объявление и инициализация объекта класса Form3;");
            listingString.Append('\n');
            Form3 form3 = new Form3(this);
            listingString.Append("Вызов функции form3.Show;");
            listingString.Append('\n');
            form3.Show();
        }

        public void CreateMatrix(int [,] matrix, int size)
        {
            listingString.Append(" NumberOfVertices = " + size + ";");
            listingString.Append('\n');
            NumberOfVertices = size;
            listingString.Append(" Amatrix = matrix;");
            listingString.Append('\n');
            Amatrix = matrix;
            listingString.Append("Вызов функции  vertexList.Clear;");
            listingString.Append('\n');
            vertexList.Clear();
            listingString.Append("Вызов функции  edgeList.Clear;");
            listingString.Append('\n');
            edgeList.Clear();
            listingString.Append("Вызов функции  graphDrawing.ClearPicture;");
            listingString.Append('\n');
            graphDrawing.ClearPicture();
            CalculationOfCoordinates();
        }

        private void правильнаяРаскраскаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vertexList.Count != 0)
            {
                listingString.Append("Срабатывание ивента правильнаяРаскраскаToolStripMenuItem_Click;");
                listingString.Append('\n');
                string resultWriteOnFile;
                string[] resultOn;
                listingString.Append("Вызов функции graphDrawing.CorrectСoloring с параметрами vertexList, Amatrix;");
                listingString.Append('\n');
                listingString.Append("Вызов функции pictureBox1.Invalidate;");
                listingString.Append('\n');
                pictureBox1.Invalidate();
                textBox1.Text = graphDrawing.CorrectСoloring(vertexList, Amatrix, listingString, OrGraph, edgeList).ToString();
                resultWriteOnFile = graphDrawing.WriteOnFile_resultsOfColoring(vertexList);
                resultOn = resultWriteOnFile.Split('|');
                if (resultOn[0] == "0")
                {
                    MessageBox.Show(resultOn[2], resultOn[1], MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Список вершин и рёбер пуст.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void BiuldButton_Click(object sender, EventArgs e)
        {
            listingString.Append("Срабатывание ивента BiuldButton_Click;");
            listingString.Append('\n');
            listingString.Append("Вызов функции buildNewGraph;");
            listingString.Append('\n');
            buildNewGraph();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            listingString.Append("Срабатывание ивента dataGridView1_CellValueChanged;");
            listingString.Append('\n');
            try
            {
                listingString.Append("if (dataGridView1.CurrentCell.Value != null);");
                listingString.Append('\n');
                if (dataGridView1.CurrentCell.Value != null)
                {
                    listingString.Append("True;");
                    listingString.Append('\n');
                    listingString.Append("if (dataGridView1.CurrentCell.Value.ToString().Length <= 1);");
                    listingString.Append('\n');
                    if (dataGridView1.CurrentCell.Value.ToString().Length <= 1)
                    {
                        listingString.Append("True;");
                        listingString.Append('\n');
                        int value = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                        listingString.Append("if (value != 0 && value != 1);");
                        listingString.Append('\n');
                        if (value != 0 && value != 1)
                        {
                            listingString.Append("True;");
                            listingString.Append('\n');
                            listingString.Append("Вывод сообщения об ошибке:Допустимы только значения 1 и 0.;");
                            listingString.Append('\n'); 
                            listingString.Append("dataGridView1.CurrentCell.Value = null;");
                            listingString.Append('\n');
                            dataGridView1.CurrentCell.Value = null;
                            MessageBox.Show("Допустимы только значения 1 и 0.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        listingString.Append("False;");
                        listingString.Append('\n');
                        listingString.Append("Вывод сообщения об ошибке:Допустимы только значения 1 и 0.;");
                        listingString.Append('\n');
                        listingString.Append("dataGridView1.CurrentCell.Value = null;");
                        listingString.Append('\n');
                        dataGridView1.CurrentCell.Value = null;
                        MessageBox.Show("Допустимы только значения 1 и 0.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                listingString.Append("Ошибка NullReferenceException;");
                listingString.Append('\n');
                return;
            }
            catch (FormatException ex)
            {
                listingString.Append("Ошибка FormatException;");
                listingString.Append('\n');
                MessageBox.Show("Требуется ввести число!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.CurrentCell.Value = null;
                return;
            }
        }

        public void buildNewGraph()
        {
            listingString.Append("Работа циклов for (int i = 0; i < dataGridView1.RowCount; i++) и  for (int j = 0; j < dataGridView1.RowCount; j++), проверка dataGridView1 на пусты ячейки;");
            listingString.Append('\n');
            int[,] Matrix = new int[dataGridView1.RowCount, dataGridView1.RowCount];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    listingString.Append("if (dataGridView1.Rows[i].Cells[j].Value == null);");
                    listingString.Append('\n');
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        listingString.Append("True;");
                        listingString.Append('\n');
                        listingString.Append("Вывод ошибки выход и функции;");
                        listingString.Append('\n');
                        MessageBox.Show("Заполните все поля матрицы!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    listingString.Append("False;");
                    listingString.Append('\n');
                    listingString.Append("Matrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);;");
                    listingString.Append('\n');
                    Matrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            listingString.Append("Работа циклов for (int i = 0; i < dataGridView1.RowCount; i++) и  for (int j = 0; j < dataGridView1.RowCount; j++) завершена;");
            listingString.Append('\n');
            listingString.Append("if (dataGridView1.Rows[i].Cells[j].Value == null) ;");
            listingString.Append('\n');
            listingString.Append("True - вывод сообщения об ошибке;");
            listingString.Append('\n');
            listingString.Append("Вызов функции CreateMatrix с параметрами Matrix и dataGridView1.RowCount;");
            listingString.Append('\n');
            CreateMatrix(Matrix, dataGridView1.RowCount);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listingString.Append("Срабатывание ивента открытьToolStripMenuItem_Click;");
            listingString.Append('\n');
            listingString.Append("if (openFileDialog1.ShowDialog()==DialogResult.OK);");
            listingString.Append('\n');
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string file;
                try
                {
                    file = openFileDialog1.FileName;
                    string line;
                    int k = 0;
                    bool HaveThisFile=false;
                    string[] lines;
                    listingString.Append("Создание потока StreamReader и связывание его с файлом:"+ file+ ";");
                    listingString.Append('\n');
                    StreamReader reader = new StreamReader(file);
                    listingString.Append("Работа цикла while ((line = reader.ReadLine()) != null), заполнение матицы Amatrix;");
                    listingString.Append('\n');
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines = line.Split(' ');
                        if (k == 0)
                        {
                            Amatrix = new int[lines.Length, lines.Length];
                            NumberOfVertices = lines.Length;
                        }
                        for (int j = 0; j < lines.Length; j++)
                        {
                            if(lines[j].Length>1)
                            {
                                throw new IOException();
                            }
                            else
                            {
                                Amatrix[k, j] = Convert.ToInt32(lines[j]);
                            }
                        }
                        k++;
                    }
                    if(k!=NumberOfVertices)
                    {
                        throw new IOException();
                    }
                    for(int i=0;i<files.Count;i++)
                    {
                        if(file==files[i])
                        {
                            HaveThisFile = true;
                        }
                    }
                    listingString.Append("Закрытие файлового потока;");
                    listingString.Append('\n');
                    reader.Close();
                    if(!HaveThisFile)
                    {
                        files.Add(file);
                    }
                    lines = file.Split('\\');
                    listingString.Append("Вывод названия файла в fileDataGridView;");
                    listingString.Append('\n');
                    if (!HaveThisFile)
                    {
                        fileDataGridView.Rows.Add(lines[lines.Length - 1]);
                        fileDataGridView.Rows[files.Count - 1].HeaderCell.Value = (files.Count).ToString();
                    }
                    listingString.Append("Вызов функции dataGridView1.Rows.Clear;");
                    listingString.Append('\n');
                    dataGridView1.Rows.Clear();
                    listingString.Append("Вызов функции dataGridView1.Columns.Clear;");
                    listingString.Append('\n');
                    dataGridView1.Columns.Clear();
                    listingString.Append("Вызов функции vertexList.Clear;");
                    listingString.Append('\n');
                    vertexList.Clear();
                    listingString.Append("Вызов функции edgeList.Clear;");
                    listingString.Append('\n');
                    edgeList.Clear();
                    listingString.Append("Вызов функции  graphDrawing.ClearPicture;");
                    listingString.Append('\n');
                    graphDrawing.ClearPicture();
                    CalculationOfCoordinates();
                }
                catch(IOException ex)
                {
                    listingString.Append("Ошибка IOException;");
                    listingString.Append('\n');
                    listingString.Append("Вывод сообщения об ошибке: Некорректный файл.;");
                    listingString.Append('\n');
                    MessageBox.Show("Некорректный файл.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch(FormatException ex)
                {
                    listingString.Append("Ошибка FormatException;");
                    listingString.Append('\n');
                    listingString.Append("Вывод сообщения об ошибке: Некорректный файл.;");
                    listingString.Append('\n');
                    MessageBox.Show("Некорректный файл.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            listingString.Append("Срабатывание ивента Form1_FormClosing;");
            listingString.Append('\n');
            try
            {
                string[] str = listingString.ToString().Split('\n');
                StreamWriter fileWrite = new StreamWriter("KDM_Listing");

                for (int i = 0; i < str.Length; i++)
                {
                    fileWrite.WriteLine(str[i]);
                }
                fileWrite.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Oшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (System.UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Oшибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
