using System;
using System.IO;
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

namespace lab11mdk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Abons> ListAbon { get; set; } = new(); 
        public MainWindow()
        {
            InitializeComponent();

        }
        public struct Abons
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Moths {  get; set; }
            public int Payedmoths { get; set; }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           ListAbon.Add(new Abons() { Name=name.Text, Price=decimal.Parse(price.Text),
           Moths=int.Parse(month.Text),Payedmoths=int.Parse(payedMoths.Text)});
           UpdateForm();
        }
        void UpdateForm()
        {
            name.Clear();
            price.Clear();
            month.Clear();
            payedMoths.Clear();
            LAbone.ItemsSource = null;
            LAbone.ItemsSource= ListAbon;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListAbon.RemoveAt(LAbone.SelectedIndex);
            UpdateForm();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("abons.dat", FileMode.OpenOrCreate)))
            {
                foreach (var item in ListAbon)
                {
                    writer.Write(item.Name);
                    writer.Write(item.Price);
                    writer.Write(item.Moths);
                    writer.Write(item.Payedmoths);
                }
            }
        }

        private void LAbone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LAbone.SelectedIndex != -1)
            {
                Abons abons = ListAbon[LAbone.SelectedIndex];
                name.Text = abons.Name;
                price.Text = abons.Price.ToString();
                month.Text = abons.Moths.ToString();
                payedMoths.Text = abons.Payedmoths.ToString();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            ListAbon.Clear();
            using (BinaryReader reader = new BinaryReader(File.Open("abons.dat", FileMode.Open)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    decimal price = reader.ReadDecimal();
                    int months = reader.ReadInt32();
                    int payedMoths = reader.ReadInt32();
                    if (payedMoths > 3) price = (decimal)((double)price * 0.93);
                    ListAbon.Add(new Abons() { Name = name, Price = price, Moths = months, Payedmoths = payedMoths });
                    UpdateForm();
                }
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open("abons.dat", FileMode.OpenOrCreate)))
            {
                foreach (var item in ListAbon)
                {
                    writer.Write(item.Name);
                    writer.Write(item.Price);
                    writer.Write(item.Moths);
                    writer.Write(item.Payedmoths);
                }
            }
        }
    }
}