using MediatR;

namespace Alicunde.System.Exam.Aplication.Events;

public class BankDeletedEvent : INotification
{
    public Guid Id { get; set; }
    
    public BankDeletedEvent(Guid id)
    {
        Id = id;
    }
}