using System.Collections.Generic;
using Exceptions.Tests.Integ.Framework;
using Telegram.Bot.Types;

namespace Exceptions.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public StickerSet EvilMindsStickerSet { get; set; }

        public IEnumerable<File> UploadedStickers { get; set; }

        public string TestStickerSetName { get; }

        public StickerSet TestStickerSet { get; set; }

        public int OwnerUserId { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestStickerSetName = $"test14_by_{testsFixture.BotUser.Username}";
            int? ownerUserId = ConfigurationProvider.TestConfigurations.StickerOwnerUserId;
            if (ownerUserId == default)
            {
                // ToDo: use /me command to select owner at test execution time
                ownerUserId = 0;
            }

            OwnerUserId = ownerUserId.Value;
        }
    }
}
