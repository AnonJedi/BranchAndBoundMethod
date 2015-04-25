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
        private List<List<double>> clm = new List<List<double>>();
        private List<double> str = new List<double>(); 
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
                str.Add(-1*Convert.ToDouble(C1Box.Text));
                str.Add(-1*Convert.ToDouble(C2Box.Text));
                str.Add(0);
                clm.Add(str);

                str = new List<double>();

                str.Add(-1*Convert.ToDouble(X1C1Box.Text));
                str.Add(-1*Convert.ToDouble(X1C2Box.Text));
                str.Add(-1*Convert.ToDouble(X1Box.Text));
                clm.Add(str);
                
                str = new List<double>();

                str.Add(-1*Convert.ToDouble(X2C1Box.Text));
                str.Add(-1*Convert.ToDouble(X2C2Box.Text));
                str.Add(-1*Convert.ToDouble(X2Box.Text));
                clm.Add(str);

                str = new List<double>();
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
            ResBox.Text += clm[1][0] + "X1" + " + (" + clm[2][0] + ")X2 + X3 = " + clm[0][0] + "\r\n";
            ResBox.Text += clm[1][1] + "X1" + " + (" + clm[2][1] + ")X2 + X4 = " + clm[0][1] + "\r\n";

            ResBox.Text += "Составим симплекс-таблицу:\r\n";

            buildTable(clm, title);

            try
            {
                while (!calcBase())
                {
                    clm = getSymTable(clm);
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
                for (int i = 1; i < clm.Count; i++)
                {
                    if (clm[i][clm[0].Count - 1] < 0)
                    {
                        PosF = false;
                    }
                }

                if (!PosF)
                {
                    try
                    {
                        calcFBase();
                        clm = getSymTable(clm);
                    }
                    catch (EntryPointNotFoundException)
                    {
                        return;
                    }
                }
            }

            ResBox.Text += "\r\nРешение ветви:\r\n" + title[0, 2] + " = " + title[0, 3] + " = 0\r\n";
            ResBox.Text += title[1, 1] + " = " + clm[0][0] + "\r\n";
            ResBox.Text += title[1, 2] + " = " + clm[0][1] + "\r\n";
            ResBox.Text += title[1, 3] + " = " + clm[0][2] + "\r\n";

            return;
        }

        private List<List<double>> getSymTable(List<List<double>> l)
        {
            ResBox.Text += "Базисный элемент: M[" + (point.X + 1) + "," + (point.Y + 1) + "] = " +
                               l[point.Y][point.X] + "\r\n\r\n";
            ResBox.Text += "Пересчёт таблицы\r\n";
            List<List<double>> oldL = new List<List<double>>();
            oldL = l;
            l = new List<List<double>>();

            for (int i = 0; i < oldL.Count; i++)
            {
                l.Add(new List<double>());
                for (int j = 0; j < oldL[0].Count; j++)
                {
                    l[i].Add(0);
                }
            }

            l[point.Y][point.X] = Math.Round(1 / oldL[point.Y][point.X], 3);     //пересчёт ведущего элемента

            for (int i = 0; i < l[point.Y].Count; i++)     //пересчёт ведущего столба
            {
                if (i == point.X)
                {
                    continue;
                }

                l[point.Y][i] = Math.Round(-oldL[point.Y][i] / oldL[point.Y][point.X], 3);
            }

            for (int i = 0; i < l.Count; i++)     //пересчёт ведущей строки
            {
                if (i == point.Y)
                {
                    continue;
                }

                l[i][point.X] = Math.Round(oldL[i][point.X] / oldL[point.Y][point.X], 3);
            }

            for (int i = 0; i < l[point.Y].Count-1; i++)     //пересчёт остальных элементов
            {
                if (i == point.X)
                {
                    continue;
                }
                for (int j = 1; j < l.Count; j++)
                {
                    if (j == point.Y)
                    {
                        continue;
                    }

                    l[j][i] = Math.Round(oldL[j][i] - oldL[j][point.X] * oldL[point.Y][i] / oldL[point.Y][point.X], 3);
                }
            }

            l[0][point.X] = Math.Round(oldL[0][point.X] / oldL[point.Y][point.X], 3);    //пересчёт БП в ведущей строке

            for (int i = 0; i < l[point.Y].Count-1; i++)     //пересчёт остальных БП
            {
                if (i == point.X)
                {
                    continue;
                }
                l[0][i] = Math.Round(oldL[0][i] - oldL[0][point.X] * oldL[point.Y][i] / oldL[point.Y][point.X], 3);
            }

            l[point.Y][l[0].Count - 1] = Math.Round(-oldL[point.Y][l[0].Count - 1] / oldL[point.Y][point.X], 3);    //пересчёт ведущего С

            for (int i = 1; i < l.Count; i++)     //пересчёт остальных С
            {
                if (i == point.Y)
                {
                    continue;
                }
                l[i][l[0].Count - 1] = Math.Round(oldL[i][l[0].Count - 1] - oldL[point.Y][l[0].Count - 1] * oldL[i][point.X] / oldL[point.Y][point.X], 3);
            }

            l[0][l[0].Count - 1] = Math.Round(oldL[0][l[0].Count - 1] - oldL[0][point.X] * oldL[point.Y][l[0].Count - 1] / oldL[point.Y][point.X], 3);    //пересчёт F

            string s = title[0, point.Y + 1];
            title[0, point.Y + 1] = title[1, point.X + 1];
            title[1, point.X + 1] = s;

            buildTable(l, title);

            return l;
        }

        private bool calcBase()
        {
            bool isPos = true;
            double temp = 0;

            for (int i = 0; i < 3; i++)
            {
                if (clm[0][i] < 0)
                {
                    isPos = false;

                    if (temp > clm[0][i])
                    {
                        temp = clm[0][i];
                        point.X = i;
                    }
                }
            }

            if (!isPos)
            {
                temp = 0;
                for (int i = 1; i < 3; i++)
                {
                    if (temp > clm[i][point.X])
                    {
                        temp = clm[i][point.X];
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

        private void buildTable(List<List<double>> l, string[,] t)
        {
            ResBox.Text += "\r\n" + t[0, 0] + t[0, 1] + t[0, 2] + t[0, 3] + "\r\n";
            for (int i = 0; i < l[0].Count; i++)
            {
                ResBox.Text += t[1, i + 1];
                for (int j = 0; j < l.Count; j++)
                {
                    ResBox.Text += "   " + l[j][i];
                }
                ResBox.Text += "\r\n";
            }
            ResBox.Text += "\r\n";

        }

        private void calcFBase()
        {
            double temp = 0;

            for (int i = 1; i < clm.Count; i++)
            {
                if (temp > clm[i][clm[0].Count - 1])
                {
                    temp = clm[i][clm[0].Count - 1];
                    point.Y = i;
                }
            }

            int n = 0;

            foreach (double d in clm[point.Y])
            {
                if (d < 0)
                {
                    n++;
                }
            }
            if (n == clm[0].Count)
            {
                ResBox.Text += "\r\nВ процессе оптимизации решения в ведущем столбце все элементы оказались неположительные. функция в области допустимых решений задачи не ограничена сверху!\r\n";
                throw new EntryPointNotFoundException();
            }
            else
            {
                List<double> tempL = new List<double>();

                for (int i = 0; i < clm[point.Y].Count - 1; i++)
                {
                    if (clm[point.Y][i] < 0)
                    {
                        tempL.Add(0);
                        continue;
                    }

                    tempL.Add(clm[0][i]/clm[point.Y][i]);
                }

                temp = Double.PositiveInfinity;
                for (int i = 0; i < tempL.Count; i++)
                {
                    if ((temp > tempL[i]) && (tempL[i] != 0))
                    {
                        temp = tempL[i];
                        point.X = i;
                    }
                }
            }
        }
    }
}
