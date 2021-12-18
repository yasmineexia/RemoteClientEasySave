using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteClientEasySave.Models
{
    class ConnectMessage : Message
    {
        public string Password;
    }
    class Message
    {
        public static readonly string OK_MESSAGE = "OK";
        public bool Error;
        public string CMD;
        public List<Backup> obj;
    }
    internal class Client
    {
        Socket socket;
        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public bool SeConnecter(string ip,string password, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                socket.Connect(endPoint);
                Message response = sendData(new ConnectMessage { Password = password, Error = false });
                return !response.Error;

            }
            catch (SocketException e)
            {
                return false;
            }
        }

        internal List<Backup> GetTasks()
        {
            Message res = sendData(new Message { CMD = "GetTasks", Error = false }) ;
            return res.obj;
            
        }

        public Message sendData(Message data)
        {
            byte[] dataRep = new Byte[1024];
            int i = 0;
            dataRep = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            socket.Send(dataRep, dataRep.Length, SocketFlags.None);
            dataRep = new Byte[1024];
            int bytesRec = socket.Receive(dataRep);
            string a = Encoding.UTF8.GetString(dataRep, 0, bytesRec);
            return JsonConvert.DeserializeObject<Message>(a);
        }
        private void Deconnecter()
        {
            // Afficher le message « Déconnexion du serveur en cours … "  
            Console.WriteLine("Deconection en cours");
            // Arrêter la communication socket passé en paramètre dans les deux sens entre le Serveur et le Client
            socket.Shutdown(SocketShutdown.Both);
            // Fermer le socket passé en paramètre 
            socket.Close();
            // Afficher le message « Déconnexion su serveur terminée. "  
            Console.WriteLine("Deconnexion finit");
        }
    }
}

