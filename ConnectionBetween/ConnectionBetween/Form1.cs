using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SuperSimpleTcp;

namespace ConnectionBetween
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Creation of the server...");
            // instantiate
            SimpleTcpServer server = new SimpleTcpServer(IP.Text);

            // set events
            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;

            // let's go!
            server.Start();

           listBox1.Items.Add("The server " + IP.Text + " is now created");

           server.Send("[ClientIp:Port]", "Hello, world!");

            IP.Visible = false;
            button1.Visible = false;
 
        }

        public void ClientConnected(object sender, ConnectionEventArgs e)
        {
            listBox1.Invoke(new MethodInvoker(delegate
            {
                listBox1.Items.Add($"[{e.IpPort}] client connected");
            }));    
        }

        public void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            listBox1.Invoke(new MethodInvoker(delegate
            {
                listBox1.Items.Add($"[{e.IpPort}] client disconnected: {e.Reason}");
            }));
            
        }

        public void DataReceived(object sender, DataReceivedEventArgs e)
        {
            listBox1.Invoke(new MethodInvoker(delegate
            {
                listBox1.Items.Add($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
            }));         
        }
    }
}
