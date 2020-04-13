using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Newtonsoft.Json;

using static ITEA_Collections.Common.Extensions;

namespace IteaSerialization
{
    [System.Runtime.InteropServices.Guid("01FDEB4A-7B33-45DD-B2A4-18B5F1DEA96E")]
    class Program
    {
        static void Main(string[] args)
        {
            //    ReadFromFile("example.txt");
            //    WriteToFile("example1.txt", "Some data");
            //    AppendToFile("example1.txt", "1");
            //    ToConsole(ReadFromFile("example.txt", ""));
            /*Person person = new Person("Alex", Gender.Man, 21, "alexs98@gmail.com");
            List<Person> people = new List<Person>
            {
                new Person("Pol", Gender.Man, 37, "pol@gmail.com"),
                new Person("Ann", Gender.Woman, 25, "ann@yahoo.com"),
                new Person("Alex", Gender.Man, 21, "alex@gmail.com"),
                new Person("Harry", Gender.Man, 58, "harry@yahoo.com"),
                new Person("Germiona", Gender.Woman, 18, "germiona@gmail.com"),
                new Person("Ron", Gender.Man, 24, "ron@yahoo.com"),
                new Person("Etc1", Gender.etc, 42, "etc1@yahoo.com"),
                new Person("Etc2", Gender.etc, 42, "etc2@gmail.com"),
            };

            Company microsoft = new Company("Microsoft");
            Company apple = new Company("Apple");

            people.ForEach(x => {
                if (x.Age < people.Average(a => a.Age))
                    x.SetCompany(microsoft);
                else
                    x.SetCompany(apple);
            }) ;

            XmlSerialize("exampleXml", people);
            JsonSerialize("microsoftJson", microsoft);
            JsonSerialize("appleJson", apple);
            Company appleFromFile = JsonDeserialize("appleJson");
            */
            //Создание сотрудников
            List<Person> people = new List<Person>
            {
                new Person("Alex", Gender.Man, 21, "alexs98@gmail.com"),
                new Person("Pol", Gender.Man, 37, "pol@gmail.com"),
                new Person("Ann", Gender.Woman, 25, "ann@yahoo.com"),
                new Person("Sonya", Gender.Woman, 21, "sonya@gmail.com"),
                new Person("Harry", Gender.Man, 58, "harry@yahoo.com"),
                new Person("Germiona", Gender.Woman, 18, "germiona@gmail.com"),
                new Person("Ron", Gender.Man, 24, "ron@yahoo.com"),
                new Person("John", Gender.Man, 42, "john@yahoo.com"),
                new Person("Mary", Gender.Woman, 42, "mary@gmail.com"),
            };

            //Создание компании
            Company company = new Company("CTCom Ltd");

            //Создание филиалов, заполнение сотрудниками, заполнение компании
            Department depAccountant = new Department("Accountants", company);
            people
                .Take(3)
                .ToList()
                .ForEach(x => { x.SetDepartment(depAccountant); });

            Department depManager = new Department("Managers", company);
            people
                .Skip(3)
                .Take(2)
                .ToList()
                .ForEach(x => { x.SetDepartment(depManager); });

            Department depITDeveloper = new Department("ITDevelopers", company);
            people
                .Skip(5)
                .ToList()
                .ForEach(x => { x.SetDepartment(depITDeveloper); });

            string fileName = "Data.json";

            //Сериализация и сохранение объекта в файл
            SaveToJsonFile(fileName, company);

            //Десериализация строки в объект Company
            Company temp = OpenJsonFile<Company>(fileName);

            //Сравнение объктов
            ToConsole("Comparing states ...");
            if (temp.Equals(company))
                ToConsole("Object's states after serialization are equal", ConsoleColor.Yellow);
            else
                ToConsole("Object's states after serialization are not Equal", ConsoleColor.Red);

            Console.ReadLine();
        }

        /// <summary>
        /// Сериализация и сохранение в файл
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        private static void SaveToJsonFile<T>(string path, T obj)
            where T: class, IModel
        {
            try
            {
                var content = JsonConvert.SerializeObject(obj, Formatting.Indented);
                using (var file = new StreamWriter(path, false))
                {
                    file.Write(content.ToArray());
                }
                ToConsole("Serialization result:");
                ToConsole(content, ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ToConsole($"Error (SaveToJsonFile): {ex.Message}", ConsoleColor.Red);
            }
        }

        private static T OpenJsonFile<T>(string path) 
            where T:class, IModel
        {
            T temp = null;
            try
            {
                using (var file = new StreamReader(path))
                {
                    var fileContent = file.ReadToEnd();
                    temp = JsonConvert.DeserializeObject<T>(fileContent);
                    if(typeof(T).Equals(typeof(ILinkUpdate)))
                    {
                        (temp as ILinkUpdate).UpdateLinks();
                    }
                }
            }
            catch(Exception ex)
            {
                ToConsole($"Error (OpenJsonFile): {ex.Message}", ConsoleColor.Red);
            }
            return temp;
        }

        #region Serialization
        public static void XmlSerialize<T>(string path, T obj) where T : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (var fs = new FileStream($"{path}.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }

            using (var stringwriter = new StringWriter())
            {
                formatter.Serialize(stringwriter, obj);
                ToConsole(stringwriter.ToString());
            }
        }

        public static void JsonSerialize<T>(string path, T obj) where T : class
        {
            using (var fs = new FileStream($"{path}.json", FileMode.OpenOrCreate))
            {
                string strObj = JsonConvert.SerializeObject(obj);
                byte[] data = strObj
                    .Select(x => (byte)x)
                    .ToArray();
                fs.Write(data, 0, data.Length);
                strObj
                    .Split(",")
                    .ToList()
                    .ForEach(x => ToConsole($"{x},", ConsoleColor.Green));
            }
        }

        public static Company JsonDeserialize(string path)
        {
            using (var streamReader = new StreamReader($"{path}.json"))
            {
                //var startMemory = GC.GetTotalMemory(true);
                string dataStr = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Company>(dataStr);
                //var endMemory = GC.GetTotalMemory(true);
                //Console.WriteLine($"Total memory: {endMemory - startMemory}");
            }
        }
        #endregion
        #region System.IO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path to file</param>
        public static void ReadFromFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                var startMemory = GC.GetTotalMemory(true);
                streamReader
                    .ReadToEnd()
                    .Split(';')
                    .ShowAll(separator: ";")
                    .Select(x => long.TryParse(x, out long l) ? l : (long?)null)
                    .Where(x => x != null)
                    .ShowAll(separator: ";");
                var endMemory = GC.GetTotalMemory(true);
                Console.WriteLine($"Total memory: {endMemory - startMemory}");
            }
        }

        public static void WriteToFile(string path, string data)
        {
            using (var fileWriter = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] array = data.Select(x => (byte)x).ToArray();
                fileWriter.Write(array, 0, array.Length);
            }

            //{
            //    FileStream fileWriter = new FileStream(path, FileMode.OpenOrCreate);
            //    try
            //    {
            //        byte[] array = data.Select(x => (byte)x).ToArray();
            //        fileWriter.Write(array, 0, array.Length);
            //    }
            //    finally
            //    {
            //        fileWriter?.Dispose();
            //    }
            //}

            using (var streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine(data);
            }
        }

        public static void AppendToFile(string path, string data)
        {
            using (var fileWriter = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] array = data.Select(x => (byte)x).ToArray();
                long fileDataLength = fileWriter.Length;
                fileWriter.Seek(fileDataLength, SeekOrigin.Begin);
                //fileWriter.Seek(0, SeekOrigin.End);
                fileWriter.Write(array, 0, array.Length);
            }
            using (var fileWriter = new FileStream(path, FileMode.Append))
            {
                byte[] array = data.Select(x => (byte)x).ToArray();
                fileWriter.Write(array, 0, array.Length);
            }
        }

        public static string ReadFromFile(string path, string notExistingEx)
        {
            notExistingEx = string.IsNullOrEmpty(notExistingEx)
                ? "Create file!"
                : notExistingEx;
            try
            {
                using (var fileReader = new FileStream(path, FileMode.Open))
                {
                    byte[] data = new byte[fileReader.Length];
                    fileReader.Read(data, 0, (int)fileReader.Length);
                    //return string.Concat(data.Select(x => (char)x));
                    return Encoding.Default.GetString(data);
                }
            }
            catch (FileNotFoundException)
            {
                ToConsole(notExistingEx, ConsoleColor.Red);
                return "Error";
            }
        }
        #endregion
    }
}
