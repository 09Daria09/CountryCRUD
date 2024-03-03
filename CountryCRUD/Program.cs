using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CountryContinentLibrary;
using CountryContinentContextLibrary;
using ContryCRUD;

namespace ContryCRUD
{
    class MainClass
    {
        static void Main()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Показать информацию");
                    Console.WriteLine("2. Добавить");
                    Console.WriteLine("3. Удалить");
                    Console.WriteLine("4. Изменить");
                    Console.WriteLine("0. Выход");

                    int mainChoice = int.Parse(Console.ReadLine()!);

                    switch (mainChoice)
                    {
                        case 1:
                            ShowInformationMenu();
                            break;
                        case 2:
                            AddInformationMenu();
                            break;
                        case 3:
                            DeleteInformationMenu();
                            break;
                        case 4:
                            UpdateInformationMenu();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие из списка.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ShowInformationMenu()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("1. Показать всю информацию о странах");
                    Console.WriteLine("2. Показать все континенты");
                    Console.WriteLine("3. Показать названия стран");
                    Console.WriteLine("4. Показать названия столиц");
                    Console.WriteLine("5. Показать названия всех европейских стран");
                    Console.WriteLine("6. Показать названия стран с площадью больше N");
                    Console.WriteLine("7. Показать все страны, у которых в названии есть буквы 'a', 'е'");
                    Console.WriteLine("8. Показать все страны, у которых название начинается с буквы 'a'");
                    Console.WriteLine("9. Показать название стран, у которых площадь находится в указанном диапазоне");
                    Console.WriteLine("10. Показать название стран, у которых количество жителей больше указанного числа");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            ShowAllCountries();
                            break;
                        case 2:
                            ShowAllContinents();
                            break;
                        case 3:
                            ShowCountryNames();
                            break;
                        case 4:
                            ShowCapitalNames();
                            break;
                        case 5:
                            ShowEuropeanCountries();
                            break;
                        case 6:
                            AskForAreaAndShowCountries();
                            break;
                        case 7:
                            ShowCountriesWithLettersAE();
                            break;
                        case 8:
                            ShowCountriesStartingWithA();
                            break;
                        case 9:
                            AskForAreaRangeAndShowCountries();
                            break;
                        case 10:
                            AskForPopulationAndShowCountries();
                            break;
                        case 0:
                            return;
                    };
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void AddInformationMenu()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Выберите, что добавить:");
                    Console.WriteLine("1. Новую страну");
                    Console.WriteLine("2. Новый континент");
                    Console.WriteLine("0. Выход");

                    int mainChoice = int.Parse(Console.ReadLine()!);

                    switch (mainChoice)
                    {
                        case 1:
                            AddNewCountry();
                            break;
                        case 2:
                            AddNewContinent();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие из списка.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void AddNewContinent()
        {
            try
            {
                using (var db = new CountryContinentContext())
                {
                    string nameContinent;
                    do
                    {
                        Console.WriteLine("Введите название континента:");
                        nameContinent = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nameContinent))
                        {
                            Console.WriteLine("Название континента не может быть пустым. Пожалуйста, введите название континента.");
                        }
                    } while (string.IsNullOrWhiteSpace(nameContinent));

                    var existingContinent = db.Continent
                        .FirstOrDefault(c => c.NameContinent.ToLower() == nameContinent.ToLower());
                    if (existingContinent != null)
                    {
                        Console.WriteLine("Континент с таким названием уже существует. Пожалуйста, введите уникальное название.");
                        return;
                    }

                    var continent = new BigContinent
                    {
                        NameContinent = nameContinent
                    };
                    db.Continent.Add(continent);
                    db.SaveChanges();

                    Console.WriteLine($"Континент {nameContinent} успешно добавлен.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при добавлении континента: {ex.Message}");
            }
        }
        private static void AddNewCountry()
        {
            try
            {
                using (var db = new CountryContinentContext())
                {
                    string nameCountry;
                    do
                    {
                        Console.WriteLine("Введите название страны:");
                        nameCountry = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nameCountry))
                        {
                            Console.WriteLine("Название страны не может быть пустым. Пожалуйста, введите название страны.");
                        }
                    } while (string.IsNullOrWhiteSpace(nameCountry));

                    string capital;
                    do
                    {
                        Console.WriteLine("Введите название столицы:");
                        capital = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(capital))
                        {
                            Console.WriteLine("Название столицы не может быть пустым. Пожалуйста, введите название столицы.");
                        }
                    } while (string.IsNullOrWhiteSpace(capital));

                    long population;
                    string populationString;
                    do
                    {
                        Console.WriteLine("Введите население:");
                        populationString = Console.ReadLine();
                        if (!long.TryParse(populationString, out population))
                        {
                            Console.WriteLine("Некорректный ввод населения. Пожалуйста, введите число.");
                        }
                    } while (!long.TryParse(populationString, out population));

                    double area;
                    string areaString;
                    do
                    {
                        Console.WriteLine("Введите площадь:");
                        areaString = Console.ReadLine();
                        if (!double.TryParse(areaString, out area))
                        {
                            Console.WriteLine("Некорректный ввод площади. Пожалуйста, введите число.");
                        }
                    } while (!double.TryParse(areaString, out area));

                    BigContinent selectedContinent = null;
                    while (selectedContinent == null)
                    {
                        Console.WriteLine("Выберите (ID) континента:");
                        var continents = db.Continent.ToList();
                        foreach (var continent in continents)
                        {
                            Console.WriteLine($"ID: {continent.id}, Название: {continent.NameContinent}");
                        }
                        if (int.TryParse(Console.ReadLine(), out int continentId))
                        {
                            selectedContinent = db.Continent.FirstOrDefault(c => c.id == continentId);
                            if (selectedContinent == null)
                            {
                                Console.WriteLine("Континент не найден! Пожалуйста, введите корректный ID континента.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод ID континента. Пожалуйста, введите число.");
                        }
                    }

                    var country = new Country
                    {
                        NameCountry = nameCountry,
                        Capital = capital,
                        Population = population,
                        Area = area,
                        BigContinents = selectedContinent
                    };
                    db.Countries.Add(country);
                    db.SaveChanges();

                    Console.WriteLine($"Страна {nameCountry} успешно добавлена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при добавлении страны: {ex.Message}");
            }
        }
        static void DeleteInformationMenu()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Выберите, что удалить:");
                    Console.WriteLine("1. Страну");
                    Console.WriteLine("2. Континент");
                    Console.WriteLine("0. Выход");

                    int mainChoice = int.Parse(Console.ReadLine()!);

                    switch (mainChoice)
                    {
                        case 1:
                            DeleteCountry();
                            break;
                        case 2:
                            DeleteContinentAndCountries();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие из списка.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void DeleteCountry()
        {
            ShowAllCountries();
            using (var db = new CountryContinentContext())
            {
                Console.WriteLine("Введите ID страны, которую хотите удалить:");
                int countryId = int.Parse(Console.ReadLine()!);

                var country = db.Countries.FirstOrDefault(c => c.Id == countryId);
                if (country != null)
                {
                    db.Countries.Remove(country);
                    db.SaveChanges();
                    Console.WriteLine("Страна успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Страна с таким ID не найдена.");
                }
            }
        }
        private static void DeleteContinentAndCountries()
        {
            ShowAllContinents();
            using (var db = new CountryContinentContext())
            {
                Console.WriteLine("Введите ID континента, который хотите удалить вместе со всеми связанными странами:");
                int continentId = int.Parse(Console.ReadLine()!);

                var continent = db.Continent.FirstOrDefault(c => c.id == continentId);
                if (continent != null)
                {
                    var countries = db.Countries.Where(c => c.BigContinents.id == continentId).ToList();
                    db.Countries.RemoveRange(countries);

                    db.Continent.Remove(continent);

                    db.SaveChanges();
                    Console.WriteLine("Континент и все связанные страны успешно удалены.");
                }
                else
                {
                    Console.WriteLine("Континент с таким ID не найден.");
                }
            }
        }
        static void UpdateInformationMenu()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Выберите, что изменить:");
                    Console.WriteLine("1. Данные о стране");
                    Console.WriteLine("2. Данные о континенте");
                    Console.WriteLine("0. Выход");

                    int mainChoice = int.Parse(Console.ReadLine()!);

                    switch (mainChoice)
                    {
                        case 1:
                            UpdateCountryData();
                            break;
                        case 2:
                            UpdateContinentData();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие из списка.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void UpdateCountryData()
        {
            ShowAllCountries(); 
            using (var db = new CountryContinentContext())
            {
                Console.WriteLine("Введите ID страны, данные которой хотите обновить:");
                int countryId = int.Parse(Console.ReadLine()!);

                var country = db.Countries.Include(c => c.BigContinents).FirstOrDefault(c => c.Id == countryId);
                if (country != null)
                {
                    Console.WriteLine("Введите новое название страны (оставьте пустым, чтобы не менять):");
                    var name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        country.NameCountry = name;
                    }

                    Console.WriteLine("Введите новую столицу (оставьте пустым, чтобы не менять):");
                    var capital = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(capital))
                    {
                        country.Capital = capital;
                    }

                    Console.WriteLine("Введите новое население (оставьте пустым, чтобы не менять):");
                    var populationString = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(populationString) && long.TryParse(populationString, out long population))
                    {
                        country.Population = population;
                    }

                    Console.WriteLine("Введите новую площадь (оставьте пустым, чтобы не менять):");
                    var areaString = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(areaString) && double.TryParse(areaString, out double area))
                    {
                        country.Area = area;
                    }

                    Console.WriteLine("Хотите изменить континент для этой страны? (да/нет):");
                    var changeContinent = Console.ReadLine();
                    if (changeContinent?.ToLower() == "да")
                    {
                        Console.WriteLine("Введите ID нового континента:");
                        ShowAllContinents(); 
                        int newContinentId = int.Parse(Console.ReadLine()!);
                        var newContinent = db.Continent.FirstOrDefault(c => c.id == newContinentId);
                        if (newContinent != null)
                        {
                            country.BigContinents = newContinent; 
                        }
                        else
                        {
                            Console.WriteLine("Континент с таким ID не найден.");
                        }
                    }

                    db.SaveChanges();
                    Console.WriteLine("Данные страны успешно обновлены.");
                }
                else
                {
                    Console.WriteLine("Страна с таким ID не найдена.");
                }
            }
        }
        private static void UpdateContinentData()
        {
            ShowAllContinents();
            using (var db = new CountryContinentContext())
            {
                Console.WriteLine("Введите ID континента, данные которого хотите обновить:");
                int continentId = int.Parse(Console.ReadLine()!);

                var continent = db.Continent.FirstOrDefault(c => c.id == continentId);
                if (continent != null)
                {
                    Console.WriteLine("Введите новое название континента (оставьте пустым, чтобы не менять):");
                    var name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        continent.NameContinent = name;
                    }

                    db.SaveChanges();
                    Console.WriteLine("Данные континента успешно обновлены.");
                }
                else
                {
                    Console.WriteLine("Континент с таким ID не найден.");
                }
            }
        }



        private static void ShowAllCountries()
        {
            try
            {
                using (var db = new CountryContinentContext())
                {
                    var countries = db.Countries.ToList();
                    if (countries.Any())
                    {
                        Console.WriteLine("Список всех стран:");
                        foreach (var country in countries)
                        {
                            Console.WriteLine($"ID: {country.Id}, Страна: {country.NameCountry}, Столица: {country.Capital}, Население: {country.Population}, Площадь: {country.Area} кв.км");
                            if (country.BigContinents != null)
                            {
                                Console.WriteLine($"\tКонтинент: {country.BigContinents.NameContinent}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("В базе данных пока нет стран.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить список всех стран: {ex.Message}");
            }
        }
        private static void AskForPopulationAndShowCountries()
        {
            Console.WriteLine("Введите минимальное количество жителей:");
            long minPopulation = long.Parse(Console.ReadLine()!);

            using (var db = new CountryContinentContext())
            {
                var countriesWithPopulation = db.Countries
                                                .Where(c => c.Population >= minPopulation)
                                                .ToList();
                foreach (var country in countriesWithPopulation)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Население: {country.Population}");
                }
            }
        }
        private static void AskForAreaRangeAndShowCountries()
        {
            Console.WriteLine("Введите минимальную площадь:");
            double minArea = double.Parse(Console.ReadLine()!);
            Console.WriteLine("Введите максимальную площадь:");
            double maxArea = double.Parse(Console.ReadLine()!);

            using (var db = new CountryContinentContext())
            {
                var countriesInRange = db.Countries
                                         .Where(c => c.Area >= minArea && c.Area <= maxArea)
                                         .ToList();
                foreach (var country in countriesInRange)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Площадь: {country.Area} кв. км");
                }
            }
        }
        private static void ShowCountriesStartingWithA()
        {
            using (var db = new CountryContinentContext())
            {
                var countriesStartingWithA = db.Countries
                                                .Where(c => c.NameCountry.ToLower().StartsWith("a"))
                                                .ToList();
                foreach (var country in countriesStartingWithA)
                {
                    Console.WriteLine(country.NameCountry);
                }
            }
        }
        static void ShowCountriesWithLettersAE()
        {
            using (var db = new CountryContinentContext())
            {
                var countriesWithAE = db.Countries
                                        .AsEnumerable()
                                        .Where(c => c.NameCountry.Contains("a") && c.NameCountry.Contains("e"))
                                        .ToList();

                foreach (var country in countriesWithAE)
                {
                    Console.WriteLine(country.NameCountry);
                }
            }
        }
        private static void ShowAllContinents()
        {
            try
            {
                using (var db = new CountryContinentContext())
                {
                    var continents = db.Continent.ToList();
                    if (continents.Any())
                    {
                        Console.WriteLine("Список всех континентов:");
                        foreach (var continent in continents)
                        {
                            Console.WriteLine($"ID: {continent.id}, Континент: {continent.NameContinent}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("В базе данных пока нет континентов.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить список всех континентов: {ex.Message}");
            }
        }
        static void ShowCountryNames()
        {
            using (var db = new CountryContinentContext())
            {
                var countryNames = db.Countries.Select(c => c.NameCountry).ToList();
                foreach (var name in countryNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
        static void ShowCapitalNames()
        {
            using (var db = new CountryContinentContext())
            {
                var capitalNames = db.Countries.Select(c => c.Capital).ToList();
                foreach (var capital in capitalNames)
                {
                    Console.WriteLine(capital);
                }
            }
        }
        static void ShowEuropeanCountries()
        {
            using (var db = new CountryContinentContext())
            {
                var europeanCountries = db.Countries
                                          .Where(c => c.BigContinents.NameContinent == "Европа")
                                          .ToList();
                foreach (var country in europeanCountries)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Столица: {country.Capital}");
                }
            }
        }
        static void AskForAreaAndShowCountries()
        {
            Console.WriteLine("Введите минимальную площадь:");
            double minArea = double.Parse(Console.ReadLine()!);
            Console.WriteLine("Введите максимальную площадь:");
            double maxArea = double.Parse(Console.ReadLine()!);

            using (var db = new CountryContinentContext())
            {
                var countriesByArea = db.Countries
                                        .Where(c => c.Area >= minArea && c.Area <= maxArea)
                                        .ToList();
                foreach (var country in countriesByArea)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Площадь: {country.Area} кв. км");
                }
            }
        }


    }
}
