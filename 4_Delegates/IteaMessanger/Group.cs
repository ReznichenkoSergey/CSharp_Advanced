
using System;
using System.Collections.Generic;
using System.Linq;
using static ITEA_Collections.Common.Extensions;

namespace IteaDelegates.IteaMessanger
{
    public class Group : Account
    {
        ///
        /// Название группы
        ///
        //public string Name { get; private set; }

        ///
        ///Список сообщений 
        ///
        //public  List<Message> Messages { get; private set; }

        public new event OnSend OnSend;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название группы</param>
        public Group(string name) : base(name)
        {
            //Name = name;
            this.Messages = new List<Message>();
            ToConsole($"Group {Username} was created!", ConsoleColor.Green);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(Message message)
        {
            this.Messages.Add(message);
            OnSend?.Invoke(this, new OnSendEventArgs(message.ReadMessage(message.From), message.From.Username, Username));
        }

        ///
        ///Вывод всех сообщений
        ///
        public void ShowAllMessages()
        {
            var messageDialog = Messages
                .OrderBy(x => x.Created)
                .ToList();
            string str = $"All messages";
            ToConsole($"---{str}---");
            foreach (Message message in messageDialog)
            {
                ToConsole($"{message.From.Username}: {message.ReadMessage(message.From)}", ConsoleColor.DarkYellow);
            }
            ToConsole($"---{string.Concat(str.Select(x => "-"))}---");
        }

        ///
        ///Вывод всех сообщений по имени отправителя
        ///
        public void ShowMessagesByUser(string username)
        {
            List<Message> messageDialog = Messages
                .Where(x => x.From.Username.Equals(username))
                .Where(x => x.Send)
                .OrderBy(x => x.Created)
                .ToList();
            string str = $"Dialog with {username}";
            ToConsole($"---{str}---");
            foreach (Message message in messageDialog)
            {
                ToConsole($"{message.From.Username}: {message.ReadMessage(message.From)}", ConsoleColor.DarkYellow);
            }
            ToConsole($"---{string.Concat(str.Select(x => "-"))}---");
        }
    }
}
