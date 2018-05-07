using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Exceptions;
using Exceptions.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Exceptions.Tests.Integ.Payments
{
    [Collection(Constants.TestCollections.Payment)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PaymentTests : IClassFixture<PaymentFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly PaymentFixture _classFixture;

        public PaymentTests(TestsFixture fixture, PaymentFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowWhenSendInvoiceInvalidJson)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        public async Task Should_Throw_When_Send_Invoice_Invalid_Provider_Data()
        {
            await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldThrowWhenSendInvoiceInvalidJson,
                chatid: _classFixture.PrivateChat.Id);

            const string payload = "my-payload";

            LabeledPrice[] prices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "CAD",
                StartParameter = "start_param",
                TotalAmount = prices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                BotClient.SendInvoiceAsync(
                    chatId: (int)_classFixture.PrivateChat.Id,
                    title: invoice.Title,
                    description: invoice.Description,
                    payload: payload,
                    providerToken: _classFixture.PaymentProviderToken,
                    startParameter: invoice.StartParameter,
                    currency: invoice.Currency,
                    prices: prices,
                    providerData: "INVALID-JSON"
                ));

            // ToDo: Add exception
            Assert.IsType<BadRequestException>(exception);
            Assert.Equal("DATA_JSON_INVALID", exception.Message);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowWhenAnswerShippingQueryWithDuplicateShippingId)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        public async Task Should_Throw_When_Answer_Shipping_Query_With_Duplicate_Shipping_Id()
        {
            await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldThrowWhenAnswerShippingQueryWithDuplicateShippingId,
                chatid: _classFixture.PrivateChat.Id);

            const string payload = "my-payload";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "USD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            await _fixture.BotClient.SendInvoiceAsync(
                chatId: (int)_classFixture.PrivateChat.Id,
                title: invoice.Title,
                description: invoice.Description,
                payload: payload,
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: productPrices,
                isFlexible: true
            );

            LabeledPrice[] shippingPrices =
            {
                new LabeledPrice("PART_OF_SHIPPING_TOTAL_PRICE_1", 500),
                new LabeledPrice("PART_OF_SHIPPING_TOTAL_PRICE_2", 299),
            };

            ShippingOption shippingOption = new ShippingOption
            {
                Id = "option1",
                Title = "OPTION-1",
                Prices = shippingPrices,
            };

            Update shippingUpdate = await GetShippingQueryUpdate();

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.AnswerShippingQueryAsync(
                    shippingQueryId: shippingUpdate.ShippingQuery.Id,
                    shippingOptions: new[] { shippingOption, shippingOption }
                )
            );

            // ToDo: Add exception
            Assert.IsType<BadRequestException>(exception);
            Assert.Equal("SHIPPING_ID_DUPLICATE", exception.Message);

            await _fixture.BotClient.AnswerShippingQueryAsync(
                shippingQueryId: shippingUpdate.ShippingQuery.Id,
                errorMessage: "✅ Test Passed"
            );
        }

        private static class FactTitles
        {
            public const string ShouldThrowWhenSendInvoiceInvalidJson =
                "Should throw exception when sending invoice with invalid provider data";

            public const string ShouldThrowWhenAnswerShippingQueryWithDuplicateShippingId =
                "Should throw exception when answering shipping query with duplicate shipping Id";
        }
    }
}
