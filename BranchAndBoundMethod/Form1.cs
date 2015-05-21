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
        private List<List<string>> title = new List<List<string>>();
        private List<string> strTit = new List<string>(); 
        private Point point = new Point();
        #endregion


        private void ExitButton_Click(object sender, EventArgs e)   //закрываем приложение
        {
            Application.Exit();
        }

        private void CalcButton_Click(object sender, EventArgs e)   //событие нажатия на кнопочку Calc
        {
            ResBox.Text = "";   //если мы уже что-то считали, а мусор нам не нужен, то очищаем окошко результатов

            if (!chekForm())    //проверка на заполненность полей
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try     //проверка на правильность заполнения
            {
                initMtrx();     
            }
            catch (Exception)
            {
                MessageBox.Show("Некоторые коэффициенты не цифры!", "Ошибка!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Render.initTitle(title, strTit, (int)numericUpDown1.Value);

            ResBox.Text += "Домножим неравества на -1. Введем дополнительные переменные\r\n";

            #region Brainfuck
            int valNumeric = (int) numericUpDown1.Value;
            if (valNumeric == 2)
            {
                ResBox.Text += clm[1][0] + "X1" + " + (" + clm[2][0] + ")X2 + X3 = " + clm[0][0] + "\r\n";
                ResBox.Text += clm[1][1] + "X1" + " + (" + clm[2][1] + ")X2 + X4 = " + clm[0][1] + "\r\n";
            }
            else if (valNumeric == 3)
            {
                ResBox.Text += clm[1][0] + "X1" + " + (" + clm[2][0] + ")X2 + (" + clm[3][0] + ")X3 + X4 = " + clm[0][0] + "\r\n";
                ResBox.Text += clm[1][1] + "X1" + " + (" + clm[2][1] + ")X2 + (" + clm[3][1] + ")X3 + X5 = " + clm[0][1] + "\r\n";
                ResBox.Text += clm[1][2] + "X1" + " + (" + clm[2][2] + ")X2 + (" + clm[3][2] + ")X3 + X5 = " + clm[0][2] + "\r\n";
            }
            else if (valNumeric == 4)
            {
                ResBox.Text += clm[1][0] + "X1" + " + (" + clm[2][0] + ")X2 + (" + clm[3][0] + ")X3 + (" + clm[4][0] + ")X4 + X5 = " + clm[0][0] + "\r\n";
                ResBox.Text += clm[1][1] + "X1" + " + (" + clm[2][1] + ")X2 + (" + clm[3][1] + ")X3 + (" + clm[4][1] + ")X4 + X6 = " + clm[0][1] + "\r\n";
                ResBox.Text += clm[1][2] + "X1" + " + (" + clm[2][2] + ")X2 + (" + clm[3][2] + ")X3 + (" + clm[4][2] + ")X4 + X7 = " + clm[0][2] + "\r\n";
                ResBox.Text += clm[1][3] + "X1" + " + (" + clm[2][3] + ")X2 + (" + clm[3][3] + ")X3 + (" + clm[4][3] + ")X4 + X8 = " + clm[0][3] + "\r\n";
            }
            ResBox.Text += "Составим симплекс-таблицу:\r\n";
            #endregion

            Render.buildTable(clm, title, ResBox);

            SymplexSolver.Solver(ref clm, ref point, ref title, ResBox);
        }

        private void initMtrx()
        {
            int valNumeric = (int)numericUpDown1.Value;
            str.Add(-1 * Convert.ToDouble(C1Box.Text));
            str.Add(-1 * Convert.ToDouble(C2Box.Text));
            if (valNumeric > 2)
            {
                str.Add(-1 * Convert.ToDouble(C3Box.Text));
                if (valNumeric == 4) str.Add(-1 * Convert.ToDouble(C4Box.Text));
            }           
            str.Add(0);
            clm.Add(str);
            str = new List<double>();

            str.Add(-1 * Convert.ToDouble(X1C1Box.Text));
            str.Add(-1 * Convert.ToDouble(X1C2Box.Text));
            if (valNumeric > 2)
            {
                str.Add(-1 * Convert.ToDouble(X1C3Box.Text));
                if (valNumeric == 4) str.Add(-1 * Convert.ToDouble(X1C4Box.Text));
            }
            str.Add(-1 * Convert.ToDouble(X1Box.Text));
            clm.Add(str);

            str = new List<double>();

            str.Add(-1 * Convert.ToDouble(X2C1Box.Text));
            str.Add(-1 * Convert.ToDouble(X2C2Box.Text));
            if (valNumeric > 2)
            {
                str.Add(-1 * Convert.ToDouble(X2C3Box.Text));
                if (valNumeric == 4) str.Add(-1 * Convert.ToDouble(X2C4Box.Text));

            }
            str.Add(-1 * Convert.ToDouble(X2Box.Text));
            clm.Add(str);

            str = new List<double>();

            if (valNumeric > 2)
            {
                str.Add(-1 * Convert.ToDouble(X3Box.Text));
                str.Add(-1 * Convert.ToDouble(X3C1Box.Text));
                str.Add(-1 * Convert.ToDouble(X3C2Box.Text));
                str.Add(-1 * Convert.ToDouble(X3C3Box.Text));
 
                if (valNumeric == 4)
                {
                    str.Add(-1 * Convert.ToDouble(X3C4Box.Text));
                    clm.Add(str);

                    str = new List<double>();
                    str.Add(-1 * Convert.ToDouble(X4Box.Text));                    
                    str.Add(-1 * Convert.ToDouble(X4C1Box.Text));
                    str.Add(-1 * Convert.ToDouble(X4C2Box.Text));
                    str.Add(-1 * Convert.ToDouble(X4C3Box.Text));
                    str.Add(-1 * Convert.ToDouble(X4C4Box.Text));                   
                }
                clm.Add(str);
            }

        }

        private bool chekForm()
        {
            if ((X1Box.Text == "") || (X2Box.Text == "") || (X1C1Box.Text == "") || (X1C2Box.Text == "") || (X2C1Box.Text == "") || (X2C2Box.Text == "") || (C1Box.Text == "") || (C2Box.Text == ""))
            {
                return false;
            }

            return true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int valNumeric = (int)numericUpDown1.Value;
            if (valNumeric == 2)
            {
                #region size2
                X3C1Box.Visible = false;
                X4C1Box.Visible = false;
                X3C2Box.Visible = false;
                X4C2Box.Visible = false;
                X4C3Box.Visible = false;
                X1C3Box.Visible = false;
                X2C3Box.Visible = false;
                X3C3Box.Visible = false;
                X4C3Box.Visible = false;
                X1C4Box.Visible = false;
                X2C4Box.Visible = false;
                X3C4Box.Visible = false;
                X4C4Box.Visible = false;
                X3Box.Visible = false;
                X4Box.Visible = false;

                C3Box.Visible = false;
                C4Box.Visible = false;
                X3C1.Visible = false;
                X4C1.Visible = false;
                X3C2.Visible = false;
                X4C2.Visible = false;
                X1C3.Visible = false;
                X2C3.Visible = false;
                X3C3.Visible = false;
                X4C3.Visible = false;
                X1C4.Visible = false;
                X2C4.Visible = false;
                X3C4.Visible = false;
                X4C4.Visible = false;
                X3.Visible = false;
                X4.Visible = false;
                equal3.Visible = false;
                equal4.Visible = false;

                #endregion
            }
            else if (valNumeric == 3)
            {
                #region size3
                X3C1Box.Visible = true;
                X4C1Box.Visible = false;
                X3C2Box.Visible = true;
                X4C2Box.Visible = false;
                X4C3Box.Visible = false;
                X1C3Box.Visible = true;
                X2C3Box.Visible = true;
                X3C3Box.Visible = true;
                X4C3Box.Visible = false;
                X1C4Box.Visible = false;
                X2C4Box.Visible = false;
                X3C4Box.Visible = false;
                X4C4Box.Visible = false;
                X3Box.Visible = true;
                X4Box.Visible = false;

                C3Box.Visible = true;
                C4Box.Visible = false;
                X3C1.Visible = true;
                X4C1.Visible = false;
                X3C2.Visible = true;
                X4C2.Visible = false;
                X1C3.Visible = true;
                X2C3.Visible = true;
                X3C3.Visible = true;
                X4C3.Visible = false;
                X1C4.Visible = false;
                X2C4.Visible = false;
                X3C4.Visible = false;
                X4C4.Visible = false;
                X3.Visible = true;
                X4.Visible = false;
                equal3.Visible = true;
                equal4.Visible = false;
                #endregion
            }
            else if (valNumeric == 4)
            {
                #region size4
                X3C1Box.Visible = true;
                X4C1Box.Visible = true;
                X3C2Box.Visible = true;
                X4C2Box.Visible = true;
                X4C3Box.Visible = true;
                X1C3Box.Visible = true;
                X2C3Box.Visible = true;
                X3C3Box.Visible = true;
                X4C3Box.Visible = true;
                X1C4Box.Visible = true;
                X2C4Box.Visible = true;
                X3C4Box.Visible = true;
                X4C4Box.Visible = true;
                X3Box.Visible = true;
                X4Box.Visible = true;

                C3Box.Visible = true;
                C4Box.Visible = true;
                X3C1.Visible = true;
                X4C1.Visible = true;
                X3C2.Visible = true;
                X4C2.Visible = true;
                X1C3.Visible = true;
                X2C3.Visible = true;
                X3C3.Visible = true;
                X4C3.Visible = true;
                X1C4.Visible = true;
                X2C4.Visible = true;
                X3C4.Visible = true;
                X4C4.Visible = true;
                X3.Visible = true;
                X4.Visible = true;
                equal3.Visible = true;
                equal4.Visible = true;

                #endregion
            }
        }
    }
}
