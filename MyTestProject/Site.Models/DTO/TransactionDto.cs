using Nethereum.Hex.HexTypes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Models.DTO
{
  public class TransactionDto
  {
    public virtual Guid? Id { get; set; }

    public Guid UserId { get; set; }

    public string Sender { get; set; }

    public string Receiver { get; set; }

    public string Password { get; set; }

    public decimal Amount { get; set; }

    public bool Confirmed { get; set; }
  }
}
