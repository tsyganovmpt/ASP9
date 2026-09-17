using System;
using System.Collections.Generic;

namespace ApiAsp0.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int UsersId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User Users { get; set; } = null!;
}
