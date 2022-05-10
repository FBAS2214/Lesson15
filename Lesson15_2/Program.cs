using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Lesson15_2
{
    class Program
    {
        static void XmlWrite()
        {
            List<Car> cars = new List<Car>()
            {
                new Car{ Model ="Mustang", Make ="Ford", Year= 1964 },
                new Car{ Model ="La Ferrari", Make ="Ferrari", Year= 2000 },
                new Car{ Model ="Chiron", Make ="Buggati", Year= 2018 }
            };


            using var writer = new XmlTextWriter("cars.xml", Encoding.Default);

            writer.Formatting = System.Xml.Formatting.Indented;

            writer.WriteStartDocument();
            writer.WriteStartElement("cars");

            foreach (Car car in cars)
            {
                writer.WriteStartElement("car");

                writer.WriteAttributeString(nameof(car.Year), car.Year.ToString());
                writer.WriteAttributeString(nameof(car.Make), car.Make);
                writer.WriteAttributeString(nameof(car.Model), car.Model);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

        }

        static void XmlRead()
        {
            XmlDocument doc = new XmlDocument();

            doc.Load("cars.xml");

            var root = doc.DocumentElement;

            if (root.HasChildNodes)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    var car = new Car
                    {
                        Year = int.Parse(node.Attributes[0].Value),
                        Make = node.Attributes[1].Value,
                        Model = node.Attributes[2].Value
                    };

                    Console.WriteLine(car);
                }
            }

        }




        static void XmlSerialize()
        {
            var army = new TranslatorArmy
            {
                Name = "Step IT Academy",
                Location = "Koroglu Rehimov",
                Translators = new List<Translator>
                {
                    new Translator("Tural", "Novruzov", 1)
                    {
                        Subjects = new List<Subject>
                        {
                            new Subject{ Name = "C#", Degree = 42, Lessons = 18},
                            new Subject{ Name = "C++", Degree = 1, Lessons = 100},
                            new Subject{ Name = "java", Degree = 15, Lessons = -15},
                        }
                    },
                    new Translator("Aga", "Akbarzade", 2)
                    {
                        Subjects = new List<Subject>
                        {
                            new Subject{ Name = "C", Degree = 35, Lessons = 17},
                            new Subject{ Name = "ASP.Net Core", Degree = 14, Lessons = 48},
                            new Subject{ Name = "HTML/CSS", Degree = 57, Lessons = 9},
                        }
                    },
                    new Translator("Ferman", "Asadov", 3)
                    {
                        Subjects = new List<Subject>
                        {
                            new Subject{ Name = "Roboto Texnika", Degree = 60, Lessons = 120},
                            new Subject{ Name = "Photoshop", Degree = 13, Lessons = 19}
                        }
                    }
                }
            };



            var xml = new XmlSerializer(typeof(TranslatorArmy));
            using var fs = new FileStream("TranslatorArmy.xml", FileMode.Create);
            xml.Serialize(fs, army);

            Console.WriteLine("Ready");
        }

        static void XmlDeserialize()
        {
            TranslatorArmy army = null;

            var xml = new XmlSerializer(typeof(TranslatorArmy));
            using var fs = new FileStream("TranslatorArmy.xml", FileMode.Open);
            army = xml.Deserialize(fs) as TranslatorArmy;

            Console.WriteLine("Deserialized");
            Console.WriteLine(army);
        }




        static void JSONSerializeMethod()
        {
            Car[] cars =
            {
                new Car{ Model ="Mustang", Make ="Ford", Year= 1964 },
                new Car{ Model ="La Ferrari", Make ="Ferrari", Year= 2000 },
                new Car{ Model ="Chiron", Make ="Buggati", Year= 2018 }
            };


            //// Way 1
            {
                // JsonSerializerOptions op = new JsonSerializerOptions();
                // op.WriteIndented = true;
                //Console.WriteLine(JsonSerializer.Serialize(cars, op));

                var jsonString = System.Text.Json.JsonSerializer.Serialize(cars);
                File.WriteAllText("cars.json", jsonString);
            }




            //// Way 2
            {
                var jsonString = JsonConvert.SerializeObject(cars, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("carsWithNewtonsoft.json", jsonString);
            }



        }



        static void JSONDeserializeMethod()
        {
            Car[] cars = null;

            //// Way 1
            {

                // using FileStream fs = new FileStream("cars.json", FileMode.Open);
                // cars = System.Text.Json.JsonSerializer.Deserialize<Car[]>(fs);
            }


            //// Way 2
            {
                var stringData = File.ReadAllText("carsWithNewtonsoft.json");
                cars = JsonConvert.DeserializeObject<Car[]>(stringData);
            }


            foreach (var car in cars)
                Console.WriteLine(car);
        }





        static void BinarySerializeMethod()
        {
            Car[] cars =
            {
                new Car{ Model ="Mustang", Make ="Ford", Year= 1964 },
                new Car{ Model ="La Ferrari", Make ="Ferrari", Year= 2000 },
                new Car{ Model ="Chiron", Make ="Buggati", Year= 2018 }
            };


            var bf = new BinaryFormatter();
            using var fs = new FileStream("binaryCars.bin", FileMode.Create);

            bf.Serialize(fs, cars);

            Console.WriteLine("Ready");

        }

        static void BinaryDeserializeMethod()
        {
            Car[] cars = null;

            var bf = new BinaryFormatter();
            using var fs = new FileStream("binaryCars.bin", FileMode.Open);

            cars = bf.Deserialize(fs) as Car[];


            foreach (var car in cars)
                Console.WriteLine(car);
        }



        static void Main()
        {
            // XmlWrite();
            // XmlRead();




            // XmlSerialize();
            // XmlDeserialize();



            // JSONSerializeMethod();
            // JSONDeserializeMethod();



            // BinarySerializeMethod();
            // BinaryDeserializeMethod();
        }
    }
}