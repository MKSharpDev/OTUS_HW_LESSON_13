using OTUS_HW_LESSON_13;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;


F test = new F();
CSVSerializer serializer = new CSVSerializer();
string resultSerialized = serializer.SerealizeObject(test);



Console.WriteLine(resultSerialized);
Console.WriteLine();

List<F> list = new List<F>() { new F(), new F(), new F() };

string resultSerializedEnumerable = serializer.SerealizeEnumerable(list);

Console.WriteLine(resultSerializedEnumerable);


string[] separator = { Environment.NewLine };
var arr = resultSerializedEnumerable.Split(separator, StringSplitOptions.RemoveEmptyEntries);

string[] types = arr[0].Split(", ");
string[] values = arr[1].Split(", ");

F deserializedObject = serializer.DeserealizeObject<F>(resultSerialized);
List<F> deserializedList = serializer.DeserealizeList<F>(resultSerializedEnumerable);
Console.WriteLine();


Stopwatch stopWatch = new Stopwatch();
int iterations = 100000;

Console.WriteLine($"Количество итераций {iterations}");

stopWatch.Start();

var test1 = new F();

for (int i = 0; i < iterations; i++)
{
    serializer.SerealizeObject(test1);
}
stopWatch.Stop();
var timeMySerializeFirst = stopWatch.ElapsedMilliseconds;

stopWatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var test2 = new F();
    serializer.SerealizeObject(test2);
}
stopWatch.Stop();
var timeMySerializeSecond= stopWatch.ElapsedMilliseconds;
Console.WriteLine($"Сериализация мой CSVSerializer: с одним классом {timeMySerializeFirst}  мс, " +
    $"создавая каждый раз новый класс {timeMySerializeSecond} мс");

stopWatch.Restart();

var test3 = new F();

for (int i = 0; i < iterations; i++)
{
    JsonSerializer.Serialize(test3);
}
stopWatch.Stop();
var timeMySerialize3 = stopWatch.ElapsedMilliseconds;


stopWatch.Restart();

for (int i = 0; i < iterations; i++)
{
    var test4 = new F();
    JsonSerializer.Serialize(test4);

}
stopWatch.Stop();
var timeMySerialize4 = stopWatch.ElapsedMilliseconds;

Console.WriteLine($"Сериализация стандартным JsonSerializer: с одним классом  {timeMySerialize3} мс, " +
    $"создавая каждый раз новый класс {timeMySerialize4} мс");

var test5 = new F();
var testSerialized1 = serializer.SerealizeObject(test5);

stopWatch.Restart();

for (int i = 0; i < iterations; i++)
{
    serializer.DeserealizeObject<F>(testSerialized1);
}
stopWatch.Stop();
var timeMySerialize5 = stopWatch.ElapsedMilliseconds;

stopWatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var test6 = new F();
    var testSerialized2 = serializer.SerealizeObject(test6);
    serializer.DeserealizeObject<F>(testSerialized2);
}
stopWatch.Stop();
var timeMySerialize6 = stopWatch.ElapsedMilliseconds;
Console.WriteLine($"Десиарилизация мой CSVSerializer: с одним классом {timeMySerialize5}  мс, " +
    $"создавая каждый раз новый класс {timeMySerialize6} мс");


var test7 = new F();
var testSerialized3 = JsonSerializer.Serialize(test7);

stopWatch.Restart();

for (int i = 0; i < iterations; i++)
{
    JsonSerializer.Deserialize<F>(testSerialized3);
}
stopWatch.Stop();

var timeMySerialize7 = stopWatch.ElapsedMilliseconds;

stopWatch.Restart();



for (int i = 0; i < iterations; i++)
{
    var test6 = new F();
    var testSerialized4 = JsonSerializer.Serialize(test6);
    JsonSerializer.Deserialize<F>(testSerialized4);

}
stopWatch.Stop();
var timeMySerialize8 = stopWatch.ElapsedMilliseconds;

Console.WriteLine($"Десиарилизация стандартным JsonSerializer: с одним классом  {timeMySerialize7} мс, " +
    $"создавая каждый раз новый класс {timeMySerialize8} мс");


