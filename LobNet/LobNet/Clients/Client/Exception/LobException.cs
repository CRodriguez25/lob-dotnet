using System;

namespace LobNet.Clients.Client
{
    public class LobException : System.Exception
    {
        public LobException(string message) : base(message)
        {
        }
    }
}