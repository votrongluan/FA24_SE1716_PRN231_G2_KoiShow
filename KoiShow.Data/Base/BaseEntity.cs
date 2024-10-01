using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace KoiShow.Data.Base;

[Index(nameof(Id), IsUnique = true, Name = "Index_Id")]
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedTime = LastUpdatedTime = DateTime.Now;
    }

    [Key]
    public int Id { get; set; }
    public int? CreatedBy { get; set; }
    public int? LastUpdatedBy { get; set; }
    public int? DeletedBy { get; set; }

    public DateTime? CreatedTime { get; set; }
    public DateTime? LastUpdatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }
}