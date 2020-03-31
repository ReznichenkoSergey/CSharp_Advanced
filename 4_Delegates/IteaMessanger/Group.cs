
using System;
using System.Collections.Generic;
using static ITEA_Collections.Common.Extensions;

namespace IteaDelegates.IteaMessanger
{
    public delegate void OnTypeMessage(Message message, bool isShort);
    
    public class Group
    {
        public string GroupName { get; private set; }
        /// <summary>
        /// Список аккаунтов
        /// </summary>
        List<Account> listAccounts;

        public OnTypeMessage NewTypeMessage { get; set; }

        public OnMessage NewMessage { get; set; }

        public event OnSend OnSend;

        public Group(string groupName)
        {
            GroupName = groupName;
            listAccounts = new List<Account>();
        }

        /*public void SendGroupMessage(string message, Account from)
        {
            foreach (Account to in this.listAccounts)
            {
                if (!from.Username.Equals(to.Username))
                {
                    Message message1 = new Message(from, to, message);
                    to.Messages.Add(message1);
                    to.NewMessage(message1);
                    OnSend?.Invoke(this, new OnSendEventArgs(message, from.Username, to.Username));
                }
            }
        }*/

        public void SendGroupMessage(Message incoming)
        {
            foreach (Account account in this.listAccounts)
            {
                if (!account.Username.Equals(incoming.From.Username))
                {
                    Message message = new Message(incoming.From, account, incoming.ReadMessage(incoming.From));
                    message.Send = true;
                    account.Messages.Add(message);
                    //
                    account.NewMessage(message);
                    OnSend?.Invoke(this, new OnSendEventArgs(incoming.ReadMessage(message.From), message.From.Username, message.To.Username));
                }
            }
        }

        /// <summary>
        /// Добавление нового подписчика
        /// </summary>
        /// <param name="account"></param>
        public void AddAccount(Account account)
        {
            var index = this.listAccounts.FindIndex(x => x.Username.Equals(account.Username, StringComparison.InvariantCultureIgnoreCase));
            if (index == -1)
            {
                NewMessage += account.OnNewMessage;
                //
                this.listAccounts.Add(account);
                //ToConsole($"'{account.Username}' was joined to the '{this.GroupName}' group!!!");
            }
            /*else
                ToConsole($"'{account.Username}' exists in the current group!!!");*/
        }

        /// <summary>
        /// Удаление подписчика
        /// </summary>
        /// <param name="account"></param>
        public void RemoveAccount(Account account)
        {
            listAccounts.RemoveAll(x => x.Username.Equals(account.Username, StringComparison.InvariantCultureIgnoreCase));
        }


    }
}
