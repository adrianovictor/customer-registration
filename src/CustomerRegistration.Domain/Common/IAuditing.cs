namespace CustomerRegistration.Domain.Common;

public interface IAuditing
{
    DateTime CreatedAt { get; set; }
    int CreatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    int? UpdatedBy { get; set; }
}
