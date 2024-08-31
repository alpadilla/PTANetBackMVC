using Alicunde.System.Exam.Contracts.Bank;
using MediatR;

namespace Alicunde.System.Exam.Aplication.Events;

public class BankUpdatedEvent : INotification
{
    public BankDto BankDto { get; set; }
    
    public BankUpdatedEvent(BankDto bankDto)
    {
        BankDto = bankDto;
    }
}