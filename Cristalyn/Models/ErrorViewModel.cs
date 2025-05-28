namespace Cristalyn.Models
{
    /// <summary>
    /// Model for handling and displaying error information to users
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the request ID associated with the error
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request ID should be shown
        /// </summary>
        /// <returns>True if RequestId is not null or empty; otherwise, false</returns>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
