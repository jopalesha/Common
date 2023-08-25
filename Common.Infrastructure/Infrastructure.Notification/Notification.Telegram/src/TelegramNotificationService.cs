using System;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Infrastructure.Notification;
using MihaZupan;
using Telegram.Bot;

namespace Jopalesha.Common.Notification.Telegram
{
    public class TelegramNotificationService : INotificationService
    {
        private readonly Lazy<TelegramBotClient> _botClient;
        private readonly string _chatId;

        public TelegramNotificationService(TelegramOptions options)
        {
            var proxy = new HttpToSocks5Proxy(options.Ip, options.Port, options.Login, options.Password)
            {
                ResolveHostnamesLocally = true
            };
            _botClient = new Lazy<TelegramBotClient>(() => new TelegramBotClient(options.Token, proxy));
            _chatId = options.ChatId;
        }

        public async Task SendMessage(string message, CancellationToken token)
        {
            await _botClient.Value.SendTextMessageAsync(_chatId, message, cancellationToken: token)
                .ConfigureAwait(false);
        }
    }
}