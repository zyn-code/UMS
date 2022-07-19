using MediatR;

namespace UMS.Application.Entities.Course.Commands.InsertCourse;

public class InsertCourseCommand : IRequest<string>
{
    public Domain.Models.Course course;

    public InsertCourseCommand(Domain.Models.Course course)
    {
        this.course = course;
    }
}