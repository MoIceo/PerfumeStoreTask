using System;
using System.Collections.Generic;

namespace StoreLibrary.Models;

public partial class Product
{
    public string ProductArticleNumber { get; set; } = null!;

    public int ProductCategoryId { get; set; }

    public int ProductStatusId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public byte[]? ProductPhoto { get; set; }

    public string ProductManufacturer { get; set; } = null!;

    public decimal ProductCost { get; set; }

    public byte? ProductDiscountAmount { get; set; }

    public int ProductQuantityInStock { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ProductStatus ProductStatus { get; set; } = null!;
}
