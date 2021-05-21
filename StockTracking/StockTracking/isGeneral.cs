using System;
using System.Windows.Forms;

namespace StockTracking
{
    public class isGeneral
    {
        internal static bool isNumber(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                return true;
            else
                return false;
        }
    }
}