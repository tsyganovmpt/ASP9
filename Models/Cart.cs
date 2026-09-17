using System;
using System.Collections.Generic;

namespace ApiAsp0.Models;

public partial class Cart
{
    public int IdCart { get; set; }

    public int UsersId { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User Users { get; set; } = null!;
}
