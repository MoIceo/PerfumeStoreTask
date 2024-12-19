using System;
using System.Collections.Generic;

namespace PerfumeLibrary.Models;

public partial class OrderProduct
{
    public int OrderId { get; set; }

    public int ProductArticleNumber { get; set; }

    public string ProductAmount { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
}
