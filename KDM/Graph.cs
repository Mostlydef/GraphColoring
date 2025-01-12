using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace KDM
{
    class Vertex
    {
        public int x,y;
        public bool RightColor=false;

        public SolidBrush solidbrush = null;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Edge
    {
        public int v1, v2;

        public Edge(int vertex1, int vertex2)
        {
            this.v1 = vertex1;
            this.v2 = vertex2;
        }

    }

    class GraphDrawing
    {
        List<List<int>> checkVertex = new List<List<int>>();
        List<int> int_list = new List<int>();
        bool[] boolList = new bool[1000];
        Bitmap bitmap;
        Random ramdom = new Random();
        Pen blackPen;
        Pen redPen;
        Pen orGraphPen;
        Graphics graphics;
        Font font;
        Brush brush;
        Point center;
        PointF point;
        //SolidBrush trnsRedBrush = new SolidBrush(Color.FromArgb(120, 255, 0, 0));
        public int radiousVertexCircle = 13;
        public GraphDrawing(int Width, int Height, StringBuilder listingString)
        {
            listingString.Append("Инициализация Bitmap c параметрами Width=" + Width + " и Height=" + Height);
            listingString.Append("\n");
            bitmap = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(bitmap);
            listingString.Append("Инициализация Graphics c параметрами bitmap=" + bitmap+ ";");
            listingString.Append("\n");
            blackPen = new Pen(Color.Black);
            listingString.Append("Инициализация blackPen c параметрами Color.Black;");
            listingString.Append("\n");
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            listingString.Append("Инициализация redPen c параметрами Color.Red;");
            listingString.Append("\n");
            redPen.Width = 2;
            orGraphPen = new Pen(Color.Black, 2);
            listingString.Append("Инициализация orGraphPen c параметрами Color.Black;");
            listingString.Append("\n");
            orGraphPen.CustomEndCap = new AdjustableArrowCap(6, 10);
            font = new Font("Arial", 11);
            listingString.Append("Инициализация font c параметрами Arial и emSize=11;");
            listingString.Append("\n");
            brush = Brushes.Black;
            listingString.Append("Инициализация Brush c параметрами Brushes.Black;");
            listingString.Append("\n");
            center = new Point((Width - 50) >> 1, (Height - 50) >> 1);
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void ClearPicture()
        {
            graphics.Clear(Color.White);
        }

        public void DrawGraph(List<Vertex> vertex, List<Edge> edge, bool OrGhraph, StringBuilder listingString)
        {
            listingString.Append("if (!OrGhraph);");
            listingString.Append("\n");
            if (!OrGhraph)
            {
                listingString.Append("True;");
                listingString.Append("\n");
                listingString.Append("Работа цикла for (int i = 0; i < edge.Count; i++);");
                listingString.Append("\n");
                for (int i = 0; i < edge.Count; i++)
                {
                    listingString.Append("if (edge[i].v1 == edge[i].v2);");
                    listingString.Append("\n");
                    if (edge[i].v1 == edge[i].v2)
                    {
                        listingString.Append("True;");
                        listingString.Append("\n");
                        listingString.Append("Вызов функции graphics.DrawArc;");
                        listingString.Append("\n");
                        graphics.DrawArc(blackPen, (vertex[edge[i].v1].x - 2 * radiousVertexCircle), (vertex[edge[i].v2].y - 2 * radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle, 90, 270);
                        point = new PointF(vertex[edge[i].v1].x - (int)(2.75 * radiousVertexCircle), vertex[edge[i].v1].y - (int)(2.75 * radiousVertexCircle));
                    }
                    else
                    {
                        listingString.Append("False;");
                        listingString.Append("\n");
                        listingString.Append("Вызов функции graphics.DrawLine;");
                        listingString.Append("\n");
                        graphics.DrawLine(blackPen, vertex[edge[i].v1].x, vertex[edge[i].v1].y, vertex[edge[i].v2].x, vertex[edge[i].v2].y);
                        point = new PointF((vertex[edge[i].v1].x + vertex[edge[i].v2].x) >> 1, (vertex[edge[i].v1].y + vertex[edge[i].v2].y) >> 1);
                    }
                }
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
                listingString.Append("Работа цикла for (int i = 0; i < edge.Count; i++);");
                listingString.Append("\n");
                for (int i = 0; i < edge.Count; i++)
                {
                    if (edge[i].v1 == edge[i].v2)
                    {
                        listingString.Append("if (edge[i].v1 == edge[i].v2);");
                        listingString.Append("\n");
                        listingString.Append("True;");
                        listingString.Append("\n");
                        listingString.Append("Вызов функции graphics.DrawArc;");
                        listingString.Append("\n");
                        graphics.DrawArc(orGraphPen, (vertex[edge[i].v1].x - 2 * radiousVertexCircle), (vertex[edge[i].v2].y - 2 * radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle, 90, 270);
                        point = new PointF(vertex[edge[i].v1].x - (int)(2.75 * radiousVertexCircle), vertex[edge[i].v1].y - (int)(2.75 * radiousVertexCircle));
                    }
                    else
                    {
                        listingString.Append("False;");
                        listingString.Append("\n");
                        listingString.Append("Вызов функции graphics.DrawLine;");
                        listingString.Append("\n");
                        graphics.DrawLine(orGraphPen, vertex[edge[i].v1].x, vertex[edge[i].v1].y, vertex[edge[i].v2].x, vertex[edge[i].v2].y);
                        point = new PointF((vertex[edge[i].v1].x + vertex[edge[i].v2].x) >> 1, (vertex[edge[i].v1].y + vertex[edge[i].v2].y) >> 1);
                    }
                }
                listingString.Append("Работа цикла for (int i = 0; i < edge.Count; i++) завершина;");
                listingString.Append("\n");
            }
            listingString.Append("Работа цикла for (int i = 0; i < vertex.Count; i++);");
            listingString.Append("\n");
            for (int i = 0; i < vertex.Count; i++)
            {
                listingString.Append("Вызов функции DrawVertex;");
                listingString.Append("\n");
                DrawVertex(vertex[i].x, vertex[i].y, (i + 1).ToString(), listingString);
            }
            listingString.Append("Работа цикла for (int i = 0; i < vertex.Count; i++) завершена;");
        }

        public void DrawVertex(int x, int y, string number, StringBuilder listingString)
        {
            listingString.Append("Вызов функции graphics.FillEllipse;");
            listingString.Append("\n");
            graphics.FillEllipse(Brushes.White, (x - radiousVertexCircle), (y - radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle);
            listingString.Append("Вызов функции  graphics.DrawEllipse;");
            listingString.Append("\n");
            graphics.DrawEllipse(blackPen, (x - radiousVertexCircle), (y - radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle);
            listingString.Append("if (number.Length == 3);");
            listingString.Append("\n");
            if (number.Length == 3)
            {
                listingString.Append("True -  point = new PointF(x - 15, y - 9);");
                listingString.Append("\n");
                point = new PointF(x - 15, y - 9);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
            }
            listingString.Append("if (number.Length == 2);");
            listingString.Append("\n");
            if (number.Length == 2)
            {
                listingString.Append("True -  point = new PointF(x - 9, y - 9);");
                listingString.Append("\n");
                point = new PointF(x - 9, y - 9);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
            }
            listingString.Append("if (number.Length == 1);");
            listingString.Append("\n");
            if (number.Length == 1)
            {
                listingString.Append("True -   point = new PointF(x - 7, y - 9);");
                listingString.Append("\n");
                point = new PointF(x - 7, y - 9);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
            }
            listingString.Append("Вызов функции  graphics.DrawString;");
            listingString.Append("\n");
            graphics.DrawString(number, font, brush, point);
        }
        //Перегрузка метода нарисовать вершину с нужным цветом
        public void DrawVertex(int x, int y, string number, Brush brushVertex, StringBuilder listingString)
        {
            listingString.Append("Вызов функции graphics.FillEllipse;");
            listingString.Append("\n");
            graphics.FillEllipse(brushVertex, (x - radiousVertexCircle), (y - radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle);
            listingString.Append("Вызов функции  graphics.DrawEllipse;");
            listingString.Append("\n");
            graphics.DrawEllipse(blackPen, (x - radiousVertexCircle), (y - radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle);
            listingString.Append("if (number.Length == 3);");
            listingString.Append("\n");
            if (number.Length == 3)
            {
                listingString.Append("True -  point = new PointF(x - 15, y - 9);");
                listingString.Append("\n");
                point = new PointF(x - 15, y - 9);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
            }
            listingString.Append("if (number.Length == 2);");
            listingString.Append("\n");
            if (number.Length == 2)
            {
                listingString.Append("True -  point = new PointF(x - 9, y - 9);");
                listingString.Append("\n");
                point = new PointF(x - 9, y - 9);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
            }
            listingString.Append("if (number.Length == 1);");
            listingString.Append("\n");
            if (number.Length == 1)
            {
                listingString.Append("True -   point = new PointF(x - 7, y - 9);");
                listingString.Append("\n");
                point = new PointF(x - 7, y - 9);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
            }
            listingString.Append("Вызов функции  graphics.DrawString;");
            listingString.Append("\n");
            graphics.DrawString(number, font, brush, point);
        }

        public void DrawEdge(Vertex vertex1, Vertex vertex2, Edge edge, int numberEdge, StringBuilder listingString)
        {
            listingString.Append("if (edge.v1 == edge.v2);");
            listingString.Append("\n");
            if (edge.v1 == edge.v2)
            {
                listingString.Append("True;");
                listingString.Append("\n");
                listingString.Append("Вызов функции graphics.DrawArc;");
                listingString.Append("\n");
                graphics.DrawArc(blackPen, (vertex1.x - 2 * radiousVertexCircle), (vertex1.y - 2 * radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle, 60, 270);
                point = new PointF(vertex1.x - (int)(2.75 * radiousVertexCircle), vertex1.y - (int)(2.75 * radiousVertexCircle));
                listingString.Append("Вызов функции DrawVertex;");
                listingString.Append("\n");
                DrawVertex(vertex1.x, vertex1.y, (edge.v1 + 1).ToString(), listingString);
            }
            else
            {
                listingString.Append("False;");
                listingString.Append("\n");
                listingString.Append("Вызов функции graphics.DrawLine;");
                listingString.Append("\n");
                graphics.DrawLine(blackPen, vertex1.x, vertex1.y, vertex2.x, vertex2.y);
                point = new PointF(vertex1.x + (int)(2.75 * radiousVertexCircle), vertex1.y + (int)(2.75 * radiousVertexCircle));
                listingString.Append("Вызов функции DrawVertex;");
                listingString.Append("\n");
                DrawVertex(vertex1.x, vertex1.y, (edge.v1 + 1).ToString(),listingString);
                DrawVertex(vertex2.x, vertex2.y, (edge.v2 + 1).ToString(),listingString);
            }
            listingString.Append("False;");
            listingString.Append("\n");
        }

        public void DrawSelectVertex(int x, int y)
        {
            graphics.DrawEllipse(redPen, (x - radiousVertexCircle), (y - radiousVertexCircle), 2 * radiousVertexCircle, 2 * radiousVertexCircle);
        }

        //-------------Функция правильной раскраски-----------------

        public int CorrectСoloring(List<Vertex> vertex, int[,] matrix, StringBuilder listingString, bool OrGraph, List<Edge> edge)
        {
            CompGrap(vertex, matrix);
            listingString.Append("\n");
            listingString.Append("----------------------------------------------------------------");
            listingString.Append("\n");
            listingString.Append("          Правильная раскраска            ");
            listingString.Append("\n");
            listingString.Append("\n");
            listingString.Append("Объявление списка solidBrushList с цветами для закрашивания вершин;");
            listingString.Append("\n");
            List<SolidBrush> solidBrushList = new List<SolidBrush>();
            solidBrushList.Add(new SolidBrush(Color.FromArgb(ramdom.Next(1, 255), ramdom.Next(1, 255), ramdom.Next(1, 233))));
            vertex[0].solidbrush = solidBrushList[0];
            listingString.Append("Создание цвета и установка его для вершины 1. Вершина 1 имеет цвет:" + vertex[0].solidbrush.Color.ToString() +";");
            listingString.Append("\n");
            listingString.Append("Вызов функции DrawVertex для закрашивания вершины 1;");
            listingString.Append("\n");
            listingString.Append("\n");
            listingString.Append("++++++++++++++++++++++++++++++++++++++++");
            listingString.Append("\n");
            DrawVertex(vertex[0].x, vertex[0].y, 1.ToString(), vertex[0].solidbrush, listingString);
            listingString.Append("++++++++++++++++++++++++++++++++++++++++");
            listingString.Append("\n");
            listingString.Append("\n");
            bool newColor=true;
            listingString.Append("bool newColor=true - переменная для определения цвета последующих вершин;");
            listingString.Append("\n");
            solidBrushList.Add(new SolidBrush(Color.FromArgb(ramdom.Next(1, 255), ramdom.Next(1, 255), ramdom.Next(1, 233))));
            listingString.Append("Добавляем в список solidBrushList новый цвет для вершин, смежных с вершиной 1; Новый цвет:"+ solidBrushList[1].Color.ToString());
            listingString.Append("\n");
            listingString.Append("if (OrGraph) - если граф ориентированный;");
            listingString.Append("\n");
            if (OrGraph)
            {
                listingString.Append("True - происходит построение матрицы смежности для основания орграфа;");
                listingString.Append("\n");
                for (int i=0;i<vertex.Count;i++)
                {
                    for(int j=0;j<vertex.Count;j++)
                    {
                        if(matrix[i,j]==1 && matrix[j,i]!=1)
                        {
                            matrix[j, i] = 1;
                        }
                        if (matrix[j, i] == 1 && matrix[i,j]!=1)
                        {
                            matrix[i, j] = 1;
                        }
                    }
                }
            }
            else
            {
                listingString.Append("False - не ориентированный;");
                listingString.Append("\n");
            }

            listingString.Append("Проверка смежности остальных вершин с 1-ой;");
            listingString.Append("\n");

            listingString.Append("Работа циклов  for (int i=1;i<vertex.Count;i++), for(int j=i;j<vertex.Count;j++);");
            listingString.Append("\n");

            for (int j = 0 + 1; j < vertex.Count; j++)
            {
                listingString.Append("if (matrix[0, j] == 1) - если вершины смежны;");
                listingString.Append("\n");
                if (matrix[0, j] == 1)
                {
                    listingString.Append("True;");
                    listingString.Append("\n");
                    vertex[j].solidbrush = solidBrushList[1];
                    listingString.Append("Установка для смежной вершины " + (j+1).ToString() + " . Вершина " + (j+1).ToString() + " имеет цвет:" + vertex[j].solidbrush.Color.ToString() + ";");
                    listingString.Append("\n");
                    listingString.Append("Вызов функции DrawVertex для закрашивания вершины "+ (j + 1).ToString());
                    listingString.Append("\n");
                    listingString.Append("\n");
                    listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                    listingString.Append("\n");
                    DrawVertex(vertex[j].x, vertex[j].y, (j + 1).ToString(), vertex[j].solidbrush, listingString);
                    listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                    listingString.Append("\n");
                    listingString.Append("\n");
                }
                else
                {
                    listingString.Append("False - вершина не смежна;");
                    listingString.Append("\n");
                    listingString.Append("Присваиваем цвет вершины 1 вершине " + (j + 1).ToString() + ". Вершина " + (j + 1).ToString() + " имеет цвет:" + vertex[0].solidbrush.Color.ToString() + ";");
                    listingString.Append("\n");
                    vertex[j].solidbrush = vertex[0].solidbrush;
                    listingString.Append("Вызов функции DrawVertex для закрашивания вершины " + (j + 1).ToString());
                    listingString.Append("\n");
                    listingString.Append("\n");
                    listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                    listingString.Append("\n");
                    DrawVertex(vertex[j].x, vertex[j].y, (j + 1).ToString(), vertex[0].solidbrush, listingString);
                    listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                    listingString.Append("\n");
                    listingString.Append("\n");
                }
            }
            int l = 0;
            if(edge.Count==0)
            {
                solidBrushList.RemoveAt(1);
                return solidBrushList.Count;
            }
            if (checkVertex.Count != 1)
            {
                return colorDisgraph(vertex, solidBrushList, matrix, listingString);
            }

            listingString.Append("Работа циклов  for (int i=1;i<vertex.Count;i++), for(int j=i;j<vertex.Count;j++) завершена;");
            listingString.Append("\n");
            listingString.Append("Работа циклов  for (int i=1;i<vertex.Count;i++), for(int j=i;j<vertex.Count;j++);");
            listingString.Append("\n");
            listingString.Append("Проход по всем вершинам, кроме 1-ой;");
            listingString.Append("\n");
            for (int i=1;i<vertex.Count;i++)
            {
                listingString.Append("i=" + i);
                listingString.Append("\n");
                listingString.Append("--Вершина " + (i+1).ToString());
                listingString.Append("\n");
                listingString.Append("Проверка смежности с следующими вершинами:");
                listingString.Append("\n");
                for (int j=i+1;j<vertex.Count;j++)
                {
                    listingString.Append("j=" + j);
                    listingString.Append("\n");
                    listingString.Append("->Вершина " + (j + 1).ToString());
                    listingString.Append("\n");
                    listingString.Append("if (matrix[0, j] == 1) - если вершины смежны;");
                    listingString.Append("\n");
                    if (matrix[i,j]==1)
                    {
                        listingString.Append("True;");
                        listingString.Append("\n");
                        listingString.Append("if (vertex[j].solidbrush == vertex[i].solidbrush) - если у них одинаковые цвета;");
                        listingString.Append("\n");
                        if (vertex[j].solidbrush == vertex[i].solidbrush)
                        {
                            listingString.Append("True;");
                            listingString.Append("\n");
                            listingString.Append("if (newColor) - требуется определить новый цвет для вершин, смежных с вершиной "+ (i + 1).ToString());
                            listingString.Append("\n");
                            if (newColor)
                            {
                                listingString.Append("True;");
                                listingString.Append("\n");
                                listingString.Append("Вызов функции CheckColor - создание нового цвета;");
                                listingString.Append("\n");
                                listingString.Append("\n");
                                listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                                listingString.Append("\n");
                                solidBrushList.Add(CheckColor(solidBrushList, vertex, listingString));
                                listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                                listingString.Append("\n");
                                listingString.Append("\n");
                                listingString.Append("newColor = false - цвет определён;");
                                listingString.Append("\n");
                                newColor = false;
                            }
                            else
                            {
                                listingString.Append("False - новый цвет не требуется;");
                                listingString.Append("\n");
                            }
                            listingString.Append("Вершина " + (j + 1).ToString() + " имеет цвет:" + solidBrushList[solidBrushList.Count - 1].Color.ToString() + ";");
                            listingString.Append("\n");
                            vertex[j].solidbrush = solidBrushList[solidBrushList.Count - 1];
                            listingString.Append("Вызов функции DrawVertex для закрашивания вершины " + (j + 1).ToString());
                            listingString.Append("\n");
                            listingString.Append("\n");
                            listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                            listingString.Append("\n");
                            DrawVertex(vertex[j].x, vertex[j].y, (j + 1).ToString(), vertex[j].solidbrush, listingString);
                            listingString.Append("++++++++++++++++++++++++++++++++++++++++");
                            listingString.Append("\n");
                            listingString.Append("\n");
                        }
                        else
                        {
                            listingString.Append("False  - цвета разные;");
                            listingString.Append("\n");
                        }
                    }
                    else
                    {
                        listingString.Append("False  - вершины не смежные;");
                        listingString.Append("\n");
                    }
                }
                listingString.Append("Работа циклa for(int j=i;j<vertex.Count;j++) завершена - цвета для смежных вершин вершине "+ (i+1).ToString()+ " определены.");
                listingString.Append(" newColor = true - требуется новый цвет;");
                listingString.Append("\n");
                newColor = true;
            }
            listingString.Append("\n");
            listingString.Append("      Алгоритм правильной раскраски завершил свою работу       ");
            listingString.Append("\n");
            listingString.Append("---------------------------------------------------------------------------");
            listingString.Append("\n");
            return solidBrushList.Count;
        }

        //----------------Функция создания нового цвета-----------------

        public SolidBrush CheckColor(List<SolidBrush> solidBrushList, List<Vertex> vertex, StringBuilder listingString)  
        {
            listingString.Append("Начало работы функции CheckColor;");
            SolidBrush newColor = new SolidBrush(Color.FromArgb(ramdom.Next(1, 255), ramdom.Next(1, 255), ramdom.Next(1, 233)));
            listingString.Append("\n");
            listingString.Append("Создание переменной SolidBrush newColor и присваивание ей цвета " + newColor.Color.ToString());
            listingString.Append("Работа циклa  for (int i=0; i<vertex.Count;i++) - проверка на существование такого цвета в списке solidBrushList;");
            listingString.Append("\n");
            for (int i=0; i<vertex.Count;i++)
            {
                listingString.Append(" if (vertex[i].solidbrush == newColor) - если такой цвет уже есть в списке solidBrushList;");
                listingString.Append("\n");
                if (vertex[i].solidbrush == newColor)
                {
                    listingString.Append("True;");
                    listingString.Append("\n");
                    newColor = new SolidBrush(Color.FromArgb(ramdom.Next(1, 255), ramdom.Next(1, 255), ramdom.Next(1, 233)));
                    listingString.Append("Создание нового цвета и присваивание его переменной newColor = " + newColor.Color.ToString() +";");
                    listingString.Append("\n");
                    listingString.Append("i = 0;");
                    listingString.Append("\n");
                    i = 0;
                }
                else
                {
                    listingString.Append("False;");
                    listingString.Append("\n");
                }
                listingString.Append("i="+i);
                listingString.Append("\n");
            }
            listingString.Append("Работа циклa  for (int i=0; i<vertex.Count;i++) завершена;");
            listingString.Append("\n");
            listingString.Append("return newColor - возвращаем новый, уникальный цвет;");
            listingString.Append("\n");
            return newColor;
        }

        public void CompGrap(List<Vertex> vertex, int[,] matrix)
        {
            checkVertex.Clear();
            for(int i=0;i<vertex.Count;i++)
            {
                boolList[i] = false;
            }
            for(int i=0;i<vertex.Count; i++)
            {
                if(!boolList[i])
                {
                    int_list = new List<int>();
                    Comp(i, vertex, matrix);
                    checkVertex.Add(int_list);
                }
            }
        }

        public void Comp(int v_i, List<Vertex> vertex, int[,] matrix)
        {
            boolList[v_i] = true;
            int_list.Add(v_i + 1);
            for(int i=0;i<vertex.Count;i++)
            {
                int v = matrix[v_i, i];
                if(v==1)
                {
                    if(!boolList[i])
                    {
                        Comp(i, vertex, matrix);
                    }
                }
            }
        }


        public int colorDisgraph(List<Vertex> vertex, List<SolidBrush> solidBrushList, int[,] matrix, StringBuilder listingString)
        {
            List<SolidBrush> useColor = new List<SolidBrush>();
            int maxColorCount=checkVertex[0].Count;
            int j_sm=0;
            for(int  i =1;i<checkVertex.Count;i++)
            {
                if(checkVertex[i].Count>checkVertex[i-1].Count)
                {
                    maxColorCount = checkVertex[i].Count;
                }
            }

            if (maxColorCount > solidBrushList.Count)
            {
                for (int i = 0; i < maxColorCount - solidBrushList.Count; i++)
                {
                    solidBrushList.Add(CheckColor(solidBrushList, vertex, listingString));
                }
            }
            foreach(var copmVertex in checkVertex)
            {
                for (int i = copmVertex[0] - 1; i < copmVertex[copmVertex.Count-1];i++)
                {
                    useColor.Add(vertex[i].solidbrush);
                    for(int j=i+1;j< vertex.Count;j++)
                    {
                        if (matrix[i,j]==1)
                        {
                            if(vertex[i].solidbrush==vertex[j].solidbrush)
                            {
                                vertex[j].solidbrush = FindColor(solidBrushList, useColor);
                                DrawVertex(vertex[j].x,vertex[j].y,(j+1).ToString(),vertex[j].solidbrush,listingString);
                                if (matrix[j, j_sm] == 1)
                                {
                                    useColor.Add(vertex[j].solidbrush);
                                }
                                j_sm = j;
                            }
                        }
                    }
                }
                useColor.Clear();
            }
            return solidBrushList.Count;
        }

        public SolidBrush FindColor(List<SolidBrush> solidbrush, List<SolidBrush> useColor)
        {
            List<SolidBrush> notUseColor = new List<SolidBrush>();
            for(int i=0;i<solidbrush.Count;i++)
            {
                notUseColor.Add(solidbrush[i]);
            }
            for(int i =0;i<useColor.Count;i++)
            {
                for (int j = 0; j < notUseColor.Count; j++)
                {
                    if (notUseColor[j] == useColor[i])
                    {
                        notUseColor.RemoveAt(j);
                    }
                }
            }
            if(notUseColor.Count!=0)
            {
                return notUseColor[0];
            }
            return new SolidBrush(Color.FromArgb(ramdom.Next(1, 255), ramdom.Next(1, 255), ramdom.Next(1, 233)));
        }


        public string WriteOnFile_resultsOfColoring(List<Vertex> vertex)
        {
            try
            {
                StreamWriter fileWrite = new StreamWriter("KDM_course_results");
                fileWrite.WriteLine("Результаты алгоритма правильной раскраски.\n");
                for (int i = 0; i < vertex.Count; i++)
                {
                    fileWrite.WriteLine("Вершина " + (i + 1).ToString() + " имеет цвет: " + vertex[i].solidbrush.Color.ToString()+"\n");
                }
                fileWrite.Close();
                return "1";
            }
            catch(IOException e)
            {
                return "0|"+"Ошибка|"+ e.StackTrace.ToString();
            }
            catch(System.UnauthorizedAccessException e)
            {
                return "0|" + "Ошибка!|"+ e.Message.ToString() + "        Проверьте доступна ли данная папка к записи.";
            }
        }



    }

}
