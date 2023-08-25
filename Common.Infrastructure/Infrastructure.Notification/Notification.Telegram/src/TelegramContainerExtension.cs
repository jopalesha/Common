using Jopalesha.Common.Infrastructure.Configuration;
using Jopalesha.Common.Infrastructure.Notification;
using SimpleInjector;

namespace Jopalesha.Common.Notification.Telegram
{
    public static class TelegramContainerExtension
    {
        public static void UseTelegramNotification(this Container container)
        {
            container.Register(() =>
                container.GetInstance<IConfiguration>().GetSection<TelegramOptions>("telegram"), Lifestyle.Singleton);
            container.Register<INotificationService, TelegramNotificationService>(Lifestyle.Scoped);
        }
    }
}
