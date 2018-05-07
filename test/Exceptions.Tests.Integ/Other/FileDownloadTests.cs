using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Extensions.Exceptions;
using Exceptions.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;
using Xunit.Abstractions;

namespace Exceptions.Tests.Integ.Other {
    [Collection (Constants.TestCollections.FileDownload)]
    [TestCaseOrderer (Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class FileDownloadTests : IClassFixture<FileDownloadTests.Fixture> {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly ITestOutputHelper _output;

        private readonly Fixture _classFixture;

        private readonly TestsFixture _fixture;

        public FileDownloadTests (TestsFixture fixture, Fixture classFixture, ITestOutputHelper output) {
            _fixture = fixture;
            _classFixture = classFixture;
            _output = output;
        }

        [OrderedFact (DisplayName = FactTitles.ShouldThrowInvalidParameterExceptionForFileId)]
        [Trait (Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
        public async Task Should_Throw_FileId_InvalidParameterException () {
            await _fixture.SendTestCaseNotificationAsync (FactTitles.ShouldThrowInvalidParameterExceptionForFileId);

            InvalidParameterException exception = await Assert.ThrowsAnyAsync<InvalidParameterException> (
                () => BotClient.GetFileAsync ("Invalid_File_id")
            );

            Assert.Equal ("file id", exception.Parameter);
        }

        [OrderedFact (DisplayName = FactTitles.ShouldThrowInvalidHttpRequestExceptionForFilePath)]
        public async Task Should_Throw_FilePath_HttpRequestException () {
            await _fixture.SendTestCaseNotificationAsync (FactTitles.ShouldThrowInvalidHttpRequestExceptionForFilePath);

            System.IO.Stream content = default;

            HttpRequestException exception = await Assert.ThrowsAnyAsync<HttpRequestException> (async () => {
                content = await BotClient.DownloadFileAsync ("Invalid_File_Path");
            });

            Assert.Contains ("404", exception.Message);
            Assert.Null (content);
        }

        private static class FactTitles {
            public const string ShouldThrowInvalidParameterExceptionForFileId =
                "Should throw InvalidParameterException while trying to get file using wrong file_id";

            public const string ShouldThrowInvalidHttpRequestExceptionForFilePath =
                "Should throw HttpRequestException while trying to download file using wrong file_path";
        }

        public class Fixture {
            public const string FileType = "pdf";

            public File File { get; set; }
        }
    }
}
