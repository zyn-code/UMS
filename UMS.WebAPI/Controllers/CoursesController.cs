using AutoMapper;
using EmailServiceTools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NpgsqlTypes;
using UMS.Application.EmailSending;
using UMS.Application.Entities.Course.Commands.InsertCourse;
using UMS.Application.Entities.Course.Queries.GetCourses;
using UMS.Application.Entities.SessionTime.Commands;
using UMS.Application.Entities.TeacherPerCourse.Commands;
using UMS.Application.Entities.TeacherPerCoursePerSession.Commands;
using UMS.Domain.Models;
using UMS.Infrastructure.Abstraction.EmailSenderInterface;
using UMS.WebAPI.DTO;

namespace UMS.WebAPI.Controllers;

[ApiController]
//[Authorize]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    
    public CoursesController(IMediator mediator, IMapper mapper, IEmailSender emailSender)
    {
        _mediator = mediator;
        _mapper = mapper;
        _emailSender = emailSender;
    }
    
    // GET
    [HttpGet("GetCourses")]
    public async Task<IActionResult> GetCourse()
    {
        //NpgsqlRange<LocalDate> range = default;
        //Console.WriteLine("Date range : "+range);
        return Ok(await _mediator.Send(new GetCoursesQuery()));
    }
    
    //POST
    [HttpPost("AddCourse")]
    public async Task<IActionResult> InsertCourse([FromHeader] string role,[FromBody] CreateCourse c)
    {
        Course course = _mapper.Map<Course>(c);
        return Ok(await _mediator.Send(new InsertCourseCommand(course)));
    }
    
    // Teacher to Course Registration
    [HttpPost("RegisterToCourse")]
    public async Task<IActionResult> InsertTeacherPerCourse([FromBody] RegisterToCourse regToCourse)
    {
        TeacherPerCourse tPerCourse = new TeacherPerCourse(){
            TeacherId = regToCourse.TeacherId,
            CourseId = regToCourse.CourseId
        };
        
        //await _mediator.Send(new InsertTeacherPerCourseCommand(tPerCourse));
        SessionTime sessionT = new SessionTime() { StartTime = regToCourse.StartTime, EndTime = regToCourse.EndTime};
        //await _mediator.Send(new InsertSessionTimeCommand(sessionT));
        //return Ok(await _mediator.Send(new InsertTeacherPerCoursePerSessionCommand(tPerCourse,sessionT)));
        return Ok(await _mediator.Send(new RegisterTeacherToCourseCommand(tPerCourse,sessionT)));
    }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmailTo([FromHeader] string address, string displayName, string subject, string content)
    {
        EmailAddress emailAddress = new EmailAddress() {Address = address, DisplayName = displayName};
        EmailSend emailSend = new EmailSend(_emailSender);
        return Ok(emailSend.SendEmail(emailAddress,subject,content));
    }
}