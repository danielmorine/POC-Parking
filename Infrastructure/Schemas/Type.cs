using Infrastructure.Schemas.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Schemas
{
    public class Type : ISchema<byte>
    {
        public string Name { get; set; }
        public byte ID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public virtual Vehicle Vehicle { get; set; }

    }
}
