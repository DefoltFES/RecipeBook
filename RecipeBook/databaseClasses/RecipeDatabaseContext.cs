using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class RecipeDatabaseContext : DbContext
    {
        public RecipeDatabaseContext()
        {
        }

        public RecipeDatabaseContext(DbContextOptions<RecipeDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookRecipe> BookRecipes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
        public virtual DbSet<ListCategory> ListCategories { get; set; }
        public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeIngridient> RecipeIngridients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=database/RecipeDatabase.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.IdBook);

                entity.ToTable("books");

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<BookRecipe>(entity =>
            {
                entity.HasKey(e => new { e.IdBook, e.IdRecipe });

                entity.ToTable("book_recipes");

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("categories");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.HasKey(e => new { e.IdRecipe, e.IdStep });

                entity.ToTable("instructions");

                entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");

                entity.Property(e => e.IdStep).HasColumnName("id_step");

                entity.Property(e => e.DescriptionStep).HasColumnName("description_step");

                entity.Property(e => e.ImageStep).HasColumnName("image_step");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Instructions)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ListCategory>(entity =>
            {
                entity.HasKey(e => new { e.IdRecipe, e.IdCategory });

                entity.ToTable("list_categories");

                entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.ListCategories)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MeasurementUnit>(entity =>
            {
                entity.HasKey(e => e.IdMeasurement);

                entity.ToTable("measurement_units");

                entity.Property(e => e.IdMeasurement).HasColumnName("id_measurement");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("products");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.IdRecipe);

                entity.ToTable("recipes");

                entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");

                entity.Property(e => e.CookTime).HasColumnName("cook_time");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.NumService).HasColumnName("num_service");
            });

            modelBuilder.Entity<RecipeIngridient>(entity =>
            {
                entity.HasKey(e => new { e.IdRecipe, e.IdProduct });

                entity.ToTable("recipe_ingridients");

                entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.IdMeasurement).HasColumnName("id_measurement");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.HasOne(d => d.MeasurementUnit)
                    .WithMany(p => p.RecipeIngridients)
                    .HasForeignKey(d => d.IdMeasurement);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RecipeIngridients)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngridients)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
