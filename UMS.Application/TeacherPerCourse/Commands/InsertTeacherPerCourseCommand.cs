using MediatR;

namespace UMS.Application.Entities.TeacherPerCourse.Commands;

public class InsertTeacherPerCourseCommand : IRequest<string>
{
    public Domain.Models.TeacherPerCourse _teacherPerCourse;

    public InsertTeacherPerCourseCommand(Domain.Models.TeacherPerCourse teacherPerCourse)
    {
        _teacherPerCourse = teacherPerCourse;
    }
}