using PRN_SafeDrive_Aplication.Admin;
using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.Test;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN_SafeDrive_Aplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowContent(new OverviewDashboard());
        }

        public void ShowContent(UserControl control)
        {
            MainContent.Content = control;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //  Prn1Context prn1Context = new Prn1Context();

        //    if (prn1Context.Users.Count() != 0)
        //    {
        //        int a = 0;
        //        while (a < 50)
        //        {
        //            TestProject s = new TestProject();
        //            a++;
        //            s.Show();
        //        }

        //    }


        //}
    }
}