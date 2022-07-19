using MediatR;

namespace UMS.Application.Entities.TeacherPerCoursePerSession.Commands;

public class RegisterTeacherToCourseCommand : IRequest<string>
{
    public Domain.Models.TeacherPerCourse _teacherPerCourse;
    public Domain.Models.SessionTime SessionT;

    public RegisterTeacherToCourseCommand(Domain.Models.TeacherPerCourse teacherPerCourse, Domain.Models.SessionTime sessionTime)
    {
        _teacherPerCourse = teacherPerCourse;
        SessionT = sessionTime;
    }
}