using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

using static System.Console;

namespace AzStorageQueue.GettingStarted
{
    internal class AzStorageQueueHelper
    {

        public static QueueClient CreateQueueClient(string connectionString, string queueName)
        {
            return new QueueClient(connectionString, queueName);
        }

        public static async Task<bool> CreateQueue(QueueClient queueClient)
        {
            try
            {
                // Create the queue
                await queueClient.CreateIfNotExistsAsync();

                if (queueClient.Exists())
                {
                    WriteLine($"Queue created: '{queueClient.Name}'");
                    return true;
                }
                else
                {
                    WriteLine($"Make sure the Azurite storage emulator running and try again.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Exception: {ex.Message}\n\n");
                WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return false;
            }
        }

        public static async Task InsertMessage(QueueClient queueClient, string queueName, string message)
        {
            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                SendReceipt sendReceipt = await queueClient.SendMessageAsync(message);
                WriteLine($"Content of Send Receipt: {sendReceipt}");
            }

            WriteLine($"Inserted: {message}");
        }

        public static async Task PeekMessage(QueueClient queueClient, string queueName)
        {
            if (queueClient.Exists())
            {
                // Peek at the next message
                PeekedMessage[] peekedMessage = await queueClient.PeekMessagesAsync();

                // Display the message
                WriteLine($"Peeked message: '{peekedMessage[0].Body}'");
            }
        }

        public static async Task UpdateMessage(QueueClient queueClient, string queueName)
        {
            if (queueClient.Exists())
            {
                // Get the message from the queue
                QueueMessage[] message = await queueClient.ReceiveMessagesAsync();

                // Update the message contents
                await queueClient.UpdateMessageAsync(message[0].MessageId,
                        message[0].PopReceipt,
                        $"Updated contents :: {message[0].Body}",
                        TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
                    );
            }
        }
    }

}
