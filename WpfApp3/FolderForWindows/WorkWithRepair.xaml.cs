using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для WorkWithRepair.xaml
    /// </summary>
    public partial class WorkWithRepair : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        int check;
        InfoForRepair inf = new InfoForRepair();
        public WorkWithRepair()
        {
            InitializeComponent();
            TakingRepair(InfoLoadOfBd());
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
                DataGridRepair.ItemsSource = db.Repair.Include(i => i.ApplicationAcceptance).Include(i => i.ApplicationAcceptance.StatusOfApplicationAcceptance).Include(i => i.StatusOfRepair).Include(i => i.EquipmentBreakdown).Include(i => i.ApplicationAcceptance.OrganizationEmployee).Include(i => i.ApplicationAcceptance.OrganizationEmployee.Position).Include(i => i.ApplicationAcceptance.EquipmentCard).ToList();
            }
            else
            {

            }
        }

        private void DataGridRepair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Repair OgrEmpl = DataGridRepair.SelectedItem as Repair;
            if (OgrEmpl != null)
            {
                check = db.Repair.Where(w => w.NumberOfRepair == OgrEmpl.NumberOfRepair).Select(s => s.idRepair).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }

        private void Btn_ifoOfRepair_Click(object sender, RoutedEventArgs e)
        {
            if (check > 0)
            {
                inf.idRep = check;
                inf.loadinfo();
                inf.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void Btn_ProRepair_Click(object sender, RoutedEventArgs e)
        {
            var inforeapirtoupdate = db.Repair.Where(w => w.idRepair == check).FirstOrDefault();
            if (check > 0)
            {
                Repair repar = new Repair()
                {
                    NumberOfRepair = inforeapirtoupdate.NumberOfRepair + "1",
                    idBreakdown = inforeapirtoupdate.idBreakdown,
                    idRequest = inforeapirtoupdate.idRequest,
                    idStatusOfRepair = inforeapirtoupdate.idStatusOfRepair,
                    RenewalOfRepair = true
                };
                db.Repair.Add(repar);
                db.SaveChanges();
            }
            else
            {
                return;
            }
        }

        private void Btn_OutOfRepair_Click(object sender, RoutedEventArgs e)
        {
            var inforeapirtoupdate = db.Repair.Where(w => w.idRepair == check).FirstOrDefault();
            if (check > 0)
            {
                var myupdate = db.Repair.Where(w => w.idRepair == check).FirstOrDefault();
                myupdate.idStatusOfRepair = 4;
                db.SaveChanges();
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
    }
}
