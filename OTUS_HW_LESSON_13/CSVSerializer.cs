using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OTUS_HW_LESSON_13
{
    public class CSVSerializer
    {
        string GetTypes<T>(T t, string[]? info) where T : class
        {
            StringBuilder typessSB = new StringBuilder();

            for (int i = 0; info.Length > i; i++)
            {
                if (i != 0)
                {
                    typessSB.Append(", ");
                }
                typessSB.Append(info[i]);
            }
            return typessSB.ToString();
        }

        string GetValues<T>(T t, string[]? info) where T : class
        {
            StringBuilder valuesSB = new StringBuilder();

            List<string> values = new List<string>();

            foreach (var item in t.GetType().GetFields())
            {
                values.Add(item.GetValue(t).ToString());
            }
            for (int i = 0; info.Length > i; i++)
            {
                if (i != 0)
                {
                    valuesSB.Append(", ");
                }
                valuesSB.Append(values[i]);
            }
            return valuesSB.ToString();
        }

        public string SerealizeObject<T>(T t) where T : class
        {

            var info = t.GetType().GetFields().Select(t => t.ToString().Split(' ').Last()).ToArray();

            StringBuilder line = new StringBuilder();

            line.Append(GetTypes(t, info));
            line.AppendLine();

            line.Append(GetValues(t, info));

            return line.ToString();
        }

        public string SerealizeEnumerable<T>(T t) where T : IEnumerable
        {
            int i = 0;
            StringBuilder line = new StringBuilder();
            string[]? info = null;
            foreach (var item in t)
            {
                if (i == 0)
                {
                    info = item.GetType().GetFields().Select(t => t.ToString().Split(' ').Last()).ToArray();
                    line.Append(GetTypes(item, info));
                    line.AppendLine();
                }

                line.Append(GetValues(item, info));
                line.AppendLine();
                i++;
            }

            return line.ToString();
        }

        public T DeserealizeObject<T>(string str) where T : class
        {
            string[] separator = { Environment.NewLine };
            var arr = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            string[] types = arr[0].Split(", ");
            string[] values = arr[1].Split(", ");

            T result = Activator.CreateInstance<T>();

            Type type = result.GetType();

            for (int i = 0; i < types.Length; i++)
            {
                type.GetField(types[i]).SetValue(result,  int.Parse(values[i]));
            }

            return result;
        }

        public List<T> DeserealizeList<T>(string str) where T : class
        {
            string[] separator = { Environment.NewLine };
            var arr = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);


            string[] types = arr[0].Split(", ");

            List<T> result = new List<T>();

            for (int i = 1; i < arr.Length; i++)
            {
                T res = Activator.CreateInstance<T>();

                Type type = res.GetType();
                string[] values = arr[i].Split(", ");

                for (int j = 0; j < types.Length; j++)
                {
                    type.GetField(types[j]).SetValue(res, int.Parse(values[j]));
                }
                result.Add(res);
            }


            return result;
        }
    }
}
