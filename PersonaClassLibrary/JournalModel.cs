using System.ComponentModel.DataAnnotations;

namespace PersonaClassLibrary;

public class JournalModel
{
    [Key]
    public int JournalId { get; set; }

    public string JournalName { get; set; } = "";

    public string Description { get; set; } = "";

    public string Photo { get; set; } = "";

    public DateTime? UserEnteredDate { get; set; }

    public DateTime? CreatedDate { get; set; }
}