using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotExo.Models
{
    public enum MessageType { Info, Erreur, Victoire };
    public class RobotEventArgs : EventArgs
    {
        public string Message = string.Empty;
        public MessageType MessageType;

        public RobotEventArgs(string msg, MessageType mt) 
        { 
            Message = msg;
            MessageType = mt;
        }
        

    }
}
