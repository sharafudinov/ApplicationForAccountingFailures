using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp3.FolderForWindows
{
    /// <summary>
    /// Логика взаимодействия для AddNewEquip.xaml
    /// </summary>
    public partial class AddNewEquip : Window
    {
        shiaDBEntities db = new shiaDBEntities();
        public AddNewEquip()
        {
            InitializeComponent();
            Register();
        }
        public void Register()
        {
            ObservableCollection<TypeOfEquipment> ItemsForRoles = new ObservableCollection<TypeOfEquipment>();

            var query = db.TypeOfEquipment.ToList();

            foreach (var roles in query)
            {
                ItemsForRoles.Add(
                    new TypeOfEquipment { idTypeOfEquipment = roles.idTypeOfEquipment, TypeOfEquipment1 = roles.TypeOfEquipment1 }
                    );
                Cmb.ItemsSource = ItemsForRoles;
            }
            Cmb.ItemsSource = ItemsForRoles;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquipWindow asd = new EquipWindow();
            this.Hide();
            asd.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            EquipmentCard Card = new EquipmentCard()
            {
                NameOfEquipment = Name.Text,
                Lifetime = Mou.Text,
                InventoryNumber = rand.Next(1000, 10000).ToString(),
                CommissioningDate = DateTime.Now,
                DecommissioningDate = DateTime.Now.AddMonths(Convert.ToInt32(Mou.Text)),
                idTypeOfEquipment = db.TypeOfEquipment.Where(w => w.TypeOfEquipment1 == Cmb.Text).Select(s => s.idTypeOfEquipment).FirstOrDefault(),
                idStatusOfEquipment = 2
            };
            db.EquipmentCard.Add(Card);
            db.SaveChanges();
            MessageBox.Show("Оборудование добавлено");
        }
    }
}
