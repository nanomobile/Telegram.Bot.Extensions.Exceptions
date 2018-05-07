using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Exceptions;
using Exceptions.Tests.Integ.Framework;
using Exceptions.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Xunit;

namespace Exceptions.Tests.Integ.Admin_Bot
{
    [Collection(Constants.TestCollections.ChannelAdminBots)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChannelAdminBotTests : IClassFixture<ChannelAdminBotTests.Fixture>
    {
        private readonly AdminBotTestFixture _classFixture;

        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public ChannelAdminBotTests(TestsFixture testsFixture, Fixture classFixture)
        {
            _fixture = testsFixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowOnDeletingChatDeletedPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnDeletingChatDeletedPhoto);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.DeleteChatPhotoAsync(_classFixture.ChatId));

            Assert.IsType<ChatNotModifiedException>(exception);
            Assert.Equal("CHAT_NOT_MODIFIED", exception.Message);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowOnSetChannelStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnSetChannelStickerSet);

            const string setName = "EvilMinds";

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                _fixture.BotClient.SetChatStickerSetAsync(_classFixture.ChatId, setName)
            );

            Assert.IsType<WrongChatTypeException>(exception);
            Assert.Equal("method is available only for supergroups", exception.Message);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldThrowOnDeletingChatDeletedPhoto =
                "Should throw exception in deleting chat photo with no photo currently set";

            public const string ShouldThrowOnSetChannelStickerSet =
                "Should throw exception when trying to set sticker set for a channel";
        }

        public class Fixture : AdminBotTestFixture
        {
            public Fixture(TestsFixture fixture)
            {
                ChatId = new ChannelChatFixture(fixture, Constants.TestCollections.ChannelAdminBots).ChannelChatId;
            }
        }
    }

}
