using System.ComponentModel.DataAnnotations;
using TodoApi.Entities;

namespace TodoApi.DTOs;

public class TodoUpdateDto : IValidatableObject
{
    [Required(ErrorMessage = "Title bo'sh bo'lishi mumkin emas.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title 1-200 belgi oralig'ida bo'lsin.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description 1000 belgidan oshmasin.")]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;

    [StringLength(100)]
    public string? Category { get; set; }

    public DateTime? DueDate { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "EstimatedHours manfiy bo'lmasin.")]
    public double EstimatedHours { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (DueDate.HasValue && DueDate.Value.Date < DateTime.UtcNow.Date)
            yield return new ValidationResult("DueDate o'tgan sana bo'lmasin.", new[] { nameof(DueDate) });
        if (!Enum.IsDefined(typeof(Priority), Priority))
            yield return new ValidationResult("Priority qiymati noto'g'ri.", new[] { nameof(Priority) });
    }
}