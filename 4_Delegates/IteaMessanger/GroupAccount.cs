
using System;
using static ITEA_Collections.Common.Extensions;

namespace IteaDelegates.IteaMessanger
{
    public class Group
    {
        public string Name { get; private set; }

        public event OnSend OnSend;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название группы</param>
        public Group(string name)
        {
            Name = name;
            ToConsole($"Group {Name} was created!", ConsoleColor.Green);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(Message message)
        {
            OnSend?.Invoke(this, new OnSendEventArgs(message.ReadMessage(message.From), message.From.Username, Name));
        }


    }
}
