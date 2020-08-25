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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        int check;
        public RequestWindow()
        {
            InitializeComponent();
            TakingEmploye(InfoLoadOfBd());
        }
        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationAcceptance OgrEmpl = GridTable.SelectedItem as ApplicationAcceptance;

            if (OgrEmpl != null)
            {
                check = db.ApplicationAcceptance.Include(i => i.StatusOfApplicationAcceptance).Include(i => i.OrganizationEmployee).Include(i => i.OrganizationEmployee.Position).Include(i => i.EquipmentCard).Where(w => w.idEquipment == OgrEmpl.idEquipment && w.ResponsiblePerson == OgrEmpl.ResponsiblePerson).Select(s => s.idRequest).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }
        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.ApplicationAcceptance.Include(i => i.StatusOfApplicationAcceptance).Include(i => i.OrganizationEmployee).Include(i => i.OrganizationEmployee.Position).Include(i => i.EquipmentCard).ToList();
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
                GridTable.ItemsSource = db.ApplicationAcceptance.Include(i => i.StatusOfApplicationAcceptance).Include(i => i.OrganizationEmployee).Include(i => i.OrganizationEmployee.Position).Include(i => i.EquipmentCard).ToList();
            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var inforeapirtoupdate = db.ApplicationAcceptance.Where(w => w.idRequest == check).FirstOrDefault();
            if (check > 0)
            {
                var myupdate = db.ApplicationAcceptance.Where(w => w.idRequest == check).FirstOrDefault();
                myupdate.FK_StatusOfApplicationAcceptance_id = 2;
                db.SaveChanges();
            }
            else
            {
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var inforeapirtoupdate = db.ApplicationAcceptance.Where(w => w.idRequest == check).FirstOrDefault();
            if (check > 0)
            {
                var myupdate = db.ApplicationAcceptance.Where(w => w.idRequest == check).FirstOrDefault();
                myupdate.FK_StatusOfApplicationAcceptance_id = 3;
                db.SaveChanges();
            }
            else
            {
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
