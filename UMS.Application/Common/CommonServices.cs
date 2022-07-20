using UMS.Domain.Models;
using UMS.Persistence;

namespace UMS.Application.Common;

public class CommonServices : ICommonServices
{
    private readonly umsContext _context;

    public CommonServices(umsContext context)
    {
        _context = context;
    }
    
    public string? GetRole(int userId)
    {
        string? role = _context.Users.Where(u => u.Id == userId).Select(r => r.Role.Name).FirstOrDefault()?.ToString();
        Console.WriteLine("Role : : : : : ");
        return role;
    }

    public int CheckCourseExists(string courseName)
    {
        return (int) _context.Courses.Where(c => c.Name == courseName).Select(c => c.Id).FirstOrDefault();
    }
}