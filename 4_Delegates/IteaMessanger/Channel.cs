using System;
using System.Collections.Generic;
using System.Text;

namespace IteaDelegates.IteaMessanger
{
    public delegate void OnChannelMessage(string message);

    public class Channel
    {
        public string Name { get; }

        public List<Account> Subscribers { get; set; }

        public List<string> Messages { private set; get; }

        public OnChannelMessage NewMessage { get; set; }

        public Channel(string name)
        {
            Name = name;
            Subscribers = new List<Account>();
            Messages = new List<string>();
        }

        public void AddSubscriber(Account user)
        {
            Subscribers.Add(user);
        }

        public void SendMessage(string message)
        {
            Messages.Add(message);
            NewMessage?.Invoke(message);
        }
    }
}
