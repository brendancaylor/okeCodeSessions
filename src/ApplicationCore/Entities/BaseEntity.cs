namespace ApplicationCore.Entities
{
    using System;

    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
