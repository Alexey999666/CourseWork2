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
        Экскурсии _экскурсии;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            cbTema.ItemsSource = _db.Выставкаs.ToList();
            cbTema.DisplayMemberPath = "Тематика";
            cbEmployee.ItemsSource = _db.Сотрудникиs.Where(s => s.Должность == "Экскурсовод").ToList();
            cbEmployee.DisplayMemberPath = "Фамилия";
            

            if (Flags.FlagADD == true)
            {
                TheFormBlank.Title = "Добавить запись";
                btnFormAdd.Content = "Добавить";
                btnFormAdd.Visibility = Visibility.Visible;
                _экскурсии = new Экскурсии();
               

                Flags.FlagADD = false;
            }
            else if (Flags.FlagEdit == true)
            {
                TheFormBlank.Title = "Изменить запись";
                btnFormAdd.Content = "Изменить";
                btnFormAdd.Visibility = Visibility.Visible;

                _экскурсии = _db.Экскурсииs.Find(Data.экскурсии.Idэкскурсии);
                
                Flags.FlagEdit = false;
            }
            else
            {
                TheFormBlank.Title = "Посмотреть запись";
                btnFormAdd.Visibility = Visibility.Collapsed;
                cbEmployee.IsEnabled = false;
                cbTema.IsEnabled = false;
                tbCoust.IsReadOnly = true;
                tbDuration.IsReadOnly = true ;
                tbKol.IsReadOnly = true;
                _экскурсии = _db.Экскурсииs.Find(Data.экскурсии.Idэкскурсии);
               
                Flags.FlagView = false;
            }
            TheFormBlank.DataContext = _экскурсии;
        }

        private void btnFormAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            



            if (cbTema.SelectedItem == null)
                errors.AppendLine("Выберите выставку");

            if (cbEmployee.SelectedItem == null)
                errors.AppendLine("Выберите сотрудника");
            

            if (!int.TryParse(tbKol.Text, out int K) || K <= 0)
                errors.AppendLine("Ошибка в количестве людей");
            if (!int.TryParse(tbDuration.Text, out int D) || D <= 0)
                errors.AppendLine("Ошибка в продолжительности");


            tbCoust.Text = tbCoust.Text.Replace(".", ",");
                if (!double.TryParse(tbCoust.Text, out double C) || C <= 0)
                    errors.AppendLine("Укажите корректную стоимость");
            

       

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString()); return;
            }
            try
            {
                if (Data.экскурсии == null)
                {
                    _db.Экскурсииs.Add(_экскурсии);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(_экскурсии).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                _db.Экскурсииs.Remove(_экскурсии);
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
