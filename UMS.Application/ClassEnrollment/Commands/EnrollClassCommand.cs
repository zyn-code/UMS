using MediatR;

namespace UMS.Application.ClassEnrollment.Commands;

public class EnrollClassCommand : IRequest<string>
{
    public string CourseName;
    public string TeacherName;
    public int StudentId;

    public EnrollClassCommand(string cName, string tName, int sId)
    {
        CourseName = cName;
        TeacherName = tName;
        StudentId = sId;
    }
}