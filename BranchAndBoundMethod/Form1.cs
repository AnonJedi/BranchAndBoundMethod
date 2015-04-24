using System;
using System.Collections.Generic;
using System.Windows.Forms;
<<<<<<< HEAD
using System.Drawing;
=======
>>>>>>> origin/master

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

<<<<<<< HEAD
        private double[,] M = new double[3,3];
        private string[,] title = new string[2,4];
        private double X1, X2, X1C1, X1C2, X2C1, X2C2, C1, C2;
        private Point point = new Point();
=======
        private double x1, x2, x3, x4;

>>>>>>> origin/master
        #endregion

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

<<<<<<< HEAD
        private void CalcButton_Click(object sender, EventArgs e)
        {
            if ((X1Box.Text == "") || (X2Box.Text == "") || (X1C1Box.Text == "") || (X1C2Box.Text == "") || (X2C1Box.Text == "") || (X2C2Box.Text == "") || (C1Box.Text == "") || (C2Box.Text == ""))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                X1 = -1*Convert.ToDouble(X1Box.Text);
                X2 = -1*Convert.ToDouble(X2Box.Text);
                X1C1 = -1*Convert.ToDouble(X1C1Box.Text);
                X1C2 = -1*Convert.ToDouble(X1C2Box.Text);
                X2C1 = -1*Convert.ToDouble(X2C1Box.Text);
                X2C2 = -1*Convert.ToDouble(X2C2Box.Text);
                C1 = -1*Convert.ToDouble(C1Box.Text);
                C2 = -1*Convert.ToDouble(C2Box.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Некоторые коэффициенты не цифры!", "Ошибка!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            ResBox.Text += "Домножим оба неравества на -1. Введем дополнительные переменные X3, X4\r\n";
            ResBox.Text += X1C1 + "X1" + " + (" + X2C1 + ")X2 + X3 = " + C1 + "\r\n";
            ResBox.Text += X1C2 + "X1" + " + (" + X2C2 + ")X2 + X4 = " + C2 + "\r\n";

            ResBox.Text += "Составим симплекс-таблицу:\r\n";

            title[0, 0] = "БП";
            title[0, 1] = "СП";
            title[0, 2] = "X1";
            title[0, 3] = "X2";
            title[1, 0] = title[0, 0];
            title[1, 1] = "X3";
            title[1, 2] = "X4";
            title[1, 3] = " F";

            M[0, 0] = C1;
            M[1, 0] = C2;
            M[2, 0] = 0;
            M[0, 1] = X1C1;
            M[1, 1] = X1C2;
            M[2, 1] = X1;
            M[0, 2] = X2C1;
            M[1, 2] = X2C2;
            M[2, 2] = X2;

            buildTable(M, title);

            try
            {
                while (!calcBase(M))
                {

                    getSymTable(M);
                }
            }
            catch (EntryPointNotFoundException)
            {
                return;
            }
        }

        private void getSymTable(double[,] M)
        {
            
        }

        private bool calcBase(double[,] M)
        {
            bool isPos = true;
            double temp = 0;

            for (int i = 0; i < 3; i++)
            {
                if (M[i, 0] <= 0)
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
            ResBox.Text += "\r\n" + t[0, 0] + "  " + t[0, 1] + "  " + t[0, 2] + "  " + t[0, 3] + "\r\n";
            for (int i = 0; i < 3; i++)
            {
                ResBox.Text += t[1, i+1];
                for (int j = 0; j < 3; j++)
                {
                    ResBox.Text += "  " + m[i, j];
                }
                ResBox.Text += "\r\n";
            }
=======
        private void CalcBtn_Click(object sender, EventArgs e)
        {

>>>>>>> origin/master
        }
    }
}
