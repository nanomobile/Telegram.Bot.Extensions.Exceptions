// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Extensions.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the sticker set is invalid
    /// </summary>
    public class StickerSetNameExistsException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="StickerSetNameExistsException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public StickerSetNameExistsException(string message)
            : base(message)
        {
        }
    }
}