using MediatR;

namespace UMS.Application.Entities.SessionTime.Commands;

public class InsertSessionTimeCommand : IRequest<string>
{
    public Domain.Models.SessionTime SessionT;

    public InsertSessionTimeCommand(Domain.Models.SessionTime sessionTime)
    {
        SessionT = sessionTime;
    }
}