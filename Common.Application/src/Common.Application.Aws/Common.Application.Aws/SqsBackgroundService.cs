using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Infrastructure.Serializer;

namespace Common.Application.Aws
{
    /// <summary>
    /// Base service for pooling messages from SQS.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SqsBackgroundService<T> : RepeatableBackgroundService
    {
        private readonly ISerializer _serializer;
        private readonly IAmazonSQS _sqsClient;
        private readonly SqsOptions _options;

        protected SqsBackgroundService(
            ISerializer serializer, 
            IAmazonSQS client, 
            SqsOptions options)
        {
            _serializer = serializer;
            _sqsClient = client;
            _options = options;
        }

        /// <summary>
        /// Message handler.
        /// </summary>
        /// <param name="message">Message to handle.</param>
        protected abstract Task OnMessage(T message);

        /// <summary>
        /// Hosted service handler.
        /// </summary>
        /// <param name="token">Stopping token.</param>
        protected override async Task Execute(CancellationToken token)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _options.QueueUrl,
                WaitTimeSeconds = _options.WaitTimeSeconds,
                MaxNumberOfMessages = _options.MaxNumberOfMessages
            };

            var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest, token);

            var messagesToBeDeleted = new List<DeleteMessageBatchRequestEntry>();

            foreach (var message in receiveMessageResponse.Messages)
            {
                var messageBody = message?.Body;

                if (!string.IsNullOrEmpty(messageBody))
                {
                    var modelEvent = _serializer.DeserializeObject<T>(messageBody);
                    await OnMessage(modelEvent);

                    messagesToBeDeleted.Add(
                        new DeleteMessageBatchRequestEntry(message.MessageId, message.ReceiptHandle));
                }
            }

            await DeleteMessages(messagesToBeDeleted, token);
        }

        private async Task DeleteMessages(List<DeleteMessageBatchRequestEntry> entries, CancellationToken token)
        {
            if (entries.Count <= 0)
            {
                return;
            }

            var deleteMessageRequest = new DeleteMessageBatchRequest(_options.QueueUrl, entries);
            await _sqsClient.DeleteMessageBatchAsync(deleteMessageRequest, token);
        }
    }
}
