﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace NCG.HR.Models
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }

        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        public AuditType AuditType { get; set; }

        public List<string> ChangedColumns { get; set; } = new List<string>();

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.UserId = UserId;
            audit.TableName = TableName;
            audit.AuditType = null == AuditType ? string.Empty : AuditType.ToString();
            audit.DateTime = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count() == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count() == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
