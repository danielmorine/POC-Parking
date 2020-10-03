using System;

namespace Infrastructure.Schemas.Interfaces
{
    public interface ISchema { }
    public interface ISchema<T> : ISchema where T : struct
    {
        public T ID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
