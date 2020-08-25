using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ForWorkAnyData fw = new ForWorkAnyData();
        shiaDBEntities db = new shiaDBEntities();
        int check;
        public MainWindow()
        {
            InitializeComponent();
            TakingRepair(InfoLoadOfBd());
            RepBtn.Visibility = Visibility.Visible;
            RepBtn.IsEnabled = true;
            AdmBtn.Visibility = Visibility.Visible;
            AdmBtn.IsEnabled = true;
        }
        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.Repair.ToList();
            if (CheckForAnyLoad.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TakingRepair(bool check)
        {
            if (check == true)
            {
                GridTable.ItemsSource = db.Repair.Include(i => i.ApplicationAcceptance).Include(i => i.ApplicationAcceptance.StatusOfApplicationAcceptance).Include(i => i.StatusOfRepair).Include(i => i.EquipmentBreakdown).Include(i => i.ApplicationAcceptance.OrganizationEmployee).Include(i => i.ApplicationAcceptance.OrganizationEmployee.Position).Include(i => i.ApplicationAcceptance.EquipmentCard).ToList();
            }
            else
            {

            }
        }

        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Repair OgrEmpl = GridTable.SelectedItem as Repair;
            if (OgrEmpl != null)
            {
                check = db.Repair.Where(w => w.NumberOfRepair == OgrEmpl.NumberOfRepair).Select(s => s.idRepair).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderForWindows.WorkWithRepair WWR = new FolderForWindows.WorkWithRepair();
            Hide();
            WWR.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderForWindows.EquipWindow EW = new FolderForWindows.EquipWindow();
            Hide();
            EW.Show();
        }

        private void AdmBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderForWindows.InfoAboutUsersSystem IAUS = new FolderForWindows.InfoAboutUsersSystem();
            Hide();
            IAUS.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FolderForWindows.RequestWindow IAUS = new FolderForWindows.RequestWindow();
            Hide();
            IAUS.Show();
        }
    }
}
