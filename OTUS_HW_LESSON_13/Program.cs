using OTUS_HW_LESSON_13;
using System.ComponentModel.Design;
using System.Reflection;
using System.Text;


F test = new F();
CSVSerializer serializer = new CSVSerializer();
string resultSerialized = serializer.SerealizeObject(test);



Console.WriteLine(resultSerialized);
Console.WriteLine();

List<F> list = new List<F>() { new F(), new F(), new F() };

string resultSerializedEnumerable = serializer.SerealizeEnumerable(list);

Console.WriteLine(resultSerializedEnumerable);

