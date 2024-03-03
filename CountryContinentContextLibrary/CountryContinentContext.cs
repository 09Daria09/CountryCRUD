using Microsoft.EntityFrameworkCore;
using CountryContinentLibrary;
using Microsoft.Extensions.Configuration;

namespace CountryContinentContextLibrary
{
    public class CountryContinentContext : DbContext
    {
        static DbContextOptions<CountryContinentContext> _options;

        static CountryContinentContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CountryContinentContext>();
            _options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
        }

        public CountryContinentContext()
            : base(_options)
        {
            Database.EnsureCreated();
            if (Database.EnsureCreated())
            {
                BigContinent europe = new BigContinent { NameContinent = "Европа" };
                BigContinent asia = new BigContinent { NameContinent = "Азия" };
                BigContinent africa = new BigContinent { NameContinent = "Африка" };
                BigContinent northAmerica = new BigContinent { NameContinent = "Северная Америка" };
                BigContinent southAmerica = new BigContinent { NameContinent = "Южная Америка" };
                BigContinent antarctica = new BigContinent { NameContinent = "Антарктида" };

                Continent.Add(europe);
                Continent.Add(asia);
                Continent.Add(africa);
                Continent.Add(northAmerica);
                Continent.Add(southAmerica);
                Continent.Add(antarctica);

                Countries.Add(new Country { NameCountry = "Украина", Capital = "Киев", Population = 41000000, Area = 603628, BigContinents = europe });
                Countries.Add(new Country { NameCountry = "Франция", Capital = "Париж", Population = 67000000, Area = 643801, BigContinents = europe });
                Countries.Add(new Country { NameCountry = "Германия", Capital = "Берлин", Population = 83000000, Area = 357022, BigContinents = europe });

                Countries.Add(new Country { NameCountry = "Китай", Capital = "Пекин", Population = 1400000000, Area = 9596961, BigContinents = asia });
                Countries.Add(new Country { NameCountry = "Япония", Capital = "Токио", Population = 126000000, Area = 377975, BigContinents = asia });
                Countries.Add(new Country { NameCountry = "Индия", Capital = "Нью-Дели", Population = 1350000000, Area = 3287263, BigContinents = asia });

                Countries.Add(new Country { NameCountry = "Египет", Capital = "Каир", Population = 102000000, Area = 1010408, BigContinents = africa });
                Countries.Add(new Country { NameCountry = "Нигерия", Capital = "Абуджа", Population = 206000000, Area = 923768, BigContinents = africa });
                Countries.Add(new Country { NameCountry = "ЮАР", Capital = "Претория", Population = 59300000, Area = 1221037, BigContinents = africa });

                Countries.Add(new Country { NameCountry = "США", Capital = "Вашингтон", Population = 331000000, Area = 9833517, BigContinents = northAmerica });
                Countries.Add(new Country { NameCountry = "Канада", Capital = "Оттава", Population = 38000000, Area = 9984670, BigContinents = northAmerica });
                Countries.Add(new Country { NameCountry = "Мексика", Capital = "Мехико", Population = 128000000, Area = 1964375, BigContinents = northAmerica });

                Countries.Add(new Country { NameCountry = "Бразилия", Capital = "Бразилиа", Population = 212000000, Area = 8515767, BigContinents = southAmerica });
                Countries.Add(new Country { NameCountry = "Аргентина", Capital = "Буэнос-Айрес", Population = 45100000, Area = 2780400, BigContinents = southAmerica });
                Countries.Add(new Country { NameCountry = "Чили", Capital = "Сантьяго", Population = 19100000, Area = 756102, BigContinents = southAmerica });

                Countries.Add(new Country { NameCountry = "Антарктида", Capital = "—", Population = 0, Area = 14000000, BigContinents = antarctica });

                SaveChanges();
            }
        }

        public DbSet<BigContinent> Continent { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}


