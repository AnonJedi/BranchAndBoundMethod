using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace BranchAndBoundMethod
{
    static class SymplexSolver
    {
        public static void getSymTable(ref List<List<double>> l, Point point, ref List<List<string>> title, TextBox ResBox)
        {
            ResBox.Text += "Базисный элемент: M[" + (point.X + 1) + "," + (point.Y + 1) + "] = " +
                               l[point.Y][point.X] + "\r\n\r\n";
            ResBox.Text += "Пересчёт таблицы\r\n";
            List<List<double>> oldMtrx = new List<List<double>>();
            oldMtrx = l;
            l = new List<List<double>>();

            for (int i = 0; i < oldMtrx.Count; i++)
            {
                l.Add(new List<double>());
                for (int j = 0; j < oldMtrx[0].Count; j++)
                {
                    l[i].Add(0);
                }
            }

            l[point.Y][point.X] = Math.Round(1 / oldMtrx[point.Y][point.X], 3);     //пересчёт ведущего элемента

            for (int i = 0; i < l[point.Y].Count; i++)     //пересчёт ведущего столба
            {
                if (i == point.X)
                {
                    continue;
                }

                l[point.Y][i] = Math.Round(-oldMtrx[point.Y][i] / oldMtrx[point.Y][point.X], 3);
            }

            for (int i = 0; i < l.Count; i++)     //пересчёт ведущей строки
            {
                if (i == point.Y)
                {
                    continue;
                }

                l[i][point.X] = Math.Round(oldMtrx[i][point.X] / oldMtrx[point.Y][point.X], 3);
            }

            for (int i = 0; i < l[point.Y].Count - 1; i++)     //пересчёт остальных элементов
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

                    l[j][i] = Math.Round(oldMtrx[j][i] - oldMtrx[j][point.X] * oldMtrx[point.Y][i] / oldMtrx[point.Y][point.X], 3);
                }
            }

            l[0][point.X] = Math.Round(oldMtrx[0][point.X] / oldMtrx[point.Y][point.X], 3);    //пересчёт БП в ведущей строке

            for (int i = 0; i < l[point.Y].Count - 1; i++)     //пересчёт остальных БП
            {
                if (i == point.X)
                {
                    continue;
                }
                l[0][i] = Math.Round(oldMtrx[0][i] - oldMtrx[0][point.X] * oldMtrx[point.Y][i] / oldMtrx[point.Y][point.X], 3);
            }

            l[point.Y][l[0].Count - 1] = Math.Round(-oldMtrx[point.Y][l[0].Count - 1] / oldMtrx[point.Y][point.X], 3);    //пересчёт ведущего С

            for (int i = 1; i < l.Count; i++)     //пересчёт остальных С
            {
                if (i == point.Y)
                {
                    continue;
                }
                l[i][l[0].Count - 1] = Math.Round(oldMtrx[i][l[0].Count - 1] - oldMtrx[point.Y][l[0].Count - 1] * oldMtrx[i][point.X] / oldMtrx[point.Y][point.X], 3);
            }

            l[0][l[0].Count - 1] = Math.Round(oldMtrx[0][l[0].Count - 1] - oldMtrx[0][point.X] * oldMtrx[point.Y][l[0].Count - 1] / oldMtrx[point.Y][point.X], 3);    //пересчёт F

            string s = title[0][point.Y + 1];
            title[0][point.Y + 1] = title[1][point.X + 1];
            title[1][point.X + 1] = s;

            Render.buildTable(l, title, ResBox);
        }

        public static void Solver(ref List<List<double>> clm, ref Point point, ref List<List<string>> title, TextBox ResBox)
        {
            try
            {
                while (!calcBase(clm, ref point, ResBox))
                {
                    getSymTable(ref clm, point, ref title, ResBox);
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
                        calcFBase(clm, ref point, ResBox);
                        getSymTable(ref clm, point, ref title, ResBox);
                    }
                    catch (EntryPointNotFoundException)
                    {
                        return;
                    }
                }
            }

            ResBox.Text += "\r\nРешение ветви:\r\n" + title[0][2] + " = " + title[0][3] + " = 0\r\n";
            ResBox.Text += title[1][1] + " = " + clm[0][0] + "\r\n";
            ResBox.Text += title[1][2] + " = " + clm[0][1] + "\r\n";
            ResBox.Text += title[1][3] + " = " + clm[0][2] + "\r\n";
        }

        public static bool calcBase(List<List<double>> clm, ref Point point, TextBox ResBox)
        {
            bool isPos = true;
            double temp = 0;

            for (int i = 0; i < clm[0].Count; i++)
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
                for (int i = 1; i < clm.Count; i++)
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

        public static void calcFBase(List<List<double>> clm, ref Point point, TextBox ResBox)
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

                    tempL.Add(clm[0][i] / clm[point.Y][i]);
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
