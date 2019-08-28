using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Conf
{
    public class CsvParser
    {

        private static string escapeString(string raw)
        {
            bool change = false;
            if (raw.IndexOf(",") >= 0)
            {
                change = true;
            }

            if (raw.IndexOf("\"") >= 0)
            {
                raw = raw.Replace("\"", "\"\"");
                change = true;
            }

            if (change)
            {
                raw = "\"" + raw + "\"";
            }
            return raw;
        }


        private static string parseValue4Csv(string val, string typeStr)
        {
            string outVal = "";
            if (typeStr == "string")
            {
                return escapeString(val);
            }
            else if (typeStr == "table")
            {
                return escapeString(val);
            }
            else if (typeStr == "float")
            {
                return val;
            }
            else if (typeStr == "integer")
            {
                return val;
            }
            return outVal;
        }


        public static string ParseCsv(List<string> keys, List<string> types, List<string> scopes, List<string> comments, List<string[]> data)
        {
            List<string> rows = new List<string>();
            rows.Add(String.Join(",", keys));
            rows.Add("#" + String.Join(",", types));
            rows.Add("#" + String.Join(",", scopes));
            rows.Add("#" + String.Join(",", comments));

            string[] typeArray = types.ToArray();
            foreach (string[] rowCells in data)
            {
                string[] parseCells = new string[typeArray.Length];
                for (int i = 0; i < typeArray.Length; i++)
                {
                    string typeStr = typeArray[i];
                    string rawValStr = rowCells[i];
                    if (rawValStr == null || rawValStr == "")
                    {
                        parseCells[i] = "";
                    }
                    else
                    {
                        string valueStr = parseValue4Csv(rawValStr, typeStr);
                        parseCells[i] = valueStr;
                    }
                }
                rows.Add(String.Join(",", parseCells));
            }

            return String.Join("\r\n", rows);
        }

    }
}
