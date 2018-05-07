using Telegram.Bot.Types;

namespace Exceptions.Tests.Integ.Admin_Bot
{
    public class AdminBotTestFixture
    {
        public ChatId ChatId { get; set; }

        public string ChatTitle { get; set; } = "Test Chat Title";

        public Message PinnedMessage { get; set; }
    }
}
