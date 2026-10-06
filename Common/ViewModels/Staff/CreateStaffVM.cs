using Common.Enums;
using System.ComponentModel.DataAnnotations;

public class CreateStaffVM
{
    [Required]
    public int HostelId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public StaffRole Role { get; set; }   

    [Required]
    [Phone]
    public string Phone { get; set; }
}