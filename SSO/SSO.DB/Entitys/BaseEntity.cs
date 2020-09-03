using System;
using System.ComponentModel.DataAnnotations;

namespace SSO.DB
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
   
    }
}
