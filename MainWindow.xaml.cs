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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vigenère_wpf
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(true);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextChanged(false);
        }

        private void TextChanged(bool Encrypt)
        {
            TextBox from = Encrypt ? this.Original : this.Encrypted;
            TextBox to = !Encrypt ? this.Original : this.Encrypted;
            if (Key.Password.Length == 0) to.Text = from.Text;
            else
            {
                string[] strs = from.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
                string temp = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    Vigenere vigenere = new Vigenere(Key.Password, strs[i]);
                    vigenere.Encrypt(Encrypt);
                    temp += vigenere.Result;
                    if (i < strs.Length - 1) temp += '\n';
                }
                to.Text = temp;
            }
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Key.Visibility = System.Windows.Visibility.Visible;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Key.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Key.Visibility = System.Windows.Visibility.Visible;
        }

        private void Key_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.KeyContent.Text = this.Key.Password;
            TextChanged(true);
        }
    }
}
