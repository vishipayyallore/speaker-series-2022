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
                await EnsureQueueExists(queueClient);

                WriteLine($"Queue created: '{queueClient.Name}'");
                return true;
            }
            catch (Exception ex)
            {
                WriteLine($"Exception: {ex.Message}\n\n");
                WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return false;
            }
        }

        public static async Task InsertMessages(QueueClient queueClient, List<string> messages)
        {
            await EnsureQueueExists(queueClient);

            foreach (var message in messages)
            {
                SendReceipt sendReceipt = await queueClient.SendMessageAsync(message);

                WriteLine($"Inserted: {message}");
                WriteLine($"Content of Send Receipt: MessageId = {sendReceipt.MessageId} InsertionTime = {sendReceipt.InsertionTime} ExpirationTime = {sendReceipt.ExpirationTime}");
            }
        }

        public static async Task PeekMessage(QueueClient queueClient)
        {
            await EnsureQueueExists(queueClient);

            PeekedMessage[] peekedMessage = await queueClient.PeekMessagesAsync();

            WriteLine($"Peeked message: '{peekedMessage[0].Body}'");
        }

        public static async Task UpdateMessage(QueueClient queueClient)
        {
            await EnsureQueueExists(queueClient);

            QueueMessage[] message = await queueClient.ReceiveMessagesAsync();
            var updatedContent = $"{message[0].Body} \nUpdated contents on {DateTime.Now}.";

            WriteLine($"Updating -> {message[0].Body} to {updatedContent}");

            await queueClient.UpdateMessageAsync(message[0].MessageId, message[0].PopReceipt,
                    updatedContent, TimeSpan.FromSeconds(10.0)); // Make it invisible for another 10 seconds
        }

        public static async Task DequeueMessages(QueueClient queueClient)
        {
            await EnsureQueueExists(queueClient);

            QueueProperties properties = await queueClient.GetPropertiesAsync();

            int messagesCountForRetrieval = 2;
            int cachedMessagesCount = properties.ApproximateMessagesCount;
            WriteLine($"Number of messages in queue: {cachedMessagesCount}");

            QueueMessage[] receivedMessages = await queueClient.ReceiveMessagesAsync(messagesCountForRetrieval, TimeSpan.FromMinutes(5));

            foreach (QueueMessage message in receivedMessages)
            {
                Console.WriteLine($"De-queued message: '{message.Body}'");

                await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }
        }

        private static async Task EnsureQueueExists(QueueClient queueClient)
        {
            if (!queueClient.Exists())
            {
                await queueClient.CreateIfNotExistsAsync();
            }
        }

    }

}
