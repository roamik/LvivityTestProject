using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Site.DAL.Abstract;
using Site.Models.DTO;
using Site.Models.Entities;
using Site.Models.Helpers;
using Xunit;

namespace Site.API.Controllers
{

  [Route("api/[controller]")]
  [Authorize(Roles = "Admin,Member")]
  public class TemplatesController : Controller
  {
    private readonly ITemplatesRepository _templateRep;
    private readonly IMapper _mapper;

    public TemplatesController(ITemplatesRepository templateRep, IMapper mapper)
    {
      _templateRep = templateRep;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTemplates([FromQuery] int page, [FromQuery] int count)
    {
      var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value);
      var templates = await _templateRep.GetPagedAsync(userId, page, count);
      var templatesCount = await _templateRep.CountAsync(userId);

      var pageReturnModel = new PageReturnModel<TemplateDto>
      {
        Items = _mapper.Map<IEnumerable<TemplateDto>>(templates),
        TotalCount = templatesCount,
        CurrentPage = page
      };
      return Ok(pageReturnModel);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTemplateByIdAsync(Guid id)
    {
      var template = await _templateRep.FirstAsync(id);
      if (template == null)
      {
        return BadRequest("Template not found!");
      }

      return Ok(_mapper.Map<TemplateDto>(template));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplateAsync([FromBody] TemplateDto model)
    {
      var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value); // Get user id from token Sid claim
      //var template = new Template { Content = model.Content, Description = model.Description, Name = model.Name, UserId = userId };
      model.Id = null;
      model.UserId = userId;
      var template = _mapper.Map<Template>(model);
      template = await _templateRep.AddAsync(template);
      await _templateRep.Save();
      return Ok(_mapper.Map<TemplateDto>(template));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
      if (!await _templateRep.ExistAsync(id))
      {
        return NotFound($"Item {id} doesn't exist!");
      }

      var template = await _templateRep.GetByIdAsync(id);

      _templateRep.Delete(template);
      await _templateRep.Save();
      return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromBody] TemplateDto model, Guid id)
    {
      if (!ModelState.IsValid || model == null)
      {
        return BadRequest(ModelState);
      }

      if (!await _templateRep.ExistAsync(id))
      {
        return NotFound($"Item {id} doesn't exist!");
      }

      //var template = await _templateRep.GetByIdAsync(id);
      ////template.Name = model.Name;
      ////template.Description = model.Description;
      ////template.Content = model.Content;
      var template = _mapper.Map<Template>(model);

      template = _templateRep.Update(template);
      await _templateRep.Save();
      return Ok(_mapper.Map<TemplateDto>(template));
    }

    [HttpPost]
    [Route("check")]
    public async Task<IActionResult> CheckContractAsync([FromBody] ContractResultDto model) //test contract (multiplies 7 * 7)
    {
      try
      {
        var receiverAddress = model.Receiver;
        var senderAddress = model.Sender;
        var password = model.Password;
        var amount = Web3.Convert.ToWei(model.Amount);

        //var value = (int)Math.Pow(10, 18);

        var account = new ManagedAccount(senderAddress, password);
        //var abi = @"[{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""name"":""initialSupply"",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";
        //var byteCode = "6060604052341561000f57600080fd5b60405160208061020583398101604052808051600160a060020a03331660009081526020819052604090205550506101b98061004c6000396000f30060606040526004361061004b5763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166370a082318114610050578063a9059cbb1461008e575b600080fd5b341561005b57600080fd5b61007c73ffffffffffffffffffffffffffffffffffffffff600435166100d1565b60405190815260200160405180910390f35b341561009957600080fd5b6100bd73ffffffffffffffffffffffffffffffffffffffff600435166024356100e3565b604051901515815260200160405180910390f35b60006020819052908152604090205481565b73ffffffffffffffffffffffffffffffffffffffff33166000908152602081905260408120548290101561011657600080fd5b73ffffffffffffffffffffffffffffffffffffffff8316600090815260208190526040902054828101101561014a57600080fd5b5073ffffffffffffffffffffffffffffffffffffffff338116600090815260208190526040808220805485900390559184168152208054820190556001929150505600a165627a7a72305820cb4d0a81752fd7b3478871c3ddfa0bc3bb185b2df30c4826157c829eae4eebad0029";
        var web3 = new Web3(account);

        web3.TransactionManager.DefaultGas = BigInteger.Parse("21000");
        web3.TransactionManager.DefaultGasPrice = 30;

        var unlockAccountResult = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120);
        Assert.True(unlockAccountResult);
                
        var transHash = await web3.TransactionManager.SendTransactionAsync(senderAddress, receiverAddress, new HexBigInteger(amount));

        var n = Convert.ToInt32(web3.Eth.Blocks.GetBlockNumber);

        

        //var transactionHash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, amount);

        //var mineStart = await web3.Miner.Start.SendRequestAsync(2);

        //var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transHash);


        //while (receipt == null)
        //{
        //  Thread.Sleep(5000);
        //  receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transHash);
        //}


        //var contractAddress = receipt.ContractAddress;

        //var contract = web3.Eth.GetContract(abi, contractAddress);

        //var transferFunction = contract.GetFunction("transfer");

        //var result = await transferFunction.CallAsync<bool>(receiverAddress, amount);

        //var mineStop = await web3.Miner.Stop.SendRequestAsync();

        return Ok("Transaction resulted successfully!");
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    //[HttpPost]
    //[Route("balance")]
    //public async Task<decimal> GetBalance([FromBody] ContractResultDto model)
    //{
    //  var res = model.Amount;
    //  res = await _templateRep.GetBalance(model.Receiver);
    //  return res;
    //}
  }
}
