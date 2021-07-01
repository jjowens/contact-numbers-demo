namespace ContactNumbers.Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactType")]
    public partial class ContactType
    {
        public int ContactTypeID { get; set; }

        [Column("ContactTypeName")]
        [Required]
        [StringLength(200)]
        public string ContactTypeName { get; set; }
    }
}
