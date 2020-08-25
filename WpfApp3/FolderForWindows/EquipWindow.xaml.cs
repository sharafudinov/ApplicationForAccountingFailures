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
    /// Логика взаимодействия для EquipWindow.xaml
    /// </summary>
    public partial class EquipWindow : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        int check;
        CreateNewRequestForRepair wind = new CreateNewRequestForRepair();
        public EquipWindow()
        {
            InitializeComponent();
            TakingInfo(InfoLoadOfBd());
        }
        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EquipmentCard EuipCard = GridTable.SelectedItem as EquipmentCard;

            if (EuipCard != null)
            {
                check = db.EquipmentCard.Where(w => w.NameOfEquipment == EuipCard.NameOfEquipment && w.InventoryNumber == EuipCard.InventoryNumber && w.CommissioningDate == EuipCard.CommissioningDate).Select(s => s.IdEquipment).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }
        public void SendRepair()
        {

        }
        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.EquipmentCard.ToList();
            if (CheckForAnyLoad.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TakingInfo(bool check)
        {
            if (check == true)
            {
                GridTable.ItemsSource = db.EquipmentCard.Include(i => i.StatusOfEquipment).Include(i => i.TypeOfEquipment).ToList();
            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var inforeapirtoupdate = db.EquipmentCard.Where(w => w.IdEquipment == check).FirstOrDefault();
            if (check > 0)
            {

                inforeapirtoupdate.idStatusOfEquipment = 1;
                db.SaveChanges();
                wind.ideq = check;
                wind.fill();
                wind.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            this.Hide();
            MW.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddNewEquip anq = new AddNewEquip();
            Hide();
            anq.ShowDialog();
            Show();

        }
    }
}
