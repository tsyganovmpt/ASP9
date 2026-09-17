using System;
using System.Collections.Generic;

namespace ApiAsp0.Models;

public partial class OrderItem
{
    public int IdOrderItem { get; set; }

    public int CartItemId { get; set; }

    public decimal OrderPrice { get; set; }

    public int Quantity { get; set; }

    public int OrderId { get; set; }

    public virtual CartItem CartItem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
