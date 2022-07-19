using MediatR;
using UMS.Persistence;

namespace UMS.Application.User.Query;

public class GetUsersHandler : IRequestHandler<GetUsersQuery,List<Domain.Models.User>>
{
    private umsContext _context = new umsContext();
    public async Task<List<Domain.Models.User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return _context.Users.ToList(); 
        throw new NotImplementedException();
    }
}