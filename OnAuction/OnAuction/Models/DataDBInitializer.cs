using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class DataDBInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext db)
        {
            DateTime CurrentTime = DateTime.Now;

            db.Roles.Add(new Role { Id = 1, Name = "buyer" });
            db.Roles.Add(new Role { Id = 2, Name = "seller" });
            db.Roles.Add(new Role { Id = 3, Name = "admin" });

            db.Users.Add(new User
            {
                Id = 1,
                Username = "AdminAdmin",
                Password = "AdminAdmin",
                Email = "AdminAdmin@OnAuction.com",
                PhoneNumber = "380000000000",
                RoleId = 3
            });

            db.Users.Add(new User
            {
                Id = 2,
                Username = "AntonAntonov",
                Password = "AntonAntonov",
                Email = "SergievSergiy@OnAuction.com",
                PhoneNumber = "380000000001",
                RoleId = 2
            });

            db.Users.Add(new User
            {
                Id = 3,
                Username = "IvanIvanov",
                Password = "IvanIvanov",
                Email = "IvanIvanov@OnAuction.com",
                PhoneNumber = "380000000002",
                RoleId = 1
            });

            db.Users.Add(new User
            {
                Id = 4,
                Username = "SergiySergiev",
                Password = "SergiySergiev",
                Email = "SergiySergiev@OnAuction.com",
                PhoneNumber = "380000000003",
                RoleId = 1
            });

            db.Categories.Add(new Category { Name = "Інше" });
            db.Categories.Add(new Category { Name = "Телефони, смартфони" });
            db.Categories.Add(new Category { Name = "Ноутбуки, комп'ютери" });
            db.Categories.Add(new Category { Name = "Фото, Відео" });
            db.Categories.Add(new Category { Name = "Електроніка" });
            db.Categories.Add(new Category { Name = "Побутова техніка" });
            db.Categories.Add(new Category { Name = "Товари для дому" });
            db.Categories.Add(new Category { Name = "Автомобілі" });
            db.Categories.Add(new Category { Name = "Інструменти, автотовари" });
            db.Categories.Add(new Category { Name = "Ремонт" });
            db.Categories.Add(new Category { Name = "Дача, сад, город" });
            db.Categories.Add(new Category { Name = "Спорт" });
            db.Categories.Add(new Category { Name = "Розваги" });
            db.Categories.Add(new Category { Name = "Одяг" });
            db.Categories.Add(new Category { Name = "Здоров'я, краса" });
            db.Categories.Add(new Category { Name = "Дитячі товари" });
            db.Categories.Add(new Category { Name = "Послуги, сервіси" });
            db.Categories.Add(new Category { Name = "Нумізматика" });
            db.Categories.Add(new Category { Name = "Боністика" });
            db.Categories.Add(new Category { Name = "Філателістика" });
            db.Categories.Add(new Category { Name = "Одяг" });

            db.Lots.Add(new Lot
            {
                Id = 1,
                Title = "Колекційна монета НБУ. Леонід Жаботинський, 2 грн.",
                Description = "Посвящена легендарному украинскому спортсмену тяжелоатлету Леоніду жаботинському.\n" +
                    "Номінал 2 грн.\n" +
                    "Випущена 23.01.2018\n",
                ImagePath = "~/Images/order1.jpg",
                StartPrice = 10,
                InstantPrice = 250,
                BetStep = 5,
                StartTime = CurrentTime.AddDays(-2),
                FinishTime = CurrentTime.AddDays(-1),
                IsFinish = false,
                UserId = 2,
                CategoryId = 18,
            });

            db.Lots.Add(new Lot
            {
                Id = 2,
                Title = "Освіжувач повітря в машину",
                Description = "Освіжувач повітря в машину з ароматом лимону та лайму.",
                ImagePath = null,
                StartPrice = 25,
                InstantPrice = 100,
                BetStep = 1,
                StartTime = CurrentTime.AddDays(-2).AddHours(12),
                FinishTime = CurrentTime.AddDays(-1).AddHours(12),
                IsFinish = false,
                UserId = 2,
                CategoryId = 9,
            });

            db.Lots.Add(new Lot
            {
                Id = 3,
                Title = "М'яч футзальний SELECT Mimas IMS",
                Description = "Футзальний м'яч клубних матчів і інтенсивних тренувань.\n" +
                    "Має сертифікат IMS(International Match Standart).\n" +
                    "Використовується для проведення клубних футзальних матчів і інтенсивних тренувань.\n" +
                    "Призначений для гри на паркеті, і штучному покритті.",
                ImagePath = "~/Images/order3.jpg",
                StartPrice = 500,
                InstantPrice = 1500,
                BetStep = 50,
                StartTime = CurrentTime.AddDays(-1).AddHours(2),
                FinishTime = CurrentTime.AddHours(2),
                IsFinish = false,
                UserId = 2,
                CategoryId = 12,
            });

            db.Lots.Add(new Lot
            {
                Id = 4,
                Title = "Arduino Nano V3",
                Description = "Arduino Nano V3.0 - це маленьке, готове до використання і добре працює з макетної платі пристрій, розроблене на мікроконтролері ATmega328.",
                ImagePath = "~/Images/order4.jpg",
                StartPrice = 10,
                InstantPrice = 250,
                BetStep = 5,
                StartTime = CurrentTime.AddDays(-1).AddHours(3).AddMinutes(35),
                FinishTime = CurrentTime.AddHours(3).AddMinutes(35),
                IsFinish = false,
                UserId = 2,
                CategoryId = 5,
            });

            db.Lots.Add(new Lot
            {
                Id = 5,
                Title = "Lenovo Ideapad 520-15IKB",
                Description = "Color: Black\n" +
                    "SSD + HDD: 128 + 1024 Gb\n" +
                    "CPU: Intel Core I5-7200U\n" +
                    "RAM: 8 Gb\n" +
                    "GPU: Nvidia GeForce 940MX, 2 Gb",
                ImagePath = "~/Images/order5.jpg",
                StartPrice = 15000,
                InstantPrice = 25000,
                BetStep = 100,
                StartTime = CurrentTime.AddDays(-1).AddHours(4),
                FinishTime = CurrentTime.AddHours(4),
                IsFinish = false,
                UserId = 2,
                CategoryId = 3,
            });

            db.Lots.Add(new Lot
            {
                Id = 6,
                Title = "Легендарна Nokia 3310",
                Description = "Та сама легендарна, оригінальна Nokia 3310",
                ImagePath = "~/Images/order6.jpg",
                StartPrice = 300,
                InstantPrice = 1000,
                BetStep = 10,
                StartTime = CurrentTime.AddDays(-1).AddHours(5),
                FinishTime = CurrentTime.AddHours(5),
                IsFinish = false,
                UserId = 2,
                CategoryId = 2,
            });

            db.Lots.Add(new Lot
            {
                Id = 7,
                Title = "Tesla Model S",
                Description = "Базова машинка HotWheels\n"+
                    "Модель: Tesla Model S",
                ImagePath = "~/Images/order7.jpg",
                StartPrice = 10,
                InstantPrice = 100,
                BetStep = 5,
                StartTime = CurrentTime.AddDays(-1).AddHours(7).AddMinutes(25),
                FinishTime = CurrentTime.AddHours(7).AddMinutes(25),
                IsFinish = false,
                UserId = 2,
                CategoryId = 1,
            });

            db.Lots.Add(new Lot
            {
                Id = 8,
                Title = "DJI Mavic Mini",
                Description = "Mavic Mini - компактний і високотехнологічний супутник на вашому творчому шляху. Зробіть моменти вашого життя особливими, зафіксувавши їх з повітря. Насолоджуйтесь польотом і отримуйте незвичайні кадри з інтуїтивно зрозумілим додатком DJI Fly.\n" +
                    "Маса Mavic Mini менше 250 г, що практично дорівнює вазі стандартного смартфона.Це означає не тільки, що пристрій надзвичайно компактний, але і що він входить в категорію дронів, які не потрібно реєструвати в багатьох країнах.",
                ImagePath = "~/Images/order8.jpg",
                StartPrice = 10000,
                InstantPrice = 15000,
                BetStep = 100,
                StartTime = CurrentTime.AddDays(-1).AddHours(9),
                FinishTime = CurrentTime.AddHours(9),
                IsFinish = false,
                UserId = 2,
                CategoryId = 1,
            });

            db.Bets.Add(new Bet
            {
                Id = 1,
                Price = 10,
                Time = CurrentTime.AddDays(-2).AddHours(3),
                LotId = 1,
                UserId = 3
            });

            db.Bets.Add(new Bet
            {
                Id = 2,
                Price = 25,
                Time = CurrentTime.AddDays(-2).AddHours(5).AddMinutes(35),
                LotId = 1,
                UserId = 4
            });

            db.Bets.Add(new Bet
            {
                Id = 3,
                Price = 100,
                Time = CurrentTime.AddDays(-2).AddHours(18),
                LotId = 1,
                UserId = 3
            });

            db.Bets.Add(new Bet
            {
                Id = 4,
                Price = 17500,
                Time = CurrentTime.AddDays(-1).AddHours(5),
                LotId = 5,
                UserId = 4
            });

            db.Bets.Add(new Bet
            {
                Id = 5,
                Price = 19000,
                Time = CurrentTime.AddDays(-1).AddHours(17).AddMinutes(10),
                LotId = 5,
                UserId = 3
            });

            db.Bets.Add(new Bet
            {
                Id = 6,
                Price = 10,
                Time = CurrentTime.AddDays(-1).AddHours(8).AddMinutes(25),
                LotId = 7,
                UserId = 3
            });

            db.Bets.Add(new Bet
            {
                Id = 7,
                Price = 25,
                Time = CurrentTime.AddDays(-1).AddHours(9).AddMinutes(10),
                LotId = 7,
                UserId = 4
            });

            db.Bets.Add(new Bet
            {
                Id = 8,
                Price = 35,
                Time = CurrentTime.AddDays(-1).AddHours(10).AddMinutes(35),
                LotId = 7,
                UserId = 3
            });

            db.Bets.Add(new Bet
            {
                Id = 9,
                Price = 12000,
                Time = CurrentTime.AddDays(-1).AddHours(10).AddMinutes(10),
                LotId = 8,
                UserId = 3
            });

            db.Bets.Add(new Bet
            {
                Id = 10,
                Price = 12500,
                Time = CurrentTime.AddDays(-1).AddHours(14).AddMinutes(45),
                LotId = 8,
                UserId = 4
            });

            base.Seed(db);
        }
    }
}
