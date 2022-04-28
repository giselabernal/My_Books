using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Books.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_Books.Data.Models
{
    public class AppDbContext: DbContext //, IEntityTypeConfiguration<Book>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //tengo duda del applyconfiguration
        //public virtual Microsoft.EntityFrameworkCore.ModelBuilder ApplyConfiguration<Book>(Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Book> configuration) where Book : class;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                
                .HasOne(b => b.Book)
                
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
               .HasOne(b => b.Author)
               .WithMany(ba => ba.Book_Authors)
               .HasForeignKey(bi => bi.AuthorId);

           // new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
        }

    /*    void IEntityTypeConfiguration<Book>.Configure(EntityTypeBuilder<Book> builder)
        {
            throw new NotImplementedException();
        }

        public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Book>
        {
            public void Configure(EntityTypeBuilder<Book> builder)
            {
                builder
                    .Property(b => b.Id)
                    .
                    .IsRequired();
            }
        }*/
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

    }
}
