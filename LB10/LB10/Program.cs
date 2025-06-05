using System.Text.Json;
using System.Xml.Serialization;

namespace LB10
{
    class Program
    {
        static void Main(string[] args)
        {
            RouteTaxi[] routeTaxis = InitializeData();

            Console.Write("Введіть пункт призначення: ");
            string destination = Console.ReadLine();

            RouteTaxi[] matching = FindMatchingRoutes(routeTaxis, destination);

            if (matching.Length == 0)
            {
                Console.WriteLine("Жоден маршрут не містить цю зупинку.");
                return;
            }

            int minTime = FindMinTravelTime(matching);

            Console.WriteLine("\nНайшвидші маршрути до пункту:");
            foreach (var route in matching)
            {
                if (route.TotalTravelTime == minTime)
                    Console.WriteLine(route);
            }

            SerializeToJson("routes.json", routeTaxis);
            SerializeToXml("routes.xml", routeTaxis);

            Console.WriteLine("\nСеріалізація у JSON та XML завершена.");
        }

        static RouteTaxi[] InitializeData()
        {
            return new RouteTaxi[]
            {
                new RouteTaxi
                {
                    RouteNumber = "101",
                    Fare = 10.00m,
                    IntervalBetweenStopsMin = 4,
                    Stops = new string[] { "Центр", "Університет", "Базар", "Автовокзал" }
                },
                new RouteTaxi
                {
                    RouteNumber = "202",
                    Fare = 12.50m,
                    IntervalBetweenStopsMin = 3,
                    Stops = new string[] { "Центр", "Магазин", "Лікарня", "Автовокзал" }
                },
                new RouteTaxi
                {
                    RouteNumber = "303",
                    Fare = 9.50m,
                    IntervalBetweenStopsMin = 5,
                    Stops = new string[] { "Школа", "Парк", "Ринок" }
                }
            };
        }

        static RouteTaxi[] FindMatchingRoutes(RouteTaxi[] routes, string stop)
        {
            int count = 0;

            foreach (var route in routes)
            {
                foreach (var s in route.Stops)
                {
                    if (s.Equals(stop, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                        break;
                    }
                }
            }

            RouteTaxi[] result = new RouteTaxi[count];
            int index = 0;

            foreach (var route in routes)
            {
                foreach (var s in route.Stops)
                {
                    if (s.Equals(stop, StringComparison.OrdinalIgnoreCase))
                    {
                        result[index++] = route;
                        break;
                    }
                }
            }

            return result;
        }

        static int FindMinTravelTime(RouteTaxi[] routes)
        {
            int min = int.MaxValue;
            foreach (var r in routes)
            {
                if (r.TotalTravelTime < min)
                    min = r.TotalTravelTime;
            }

            return min;
        }

        static void SerializeToJson(string path, RouteTaxi[] data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        static void SerializeToXml(string path, RouteTaxi[] data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RouteTaxi[]));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }
        }
    }
}
