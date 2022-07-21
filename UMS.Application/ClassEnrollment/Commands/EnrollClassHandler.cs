using MediatR;
using UMS.Application.Common;
using UMS.Persistence;

namespace UMS.Application.ClassEnrollment.Commands;

public class EnrollClassHandler : IRequestHandler<EnrollClassCommand, string>
{
    private readonly umsContext _context;
    private readonly ICommonServices _common;

    public EnrollClassHandler(ICommonServices common, umsContext context)
    {
        _context = context;
        _common = common;
    }
    public async Task<string> Handle(EnrollClassCommand request, CancellationToken cancellationToken)
    {
        var classId = _common.GetClassId(request.EnrollmentInfo);
        
        var res = await _context.AddAsync(new Domain.Models.ClassEnrollment()
        {
            StudentId = request.EnrollmentInfo.StudentId,
            ClassId = classId
        });
        _context.SaveChanges();
        return "Enrolled successfully!";
    }
}