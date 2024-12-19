using System;
using System.Collections.Generic;

namespace StoreLibrary.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int PickupPointId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime? OrderDeliveryDate { get; set; }

    public DateTime OrderDate { get; set; }

    public short OrderPickupCode { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual PickupPoint PickupPoint { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
