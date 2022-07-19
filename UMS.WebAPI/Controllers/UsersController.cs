using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS.Domain.Models;
using UMS.Persistence;

namespace UMS.WebAPI.Controllers;


public class UsersController : ODataController
{
    private readonly umsContext _context;

    public UsersController(umsContext context)
    {
        _context = context;
    }
    
    [EnableQuery]
    public IQueryable<User> GetUsers()
    {
        return _context.Users.AsQueryable();
    }
}