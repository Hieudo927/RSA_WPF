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
        public MainWindow()
        {
            InitializeComponent();
        }

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        private string filePath;
        private string publicKeyPath;
        private string privateKeyPath;
        //private string encryptedFilePath;



        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                txtEditor.Text = fileContent;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            string DataPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Keys";
            string[] files = Directory.GetFiles(DataPath);
            try
            {
                // Xóa từng tệp tin trong thư mục
                foreach (string file in files)
                {
                    File.Delete(file);
                }
                MessageBox.Show("Reset thành công", "Thông báo");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Có lỗi trong quá trình Reset" + ex);
            }
            
            
            txtEditor.Text = null;
            txtResult.Text = null;
            filePath = null;
            txtPrivateKey.Text = txtPublicKey.Text = null;
        }

        private void btnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                // Lấy khóa công khai dưới dạng chuỗi
                string publicKey = rsa.ToXmlString(false);

                // Lấy khóa bí mật dưới dạng chuỗi
                string privateKey = rsa.ToXmlString(true);

                // Hiển thị khóa công khai và khóa bí mật trên TextBox
                txtPublicKey.Text = publicKey;
                txtPrivateKey.Text = privateKey;

                MessageBox.Show("Đã tạo cặp khóa thành công", "Thông báo");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                publicKeyPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Keys\PublicKey.xml"; // Đường dẫn tới khóa công khai
                //encryptedFilePath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Data\encrypted_file.txt"; // Đường dẫn tới file đã được mã hóa

                // Đọc dữ liệu từ file text
                string plainText = File.ReadAllText(filePath);

                if (!File.Exists(publicKeyPath))
                {
                    MessageBox.Show("Không tồn tại khóa công khai", "Thông báo");
                }
                else
                {
                    rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                    // Mã hóa dữ liệu
                    byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), false);

                    // Ghi dữ liệu đã mã hóa vào file
                    File.WriteAllBytes(filePath, encryptedData);
                    string content = File.ReadAllText(filePath);
                    txtResult.Text = content;

                    MessageBox.Show("File đã được mã hóa thành công", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnSaveKeys_Click(object sender, RoutedEventArgs e)
        {
            string content1 = txtPrivateKey.Text;
            string conten2 = txtPublicKey.Text;
            // Đường dẫn cụ thể cho file lưu
            privateKeyPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Keys\PrivateKey.xml";
            publicKeyPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Keys\PublicKey.xml";
            //if (!File.Exists(privateKeyPath))
            //{
            //    // Tạo file mới nếu chưa tồn tại
            //    File.Create(privateKeyPath).Dispose();
            //}
            if (string.IsNullOrEmpty(content1))
            {
                MessageBox.Show("Chưa tạo khóa", "Thông báo");
            }
            else
            {
                // Lưu nội dung vào file
                File.WriteAllText(privateKeyPath, content1);
                File.WriteAllText(publicKeyPath, conten2);
                MessageBox.Show("Lưu cặp khóa thành công!", "Thông báo");
            }

        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    privateKeyPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Keys\PrivateKey.xml"; // Đường dẫn tới khóa công khai
            //    //encryptedFilePath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Data\encrypted_file.txt"; // Đường dẫn tới file đã được mã hóa

            //    // Đọc dữ liệu từ file text
            //    string plainText = File.ReadAllText(filePath);

            //    if (!File.Exists(privateKeyPath))
            //    {
            //        MessageBox.Show("Không tồn tại khóa riêng tư", "Thông báo");
            //    }
            //    else
            //    {
            //        rsa.FromXmlString(File.ReadAllText(privateKeyPath));

            //        // Mã hóa dữ liệu
            //        byte[] decryptedData = rsa.Decrypt(Encoding.UTF8.GetBytes(plainText), false);

            //        // Ghi dữ liệu đã mã hóa vào file
            //        File.WriteAllBytes(filePath, decryptedData);
            //        string content = File.ReadAllText(filePath);
            //        txtResult.Text = content;

            //        MessageBox.Show("File đã được giải hóa thành công", "Thông báo");
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Chưa chọn file cần giải mã" + ex.Message, "Thông báo");
            //}

            try
            {
                string privateKeyPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Keys\PrivateKey.xml"; // Đường dẫn tới khóa bí mật
                //string encryptedFile = "encrypted_text.txt"; // Đường dẫn tới file đã được mã hóa
                //string decryptedFile = "decrypted_text.txt"; // Đường dẫn tới file sau khi giải mã

                // Đọc dữ liệu đã mã hóa từ file
                byte[] encryptedData = File.ReadAllBytes(filePath);

                // Tạo đối tượng RSA từ khóa bí mật
                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));

                // Giải mã khóa đối xứng sử dụng RSA
                byte[] decryptData = rsa.Decrypt(encryptedData, false);

                
                // Ghi dữ liệu đã giải mã vào file
                File.WriteAllBytes(filePath, decryptData);
                string content = File.ReadAllText(filePath);
                txtResult.Text = content;
                MessageBox.Show("File đã được giải mã thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        //private void btnSavePuKey_Click(object sender, RoutedEventArgs e)
        //{
        //    string content = txtPublicKey.Text;

        //    // Đường dẫn cụ thể cho file lưu
        //    publicKeyPath = @"D:\Ky6\AT&BMTT\Code\RSA_WPF\RSA_WPF\Key\PublicKey.xml";
        //    if (!File.Exists(publicKeyPath))
        //    {
        //        // Tạo file mới nếu chưa tồn tại
        //        File.Create(publicKeyPath).Dispose();
        //    }
        //    if (string.IsNullOrEmpty(content))
        //    {
        //        MessageBox.Show("Chưa tạo khóa", "Thông báo");
        //    }
        //    else
        //    {
        //        // Lưu nội dung vào file
        //        File.WriteAllText(publicKeyPath, content);
        //        MessageBox.Show("Lưu khóa công khai thành công!", "Thông báo");
        //    }
        //}
    }
}

