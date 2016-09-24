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
using System.Net;
using System.Net.Sockets;
namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient client;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), Int32.Parse(textBoxPort.Text));

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream netStream = client.GetStream();
                byte[] messageArray = Encoding.Unicode.GetBytes(textBoxMessage.Text);
                netStream.Write(messageArray, 0, messageArray.Length);

                client.Close();
            }
            catch(SocketException ex)
            {
                MessageBox.Show("Socket error: " + ex.Message, "Socket error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
