using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace BranchAndBoundMethod
{
    public partial class Form1 : Form
    {
        #region Ctor
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Var
        private double[,] M = new double[3,3];
        private string[,] title = new string[2,4];
        private Point point = new Point();
        #endregion


        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            ResBox.Text = "";
            if ((X1Box.Text == "") || (X2Box.Text == "") || (X1C1Box.Text == "") || (X1C2Box.Text == "") || (X2C1Box.Text == "") || (X2C2Box.Text == "") || (C1Box.Text == "") || (C2Box.Text == ""))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                M[2, 1] = -1*Convert.ToDouble(X1Box.Text);
                M[2, 2] = -1*Convert.ToDouble(X2Box.Text);
                M[0, 1] = -1*Convert.ToDouble(X1C1Box.Text);
                M[1, 1] = -1*Convert.ToDouble(X1C2Box.Text);
                M[0, 2] = -1*Convert.ToDouble(X2C1Box.Text);
                M[1, 2] = -1*Convert.ToDouble(X2C2Box.Text);
                M[0, 0] = -1*Convert.ToDouble(C1Box.Text);
                M[1, 0] = -1*Convert.ToDouble(C2Box.Text);
                M[2, 0] = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Некоторые коэффициенты не цифры!", "Ошибка!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            #region init
            title[0, 0] = " БП ";
            title[0, 1] = " СП ";
            title[0, 2] = " X1 ";
            title[0, 3] = " X2 ";
            title[1, 0] = title[0, 0];
            title[1, 1] = " X3 ";
            title[1, 2] = " X4 ";
            title[1, 3] = "  F  ";
            #endregion

            ResBox.Text += "Домножим оба неравества на -1. Введем дополнительные переменные X3, X4\r\n";
            ResBox.Text += M[0, 1] + "X1" + " + (" + M[0, 2] + ")X2 + X3 = " + M[0, 0] + "\r\n";
            ResBox.Text += M[1, 1] + "X1" + " + (" + M[1, 2] + ")X2 + X4 = " + M[1, 0] + "\r\n";

            ResBox.Text += "Составим симплекс-таблицу:\r\n";

            buildTable(M, title);

            try
            {
                while (!calcBase())
                {
                    M = getSymTable(M);
                }
            }
            catch (EntryPointNotFoundException)
            {
                return;
            }

            ResBox.Text += "Считаем, что допустимое базисное решение найдено. Просматриваем коэффициенты строки функции цели F(x)\r\n";

            bool PosF = false;

            while (!PosF)
            {
                PosF = true;
                for (int i = 1; i < 3; i++)
                {
                    if (M[2, i] < 0)
                    {
                        PosF = false;
                    }
                }

                if (!PosF)
                {
                    try
                    {
                        calcFBase();
                        M = getSymTable(M);
                    }
                    catch (EntryPointNotFoundException)
                    {
                        return;
                    }
                }
            }

            ResBox.Text += "\r\nРешение ветви:\r\n" + title[0, 2] + " = " + title[0, 3] + " = 0\r\n";
            ResBox.Text += title[1, 1] + " = " + M[0, 0] + "\r\n";
            ResBox.Text += title[1, 2] + " = " + M[1, 0] + "\r\n";
            ResBox.Text += title[1, 3] + " = " + M[2, 0] + "\r\n";

            return;
        }

        private double[,] getSymTable(double[,] m)
        {
            ResBox.Text += "Базисный элемент: M[" + (point.X + 1) + "," + (point.Y + 1) + "] = " +
                               M[point.X, point.Y] + "\r\n\r\n";
            ResBox.Text += "Пересчёт таблицы\r\n";
            double[,] oldM = new double[3, 3];
            oldM = m;
            m = new double[3, 3];

            m[point.X, point.Y] = Math.Round(1/oldM[point.X, point.Y], 3);     //пересчёт ведущего элемента

            for (int i = 0; i < 3; i++)     //пересчёт ведущего столба
            {
                if (i == point.X)
                {
                    continue;
                }

                m[i, point.Y] = Math.Round(-oldM[i, point.Y] / oldM[point.X, point.Y], 3);
            }

            for (int i = 0; i < 3; i++)     //пересчёт ведущей строки
            {
                if (i == point.Y)
                {
                    continue;
                }

                m[point.X, i] = Math.Round(oldM[point.X, i]/oldM[point.X, point.Y], 3);
            }

            for (int i = 0; i < 2; i++)     //пересчёт остальных элементов
            {
                if (i == point.X)
                {
                    continue;
                }
                for (int j = 1; j < 3; j++)
                {
                    if (j == point.Y)
                    {
                        continue;
                    }

                    m[i, j] = Math.Round(oldM[i, j] - oldM[point.X, j] * oldM[i, point.Y] / oldM[point.X, point.Y], 3);
                }
            }

            m[point.X, 0] = Math.Round(oldM[point.X, 0] / oldM[point.X, point.Y], 3);    //пересчёт БП в ведущей строке

            for (int i = 0; i < 2; i++)     //пересчёт остальных БП
            {
                if (i == point.X)
                {
                    continue;
                }
                m[i, 0] = Math.Round(oldM[i, 0] - oldM[point.X, 0] * oldM[i, point.Y] / oldM[point.X, point.Y], 3);
            }

            m[2, point.Y] = Math.Round(-oldM[2, point.Y] / oldM[point.X, point.Y], 3);    //пересчёт ведущего С

            for (int i = 1; i < 3; i++)     //пересчёт остальных С
            {
                if (i == point.Y)
                {
                    continue;
                }
                m[2, i] = Math.Round(oldM[2, i] - oldM[2, point.Y] * oldM[point.X, i] / oldM[point.X, point.Y], 3);
            }

            m[2, 0] = Math.Round(oldM[2, 0] - oldM[point.X, 0] * oldM[2, point.Y] / oldM[point.X, point.Y], 3);    //пересчёт F

            string s = title[0, point.Y + 1];
            title[0, point.Y + 1] = title[1, point.X + 1];
            title[1, point.X + 1] = s;

            buildTable(m, title);

            return m;
        }

        private bool calcBase()
        {
            bool isPos = true;
            double temp = 0;

            for (int i = 0; i < 3; i++)
            {
                if (M[i, 0] < 0)
                {
                    isPos = false;

                    if (temp > M[i, 0])
                    {
                        temp = M[i, 0];
                        point.X = i;
                    }
                }
            }

            if (!isPos)
            {
                temp = 0;
                for (int i = 1; i < 3; i++)
                {
                    if (temp > M[point.X, i])
                    {
                        temp = M[point.X, i];
                        point.Y = i;
                    }
                } 

                if (temp == 0)
                {
                    ResBox.Text += "Cистема ограничений несовместна и задача не имеет решения!\r\n";
                    throw new EntryPointNotFoundException();
                }
            }

            return isPos;
        }

        private void buildTable(double[,] m, string[,] t)
        {
            ResBox.Text += "\r\n" + t[0, 0] + t[0, 1] + t[0, 2] + t[0, 3] + "\r\n";
            for (int i = 0; i < 3; i++)
            {
                ResBox.Text += t[1, i + 1];
                for (int j = 0; j < 3; j++)
                {
                    ResBox.Text += "   " + m[i, j];
                }
                ResBox.Text += "\r\n";
            }
            ResBox.Text += "\r\n";

        }

        private void calcFBase()
        {
            double temp = 0;

            for (int i = 1; i < 3; i++)
            {
                if (temp > M[2, i])
                {
                    temp = M[2, i];
                    point.Y = i;
                }
            }

            if ((M[0, point.Y] < 0) && (M[1, point.Y] < 0))
            {
                ResBox.Text += "\r\nВ процессе оптимизации решения в ведущем столбце все элементы оказались неположительные. функция в области допустимых решений задачи не ограничена сверху!\r\n";
                throw new EntryPointNotFoundException();
            }
            else
            {
                if ((M[0, point.Y] == 0) && (M[1, point.Y] > 0))
                {
                    point.X = 1;
                    return;
                }
                else if ((M[0, point.Y] > 0) && (M[1, point.Y] == 0))
                {
                    point.X = 0;
                    return;
                }

                temp = M[0, 0] / M[0, point.Y];
                point.X = 0;

                if (temp < M[1, 0] / M[1, point.Y])
                {
                    point.X = 1;
                }
            }
        }
    }
}
