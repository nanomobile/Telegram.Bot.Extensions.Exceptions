using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Tests.Integ.Framework;
using Exceptions.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Exceptions.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.AlbumMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class AlbumMessageTests : IClassFixture<EntitiesFixture<Message>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly EntitiesFixture<Message> _classFixture;

        private readonly TestsFixture _fixture;

        public AlbumMessageTests(TestsFixture testsFixture, EntitiesFixture<Message> classFixture)
        {
            _fixture = testsFixture;
            _classFixture = classFixture;
        }

        /// <remarks>
        /// URLs have a redundant query string to make sure Telegram doesn't use cached images
        /// </remarks>
        [OrderedFact(DisplayName = FactTitles.ShouldSendUrlPhotosInAlbum)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMediaGroup)]
        public async Task Should_Send_Photo_Album_Using_Url()
        {
            // ToDo add exception: Bad Request: failed to get HTTP URL content
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendUrlPhotosInAlbum);

            const string url1 = "https://cdn.pixabay.com/photo/2017/06/20/19/22/fuchs-2424369_640.jpg";
            const string url2 = "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg";
            int replyToMessageId = _classFixture.Entities.First().MessageId;

            Message[] messages = await BotClient.SendMediaGroupAsync(
                chatId: _fixture.SupergroupChat.Id,
                media: new[]
                {
                    new InputMediaPhoto {Media = url1},
                    new InputMediaPhoto {Media = url2},
                },
                replyToMessageId: replyToMessageId
            );

            Assert.Equal(2, messages.Length);
            Assert.All(messages, msg => Assert.Equal(MessageType.Photo, msg.Type));
            Assert.All(messages, msg => Assert.Equal(replyToMessageId, msg.ReplyToMessage.MessageId));
        }

        private static class FactTitles
        {
            public const string ShouldSendUrlPhotosInAlbum =
                "Should send an album using HTTP urls in reply to 1st album message";
        }
    }
}
