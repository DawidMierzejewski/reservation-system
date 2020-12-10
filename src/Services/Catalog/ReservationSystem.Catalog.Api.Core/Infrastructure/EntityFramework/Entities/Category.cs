using System;

namespace ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities
{
    public class Category
    {
        public int CategoryId { get; }

        public string Name { get; }

        public DateTime CreateDate { get; }

        public Guid CreatedBy { get; }

        public Category(string name, Guid createdBy)
        {
            Name = name;
            CreatedBy = createdBy;
        }
    }
}
