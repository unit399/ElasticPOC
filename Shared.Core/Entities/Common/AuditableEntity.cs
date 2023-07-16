using System.Globalization;

namespace Shared.Core.Entities.Common;

public class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = DateTime.Now.ToString(CultureInfo.InvariantCulture);
    public string? UpdatedBy { get; set; }
}