namespace AgeRanger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgeGroup")]
    public partial class AgeGroup
    {
        public int Id { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
