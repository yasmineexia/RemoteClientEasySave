using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        public object obj;
    }
    internal class Client
    {
        Socket socket;
        public Thread Thread;
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
            Debug.WriteLine(res);
            return ((JArray)res.obj).ToObject<List<Backup>>();
            
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

        public List<Backup> PauseTask(Backup backup)
        {
            var obj = new List<Backup>();
            obj.Add(backup);
            Message res = sendData(new Message { CMD="PauseTask",obj = obj, Error = false }) ;
            if (res.Error) return new List<Backup>();
            return ((JArray)res.obj).ToObject<List<Backup>>();
        }
        public List<Backup> StartTask(Backup backup)
        {
            var obj = new List<Backup> { backup };
            Message res = sendData(new Message { CMD="StartTask",obj=obj, Error = false }) ;
            if(res.Error) return new List<Backup>();
            return ((JArray)res.obj).ToObject<List<Backup>>();
        }
        public List<Backup> StopTask(Backup backup)
        {
            var obj = new List<Backup> { backup };
            Message res = sendData(new Message { CMD="StopTask",obj=obj, Error = false }) ;
            if(res.Error) return new List<Backup>();
            return ((JArray)res.obj).ToObject<List<Backup>>();
        }

    }
}

