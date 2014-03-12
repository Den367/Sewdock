
namespace Mayando.Web.Models
{
    /// <summary>
    /// Indicates that an entity supports a sequence number.
    /// </summary>
    public interface ISupportSequence
    {
        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        int Sequence { get; set; }
    }
}