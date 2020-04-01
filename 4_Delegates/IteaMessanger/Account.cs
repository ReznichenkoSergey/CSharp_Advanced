using System;
using System.Collections.Generic;
using System.Linq;

using static ITEA_Collections.Common.Extensions;

namespace IteaDelegates.IteaMessanger
{
    public delegate void OnMessage(Message message);
    public delegate void OnSend(object sender, OnSendEventArgs e);
    
    public class Account
    {
        public string Username { get; private set; }
        public List<Message> Messages { get; set; }

        public event OnSend OnSend;

        public OnMessage NewMessage { get; set; }

        public Account(string username)
        {
            Username = username;
            Messages = new List<Message>();
            NewMessage += OnNewMessage;
        }

        public Message CreateMessage(string text, Account to)
        {
            var message = new Message(this, to, text);
            Messages.Add(message);
            return message;
        }

        public void Send(Message message)
        {
            message.Send = true;
            message.To.Messages.Add(message);
            message.To.NewMessage(message);
            OnSend?.Invoke(this, new OnSendEventArgs(message.ReadMessage(this), message.From.Username, message.To.Username));
        }


        #region Group Mode

        /// <summary>
        /// Подписка на событие
        /// </summary>
        /// <param name="group"></param>
        /// <param name="isShortMessage"></param>        
        public void Subscribe(Group group, bool isShortMessage)
        {
            if (isShortMessage)
            {
                group.OnSend += GroupShortMessage;
                ToConsole($"{Username} was subscribed in the {group.Name}! Short type.", ConsoleColor.Yellow);
            }
            else
            {
                group.OnSend += GroupStandartMessage;
                ToConsole($"{Username} was subscribed in the {group.Name}! Standart type.", ConsoleColor.DarkYellow);
            }
        }

        /// <summary>
        /// Отправка сообщения в группу
        /// </summary>
        /// <param name="message"></param>
        /// <param name="group"></param>
        public void SendGroupMessage(Message message, Group group)
        {
            message.Send = true;
            group.SendMessage(message);
        }

        /// <summary>
        /// Стандартное уведомление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GroupStandartMessage(object sender, OnSendEventArgs e)
        {
            ToConsole($"Сообщение от {e.From}: {e.Text}", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Краткое уведомление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GroupShortMessage(object sender, OnSendEventArgs e)
        {
            ToConsole($"В группе {e.To} новое сообщение!", ConsoleColor.DarkYellow);
        }

        #endregion

        public void OnNewMessage(Message message)
        {
            if (message.Send)
                ToConsole($"OnNewMessage: {message.From.Username}: {message.Preview}", ConsoleColor.DarkYellow);
        }
        
        public void ShowDialog(string username)
        {
            List<Message> messageDialog = Messages
                .Where(x => x.To.Username.Equals(username) || x.From.Username.Equals(username))
                .Where(x => x.Send)
                .OrderBy(x => x.Created)
                .ToList();
            string str = $"Dialog with {username}";
            ToConsole($"---{str}---");
            foreach (Message message in messageDialog)
            {
                ToConsole($"{(message.From.Username.Equals(username) ? username : Username)}: {message.ReadMessage(this)}",
                    message.From.Username.Equals(username) ? ConsoleColor.Cyan : ConsoleColor.DarkYellow);
            }
            ToConsole($"---{string.Concat(str.Select(x => "-"))}---");
        }
    }
}
