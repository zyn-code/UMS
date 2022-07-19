using MediatR;
using UMS.Persistence;

namespace UMS.Application.ClassEnrollment.Commands;

public class EnrollClassHandler : IRequestHandler<EnrollClassCommand, string>
{
    private umsContext _context = new umsContext();
    public async Task<string> Handle(EnrollClassCommand request, CancellationToken cancellationToken)
    {
        int teacherPerCourseId = (int)_context.TeacherPerCourses.Where(t=>t.Teacher.Name==request.TeacherName
                                & t.Course.Name==request.CourseName)
                                .Select(t=>t.Id).FirstOrDefault();
        Console.WriteLine("Course ID : " + teacherPerCourseId);
        var res = await _context.AddAsync(new Domain.Models.ClassEnrollment()
        {
            StudentId = request.StudentId,
            ClassId = teacherPerCourseId
        });
        _context.SaveChanges();
        return "Enrolled successfully!";
        throw new NotImplementedException();
    }
}