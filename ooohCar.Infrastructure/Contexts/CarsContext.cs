using ooohCar.Application.Interfaces.Services;
using ooohCar.Application.Models.Chat;
using ooohCar.Application.Models.Identity;
using ooohCar.Domain.Contracts;
using ooohCar.Domain.Entities;
using ooohCar.Domain.Entities.Catalog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ooohCar.Domain.Entities.Cars;

namespace ooohCar.Infrastructure.Contexts
{
    public class CarsContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public CarsContext(DbContextOptions<CarsContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<ChatHistory> ChatHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Document> Documents { get; set; }

        // Configure Cars related tables
        public virtual DbSet<CarColor> CarColors { get; set; }
        public virtual DbSet<CarEquipment> CarEquipments { get; set; }
        public virtual DbSet<CarGeneration> CarGenerations { get; set; }
        public virtual DbSet<CarMake> CarMakes { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<CarOption> CarOptions { get; set; }
        public virtual DbSet<CarOptionValue> CarOptionValues { get; set; }
        public virtual DbSet<CarSale> CarSales { get; set; }
        public virtual DbSet<CarSaleImage> CarSaleImages { get; set; }
        public virtual DbSet<CarSerie> CarSeries { get; set; }
        public virtual DbSet<CarSpecification> CarSpecifications { get; set; }
        public virtual DbSet<CarSpecificationValue> CarSpecificationValues { get; set; }
        public virtual DbSet<CarTrim> CarTrims { get; set; }
        public virtual DbSet<CarType> CarTypes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_currentUserService.UserId);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
            builder.Entity<ChatHistory>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<CarUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", "Identity");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });


            builder.Entity<CarColor>(entity =>
            {
                entity.HasKey(e => e.IdCarColor);

                entity.ToTable("car_color");

                entity.Property(e => e.IdCarColor).HasColumnName("id_car_color");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            builder.Entity<CarEquipment>(entity =>
            {
                entity.HasKey(e => e.IdCarEquipment)
                    .HasName("pk_car_equipment");

                entity.ToTable("car_equipment");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarEquipment)
                    .HasColumnName("id_car_equipment")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarTrim)
                    .HasColumnName("id_car_trim")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial109)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL109")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarEquipments)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_equipment_car_type");
            });

            builder.Entity<CarGeneration>(entity =>
            {
                entity.HasKey(e => e.IdCarGeneration)
                    .HasName("pk_car_generation");

                entity.ToTable("car_generation");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarGeneration)
                    .HasColumnName("id_car_generation")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarModel)
                    .HasColumnName("id_car_model")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial112)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL112")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.Property(e => e.YearBegin)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("year_begin")
                    .HasComment("TRIAL");

                entity.Property(e => e.YearEnd)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("year_end")
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarModelNavigation)
                    .WithMany(p => p.CarGenerations)
                    .HasForeignKey(d => d.IdCarModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_generation_car_model");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarGenerations)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_generation_car_type");
            });

            builder.Entity<CarMake>(entity =>
            {
                entity.HasKey(e => e.IdCarMake)
                    .HasName("pk_car_make");

                entity.ToTable("car_make");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarMake)
                    .HasColumnName("id_car_make")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial112)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL112")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarMakes)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_make_car_type");
            });

            builder.Entity<CarModel>(entity =>
            {
                entity.HasKey(e => e.IdCarModel)
                    .HasName("pk_car_model");

                entity.ToTable("car_model");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarModel)
                    .HasColumnName("id_car_model")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarMake)
                    .HasColumnName("id_car_make")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial116)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL116")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarMakeNavigation)
                    .WithMany(p => p.CarModels)
                    .HasForeignKey(d => d.IdCarMake)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_model_car_make");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarModels)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_model_car_type");
            });

            builder.Entity<CarOption>(entity =>
            {
                entity.HasKey(e => e.IdCarOption)
                    .HasName("pk_car_option");

                entity.ToTable("car_option");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarOption)
                    .HasColumnName("id_car_option")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdParent)
                    .HasColumnName("id_parent")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial119)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL119")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarOptions)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_option_car_type");

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.InverseIdParentNavigation)
                    .HasForeignKey(d => d.IdParent)
                    .HasConstraintName("fk_car_option_car_option");
            });

            builder.Entity<CarOptionValue>(entity =>
            {
                entity.HasKey(e => e.IdCarOptionValue)
                    .HasName("pk_car_option_value");

                entity.ToTable("car_option_value");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarOptionValue)
                    .HasColumnName("id_car_option_value")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarEquipment)
                    .HasColumnName("id_car_equipment")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarOption)
                    .HasColumnName("id_car_option")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.IsBase)
                    .HasColumnName("is_base")
                    .HasDefaultValueSql("((1))")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial122)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL122")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarEquipmentNavigation)
                    .WithMany(p => p.CarOptionValues)
                    .HasForeignKey(d => d.IdCarEquipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_option_value_car_equipment");

                entity.HasOne(d => d.IdCarOptionNavigation)
                    .WithMany(p => p.CarOptionValues)
                    .HasForeignKey(d => d.IdCarOption)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_option_value_car_option");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarOptionValues)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_option_value_car_type");
            });

            builder.Entity<CarSale>(entity =>
            {
                entity.ToTable("car_sale");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarKmDriven).HasColumnName("car_km_driven");

                entity.Property(e => e.CarLocation)
                    .HasMaxLength(100)
                    .HasColumnName("car_location");

                entity.Property(e => e.CarOwnership)
                    .HasMaxLength(50)
                    .HasColumnName("car_ownership");

                entity.Property(e => e.CarPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("car_price");

                entity.Property(e => e.CarSaleImageId).HasColumnName("car_sale_image_id");

                entity.Property(e => e.CarSallerContact)
                    .HasMaxLength(250)
                    .HasColumnName("car_saller_contact");

                entity.Property(e => e.CarSallerContactType)
                    .HasMaxLength(50)
                    .HasColumnName("car_saller_contact_type");

                entity.Property(e => e.CarSallerName).HasColumnName("car_saller_name");

                entity.Property(e => e.CarYear).HasColumnName("car_year");

                entity.Property(e => e.IdCarColor).HasColumnName("id_car_color");

                entity.Property(e => e.IdCarMake).HasColumnName("id_car_make");

                entity.Property(e => e.IdCarModel).HasColumnName("id_car_model");

                entity.Property(e => e.IdCarTrim).HasColumnName("id_car_trim");

                entity.Property(e => e.IdCarType).HasColumnName("id_car_type");
            });

            builder.Entity<CarSaleImage>(entity =>
            {
                entity.ToTable("car_sale_images");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarImageName).HasColumnName("car_image_name");
            });

            builder.Entity<CarSerie>(entity =>
            {
                entity.HasKey(e => e.IdCarSerie)
                    .HasName("pk_car_serie");

                entity.ToTable("car_serie");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarSerie)
                    .HasColumnName("id_car_serie")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarGeneration)
                    .HasColumnName("id_car_generation")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarModel)
                    .HasColumnName("id_car_model")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial129)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL129")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarGenerationNavigation)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.IdCarGeneration)
                    .HasConstraintName("fk_car_serie_car_generation");

                entity.HasOne(d => d.IdCarModelNavigation)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.IdCarModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_serie_car_model");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarSeries)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_serie_car_type");
            });

            builder.Entity<CarSpecification>(entity =>
            {
                entity.HasKey(e => e.IdCarSpecification)
                    .HasName("pk_car_specification");

                entity.ToTable("car_specification");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarSpecification)
                    .HasColumnName("id_car_specification")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdParent)
                    .HasColumnName("id_parent")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial132)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL132")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarSpecifications)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_specification_car_type");

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.InverseIdParentNavigation)
                    .HasForeignKey(d => d.IdParent)
                    .HasConstraintName("fk_car_specification_car_specification");
            });

            builder.Entity<CarSpecificationValue>(entity =>
            {
                entity.HasKey(e => e.IdCarSpecificationValue)
                    .HasName("pk_car_specification_value");

                entity.ToTable("car_specification_value");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarSpecificationValue)
                    .HasColumnName("id_car_specification_value")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarSpecification)
                    .HasColumnName("id_car_specification")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarTrim)
                    .HasColumnName("id_car_trim")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial132)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL132")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.Property(e => e.Unit)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("unit")
                    .HasComment("TRIAL");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("value")
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarSpecificationNavigation)
                    .WithMany(p => p.CarSpecificationValues)
                    .HasForeignKey(d => d.IdCarSpecification)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_specification_value_car_specification");

                entity.HasOne(d => d.IdCarTrimNavigation)
                    .WithMany(p => p.CarSpecificationValues)
                    .HasForeignKey(d => d.IdCarTrim)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_specification_value_car_trim");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarSpecificationValues)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_specification_value_car_type");
            });

            builder.Entity<CarTrim>(entity =>
            {
                entity.HasKey(e => e.IdCarTrim)
                    .HasName("pk_car_trim");

                entity.ToTable("car_trim");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarTrim)
                    .HasColumnName("id_car_trim")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateCreate)
                    .HasColumnName("date_create")
                    .HasComment("TRIAL");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("date_update")
                    .HasComment("TRIAL");

                entity.Property(e => e.EndProductionYear)
                    .HasColumnName("end_production_year")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarModel)
                    .HasColumnName("id_car_model")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarSerie)
                    .HasColumnName("id_car_serie")
                    .HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.StartProductionYear)
                    .HasColumnName("start_production_year")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial148)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL148")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");

                entity.HasOne(d => d.IdCarModelNavigation)
                    .WithMany(p => p.CarTrims)
                    .HasForeignKey(d => d.IdCarModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_trim_car_model");

                entity.HasOne(d => d.IdCarSerieNavigation)
                    .WithMany(p => p.CarTrims)
                    .HasForeignKey(d => d.IdCarSerie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_trim_car_serie");

                entity.HasOne(d => d.IdCarTypeNavigation)
                    .WithMany(p => p.CarTrims)
                    .HasForeignKey(d => d.IdCarType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_car_trim_car_type");
            });

            builder.Entity<CarType>(entity =>
            {
                entity.HasKey(e => e.IdCarType)
                    .HasName("pk_car_type");

                entity.ToTable("car_type");

                entity.HasComment("TRIAL");

                entity.Property(e => e.IdCarType)
                    .HasColumnName("id_car_type")
                    .HasComment("TRIAL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .HasComment("TRIAL");

                entity.Property(e => e.Trial152)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRIAL152")
                    .IsFixedLength(true)
                    .HasComment("TRIAL");
            });

        }
    }
}