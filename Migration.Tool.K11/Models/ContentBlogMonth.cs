using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migration.Tool.K11.Models;

[Table("CONTENT_BlogMonth")]
public class ContentBlogMonth
{
    [Key]
    [Column("BlogMonthID")]
    public int BlogMonthId { get; set; }

    [StringLength(100)]
    public string BlogMonthName { get; set; } = null!;

    public DateTime BlogMonthStartingDate { get; set; }
}
