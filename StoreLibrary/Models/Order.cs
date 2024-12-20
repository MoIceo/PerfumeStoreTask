using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoreLibrary.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int PickupPointId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime? OrderDeliveryDate { get; set; }

    public DateTime OrderDate { get; set; }

    public short OrderPickupCode { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    [JsonIgnore]
    public virtual PickupPoint? PickupPoint { get; set; } = null!;
    [JsonIgnore]
    public virtual User? User { get; set; } = null!;
}
