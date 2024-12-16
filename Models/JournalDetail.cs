using System;
using System.Collections.Generic;

namespace JournyTask.Models;

public partial class JournalDetail
{
    public Guid Id { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public Guid AccountId { get; set; }

    public Guid JournalHeaderId { get; set; }

    public virtual AccountsChart Account { get; set; } = null!;

    public virtual JournalHeader JournalHeader { get; set; } = null!;
}
