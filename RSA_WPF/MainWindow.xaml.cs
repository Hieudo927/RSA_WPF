using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace RSA_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RSACryptoServiceProvider rsa;
        public MainWindow()
        {
            InitializeComponent();

        }

        OpenFileDialog openFileDialog = new OpenFileDialog();
        

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                txtEditor.Text = fileContent;
            }
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            string content = txtEditor.Text;

            // Đường dẫn cụ thể cho file lưu
            string filePath = @"D:\Ky6\AT&BMTT\Code\plaintext.txt";
            if (!File.Exists(filePath))
            {
                // Tạo file mới nếu chưa tồn tại
                File.Create(filePath).Dispose();
            }
            // Lưu nội dung vào file
            File.WriteAllText(filePath, content);
            MessageBox.Show("Lưu thành công!", "Thông báo");
        }

        private void bntGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    // Lấy khóa công khai dưới dạng chuỗi
                    string publicKey = rsa.ToXmlString(false);

                    // Lấy khóa bí mật dưới dạng chuỗi
                    string privateKey = rsa.ToXmlString(true);

                    // Hiển thị khóa công khai và khóa bí mật trên TextBox
                    txtPublicKey.Text = publicKey;
                    txtPrivateKey.Text = privateKey;

                    MessageBox.Show("Đã tạo cặp khóa thành công", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

