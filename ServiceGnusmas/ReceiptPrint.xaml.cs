using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ServiceGnusmas
{
    /// <summary>
    /// Логика взаимодействия для ReceiptPrint.xaml
    /// </summary>
    public partial class ReceiptPrint : Window
    {
        public ReceiptPrint(DataTable dt)
        {
            InitializeComponent();
            foreach (DataRow dr in dt.Rows)
            {
                receiverlbl.Content = dr["FIO"].ToString();
                ClientNamelbl.Content = dr["ClientName"].ToString();
                Codelbl.Content = dr["PersonalCode"].ToString();
                Datelbl.Content = dr["TransferDate"].ToString();


            }
            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() == true)
            {
                p.PrintVisual(PrintGrid, "Печать");
            }
        }
    }
}
