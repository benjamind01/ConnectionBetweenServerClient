using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;

namespace Client
{
    public partial class Form1 : Form
    {
        SimpleTcpClient client;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Connection to " + IP.Text + "...");
            // instantiate
            client = new SimpleTcpClient(IP.Text);

            // set events
            client.Events.Connected += Connected;
            client.Events.Disconnected += Disconnected;
            client.Events.DataReceived += DataReceived;

            // let's go!
            client.Connect();

            IP.Visible = false;
            button1.Visible = false;
            msg.Visible = true;
            button2.Visible = true;
        }

        private void Connected(object sender, ConnectionEventArgs e)
        {  
            listBox1.Invoke(new MethodInvoker(delegate
            {
                listBox1.Items.Add($"Server {e.IpPort} connected");
            }));
        }

        private void Disconnected(object sender, ConnectionEventArgs e)
        {
            listBox1.Invoke(new MethodInvoker(delegate
            {
                listBox1.Items.Add($"Server {e.IpPort} disconnected");
            }));
        }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            listBox1.Invoke(new MethodInvoker(delegate
            {
                listBox1.Items.Add($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data)}");
            }));
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.Send(msg.Text);
            msg.Text = "";
        }
    }
}
