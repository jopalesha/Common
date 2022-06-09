using System;
using Amazon.SQS.Model;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Application.BackgroundServices;

namespace Common.Application.Aws
{
    /// <summary>
    /// AWS SQS Options
    /// </summary>
    public class SqsOptions : BackgroundServiceOptions
    {
        public SqsOptions(
            TimeSpan interval,
            string queueUrl,
            int maxNumberOfMessages = 1,
            int waitTimeInSeconds = 30) : base(interval)
        {
            QueueUrl = Check.NotEmpty(queueUrl);
            MaxNumberOfMessages = Check.InBounds(maxNumberOfMessages, 1, 10, nameof(maxNumberOfMessages));
            WaitTimeSeconds = Check.InBounds(waitTimeInSeconds, 1, 600, nameof(waitTimeInSeconds));
        }

        ///<inheritdoc cref="ReceiveMessageRequest.QueueUrl"/>
        public string QueueUrl { get; }

        ///<inheritdoc cref="ReceiveMessageRequest.MaxNumberOfMessages"/>
        public int MaxNumberOfMessages { get; }

        ///<inheritdoc cref="ReceiveMessageRequest.WaitTimeSeconds"/>
        public int WaitTimeSeconds { get; }
    }
}