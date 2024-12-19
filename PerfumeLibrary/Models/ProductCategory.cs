using System;
using System.Collections.Generic;

namespace PerfumeLibrary.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
