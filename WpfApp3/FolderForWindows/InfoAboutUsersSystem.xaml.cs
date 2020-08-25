using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.Entity;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для InfoAboutUsersSystem.xaml
    /// </summary>
    public partial class InfoAboutUsersSystem : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        int check;
        FolderForWindows.WindowOfInfoSotr inf = new WindowOfInfoSotr();
        public InfoAboutUsersSystem()
        {
            InitializeComponent();
            TakingEmploye(InfoLoadOfBd());
        }
        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.OrganizationEmployee.ToList();
            if (CheckForAnyLoad.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TakingEmploye(bool check)
        {
            if (check == true)
            {
                GridTable.ItemsSource = db.OrganizationEmployee.Include(i => i.Position).ToList();
            }
            else
            {

            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

            if (check > 0)
            {
                inf.idSotr = check;
                inf.ConstructForm(0);
                inf.Fill();
                inf.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrganizationEmployee OgrEmpl = GridTable.SelectedItem as OrganizationEmployee;

            if (OgrEmpl != null)
            {
                check = db.OrganizationEmployee.Where(w => w.FirstName == OgrEmpl.FirstName && w.LastName == OgrEmpl.LastName && w.MiddleName == OgrEmpl.MiddleName).Select(s => s.idEmployee).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }

        private void Btn1_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (check > 0)
            {
                inf.idSotr = check;
                inf.ConstructForm(1);
                inf.Fill();
                inf.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            this.Hide();
            MW.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddNewUser ad = new AddNewUser();
            ad.Show();
        }
    }
}
