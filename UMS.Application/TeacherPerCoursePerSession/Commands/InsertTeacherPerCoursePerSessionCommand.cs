using MediatR;

namespace UMS.Application.Entities.TeacherPerCoursePerSession.Commands;

public class InsertTeacherPerCoursePerSessionCommand : IRequest<string>
{
    public Domain.Models.TeacherPerCourse TPerCourse;
    public Domain.Models.SessionTime STime;

    public InsertTeacherPerCoursePerSessionCommand(Domain.Models.TeacherPerCourse tPerCourse, Domain.Models.SessionTime sTime)
    {
        TPerCourse = tPerCourse;
        STime = sTime;
    }
}