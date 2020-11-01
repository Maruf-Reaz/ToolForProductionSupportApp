using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dynamo.Model.Common.Authentication;
using Dynamo.Model.Common;
using Dynamo.Model.Common.Infrastructure;
using Dynamo.Model.Factories;
using Dynamo.Model.Operations;
using Dynamo.Model.Skills;
using Dynamo.Model.Machines;
using Dynamo.Model.Incentives;
using Dynamo.Model.ELearnings;
using Dynamo.Model.Sot;
using Dynamo.Model.LineBalancing;
using Dynamo.Model.CICalendar;
using Dynamo.Model.TPSProjects;
using Microsoft.AspNetCore.Identity;
using Dynamo.Model.Common.UserProfiles;

namespace Dynamo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        //Join Table for Many to Many Relationships between Models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SectionStandardOperationTime>()
            .HasKey(m => new { m.StandardOperationTimeId, m.SectionId });

            modelBuilder.Entity<SectionStandardOperationTime>()
                .HasOne(m => m.StandardOperationTime)
                .WithMany(d => d.SectionStandardOperationTimes)
                .HasForeignKey(k => k.StandardOperationTimeId);

            modelBuilder.Entity<SectionStandardOperationTime>()
                .HasOne(m => m.Section)
                .WithMany(d => d.SectionStandardOperationTimes)
                .HasForeignKey(k => k.SectionId);

            modelBuilder.Entity<ManualJobStandardOperationTimeItem>()
            .HasKey(m => new { m.StandardOperationTimeItemId, m.ManualJobId });

            modelBuilder.Entity<ManualJobStandardOperationTimeItem>()
                .HasOne(m => m.StandardOperationTimeItem)
                .WithMany(d => d.ManualJobStandardOperationTimeItems)
                .HasForeignKey(k => k.StandardOperationTimeItemId);

            modelBuilder.Entity<ManualJobStandardOperationTimeItem>()
                .HasOne(m => m.ManualJob)
                .WithMany(d => d.ManualJobStandardOperationTimeItems)
                .HasForeignKey(k => k.ManualJobId);

            modelBuilder.Entity<CssClass>().HasData(
                new CssClass { Id = 1, Name = "ToogleSideMenuClass", IsActive = true },
                new CssClass { Id = 2, Name = "Sidebar Radius", IsActive = false },
                new CssClass { Id = 3, Name = "Dark Navigation", IsActive = false }
             );

            modelBuilder.Entity<Factory>().HasData(
                new Factory { Id = 1, Name = "KSI", Address = "Chittagong", PhoneNumber = "A-001" },
                new Factory { Id = 2, Name = "YSS", Address = "Chittagong", PhoneNumber = "A-002" },
                new Factory { Id = 3, Name = "MAF", Address = "Chittagong", PhoneNumber = "A-003" },
                new Factory { Id = 4, Name = "RFL", Address = "Chittagong", PhoneNumber = "A-004" },
                new Factory { Id = 5, Name = "APEX", Address = "Chittagong", PhoneNumber = "A-005" },
                new Factory { Id = 6, Name = "EDISON", Address = "Chittagong", PhoneNumber = "A-006" }
             );

            modelBuilder.Entity<DataSource>().HasData(
                new DataSource { Id = 1, Name = "Supplier Vdo", CalculationFileName = "STOPWATCH/VIDEO" },
                new DataSource { Id = 2, Name = "Sam Pace", CalculationFileName = "PACE" }
             );

            modelBuilder.Entity<ApplicationRole>().HasData(
                //Super Admin Role
                new ApplicationRole { Id = "f686b56a-6135-4221-a0b0-bdec547e3waw", Name = "SuperAdmin", NormalizedName = "SUPERADMIN", ConcurrencyStamp = "da9a3b0e-8b6f-8529-71d0-4fd255e23f15", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has All Permissions" },
                //Admin Roles
                new ApplicationRole { Id = "w585b57b-7456-8222-c0d0-bdec765e3awa", Name = "MAFAdmin", NormalizedName = "MAFADMIN", ConcurrencyStamp = "ea9a3b0f-9b5f-7153-81e0-4fd799e23f16", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Maximum Permissions" },
                new ApplicationRole { Id = "m121b57c-9025-9223-e0f0-bdec765e3bgb", Name = "KSIAdmin", NormalizedName = "KSIADMIN", ConcurrencyStamp = "la9a3b0g-8b4f-6218-91e0-4fd366e23f17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Maximum Permissions" },
                new ApplicationRole { Id = "m347b57d-8617-9224-g0h0-bdec765e3lml", Name = "YSSAdmin", NormalizedName = "YSSADMIN", ConcurrencyStamp = "la9a3b0g-7b3f-8412-11e0-4fd366e23f18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Maximum Permissions" },
                new ApplicationRole { Id = "m915b57e-2431-9225-i0j0-bdec765e3pgp", Name = "RFLAdmin", NormalizedName = "RFLADMIN", ConcurrencyStamp = "la9a3b0g-6b2f-9864-21e0-4fd366e23f19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Maximum Permissions" },
                new ApplicationRole { Id = "m173b57f-8519-9226-k0l0-bdec765e3rir", Name = "APEXAdmin", NormalizedName = "APEXADMIN", ConcurrencyStamp = "la9a3b0g-5b1f-7852-31e0-4fd366e23f20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Maximum Permissions" },
                new ApplicationRole { Id = "m354b57g-4013-9227-m0n0-bdec765e3oeo", Name = "EDISONAdmin", NormalizedName = "EDISONADMIN", ConcurrencyStamp = "la9a3b0g-4b9f-4102-41e0-4fd366e23f21", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Maximum Permissions" },
                //General User Roles
                //For MAF
                new ApplicationRole { Id = "e858c58l-3251-9888-c1d1-bdec765e3pip", Name = "MAFUserOfLineBalancing", NormalizedName = "MAFUSEROFLINEBALANCING", ConcurrencyStamp = "ah8b4b9j-3n6o-3491-73e1-4fd800e23g17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "e858c58l-3252-9889-c2d2-bdec765e3pip", Name = "MAFUserOfIncentive", NormalizedName = "MAFUSEROFINCENTIVE", ConcurrencyStamp = "ah8b4b9j-3n6o-3492-73e1-4fd800e23g17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "e858c58l-3253-9890-c3d3-bdec765e3pip", Name = "MAFUserOfSkillMatrix", NormalizedName = "MAFUSEROFSKILLMATRIX", ConcurrencyStamp = "ah8b4b9j-3n6o-3493-73e1-4fd800e23g17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "e858c58l-3254-9891-c4d4-bdec765e3pip", Name = "MAFUserOfSOT", NormalizedName = "MAFUSEROFSOT", ConcurrencyStamp = "ah8b4b9j-3n6o-3494-73e1-4fd800e23g17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "e858c58l-3255-9892-c5d5-bdec765e3pip", Name = "MAFUserOfArchive", NormalizedName = "MAFUSEROFARCHIVE", ConcurrencyStamp = "aar8b4b9j-3n6o-3495-73e1-4fd157e23g17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                //For KSI
                new ApplicationRole { Id = "a212b59c-5209-3999-e1f1-bdec785e4lil", Name = "KSIUserOfLineBalancing", NormalizedName = "KSIUSEROFLINEBALANCING", ConcurrencyStamp = "eu5a3b0g-9c5g-9126-45e1-4fd366e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "a212b59c-5210-4000-e2f2-bdec785e4lil", Name = "KSIUserOfIncentive", NormalizedName = "KSIUSEROFINCENTIVE", ConcurrencyStamp = "eu5a3b0g-9c5g-9127-45e1-4fd366e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "a212b59c-5211-4001-e3f3-bdec785e4lil", Name = "KSIUserOfSkillMatrix", NormalizedName = "KSIUSEROFSKILLMATRIX", ConcurrencyStamp = "eu5a3b0g-9c5g-9128-45e1-4fd366e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "a212b59c-5212-4002-e4f4-bdec785e4lil", Name = "KSIUserOfSOT", NormalizedName = "KSIUSEROFSOT", ConcurrencyStamp = "eu5a3b0g-9c5g-9129-45e1-4fd366e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "a212b59c-5213-4003-e5f5-bdec785e4lil", Name = "KSIUserOfArchive", NormalizedName = "KSIUSEROFARCHIVE", ConcurrencyStamp = "aar5a3b0g-9c5g-9130-99e1-4fd155e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                //For YSS
                new ApplicationRole { Id = "g734b69c-9461-6111-g1h1-bdec785e4imi", Name = "YSSUserOfLineBalancing", NormalizedName = "YSSUSEROFLINEBALANCING", ConcurrencyStamp = "ri5a3b0g-8d4j-2184-95e1-4fd352e23i19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "g734b69c-9462-6112-g2h2-bdec785e4imi", Name = "YSSUserOfIncentive", NormalizedName = "YSSUSEROFINCENTIVE", ConcurrencyStamp = "ri5a3b0g-8d4j-2185-95e1-4fd352e23i19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "g734b69c-9463-6113-g3h3-bdec785e4imi", Name = "YSSUserOfSkillMatrix", NormalizedName = "YSSUSEROFSKILLMATRIX", ConcurrencyStamp = "ri5a3b0g-8d4j-2186-95e1-4fd352e23i19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "g734b69c-9464-6114-g4h4-bdec785e4imi", Name = "YSSUserOfSOT", NormalizedName = "YSSUSEROFSOT", ConcurrencyStamp = "ri5a3b0g-8d4j-2187-95e1-4fd352e23i19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "g734b69c-9465-6115-g5h5-bdec785e4imi", Name = "YSSUserOfArchive", NormalizedName = "YSSUSEROFARCHIVE", ConcurrencyStamp = "aar5a3b0g-8d4j-2188-95e1-4fd156e23i19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                //For RFL
                new ApplicationRole { Id = "z323b59c-5209-5344-i1j1-bdec735e4gpg", Name = "RFLUserOfLineBalancing", NormalizedName = "RFLUSEROFLINEBALANCING", ConcurrencyStamp = "al5a3b0g-7c3p-4517-45e1-4fd377e23i20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "z323b59c-5210-5345-i2j2-bdec735e4gpg", Name = "RFLUserOfIncentive", NormalizedName = "RFLUSEROFINCENTIVE", ConcurrencyStamp = "al5a3b0g-7c3p-4518-45e1-4fd377e23i20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "z323b59c-5211-5346-i3j3-bdec735e4gpg", Name = "RFLUserOfSkillMatrix", NormalizedName = "RFLUSEROFSKILLMATRIX", ConcurrencyStamp = "al5a3b0g-7c3p-4519-45e1-4fd377e23i20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "z323b59c-5212-5347-i4j4-bdec735e4gpg", Name = "RFLUserOfSOT", NormalizedName = "RFLUSEROFSOT", ConcurrencyStamp = "al5a3b0g-7c3p-4520-45e1-4fd377e23i20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "z323b59c-5213-5348-i5j5-bdec735e4gpg", Name = "RFLUserOfArchive", NormalizedName = "RFLUSEROFARCHIVE", ConcurrencyStamp = "aar5a3b0g-7c3p-4521-45e1-4fd158e23i20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                //For APEX
                new ApplicationRole { Id = "x212b59c-3269-1666-k1l1-bdec785e4gig", Name = "APEXUserOfLineBalancing", NormalizedName = "APEXUSEROFLINEBALANCING", ConcurrencyStamp = "lq5a3b1g-6c2g-3254-93e1-4fd255e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "x212b59c-3270-1667-k2l2-bdec785e4gig", Name = "APEXUserOfIncentive", NormalizedName = "APEXUSEROFINCENTIVE", ConcurrencyStamp = "lq5a3b1g-6c2g-3255-93e1-4fd255e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "x212b59c-3271-1668-k3l3-bdec785e4gig", Name = "APEXUserOfSkillMatrix", NormalizedName = "APEXUSEROFSKILLMATRIX", ConcurrencyStamp = "lq5a3b1g-6c2g-3256-93e1-4fd255e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "x212b59c-3272-1669-k4l4-bdec785e4gig", Name = "APEXUserOfSOT", NormalizedName = "APEXUSEROFSOT", ConcurrencyStamp = "lq5a3b1g-6c2g-3257-93e1-4fd255e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "x212b59c-3273-1670-k5l5-bdec785e4gig", Name = "APEXUserOfArchive", NormalizedName = "APEXUSEROFARCHIVE", ConcurrencyStamp = "aar5a3b1g-6c2g-3258-93e1-4fd159e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                //For EDISON
                new ApplicationRole { Id = "v212b59c-6348-6121-m1n1-bdec785e4iki", Name = "EDISONUserOfLineBalancing", NormalizedName = "EDISONUSEROFLINEBALANCING", ConcurrencyStamp = "ql5a3b1g-5c2g-6512-81e1-4fd511e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "v212b59c-6349-6122-m2n2-bdec785e4iki", Name = "EDISONUserOfIncentive", NormalizedName = "EDISONUSEROFINCENTIVE", ConcurrencyStamp = "ql5a3b1g-5c2g-6513-81e1-4fd511e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "v212b59c-6350-6123-m3n3-bdec785e4iki", Name = "EDISONUserOfSkillMatrix", NormalizedName = "EDISONUSEROFSKILLMATRIX", ConcurrencyStamp = "ql5a3b1g-5c2g-6514-81e1-4fd511e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "v212b59c-6351-6124-m4n4-bdec785e4iki", Name = "EDISONUserOfSOT", NormalizedName = "EDISONUSEROFSOT", ConcurrencyStamp = "ql5a3b1g-5c2g-6515-81e1-4fd511e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },
                new ApplicationRole { Id = "v212b59c-6352-6125-m5n5-bdec785e4iki", Name = "EDISONUserOfArchive", NormalizedName = "EDISONUSEROFARCHIVE", ConcurrencyStamp = "aar5a3b1g-5c2g-6516-81e1-4fd160e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Several Permissions" },

                //Viewer Roles
                new ApplicationRole { Id = "e858c58l-1212-4222-c2d2-bdec131e3pbp", Name = "MAFViewer", NormalizedName = "MAFVIEWER", ConcurrencyStamp = "ah8b4b9j-3n6o-3491-73e1-4fd800e23g17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" },
                new ApplicationRole { Id = "a212b59c-3434-4333-e2f2-bdec132e4lbl", Name = "KSIViewer", NormalizedName = "KSIVIEWER", ConcurrencyStamp = "eu5a3b0g-9c5g-9126-45e1-4fd366e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" },
                new ApplicationRole { Id = "g734b69c-5656-4555-g2h2-bdec133e4ibi", Name = "YSSViewer", NormalizedName = "YSSVIEWER", ConcurrencyStamp = "ri5a3b0g-8d4j-2184-95e1-4fd352e23i19", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" },
                new ApplicationRole { Id = "z323b59c-7878-4666-i2j2-bdec134e4gbg", Name = "RFLViewer", NormalizedName = "RFLVIEWER", ConcurrencyStamp = "al5a3b0g-7c3p-4517-45e1-4fd377e23i20", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" },
                new ApplicationRole { Id = "x212b59c-9191-4777-k2l2-bdec135e4gbg", Name = "APEXViewer", NormalizedName = "APEXVIEWER", ConcurrencyStamp = "lq5a3b1g-6c2g-3254-93e1-4fd255e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" },
                new ApplicationRole { Id = "v212b59c-3113-4888-m2n2-bdec136e4ibi", Name = "EDISONViewer", NormalizedName = "EDISONVIEWER", ConcurrencyStamp = "ql5a3b1g-5c2g-6512-81e1-4fd511e23i18", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" }
             );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser() { Id = "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", UserName = "SuperAdmin", NormalizedUserName = "SUPERADMIN", Email = "monir.hossain@decathlon.com", NormalizedEmail = "MONIR.HOSSAIN@DECATHLON.COM", PasswordHash = "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", SecurityStamp = "5TDMS5CNA2GYJK2URB4GDOCQI2NI7EHJ", ConcurrencyStamp = "26d21881-0a3a-44ab-aa4d-10664ace1e2d", FactoryId = 1 },
                new ApplicationUser() { Id = "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", UserName = "NeuroStorm", NormalizedUserName = "NEUROSTORM", Email = "info@neurostormsoft.com", NormalizedEmail = "INFO@NEUROSTORMSOFT.COM", PasswordHash = "AQAAAAEAACcQAAAAEDNQpE8hqBcgCek1F6WkCX1siCTa4B6dSKD+ZjbxScznzTuQffacsPK9nKL7gK+3DQ==", SecurityStamp = "6UEMS5CNA2GYLO2URB5GDOQQI2NI7FIL", ConcurrencyStamp = "62d21881-0b3b-55ab-aa4d-61220ace1e2e", FactoryId = 1 }
             );

            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile { Id = 1, ApplicationUserId = "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", PhotoName = "No File" },
                new UserProfile { Id = 2, ApplicationUserId = "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", PhotoName = "No File" }
             );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                //Super Admin
                new IdentityUserRole<string>() { UserId = "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", RoleId = "f686b56a-6135-4221-a0b0-bdec547e3waw" },
                new IdentityUserRole<string>() { UserId = "9cd7ff85-f36c-69b6-ea27-dbb23cbfb508", RoleId = "f686b56a-6135-4221-a0b0-bdec547e3waw" }
             );

            modelBuilder.Entity<NotificationType>().HasData(
                new NotificationType { Id = 1, Name = "Chat" },
                new NotificationType { Id = 2, Name = "Event" }
             );

            modelBuilder.Entity<SkillMatrixRange>().HasData(
                new SkillMatrixRange { Id = 1, Code = 1, Level = "", FactoryId = 1 },
                new SkillMatrixRange { Id = 2, Code = 2, Level = "", FactoryId = 1 },
                new SkillMatrixRange { Id = 3, Code = 3, Level = "", FactoryId = 1 },
                new SkillMatrixRange { Id = 4, Code = 4, Level = "", FactoryId = 1 },
                new SkillMatrixRange { Id = 5, Code = 5, Level = "", FactoryId = 1 },

                new SkillMatrixRange { Id = 6, Code = 1, Level = "", FactoryId = 2 },
                new SkillMatrixRange { Id = 7, Code = 2, Level = "", FactoryId = 2 },
                new SkillMatrixRange { Id = 8, Code = 3, Level = "", FactoryId = 2 },
                new SkillMatrixRange { Id = 9, Code = 4, Level = "", FactoryId = 2 },
                new SkillMatrixRange { Id = 10, Code = 5, Level = "", FactoryId = 2 },

                new SkillMatrixRange { Id = 11, Code = 1, Level = "", FactoryId = 3 },
                new SkillMatrixRange { Id = 12, Code = 2, Level = "", FactoryId = 3 },
                new SkillMatrixRange { Id = 13, Code = 3, Level = "", FactoryId = 3 },
                new SkillMatrixRange { Id = 14, Code = 4, Level = "", FactoryId = 3 },
                new SkillMatrixRange { Id = 15, Code = 5, Level = "", FactoryId = 3 },

                new SkillMatrixRange { Id = 16, Code = 1, Level = "", FactoryId = 4 },
                new SkillMatrixRange { Id = 17, Code = 2, Level = "", FactoryId = 4 },
                new SkillMatrixRange { Id = 18, Code = 3, Level = "", FactoryId = 4 },
                new SkillMatrixRange { Id = 19, Code = 4, Level = "", FactoryId = 4 },
                new SkillMatrixRange { Id = 20, Code = 5, Level = "", FactoryId = 4 },

                new SkillMatrixRange { Id = 21, Code = 1, Level = "", FactoryId = 5 },
                new SkillMatrixRange { Id = 22, Code = 2, Level = "", FactoryId = 5 },
                new SkillMatrixRange { Id = 23, Code = 3, Level = "", FactoryId = 5 },
                new SkillMatrixRange { Id = 24, Code = 4, Level = "", FactoryId = 5 },
                new SkillMatrixRange { Id = 25, Code = 5, Level = "", FactoryId = 5 },

                new SkillMatrixRange { Id = 26, Code = 1, Level = "", FactoryId = 6 },
                new SkillMatrixRange { Id = 27, Code = 2, Level = "", FactoryId = 6 },
                new SkillMatrixRange { Id = 28, Code = 3, Level = "", FactoryId = 6 },
                new SkillMatrixRange { Id = 29, Code = 4, Level = "", FactoryId = 6 },
                new SkillMatrixRange { Id = 30, Code = 5, Level = "", FactoryId = 6 }

             );

            modelBuilder.Entity<CalculationStatus>().HasData(
                new CalculationStatus { Id = 1, Name = "Not Done" },
                new CalculationStatus { Id = 2, Name = "Done" },
                new CalculationStatus { Id = 3, Name = "In Progress" }
             );

            modelBuilder.Entity<ValidationStatus>().HasData(
                new ValidationStatus { Id = 1, Name = "Validated" },
                new ValidationStatus { Id = 2, Name = "Not Validated" },
                new ValidationStatus { Id = 3, Name = "In Progress" }
             );

        }

        public DbSet<CssClass> CssClasses { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationApplicationUser> UserNotifications { get; set; }
        public DbSet<NotificationGroup> NotificationGroups { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Chat> Chats { get; set; }

        //Factories
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Section> Sections { get; set; }

        //Operation
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Operator> Operators { get; set; }

        //Skills
        public DbSet<SkillMatrix> SkillMatrixs { get; set; }
        public DbSet<SkillMatrixRange> SkillMatrixRanges { get; set; }

        //Machine
        public DbSet<Machine> Machines { get; set; }

        //Incentives 
        public DbSet<LineIncentive> LineIncentives { get; set; }
        public DbSet<IncentiveVariable> IncentiveVariables { get; set; }
        public DbSet<MonthlyVariableValue> MonthlyVariableValues { get; set; }
        public DbSet<MonthlySectionEarningPoint> MonthlySectionEarningPoints { get; set; }
        public DbSet<TargetValue> TargetValues { get; set; }
        public DbSet<AchievedValue> AchievedValues { get; set; }
        public DbSet<PointValue> PointValues { get; set; }

        //ELearnings
        public DbSet<ELearning> ELearnings { get; set; }

        //Projects
        public DbSet<TPSProject> TPSProjects { get; set; }

        //SOT
        public DbSet<SotModel> SotModels { get; set; }
        public DbSet<SignSport> SignSports { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<StandardOperationTime> StandardOperationTimes { get; set; }
        public DbSet<SectionStandardOperationTime> SectionStandardOperationTimes { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<StandardOperationTimeItem> StandardOperationTimeItems { get; set; }
        public DbSet<StandardOperationTimeSubItem> StandardOperationTimeSubItems { get; set; }
        public DbSet<ManualJob> ManualJobs { get; set; }
        public DbSet<ManualJobStandardOperationTimeItem> ManualJobStandardOperationTimeItems { get; set; }
        public DbSet<CalculationStatus> CalculationStatuses { get; set; }
        public DbSet<ValidationStatus> ValidationStatuses { get; set; }

        //Line Balancing
        public DbSet<LineBalancingState> LineBalancingStates { get; set; }
        public DbSet<ParticularLineBalancing> ParticularLineBalancings { get; set; }

        //Ci Calendar
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<EventGuest> EventGuests { get; set; }

        //User Profile
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
