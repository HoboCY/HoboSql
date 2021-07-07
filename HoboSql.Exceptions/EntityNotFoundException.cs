using System;
using System.Collections.Generic;
using System.Text;

namespace HoboSql.Exceptions
{
    public class EntityNotFoundException : Exception

    {
        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base(
                id == null
                    ? $"There is no such an entity given given id. Entity type: {entityType.FullName}"
                    : $"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        { }

        public EntityNotFoundException(Type entityType, object id) : this(entityType, id, null) { }

        public EntityNotFoundException(Type entityType) : this(entityType, null, null) { }
    }
}
