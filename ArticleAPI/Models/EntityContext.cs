namespace ArticleAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityContext : DbContext
    {
        public EntityContext()
            : base("name=EntityContext")
        {
        }

        public virtual DbSet<ArtUser> ArtUsers { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<menu> menus { get; set; }
        public virtual DbSet<page> pages { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ArtUser>()
        //        .HasMany(e => e.menus)
        //        .WithRequired(e => e.ArtUser)
        //        .HasForeignKey(e => e.user_id)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<ArtUser>()
        //        .HasMany(e => e.pages)
        //        .WithRequired(e => e.ArtUser)
        //        .HasForeignKey(e => e.user_id)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<ArtUser>()
        //        .HasMany(e => e.posts)
        //        .WithOptional(e => e.ArtUser)
        //        .HasForeignKey(e => e.user_id);

        //    modelBuilder.Entity<category>()
        //        .Property(e => e.name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<category>()
        //        .Property(e => e.description)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<category>()
        //        .HasMany(e => e.posts)
        //        .WithRequired(e => e.category)
        //        .HasForeignKey(e => e.category_id)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<menu>()
        //        .Property(e => e.title)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<menu>()
        //        .HasMany(e => e.menu1)
        //        .WithOptional(e => e.menu2)
        //        .HasForeignKey(e => e.parent_id);

        //    modelBuilder.Entity<page>()
        //        .Property(e => e.url)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<page>()
        //        .Property(e => e.title)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<page>()
        //        .Property(e => e.contents)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<page>()
        //        .HasMany(e => e.menus)
        //        .WithRequired(e => e.page)
        //        .HasForeignKey(e => e.page_id)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<post>()
        //        .Property(e => e.title)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<post>()
        //        .Property(e => e.texts)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<post>()
        //        .Property(e => e.image)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<post>()
        //        .Property(e => e.author)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<UserRole>()
        //        .HasMany(e => e.ArtUsers)
        //        .WithRequired(e => e.UserRole)
        //        .HasForeignKey(e => e.role_id)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<UserRole>()
        //        .HasMany(e => e.ArtUsers1)
        //        .WithRequired(e => e.UserRole1)
        //        .HasForeignKey(e => e.role_id)
        //        .WillCascadeOnDelete(false);
        //}
    }
}
