using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BranchAndBoundMethod
{
    static class Render
    {
        public static void initTitle(List<List<string>> title, List<string> strTit)
        {
            strTit.Add(" БП ");
            strTit.Add(" СП ");
            strTit.Add(" X1 ");
            strTit.Add(" X2 ");
            title.Add(strTit);

            strTit = new List<string>();

            strTit.Add(" БП ");
            strTit.Add(" X3 ");
            strTit.Add(" X4 ");
            strTit.Add("  F  ");
            title.Add(strTit);
        }

        public static void buildTable(List<List<double>> l, List<List<string>> t, TextBox ResBox)
        {
            ResBox.Text += "\r\n" + t[0][0] + t[0][1] + t[0][2] + t[0][3] + "\r\n";
            for (int i = 0; i < l[0].Count; i++)
            {
                ResBox.Text += t[1][i + 1];
                for (int j = 0; j < l.Count; j++)
                {
                    ResBox.Text += "   " + l[j][i];
                }
                ResBox.Text += "\r\n";
            }
            ResBox.Text += "\r\n";

        }
    }
}
