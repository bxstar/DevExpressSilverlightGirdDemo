using System;
using System.Windows.Input;
using DevExpress.Xpo;

namespace GridDemo
{
    [Persistent("dbo.SCIssuesDemo")]
    public class Question : XPLiteObject
    {
        [Key(true)]
        public Guid Oid { get; set; }
        public string Id { get; set; }
        public string Subject { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProductName { get; set; }
        public string TechnologyName { get; set; }
        public bool Urgent { get; set; }
        public string Status { get; set; }
        public Question(Session session) : base(session) { }
        public Question() : base(Session.DefaultSession) { }
    }
}
