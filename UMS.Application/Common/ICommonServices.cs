using UMS.WebAPI.DTO;

namespace UMS.Application.Common;

public interface ICommonServices
{
    public string? GetRole(int userId);
    public int? CheckCourseExists(string courseName);
    public int GetClassId(EnrollClass enrollmentInfo);
}