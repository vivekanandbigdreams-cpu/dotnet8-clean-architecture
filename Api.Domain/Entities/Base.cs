using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    //Base class for entities common properties
    public class Base<T>
    {
        [Key]
        public T? Id { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }  
    }
}
