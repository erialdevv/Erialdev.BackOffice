namespace Erialdev.BackOffice.Api.Domain.Entites
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public DateTime CreateDate { get; protected set; }
        public string CreateAt { get; protected set; } 
        public DateTime? EditDate { get; protected set; }
        public string? EditAt { get; protected set; }
        public DateTime? CancelDate { get; protected set; }
        public string? CancelAt { get; protected set; }
        public bool IsCanceled { get; protected set; }
        public string? Pcid { get; protected set; }

        protected Entity(string createdBy,string pcid)
        {
            Id = Guid.CreateVersion7();
            CreateDate = DateTime.Now;
            CreateAt = createdBy;
            Pcid = pcid;
        }
    
        
        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity(Guid id, DateTime createDate, string createAt, string? editAt, DateTime? editDate, string? pcid, bool isCanceled, DateTime? cancelDate, string? cancelAt)
        {
            Id = id;
            CreateDate = createDate;
            CreateAt = createAt;
            EditAt = editAt;
            EditDate = editDate;
            Pcid = pcid;
            IsCanceled = isCanceled;
            CancelDate = cancelDate;
            CancelAt = cancelAt;
        }

        
    }
}
