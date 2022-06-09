using System.ComponentModel.DataAnnotations;

namespace Jopalesha.Common.Contract
{
    /// <summary>
    /// Get all request model.
    /// </summary>
    public class GetAllRequest
    {
        /// <summary>
        /// Gets or sets items count.
        /// </summary>
        [Range(1, 1000)]
        public int Count { get; set; } = 100;
    }
}
