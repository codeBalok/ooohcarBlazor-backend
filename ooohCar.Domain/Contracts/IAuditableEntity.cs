using System;
using ooohCar.Domain.Entities;

namespace ooohCar.Domain.Contracts
{
    public interface IAuditableEntity : IEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedOn { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedOn { get; set; }
    }
}