using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace _13Practical_Lesson
{
    
    //[Serializable]
    public class Fruit
    {
        private string _name;
        private string _color;

        public string Name { get { return _name; } set { _name = value; } }
        public string Color { get { return _color; } set { _color = value; } }

        public Fruit(string name, string color)
        {
            _name = name;
            _color = color;
        }

        public virtual void Input()
        {

            Console.Write("Enter name of the fruit : ");
            _name = Console.ReadLine();
            Console.WriteLine("Enter color of this fruit");
            _color = Console.ReadLine();


        }

        public virtual void Print()
        {
            Console.WriteLine($"{Name} - {Color}");
        }

        public override string ToString()
        {
            return $"Name :  {_name}  -  Color :  {_color}";
        }
    }

    //[Serializable]
    public class Citrus : Fruit
    {
        //[JsonInclude]
        private int _vitaminC;

        public int VitaminC { get { return _vitaminC; } set { _vitaminC = value; } }

        public Citrus(string name, string color, int vitaminC)
            : base(name, color)
        {
            _vitaminC = vitaminC;
        }
        public override void Input()
        {
            base.Input();

            int number;

            while (true)
            {
                Console.Write("Please enter a valid integer: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
            VitaminC = number;

        }

        public override void Print()
        {
            Console.WriteLine($"{Name} - {Color} - {VitaminC}");
        }

        public override string ToString()
        {
            return $"Name :  {Name}  -  Color :  {Color}  -  Vitamin C in grams :  {_vitaminC}";
        }
    }

    public class Parent
    {
        public string ParentPrivateField { get; set; }
        public string ParentProperty { get; set; }
    }

    public class Child : Parent
    {
        public string ChildProperty { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            List<Fruit> fruits = new List<Fruit>()
            {
                new Fruit("aaa", "red"),
                new Fruit("bbb", "yellow"),
                new Citrus("orange", "yellow", 3),
                new Fruit("ddd", "black"),
                new Citrus("orange2", "orange", 7)
            };
            


            var yellowFruits = fruits.Where(n => n.Color == "yellow");
            foreach (var item in yellowFruits)
            {
                item.Print();
            }

            var sortFruitsByName = fruits.OrderBy(n => n.Name).ToList();

            string filepath = "C:\\Users\\HP\\source\\repos\\13Practical_Lesson\\13Practical_Lesson\\fruit.txt";

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (Fruit item in sortFruitsByName)
                {
                    writer.WriteLine(item);
                }
            }




            string jsonFilePath = "C:\\Users\\HP\\source\\repos\\13Practical_Lesson\\13Practical_Lesson\\fruit.json";
            string jsonString = JsonSerializer.Serialize(fruits, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, jsonString);

            string readJsonString = File.ReadAllText(jsonFilePath);
            List<Fruit> deserializedJsonFruits = JsonSerializer.Deserialize<List<Fruit>>(readJsonString);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deserialized Fruits:");
            foreach (var fruit in deserializedJsonFruits)
            {
                fruit.Print();
            }


            List<Parent> items = new List<Parent>
            {
                new Parent { ParentPrivateField = "PrivateParent 1", ParentProperty = "Parent 1" },
                new Child { ParentPrivateField = "PrivateParent 2", ParentProperty = "Parent 2", ChildProperty = "Child 1" }
            };

            // Серіалізація з явним типом
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(items, options);
            Console.WriteLine(json);

            // ---------------------------------- Need to fix -------------------------------
            /*string xmlFilePath = "C:\\Users\\HP\\source\\repos\\13Practical_Lesson\\13Practical_Lesson\\fruit.xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Fruit>));
            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, fruits);
            }

            List<Fruit> deserializedXmlFruits;
            using (StreamReader reader = new StreamReader(xmlFilePath))
            {
                deserializedXmlFruits = (List<Fruit>)xmlSerializer.Deserialize(reader);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deserialized Fruits:");
            foreach (var fruit in deserializedXmlFruits)
            {
                fruit.Print();
            }*/
            // -------------------------------------------------------------------------------
        }
    }
}
