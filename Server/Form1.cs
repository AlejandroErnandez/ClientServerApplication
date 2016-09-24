using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
                listener.Start();

                Thread th = new Thread(threadRead);
                th.IsBackground = true;
                th.Start();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket error: " + ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void threadRead()
        {
            TcpClient cl = listener.AcceptTcpClient();

            StreamReader sr = new StreamReader(cl.GetStream(), Encoding.Unicode);
            String str = sr.ReadLine();

            listBox1.Items.Add("Recieved from: " + cl.Client.RemoteEndPoint.ToString());
            listBox1.Items.Add(str);
            cl.Close();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (listener != null)
                listener.Stop();        
        }
    }
}
