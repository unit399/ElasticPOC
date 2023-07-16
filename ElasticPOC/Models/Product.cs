using Shared.Core.Entities.Common;

namespace ElasticPOC.Models;

public class Product : AuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Price { get; set; }
    public int Quantity { get; set; }
}