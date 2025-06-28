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
using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.Test;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SafeDriveApplicationContext o = new SafeDriveApplicationContext();

            if (o.Users.Count() != 0)
            {
                int a = 0;
                while (a < 50)
                {
                    TestProject s = new TestProject();
                    a++;
                    s.Show();
                }

            }


        }
    }
}