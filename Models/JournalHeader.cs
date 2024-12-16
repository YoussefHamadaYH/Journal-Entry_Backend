using System;
using System.Collections.Generic;

namespace JournyTask.Models;

public partial class JournalHeader
{
    public Guid Id { get; set; }

    public DateTime EntryDate { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<JournalDetail> JournalDetails { get; set; } = new List<JournalDetail>();
}
