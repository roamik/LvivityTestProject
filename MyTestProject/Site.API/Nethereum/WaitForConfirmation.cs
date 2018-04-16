using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.API.Nethereum
{
  public class WaitForConfirmation
  {
    public async Task<bool> ConfirmedAsync(string transHash)
    {
      var web3 = new Web3();

      var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transHash);

      while (receipt == null)
      {
        Thread.Sleep(5000);
        receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transHash);
      }

      return receipt != null ? true : false;
    }
  }
}
