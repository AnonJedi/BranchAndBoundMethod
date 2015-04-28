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

            Render.initTitle(title, strTit);

            ResBox.Text += "Домножим оба неравества на -1. Введем дополнительные переменные X3, X4\r\n";
            ResBox.Text += clm[1][0] + "X1" + " + (" + clm[2][0] + ")X2 + X3 = " + clm[0][0] + "\r\n";
            ResBox.Text += clm[1][1] + "X1" + " + (" + clm[2][1] + ")X2 + X4 = " + clm[0][1] + "\r\n";

            ResBox.Text += "Составим симплекс-таблицу:\r\n";

            Render.buildTable(clm, title, ResBox);

            SymplexSolver.Solver(ref clm, ref point, ref title, ResBox);
        }

        private void initMtrx()
        {
            str.Add(-1 * Convert.ToDouble(C1Box.Text));
            str.Add(-1 * Convert.ToDouble(C2Box.Text));
            str.Add(0);
            clm.Add(str);

            str = new List<double>();

            str.Add(-1 * Convert.ToDouble(X1C1Box.Text));
            str.Add(-1 * Convert.ToDouble(X1C2Box.Text));
            str.Add(-1 * Convert.ToDouble(X1Box.Text));
            clm.Add(str);

            str = new List<double>();

            str.Add(-1 * Convert.ToDouble(X2C1Box.Text));
            str.Add(-1 * Convert.ToDouble(X2C2Box.Text));
            str.Add(-1 * Convert.ToDouble(X2Box.Text));
            clm.Add(str);

            str = new List<double>();
        }

        private bool chekForm()
        {
            if ((X1Box.Text == "") || (X2Box.Text == "") || (X1C1Box.Text == "") || (X1C2Box.Text == "") || (X2C1Box.Text == "") || (X2C2Box.Text == "") || (C1Box.Text == "") || (C2Box.Text == ""))
            {
                return false;
            }

            return true;
        }
    }
}
