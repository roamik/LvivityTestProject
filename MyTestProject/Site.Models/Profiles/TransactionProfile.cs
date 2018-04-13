using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Site.Models.DTO;
using Site.Models.Entities;

namespace Site.Models.Profiles
{
  public class TransactionProfile : Profile
  {
    public TransactionProfile()
    {
      CreateMap<Transaction, TransactionDto>().MaxDepth(1);
      CreateMap<TransactionDto, Transaction>().MaxDepth(1);
    }
  }
}
