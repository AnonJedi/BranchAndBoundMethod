using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MathParser;

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
        private List<String> data;
        private int conCntr = 0;
        private Parser parser;
        #endregion

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            parser = new Parser();
            Regex reg = new Regex("x", RegexOptions.IgnoreCase);
            FTextBox.Text = reg.Replace(FTextBox.Text, "2");

            data.Insert(0, FTextBox.Text);

            if (parser.Evaluate(data[0]))
            {
                FTextBox.Text = parser.Result.ToString();
            }
            else
            {
                MessageBox.Show("Error of function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                data.Insert(0, "0");
            }

        }
        
        private void AddConButton_Click(object sender, EventArgs e)
        {
            parser = new Parser();
            if (parser.Evaluate(CTextBox.Text))
            {
                if (data.Count == 0)
                {
                    data.Insert(0, "0");
                    data.Insert(1, CTextBox.Text);
                }
                else
                {
                    data.Add(CTextBox.Text);
                }
            }
            else
            {
                MessageBox.Show("Error of condition", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
