using System.Collections.Generic;
using WebGrease.Css.Extensions;

namespace HedyoCom_AspNet.Models
{
    public class GiftTemplate
    {
        public int Id { get; set; }
        public virtual GiftImage GiftImage { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

        //private static readonly GiftTemplate[] _templates = {
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 1, Filename = "aspirator.png"}, Name = "Aspiratör", Price = 500m},    // aspirator.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 2, Filename = "bilgisayar.png"}, Name = "Bilgisayar", Price = 4500m},   // bilgisayar.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 3, Filename = "bulasik.png"}, Name = "Bulaşık Makinesi", Price = 3500m},      // bulasik.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 4, Filename = "buzdolabi.png"}, Name = "Buzdolabı", Price = 5000m},    // buzdolabi.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 5, Filename = "calismaodasi.png"}, Name = "Çalışma Odası", Price = 15000m}, // calismaodasi.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 6, Filename = "camasir.png"}, Name = "Çamaşır Makinesi", Price = 3000m},      // camasir.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 7, Filename = "caymakinesi.png"}, Name = "Çay Makinesi", Price = 100m},  // caymakinesi.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 8, Filename = "diger.png"}, Name = "Diğer", Price = 5000m},        // diger.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 9, Filename = "dikis.png"}, Name = "Dikiş Makinesi", Price = 500m},        // dikis.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 10, Filename = "ekmekkizartma.png"}, Name = "Ekmek Kızartma Makinesi", Price = 200m},// ekmekkizartma.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 11, Filename = "firin.png"}, Name = "Fırın-Ocak", Price = 4500m},        // firin.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 12, Filename = "fotograf.png"}, Name = "Fotoğraf Makinesi", Price = 1500m},     // fotograf.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 13, Filename = "fritoz.png"}, Name = "Fritöz", Price = 600m},       // fritoz.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 14, Filename = "gardrop.png"}, Name = "Gardırop", Price = 2000m},      // gardrop.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 15, Filename = "kahvemakinesi.png"}, Name = "Kahve Makinesi", Price = 400m},// kahvemakinesi.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 16, Filename = "klima.png"}, Name = "Klima", Price = 2000m},        // klima.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 17, Filename = "kutuphane.png"}, Name = "Kütüphane", Price = 5000m},    // kutuphane.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 18, Filename = "matkap.png"}, Name = "Matkap", Price = 200m},       // matkap.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 19, Filename = "microfirin.png"}, Name = "Mikrodalga Fırın", Price = 1500m},   // microfirin.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 20, Filename = "mikser.png"}, Name = "Mikser", Price = 300m},       // mikser.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 21, Filename = "muzikseti.png"}, Name = "Müzik Seti", Price = 1000m},    // muzikseti.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 22, Filename = "oturmaodasi.png"}, Name = "Oturma Odası", Price = 24000m},  // oturmaodasi.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 23, Filename = "sackurutma.png"}, Name = "Saç Kurutma Makinesi", Price = 200m},   // sackurutma.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 24, Filename = "salon.png"}, Name = "Salon Takımı", Price = 30000m},        // salon.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 25, Filename = "supurge.png"}, Name = "Elektrik Süpürgesi", Price = 1200m},      // supurge.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 26, Filename = "tablet.png"}, Name = "Tablet", Price = 1000m},       // tablet.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 27, Filename = "televizyon.png"}, Name = "Televizyon", Price = 3500m},   // televizyon.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 28, Filename = "utu.png"}, Name = "Ütü", Price = 800m},          // utu.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 29, Filename = "yatakodasi.png"}, Name = "Yatak Odası", Price = 23000m},   // yatakodasi.png
        //    new GiftTemplate {GiftImage = new GiftImage {Id = 30, Filename = "yazici.png"}, Name = "Yazıcı", Price = 400m}        // yazici.png
        //};

        //public static IReadOnlyCollection<GiftTemplate> Templates => _templates.ToSafeReadOnlyCollection();

        /* TODO RELEASE: SQL FOR INITIALIZING
         * SET IDENTITY_INSERT [dbo].[GiftImages] ON
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (1, N'aspirator.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (2, N'bilgisayar.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (3, N'bulasik.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (4, N'buzdolabi.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (5, N'calismaodasi.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (6, N'camasir.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (7, N'caymakinesi.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (8, N'diger.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (9, N'dikis.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (10, N'ekmekkizartma.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (11, N'firin.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (12, N'fotograf.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (13, N'fritoz.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (14, N'gardrop.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (15, N'kahvemakinesi.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (16, N'klima.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (17, N'kutuphane.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (18, N'matkap.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (19, N'microfirin.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (20, N'mikser.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (21, N'muzikseti.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (22, N'oturmaodasi.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (23, N'sackurutma.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (24, N'salon.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (25, N'supurge.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (26, N'tablet.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (27, N'televizyon.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (28, N'utu.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (29, N'yatakodasi.png')
INSERT INTO [dbo].[GiftImages] ([Id], [Filename]) VALUES (30, N'yazici.png')
SET IDENTITY_INSERT [dbo].[GiftImages] OFF
         */
    }
}