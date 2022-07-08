using MediatR;
using UMS.Domain.Models;
using UMS.Persistence;

namespace UMS.Application.Entities.TeacherPerCoursePerSession.Commands;

public class InsertTeacherPerCoursePerSessionHandler : IRequestHandler<InsertTeacherPerCoursePerSessionCommand,string>
{
    private readonly umsContext _context = new umsContext();
    public async Task<string> Handle(InsertTeacherPerCoursePerSessionCommand request, CancellationToken cancellationToken)
    {
        await _context.TeacherPerCoursePerSessionTimes.AddAsync(new TeacherPerCoursePerSessionTime()
        {
            TeacherPerCourseId = request.TPerCourse.Id,
            SessionTimeId = request.STime.Id
        });
        await _context.SaveChangesAsync();
        return "Teacher registered to course successfully!!";
    }
}