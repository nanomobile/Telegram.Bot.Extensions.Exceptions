using System;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Exceptions.Tests.Integ.Admin_Bot
{
    [Collection(Constants.TestCollections.ChatMemberAdministration)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChatMemberAdministrationTests : IClassFixture<ChatMemberAdministrationTestFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly ChatMemberAdministrationTestFixture _classFixture;

        public ChatMemberAdministrationTests(TestsFixture fixture, ChatMemberAdministrationTestFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldPromoteUserToChangeChatInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        public async Task Should_Promote_User_To_Change_Chat_Info()
        {
            //ToDo exception when user isn't in group. Bad Request: bots can't add new chat members

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPromoteUserToChangeChatInfo);

            await BotClient.PromoteChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                canChangeInfo: true
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDemoteUser)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        public async Task Should_Demote_User()
        {
            //ToDo exception when user isn't in group. Bad Request: USER_NOT_MUTUAL_CONTACT

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDemoteUser);

            await BotClient.PromoteChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                canChangeInfo: false
            );
        }

        private static class FactTitles
        {
            public const string ShouldPromoteUserToChangeChatInfo =
                "Should promote chat member to change chat information";

            public const string ShouldDemoteUser =
                "Should demote chat member by taking his/her only admin right: change_info";
       }
    }
}
