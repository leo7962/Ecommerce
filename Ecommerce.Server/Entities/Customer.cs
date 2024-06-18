﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities
{
    public class Customer
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}