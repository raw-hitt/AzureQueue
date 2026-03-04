using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;


namespace AzureQueues
{
    internal class Azure_Queue
    {
        private static string storage_connection_string = "";
        private static string queue_name = "queuebyrohit";

        public QueueClient _client { get; set; }
        public Azure_Queue()
        {
            _client = new QueueClient(storage_connection_string, queue_name);
            _client.CreateIfNotExists();

        }
        public void AddQueue()
        {


            try
            {
                string _message;
                if (_client.Exists())
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _message = $"Hello this is a test message {i}";
                        _client.SendMessage(_message);
                    }
                    Console.WriteLine("All the messages have been sent");
                }
                else
                {
                    Console.WriteLine("Queue does not exist");
                }


            }
            catch (Exception e)
            {

                Console.WriteLine("Error when inserting into Queue " + e.Message);
            }
        }

        public void PeekQueue(int msgCount)
        {
            if (_client.Exists())
            {
                PeekedMessage[] _messages = _client.PeekMessages(msgCount);
                foreach (PeekedMessage _message in _messages)
                {
                    Console.WriteLine($"The Message ID is {_message.MessageId}");
                    Console.WriteLine($"The Message was inserted on {_message.InsertedOn}");
                    Console.WriteLine($"The Message body is {_message.Body.ToString()}");
                }
            }
        }

        public void ReceiveMsg()
        {
            if (_client.Exists())
            {
                QueueMessage _queue_message = _client.ReceiveMessage();

                Console.WriteLine(_queue_message.Body.ToString());

                _client.DeleteMessage(_queue_message.MessageId, _queue_message.PopReceipt);

                Console.WriteLine("Message deleted");
            }
        }

        public void AddQueueBase64()
        {


            try
            {
                string _message;
                if (_client.Exists())
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _message = $"Hello this is a test message {i}";

                        //convert the message to base64 string
                        var txtbytes=Encoding.UTF8.GetBytes(_message);
                        string Base64String=System.Convert.ToBase64String(txtbytes);

                        _client.SendMessage(Base64String);
                    }
                    Console.WriteLine("All the messages have been sent");
                }
                else
                {
                    Console.WriteLine("Queue does not exist");
                }


            }
            catch (Exception e)
            {

                Console.WriteLine("Error when inserting into Queue " + e.Message);
            }
        }

    }
}
