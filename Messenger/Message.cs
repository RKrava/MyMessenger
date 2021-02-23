using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger
{
    public class Message
    {
        public int ID { get; }
        public string Text { get; }
        public string Sender { get; }
        public string Reciever { get; }
        public HorizontalAlignment Alignment { get; }

		public DateTime date;

		public Message(int id, string text, string sender, string reciever, DateTime date)
		{
			ID = id;
			Text = text;
			Sender = sender;
			Reciever = reciever;
			this.date = date;
			Alignment = MessengerController.MyLogin == sender ? HorizontalAlignment.Right : HorizontalAlignment.Left;
			date = new DateTime();
		}
        public Message()
        {
            
        }
		public string Date => $"{date.Hour}:{date.Minute} {date.Day}.{date.Month}.{date.Year}";
    }
}
