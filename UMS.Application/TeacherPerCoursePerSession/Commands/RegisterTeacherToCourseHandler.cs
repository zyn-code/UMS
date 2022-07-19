using MediatR;
using UMS.Application.Entities.TeacherPerCourse.Commands;
using UMS.Domain.Models;
using UMS.Persistence;

namespace UMS.Application.Entities.TeacherPerCoursePerSession.Commands;

public class RegisterTeacherToCourseHandler :IRequestHandler<RegisterTeacherToCourseCommand,string>
{
    private readonly umsContext _context = new umsContext();
    public async Task<string> Handle(RegisterTeacherToCourseCommand request, CancellationToken cancellationToken)
    {
        await _context.TeacherPerCourses.AddAsync(request._teacherPerCourse, cancellationToken);
        await _context.SaveChangesAsync();
        await _context.SessionTimes.AddAsync(request.SessionT);
        await _context.SaveChangesAsync();
        await _context.TeacherPerCoursePerSessionTimes.AddAsync(new TeacherPerCoursePerSessionTime()
        {
            TeacherPerCourseId = request._teacherPerCourse.Id,
            SessionTimeId = request.SessionT.Id
        });
        _context.SaveChanges();
        return "Course Registered Successfully!";
    }

}