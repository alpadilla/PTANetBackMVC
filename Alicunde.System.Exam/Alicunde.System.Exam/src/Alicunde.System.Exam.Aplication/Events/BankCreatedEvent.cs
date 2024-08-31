using Alicunde.System.Exam.Contracts.Bank;
using MediatR;

namespace Alicunde.System.Exam.Aplication.Events;

public class BankCreatedEvent : INotification
{
    public BankDto BankDto { get; set; }
    
    public BankCreatedEvent(BankDto bankDto)
    {
        BankDto = bankDto;
    }
}