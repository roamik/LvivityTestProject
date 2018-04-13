using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Models.Entities
{
  public class Transaction : Entity<Guid>
  {
    public Guid UserId { get; set; }

    public string Sender { get; set; }

    public string Receiver { get; set; }

    public decimal Amount { get; set; }

    public bool Confirmed { get; set; }

  }
}
