using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Site.API.Nethereum
{
  public class TestClass
  {
    public async Task AbleToDeployAContract()
    {
      var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
      var password = "password";
      var abi = @"[{""constant"":false,""inputs"":[{""name"":""val"",""type"":""int256""}],""name"":""multiply"",""outputs"":[{""name"":""d"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""name"":""multiplier"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";
      var byteCode = "0x6060604052341561000f57600080fd5b6040516020806100d0833981016040528080516000555050609b806100356000396000f300606060405260043610603e5763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416631df4f14481146043575b600080fd5b3415604d57600080fd5b60566004356068565b60405190815260200160405180910390f35b60005402905600a165627a7a7230582081f6c0a33321c90e527d5faf375a1f3a3cdad15609e73b49d7148dd52f3335f80029";
      var multiplier = 7;
      var web3 = new Web3Geth();
      var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, new HexBigInteger(120));
      Assert.True(unlockAccountResult);
      var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, multiplier);
      var mineResult = await web3.Miner.Start.SendRequestAsync(6);
      Assert.True(mineResult);

      var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

      while (receipt == null)
      {
        Thread.Sleep(5000);
        receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
      }

      var contractAddress = receipt.ContractAddress;

      var contract = web3.Eth.GetContract(abi, contractAddress);

      var multiplyFunction = contract.GetFunction("multiply");

      var result = await multiplyFunction.CallAsync<int>(7);

      Assert.Equal(49, result);
    }
  }
}
