using CourseWork2.ModelsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

namespace CourseWork2
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Задание 1.Создать таблицы: \r\nТаблица 1. Справочник услуг.\r\nСтруктура таблицы: Код услуги, Наименование, Цена \r\nТаблица 2. Заказы.\r\nСтруктура таблицы: Номер заказа, Дата заказа, Код услуги, Стоимость услуги, Форма \r\nоплаты. Форма оплаты может быть наличными или по безналичному расчету. \r\nТаблица 3. Клиенты.\r\nСтруктура таблицы: Код клиента, Фамилия клиента, Адрес клиента, Телефон (Номер \r\nтелефона должен содержать код города) \r\nТаблица 4. Реестр заказов.\r\nСтруктура таблицы: Номер заказа, Код клиента, Стоимость заказа.\r\nЗадание 2. С помощью SQL-запроса подсчитать стоимость заказов по каждому клиенту. \r\nЗапрос должен содержать поля: Фамилия клиента, Стоимость заказа. \r\nЗадание 3. Составить SQL-запрос, позволяющий просмотреть данные обо всех клиентах, \r\nтелефон которых содержит код города Рязань.", "Задание", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void btnDeveloper_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Андрианов Алексей Вариант ", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            LoudDataBaseDG();
        }
        void LoudDataBaseDG()
        {
            using (УчетДеятельностиМузеяContext _db = new УчетДеятельностиМузеяContext())
            {
               
                int selectIndex = DGDataBase.SelectedIndex;
                int selectIndexCl = DGExp.SelectedIndex;
                _db.Выставкаs.Load();
                _db.Поставкиs.Load();
                _db.Сотрудникиs.Load();
                _db.Экспонатs.Load();

                DGDataBase.ItemsSource = _db.Экскурсииs.ToList();
                DGExp.ItemsSource = _db.Экспонатs.ToList();

                if (selectIndex != -1)
                {
                    if (selectIndex >= DGDataBase.Items.Count) selectIndex = DGDataBase.Items.Count - 1;
                    DGDataBase.SelectedIndex = selectIndex;
                    DGDataBase.ScrollIntoView(DGDataBase.SelectedItem);
                }
                if (selectIndexCl != -1)
                {
                    if (selectIndexCl >= DGExp.Items.Count) selectIndexCl = DGExp.Items.Count - 1;
                    DGExp.SelectedIndex = selectIndexCl;
                    DGExp.ScrollIntoView(DGExp.SelectedItem);
                }
                DGDataBase.Focus();
            }
        }

        private void btnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            Flags.FlagADD = true;
            Data.экскурсии = null;
            TheForm f = new TheForm();
            f.Owner = this;
            f.ShowDialog();
            LoudDataBaseDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGDataBase.SelectedItem != null)
            {
                Flags.FlagEdit = true;
                Data.экскурсии = (Экскурсии)DGDataBase.SelectedItem;
                
                TheForm f = new TheForm();
                f.Owner = this;
                f.ShowDialog();
                LoudDataBaseDG();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Экскурсии row = (Экскурсии)DGDataBase.SelectedItem;

                    if (row != null)
                    {
                        using (УчетДеятельностиМузеяContext _db = new УчетДеятельностиМузеяContext())
                        {
                            _db.Экскурсииs.Remove(row);
                            _db.SaveChanges();
                        }
                        LoudDataBaseDG();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка заполнения");
                }

            }
            else DGDataBase.Focus();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (DGDataBase.SelectedItem != null)
            {
                Flags.FlagView = true;
                Data.экскурсии = (Экскурсии)DGDataBase.SelectedItem;
                TheForm f = new TheForm();
                f.Owner = this;
                f.ShowDialog();
                LoudDataBaseDG();
            }
        }

        

        private void btnFilt_Click(object sender, RoutedEventArgs e)
        {
            if (tbFilt.Text.IsNullOrEmpty() == false)
            {
                using (УчетДеятельностиМузеяContext _db = new УчетДеятельностиМузеяContext())
                {
                    _db.Выставкаs.Load();
                    _db.Поставкиs.Load();
                    var filtered = _db.Экспонатs.Where(p => p.КодВыставкиNavigation.Тематика.Contains(tbFilt.Text));

                    DGExp.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoudDataBaseDG();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFilt.Clear();
        }
    }
}