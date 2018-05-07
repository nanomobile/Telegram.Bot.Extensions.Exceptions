using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Exceptions.Tests.Integ.Inline_Mode
{
    [Collection(Constants.TestCollections.InlineQuery)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class InlineQueryTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public InlineQueryTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithHtmlVideo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        public async Task Should_Answer_Inline_Query_With_HTML_Video()
        {
            // ToDo exception when input_message_content not specified. Bad Request: SEND_MESSAGE_MEDIA_INVALID

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithHtmlVideo,
                startInlineQuery: true);

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            const string resultId = "fireworks_video";
            InlineQueryResultBase[] results =
            {
                new InlineQueryResultVideo(
                    id: resultId,
                    videoUrl: "https://www.youtube.com/watch?v=56MDJ9tD6MY",
                    mimeType: "text/html",
                    thumbUrl: "https://www.youtube.com/watch?v=56MDJ9tD6MY",
                    title: "30 Rare Goals We See in Football"
                )
                {
                    InputMessageContent =
                        new InputTextMessageContent(
                            "[30 Rare Goals We See in Football](https://www.youtube.com/watch?v=56MDJ9tD6MY)")
                        {
                            ParseMode = ParseMode.Markdown
                        }
                }
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: iqUpdate.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );

            (Update messageUpdate, Update chosenResultUpdate) = await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(MessageType.Text);
            Update resultUpdate = chosenResultUpdate;

            Assert.Equal(MessageType.Text, messageUpdate.Message.Type);
            Assert.Equal(resultId, resultUpdate.ChosenInlineResult.ResultId);
            Assert.Equal(iqUpdate.InlineQuery.Query, resultUpdate.ChosenInlineResult.Query);
        }

        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithHtmlVideo =
                "Should answer inline query with a YouTube video (HTML page)";
        }
    }
}
