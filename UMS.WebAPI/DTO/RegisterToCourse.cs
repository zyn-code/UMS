using System.ComponentModel.DataAnnotations;

namespace UMS.WebAPI.DTO;

public class RegisterToCourse
{
    [Required(ErrorMessage = "Course Id is required!")]
    public int TeacherId { get; set; }
    
    [Required(ErrorMessage = "Course Id is required!")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Start Time is required!")]
    public DateTime StartTime { get; set; }
    
    [Required(ErrorMessage = "End Time is required!")]
    public DateTime EndTime { get; set; }
}