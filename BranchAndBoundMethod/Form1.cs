using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        private double x1, x2, x3, x4;

        #endregion

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
