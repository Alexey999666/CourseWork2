using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseWork2.ModelsDB;

public partial class УчетДеятельностиМузеяContext : DbContext
{
    public УчетДеятельностиМузеяContext()
    {
    }

    public УчетДеятельностиМузеяContext(DbContextOptions<УчетДеятельностиМузеяContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExhibitionManyExhibitsAndHighOrAverageAttendance> ExhibitionManyExhibitsAndHighOrAverageAttendances { get; set; }

    public virtual DbSet<ExhibitsAndExhibitionsByAttendance> ExhibitsAndExhibitionsByAttendances { get; set; }

    public virtual DbSet<GuidedtoursByCostAndDuration> GuidedtoursByCostAndDurations { get; set; }

    public virtual DbSet<InformationExhibitByName> InformationExhibitByNames { get; set; }

    public virtual DbSet<SuppliesPurchasesWithHighWorkExperience> SuppliesPurchasesWithHighWorkExperiences { get; set; }

    public virtual DbSet<Выставка> Выставкаs { get; set; }

    public virtual DbSet<Поставки> Поставкиs { get; set; }

    public virtual DbSet<Сотрудники> Сотрудникиs { get; set; }

    public virtual DbSet<Экскурсии> Экскурсииs { get; set; }

    public virtual DbSet<Экспонат> Экспонатs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=Учет деятельности музея; User=исп-31; Password= 1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExhibitionManyExhibitsAndHighOrAverageAttendance>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ExhibitionManyExhibitsAndHighOrAverageAttendance");

            entity.Property(e => e.Тематика).HasMaxLength(50);
        });

        modelBuilder.Entity<ExhibitsAndExhibitionsByAttendance>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ExhibitsAndExhibitionsByAttendance");

            entity.Property(e => e.ДатаНачала).HasColumnType("datetime");
            entity.Property(e => e.ДатаОкончания).HasColumnType("datetime");
            entity.Property(e => e.НазваниеЭкспоната).HasMaxLength(50);
            entity.Property(e => e.Тематика).HasMaxLength(50);
        });

        modelBuilder.Entity<GuidedtoursByCostAndDuration>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GuidedtoursByCostAndDuration");

            entity.Property(e => e.Idсотрудника).HasColumnName("IDСотрудника");
            entity.Property(e => e.Idэкскурсии)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDЭкскурсии");
            entity.Property(e => e.Стоимость).HasColumnType("money");
        });

        modelBuilder.Entity<InformationExhibitByName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("InformationExhibitByName");

            entity.Property(e => e.ИнвентарныйНомер).ValueGeneratedOnAdd();
            entity.Property(e => e.НазваниеЭкспоната).HasMaxLength(50);
            entity.Property(e => e.Подлиность).HasMaxLength(50);
            entity.Property(e => e.Стоимость).HasColumnType("money");
        });

        modelBuilder.Entity<SuppliesPurchasesWithHighWorkExperience>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SuppliesPurchasesWithHighWorkExperience");

            entity.Property(e => e.ДатаПоставки).HasColumnType("datetime");
            entity.Property(e => e.СпособПолучения).HasMaxLength(50);
        });

        modelBuilder.Entity<Выставка>(entity =>
        {
            entity.HasKey(e => e.КодВыставки);

            entity.ToTable("Выставка");

            entity.Property(e => e.КодВыставки).ValueGeneratedNever();
            entity.Property(e => e.ДатаНачала).HasColumnType("datetime");
            entity.Property(e => e.ДатаОкончания).HasColumnType("datetime");
            entity.Property(e => e.Посещаемость).HasMaxLength(50);
            entity.Property(e => e.Тематика).HasMaxLength(50);
        });

        modelBuilder.Entity<Поставки>(entity =>
        {
            entity.HasKey(e => e.КодПоставки);

            entity.ToTable("Поставки");

            entity.Property(e => e.КодПоставки).ValueGeneratedNever();
            entity.Property(e => e.ДатаПоставки).HasColumnType("datetime");
            entity.Property(e => e.СпособПолучения).HasMaxLength(50);

            entity.HasOne(d => d.ОтветственноеЛицоNavigation).WithMany(p => p.Поставкиs)
                .HasForeignKey(d => d.ОтветственноеЛицо)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Поставки_Сотрудники");
        });

        modelBuilder.Entity<Сотрудники>(entity =>
        {
            entity.ToTable("Сотрудники", tb => tb.HasTrigger("trg_CorrectWorkExperience"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Должность).HasMaxLength(50);
            entity.Property(e => e.Имя).HasMaxLength(50);
            entity.Property(e => e.Отчество).HasMaxLength(50);
            entity.Property(e => e.Фамилия).HasMaxLength(50);
        });

        modelBuilder.Entity<Экскурсии>(entity =>
        {
            entity.HasKey(e => e.Idэкскурсии);

            entity.ToTable("Экскурсии", tb => tb.HasTrigger("trg_CheckTourSizeInt"));

            entity.Property(e => e.Idэкскурсии).HasColumnName("IDЭкскурсии");
            entity.Property(e => e.Idсотрудника).HasColumnName("IDСотрудника");
            entity.Property(e => e.Стоимость).HasColumnType("money");

            entity.HasOne(d => d.IdсотрудникаNavigation).WithMany(p => p.Экскурсииs)
                .HasForeignKey(d => d.Idсотрудника)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Экскурсии_Сотрудники");

            entity.HasOne(d => d.КодВыставкиNavigation).WithMany(p => p.Экскурсииs)
                .HasForeignKey(d => d.КодВыставки)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Экскурсии_Выставка");
        });

        modelBuilder.Entity<Экспонат>(entity =>
        {
            entity.HasKey(e => e.ИнвентарныйНомер);

            entity.ToTable("Экспонат", tb => tb.HasTrigger("trg_CheckExhibitUnique"));

            entity.Property(e => e.НазваниеЭкспоната).HasMaxLength(50);
            entity.Property(e => e.Подлиность).HasMaxLength(50);
            entity.Property(e => e.Стоимость).HasColumnType("money");

            entity.HasOne(d => d.КодВыставкиNavigation).WithMany(p => p.Экспонатs)
                .HasForeignKey(d => d.КодВыставки)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Экспонат_Выставка");

            entity.HasOne(d => d.КодПоставкиNavigation).WithMany(p => p.Экспонатs)
                .HasForeignKey(d => d.КодПоставки)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Экспонат_Поставки");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
