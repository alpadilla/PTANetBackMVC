using Alicunde.System.Exam.Aplication.Events;
using Alicunde.System.Exam.Contracts;
using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.Services.Bank.Commands;
using Alicunde.System.Exam.Services.Bank.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Alicunde.System.Exam.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class BankController : ControllerBase
{
    private readonly ILogger<BankController> _logger;
    private readonly IMediator _mediator;

    public BankController(
        ILogger<BankController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ResponseDto<IEnumerable<BankDto>>> GetAsync()
    {
        try
        {
            var bankDtos = await _mediator.Send(new GetBacksQuery());
            return new ResponseDto<IEnumerable<BankDto>>(bankDtos);
        }
        catch (Exception e)
        {
            var response = new ResponseDto<IEnumerable<BankDto>>(new List<BankDto>());
            var message = "An error occured while getting the banks";
            response.Errors.Add(message);
            _logger.LogError(e.Message);
            return response;  
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ResponseDto<BankDto>> GetByIdAsync(Guid id)
    {
        var bankDto = await _mediator.Send(new GetBackByIdQuery(id));
        var response = new ResponseDto<BankDto>(bankDto);
        
        if (bankDto is not null) return response;

        var message = "There's no bank with the provided ID";
        response.Errors.Add(message);
        _logger.LogError(message);
        return response;

    }
    
    [HttpPost]
    public async Task<ResponseDto<BankDto>> CreateAsync(BankCreateDto bankCreateDto)
    {
        try
        {
            var bankDto = await _mediator.Send(new CreateBankCommand(bankCreateDto));
            await _mediator.Publish(new BankCreatedEvent(bankDto));
            return new ResponseDto<BankDto>(bankDto);
        }
        catch (Exception e)
        {
            var response = new ResponseDto<BankDto>(null);
            var message = "An error occured while creating the bank with the provided information";
            response.Errors.Add(message);
            _logger.LogError(e.Message);
            return response;
        }
    }
    
    [HttpPut("{id}")]
    public async Task<ResponseDto<BankDto>> UpdateAsync(Guid id, BankUpdateDto bankUpdateDto)
    {
        try
        {
            var bankDto = await _mediator.Send(new UpdateBankCommand(id, bankUpdateDto));
            await _mediator.Publish(new BankUpdatedEvent(bankDto));
            return new ResponseDto<BankDto>(bankDto);
        }
        catch (Exception e)
        {
            var response = new ResponseDto<BankDto>(null);
            var message = "An error occured while updating the bank with the provided information";
            response.Errors.Add(message);
            _logger.LogError(e.Message);
            return response;
        }
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _mediator.Send(new DeleteBankCommand(id));
        await _mediator.Publish(new BankDeletedEvent(id));
    }
    
    [HttpGet("load/from/open-data-api")]
    public async Task<ResponseDto<bool>> LoadFromOpenDataApiAsync()
    {
        try
        {
            await _mediator.Send(new LoadBanksFromOpenDataApiCommand());
            return new ResponseDto<bool>(true);
        }
        catch (Exception e)
        {
            var response = new ResponseDto<bool>(false);
            response.Errors.Add(e.Message);
            _logger.LogError(e.Message);
            return response;
        }
    }
}