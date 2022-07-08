using MediatR;
using UMS.Persistence;

namespace UMS.Application.Entities.TeacherPerCourse.Commands;

public class InsertTeacherPerCourseHandler : IRequestHandler<InsertTeacherPerCourseCommand,string>
{
    private readonly umsContext _context = new umsContext();
    public async Task<string> Handle(InsertTeacherPerCourseCommand request, CancellationToken cancellationToken)
    {
        await _context.TeacherPerCourses.AddAsync(request._teacherPerCourse, cancellationToken);
        _context.SaveChanges();
        return "Course Registered Successfully!";
    }
}