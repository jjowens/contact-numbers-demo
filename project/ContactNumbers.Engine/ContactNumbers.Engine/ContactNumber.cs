namespace ContactNumbers.Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactNumber")]
    public partial class ContactNumber
    {
        public int ContactNumberID { get; set; }

        [Column("ContactNumber")]
        [Required]
        [StringLength(200)]
        public string ContactNumber1 { get; set; }

        public int ContactNumberTypeID { get; set; }
        public int CustomerID { get; set; }
    }
}
