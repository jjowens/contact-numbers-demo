namespace ContactNumbers.Engine
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContactNumbersDB : DbContext
    {
        public ContactNumbersDB()
            : base("name=ContactNumbersDB")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ContactNumber> ContactNumbers { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<ContactNumber>()
                .Property(e => e.ContactNumber1)
                .IsUnicode(false);

            modelBuilder.Entity<ContactType>()
                .Property(e => e.ContactTypeName)
                .IsUnicode(false);
        }
    }
}
