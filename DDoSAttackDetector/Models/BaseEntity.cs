using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BaseEntity:System.Object
    {
        public BaseEntity()
        {
        }
        
        [Key]
        [Required]      
        public int Id { get; set; }
        [Timestamp]
        public byte[] ts { get; set; }
    }
}
