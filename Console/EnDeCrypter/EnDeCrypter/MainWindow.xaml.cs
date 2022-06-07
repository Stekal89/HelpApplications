using EnDeCrypter.Model;
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

namespace EnDeCrypter
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

        private void BtnEnDeCryptClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(pwdPassword.Password))
            {
                DeEncrypter deEncrypter = new DeEncrypter();
                // Using the Random generated Key
                //string encryptKey = deEncrypter.GenerateEncryptionKey();

                // Using the static key, so you get everytime the same result to encrypt and decrypt the
                // passwords
                string encryptKey = "ABC123XYZ8910";

                string encrypted = deEncrypter.Encrypt(pwdPassword.Password, encryptKey);
                string decrypted = deEncrypter.Decrypt(encrypted, encryptKey);

                lblEncryptedPwd.Content = encrypted;
                lblDecryptedPwd.Content = decrypted;
            }
            else
            {
                MessageBox.Show("You did not enter a Password!","Attention",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
