using Encoder.Client.Interfaces;
using Encoder.Client.Services;
using System;

namespace Encoder.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Hello! Please, enter the message you want to proceed.");
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                    break;
                var id = Guid.NewGuid();

                IMessageDatabaseFacade dbFacade = new MessageDatabaseFacade();
                var inserted = dbFacade.InsertMessage(id.ToString(), message);
                if (!inserted)
                    break;
            }
        }
    }
}
