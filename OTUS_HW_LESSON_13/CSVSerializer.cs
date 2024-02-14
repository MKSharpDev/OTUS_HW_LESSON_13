using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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


    }
}
