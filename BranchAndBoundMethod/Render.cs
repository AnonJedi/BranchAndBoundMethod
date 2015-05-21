using System.Collections.Generic;
using System.Windows.Forms;

namespace BranchAndBoundMethod
{
    static class Render
    {
        public static void initTitle(List<List<string>> title, List<string> strTit, int valNumeric)
        {
            strTit.Add(" БП ");
            strTit.Add(" СП ");
            strTit.Add(" X1 ");
            strTit.Add(" X2 ");
            if (valNumeric > 2)
            {
                strTit.Add(" X3 ");
                if (valNumeric == 4) strTit.Add(" X4 ");
            }
            title.Add(strTit);

            strTit = new List<string>();

            if (valNumeric == 2)
            {
                strTit.Add(" БП ");
                strTit.Add(" X3 ");
                strTit.Add(" X4 ");
                strTit.Add("  F  ");
                title.Add(strTit);
            }
            else if (valNumeric == 3)
            {
                strTit.Add(" БП ");
                strTit.Add(" X4 ");
                strTit.Add(" X5 ");
                strTit.Add(" X6 ");
                strTit.Add("  F  ");
                title.Add(strTit);
            }
            else if (valNumeric == 4)
            {
                strTit.Add(" БП ");
                strTit.Add(" X5 ");
                strTit.Add(" X6 ");
                strTit.Add(" X7 ");
                strTit.Add(" X8 ");
                strTit.Add("  F  ");
                title.Add(strTit);
            }
        }

        public static void buildTable(List<List<double>> l, List<List<string>> t, TextBox ResBox)
        {
            ResBox.Text += "\r\n";
            foreach (string s in t[0]) ResBox.Text += s;
            ResBox.Text += "\r\n";

            for (int i = 0; i < l[0].Count; i++)
            {
                ResBox.Text += t[1][i + 1];
                for (int j = 0; j < l.Count; j++) ResBox.Text += "   " + l[j][i];
                ResBox.Text += "\r\n";
            }
            ResBox.Text += "\r\n";
        }
    }
}
