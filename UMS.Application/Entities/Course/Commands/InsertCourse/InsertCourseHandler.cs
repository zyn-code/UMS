using System.Net;
using UMS.Persistence;

namespace UMS.Application.Entities.Course.Commands.InsertCourse;

public class InsertCourseHandler : MediatR.IRequestHandler<InsertCourseCommand, string>
{
    private readonly umsContext _context = new umsContext();
    public async Task<string> Handle(InsertCourseCommand request, CancellationToken cancellationToken)
    {
        //request.course.EnrolmentDateRange = _context.Courses.First().EnrolmentDateRange;
        var res = _context.Courses.AddAsync(request.course);
        Console.WriteLine("Result : " + res);
        var r=_context.SaveChanges();
        return "Inserted Successfully!";
        throw new NotImplementedException();
    }
}