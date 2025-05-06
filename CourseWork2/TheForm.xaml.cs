using CourseWork2.ModelsDB;
using Microsoft.EntityFrameworkCore;
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

namespace CourseWork2
{
    /// <summary>
    /// Логика взаимодействия для TheForm.xaml
    /// </summary>
    public partial class TheForm : Window
    {
        public TheForm()
        {
            InitializeComponent();
        }



        УчетДеятельностиМузеяContext _db = new УчетДеятельностиМузеяContext();
        Экспонат _экспонат;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbTema.ItemsSource = _db.Выставкаs.ToList();
            cbTema.DisplayMemberPath = "Тематика";
            //cbMethodReceipt.ItemsSource = _db.Поставкиs.ToList();
            //cbMethodReceipt.DisplayMemberPath = "СпособПолучения";


            if (Flags.FlagADD == true)
            {
                TheFormBlank.Title = "Добавить запись";
                btnFormAdd.Content = "Добавить";
                btnFormAdd.Visibility = Visibility.Visible;
                _экспонат = new Экспонат();

                Flags.FlagADD = false;
            }
            else if (Flags.FlagEdit == true)
            {
                TheFormBlank.Title = "Изменить запись";
                btnFormAdd.Content = "Изменить";
                btnFormAdd.Visibility = Visibility.Visible;
                _экспонат = _db.Экспонатs.Find(Data.экспонат.ИнвентарныйНомер);
                Flags.FlagEdit = false;
            }
            else
            {
                TheFormBlank.Title = "Посмотреть запись";
                btnFormAdd.Visibility = Visibility.Collapsed;
                _экспонат = _db.Экспонатs.Find(Data.экспонат.ИнвентарныйНомер);
                Flags.FlagView = false;
            }
            TheFormBlank.DataContext = _экспонат;
        }

        private void btnFormAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            //if (!int.TryParse(tbNumber.Text, out int N) || N <= 0)
            //    errors.AppendLine("Ошибка в номере");
            //if (dpDate.SelectedDate == null || dpDate.SelectedDate.Value == default(DateTime))
            //    errors.AppendLine("Заполните корректную дату");
            //else if (dpDate.SelectedDate.Value > DateTime.Now)
            //    errors.AppendLine("Дата не может быть в будущем");
            //if (string.IsNullOrWhiteSpace(cbNumber.Text))
            //    errors.AppendLine("Заполните заказ");
            //if (string.IsNullOrWhiteSpace(cbKod.Text))
            //    errors.AppendLine("Заполните услугу");
            //tbCoust.Text = tbCoust.Text.Replace(".", ",");
            //if (!double.TryParse(tbCoust.Text, out double D) || D <= 0)
            //    errors.AppendLine("Ошибка в цене");
            //if (string.IsNullOrWhiteSpace(cbFormBuy.Text))
            //    errors.AppendLine("Заполните форму оплаты");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString()); return;
            }
            try
            {
                if (Data.экспонат == null)
                {
                    _db.Экспонатs.Add(_экспонат);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(_экспонат).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                _db.Экспонатs.Remove(_экспонат);
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
