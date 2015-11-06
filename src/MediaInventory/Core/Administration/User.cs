using System;
using MediaInventory.Infrastructure.Framework.Data.Orm;

namespace MediaInventory.Core.Administration
{
    public class User : IIdEntity, ITimestampedEntity
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; protected set; }
        public virtual DateTime? LastLogin { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }

        public virtual User SetPassword(string password)
        {
            //Password = HashProvider.Hash(password.Trim()).ToBase64();
            return this;
        }
    }
}