using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRTest2
{
    public partial class Form1 : Form
    {

        private IHubProxy chat;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var hubConnection = new HubConnection("http://localhost:61575");
            chat = hubConnection.CreateHubProxy("chat");
            chat.On<string>("newMessage",
                msg => txtMessages.Invoke(new Action(() => txtMessages.Text += msg + "\r\n" ))
            );

            hubConnection.Start().Wait();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            chat.Invoke<string>("sendMEssage", txtMessage.Text);
        }
    }
}
