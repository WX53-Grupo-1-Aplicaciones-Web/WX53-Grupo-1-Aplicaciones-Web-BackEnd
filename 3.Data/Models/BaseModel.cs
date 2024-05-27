using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3.Data.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public int CreatedUserId { get; set; }
        
        public int? UpdatedUserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
        [DefaultValue(true)] public Boolean IsActive { get; set; }

    }
}