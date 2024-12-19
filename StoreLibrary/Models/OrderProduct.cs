using System;
using System.Collections.Generic;

namespace StoreLibrary.Models;

public partial class OrderProduct
{
    public int OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public string ProductAmount { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
}
