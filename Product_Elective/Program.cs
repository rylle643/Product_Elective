using Product_Elective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Elective
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Cashier());
            Application.Run(new Save_Product());
            Application.Run(new products_reports());
            //Application.Run(new BarcodeGenerator());
        }
    }
}
