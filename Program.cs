using System.Text.Json;
using System.Xml.Serialization;

var emp = new Employee
{
    Id = 1,
    Name = "Олена Коваль",
    Age = 28,
    Position = "Junior Developer",
    Salary = 35000.00
};

var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
string json = JsonSerializer.Serialize(emp, options);
File.WriteAllText("employee.json", json);
Console.WriteLine("=== JSON ===");
Console.WriteLine(json);

var serializer = new XmlSerializer(typeof(Employee));
using (StreamWriter writer = new StreamWriter("employee.xml"))
{
    serializer.Serialize(writer, emp);
}
string xml = File.ReadAllText("employee.xml");
Console.WriteLine("\n=== XML ===");
Console.WriteLine(xml);

long jsonSize = new FileInfo("employee.json").Length;
long xmlSize  = new FileInfo("employee.xml").Length;
Console.WriteLine($"\n=== Розмір файлів ===");
Console.WriteLine($"JSON: {jsonSize} байт");
Console.WriteLine($"XML:  {xmlSize} байт");

[XmlRoot("Employee")]
public class Employee
{
    [XmlElement("ID")]    public int Id { get; set; }
    [XmlElement("FullName")] public string Name { get; set; }
    [XmlElement("Age")]   public int Age { get; set; }
    [XmlElement("Position")] public string Position { get; set; }
    [XmlElement("Salary")] public double Salary { get; set; }
}