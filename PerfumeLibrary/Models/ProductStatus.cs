using System;
using System.Collections.Generic;

namespace PerfumeLibrary.Models;

public partial class ProductStatus
{
    public int ProductStatusId { get; set; }

    public string ProductAvailability { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
