using System;
namespace Persona.Models;

public class JournalModel
{
	public int JournalID { get; set; }

	public String Description { get; set; } = "";

	public String photo { get; set; } = "";

	public DateTime? UserEnteredDate { get; set; }

	public DateTime? CreatedDate { get; set; }
}

