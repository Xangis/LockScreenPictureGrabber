using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Threading.Tasks;

namespace LockScreenPictureGrabber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // http://lifehacker.com/how-to-save-the-windows-10-lock-screen-images-you-like-1768783711
            String localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            String path = localPath + "\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets";
            label1.Content = path;
            //String[] files = { };
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] files = di.GetFileSystemInfos();
            var orderedFiles = files.OrderBy(f => f.CreationTime);
            foreach (FileSystemInfo file in files)
            {
                try
                {
                    Image image = new Image();
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(file.FullName);
                    bmp.EndInit();
                    image.Source = bmp;
                    image.Width = 240;
                    if (image.Height > 300)
                    {
                        image.Height = 300;
                    }
                    listBox1.Items.Add(image);
                }
                catch( NotSupportedException e)
                {
                    // Pass. We ignore files that we don't know how to handle.
                }
            }
            textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Image.jpg";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String filename = ((BitmapImage)((Image)listBox1.SelectedItem).Source).UriSource.AbsolutePath;
            String destinationPath = textBox1.Text;
            File.Copy(filename, destinationPath);
            MessageBox.Show("Image saved to " + destinationPath);
        }
    }
}
