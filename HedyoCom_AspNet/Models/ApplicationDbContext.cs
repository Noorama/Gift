using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HedyoCom_AspNet.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {}

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gift>().Property(p => p.Price).HasPrecision(12, 2);
            modelBuilder.Entity<GiftPayment>().Property(p => p.Amount).HasPrecision(12, 2);
            modelBuilder.Entity<GiftTemplate>().Property(p => p.Price).HasPrecision(12, 2);

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventInvitation> EventInvitations { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftPayment> GiftPayments { get; set; }
        public DbSet<GiftImage> GiftImages { get; set; }
        public DbSet<GiftTemplate> GiftTemplates { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
    }

    class ApplicationContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            var wedding = new EventType { Name = "wedding" };
            var birthday = new EventType { Name = "birthday" };
            var babyBorn = new EventType { Name = "babyBorn" };
            var new_House_Keeping = new EventType { Name = "new_House_Keeping" };
            var new_Work_Open = new EventType { Name = "new_Work_Open" };
            var pipi_Cuting = new EventType { Name = "pipi_Cuting" };
            var custom = new EventType { Name = "custom" };

            var store = new UserStore<ApplicationUser>(db);
            var manager = new ApplicationUserManager(store);
            var testUser = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", EmailConfirmed = true };
            var result = manager.Create(testUser, "123456");

            var events = new List<EventType> { wedding, birthday, babyBorn, new_House_Keeping, new_Work_Open, pipi_Cuting, custom };
            var giftTemplate = new List<GiftTemplate>
            {
                new GiftTemplate { GiftImage = new GiftImage { Filename = "aspirator.png" }, Name = "Aspiratör", Price = 500m },    // aspirator.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "bilgisayar.png" }, Name = "Bilgisayar", Price = 4500m },   // bilgisayar.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "bulasik.png" }, Name = "Bulaşık Makinesi", Price = 3500m },      // bulasik.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "buzdolabi.png" }, Name = "Buzdolabı", Price = 5000m },    // buzdolabi.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "calismaodasi.png" }, Name = "Çalışma Odası", Price = 15000m }, // calismaodasi.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "camasir.png" }, Name = "Çamaşır Makinesi", Price = 3000m },      // camasir.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "caymakinesi.png" }, Name = "Çay Makinesi", Price = 100m },  // caymakinesi.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "diger.png" }, Name = "Diğer", Price = 5000m },        // diger.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "dikis.png" }, Name = "Dikiş Makinesi", Price = 500m },        // dikis.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "ekmekkizartma.png" }, Name = "Ekmek Kızartma Makinesi", Price = 200m },// ekmekkizartma.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "firin.png" }, Name = "Fırın-Ocak", Price = 4500m },        // firin.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "fotograf.png" }, Name = "Fotoğraf Makinesi", Price = 1500m },     // fotograf.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "fritoz.png" }, Name = "Fritöz", Price = 600m },       // fritoz.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "gardrop.png" }, Name = "Gardırop", Price = 2000m },      // gardrop.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "kahvemakinesi.png" }, Name = "Kahve Makinesi", Price = 400m },// kahvemakinesi.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "klima.png" }, Name = "Klima", Price = 2000m },        // klima.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "kutuphane.png" }, Name = "Kütüphane", Price = 5000m },    // kutuphane.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "matkap.png" }, Name = "Matkap", Price = 200m },       // matkap.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "microfirin.png" }, Name = "Mikrodalga Fırın", Price = 1500m },   // microfirin.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "mikser.png" }, Name = "Mikser", Price = 300m },       // mikser.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "muzikseti.png" }, Name = "Müzik Seti", Price = 1000m },    // muzikseti.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "oturmaodasi.png" }, Name = "Oturma Odası", Price = 24000m },  // oturmaodasi.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "sackurutma.png" }, Name = "Saç Kurutma Makinesi", Price = 200m },   // sackurutma.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "salon.png" }, Name = "Salon Takımı", Price = 30000m },        // salon.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "supurge.png" }, Name = "Elektrik Süpürgesi", Price = 1200m },      // supurge.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "tablet.png" }, Name = "Tablet", Price = 1000m },       // tablet.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "televizyon.png" }, Name = "Televizyon", Price = 3500m },   // televizyon.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "utu.png" }, Name = "Ütü", Price = 800m },          // utu.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "yatakodasi.png" }, Name = "Yatak Odası", Price = 23000m },   // yatakodasi.png
                new GiftTemplate { GiftImage = new GiftImage { Filename = "yazici.png" }, Name = "Yazıcı", Price = 400m }        // yazici.png
            };

            giftTemplate.ForEach(giftTemp => db.GiftTemplates.Add(giftTemp));
            events.ForEach(eve => db.EventTypes.Add(eve));

            db.SaveChanges();
        }
    }
}