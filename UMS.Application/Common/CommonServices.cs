using UMS.Application.Exceptions;
using UMS.Persistence;
using UMS.WebAPI.DTO;

namespace UMS.Application.Common;

public class CommonServices : ICommonServices
{
    private readonly umsContext _context;

    public CommonServices(umsContext context)
    {
        _context = context;
    }
    
    public string? GetRole(int userId)
    {
        string? role = _context.Users.Where(u => u.Id == userId).Select(r => r.Role.Name).FirstOrDefault()?.ToString();
        if (role == null)
            throw new UserNotFoundException("No user with the specified ID");
        return role;
    }

    public int? CheckCourseExists(string courseName)
    {
        var courseId = (int?)_context.Courses.Where(c => c.Name == courseName).Select(c => c.Id).FirstOrDefault();
        if (courseId == 0)
            throw new CourseNotFoundException("This course does not exist!");
        return courseId;
    }

    private int GetTeacherPerCourseId(string teacherName, string courseName)
    {
        var teacherPerCourseId = (int) _context.TeacherPerCourses.Where(t => t.Teacher.Name == teacherName & t.Course.Name == courseName)
                .Select(t=>t.Id).SingleOrDefault();
        if (teacherPerCourseId == 0)
            throw new CourseNotFoundException("This course is not taught by the specified teacher!");
        return teacherPerCourseId;
    }

    private int GetSessionId(DateTime startTime, DateTime endTime)
    {
        var sessionId = (int) _context.SessionTimes.Where(s => s.StartTime == startTime & s.EndTime == endTime)
            .Select(t=>t.Id).SingleOrDefault();
        if (sessionId == 0)
            throw new SessionNotFoundException("No courses are given in the specified time range!");
        return sessionId;
    }
    public int GetClassId(EnrollClass enrollmentInfo)
    {
        var teacherPerCourseId = GetTeacherPerCourseId(enrollmentInfo.TeacherName, enrollmentInfo.ClassName);
        var sessionId = GetSessionId(enrollmentInfo.StartTime,enrollmentInfo.EndTime);
        var tPId = (int) _context.TeacherPerCoursePerSessionTimes.Where(t => t.TeacherPerCourseId == teacherPerCourseId & t.SessionTimeId == sessionId)
            .Select(t => t.Id).SingleOrDefault();
        if (tPId == 0)
            throw new CourseNotFoundException(enrollmentInfo.TeacherName + " doesn't give this course in the specified time!");
        return tPId;
    }
}