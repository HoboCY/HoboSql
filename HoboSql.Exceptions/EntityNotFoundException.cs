using System;

namespace HoboSql.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; set; }

        public object Parameter { get; set; }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public EntityNotFoundException(Type entityType, object parameter, Exception innerException)
            : base(
                parameter == null
                    ? $"There is no such an entity given parameter. Entity type：{entityType.Name}"
                    : $"There is no such an entity. Entity type：{entityType.Name}，parameter：{parameter}", innerException)
        {
            EntityType = entityType;
            Parameter = parameter;
        }

        public EntityNotFoundException(Type entityType, object parameter)
            : this(entityType, parameter, null)
        {
        }
    }
}
