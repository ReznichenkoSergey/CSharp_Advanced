﻿
using System;
using System.Collections.Generic;
using System.Linq;
using static ITEA_Collections.Common.Extensions;

namespace IteaDelegates.IteaMessanger
{
    public class Group
    {
        ///
        /// Название группы
        ///
        public string Name { get; private set; }

        ///
        ///Список сообщений 
        ///
        public List<Message> Messages { get; private set; }

        public event OnSend OnSend;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название группы</param>
        public Group(string name)
        {
            Name = name;
            this.Messages = new List<Message>();
            ToConsole($"Group {Name} was created!", ConsoleColor.Green);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(Message message)
        {
            this.Messages.Add(message);
            OnSend?.Invoke(this, new OnSendEventArgs(message.ReadMessage(message.From), message.From.Username, Name));
        }

        ///
        ///Вывод всех сообщений
        ///
        public void ShowDialog()
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
        public void ShowDialog(string username)
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
