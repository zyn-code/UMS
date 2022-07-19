using MediatR;

namespace UMS.Application.User.Query;

public class GetUsersQuery : IRequest<List<Domain.Models.User>>
{
    
}