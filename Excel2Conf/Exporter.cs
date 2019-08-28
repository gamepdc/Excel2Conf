using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace Excel2Conf
{
    public class Exporter
    {
        private static int scopeRow = 3;
        private static int typeRow = 2;
        private static int commentRow = 4;
        private static int keyRow = 5;

        private static string lastError;

        Exporter()
        {
        }

        public static string LastErr
        {
            get { return lastError; }
            set { lastError = value; }
        }

        public static bool ParseConfig(Excel.Worksheet workSheet, ref string csvText, ref string luaText)
        {
            string sheetName = workSheet.Name;
            int namePos = sheetName.LastIndexOf('_');
            if (namePos == -1)
            {
                return false;
            }
            string fileName = sheetName.Substring(namePos + 1);

            List<string> keys = new List<string>();
            List<string> types = new List<string>();
            List<string> scopes = new List<string>();
            List<string> comments = new List<string>();
            List<string[]> data = new List<string[]>();
            if (!readHead(workSheet, keys, types, scopes, comments))
            {
                return false;
            }
            if (!readPlainData(workSheet, keys.Count, data))
            {
                return false;
            }

            csvText = CsvParser.ParseCsv(keys, types, scopes, comments, data);
            luaText = LuaParser.ParseLua(keys, types, scopes, comments, data);
            return true;
        }



        private static bool isValidType(string typeVal)
        {
            switch (typeVal)
            {
                case "string":
                case "table":
                case "float":
                case "integer":
                    break;
                default:
                    return false;
            }
            return true;
        }
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

            raw = raw.Replace("\n", "");

            if (change)
            {
                raw = "\"" + raw + "\"";
            }
            return raw;
        }

        private static bool readHead(Excel.Worksheet workSheet, List<string> keys, List<string> types, List<string> scopes, List<string> comments)
        {
            Excel.Range range = workSheet.UsedRange;
            int colCount = range.Columns.Count;

            Dictionary<string, int> keyDict = new Dictionary<string, int>();

            for (int j = 1; j <= colCount; j++)
            {
                string keyVal = getCellValue(range, keyRow, j);
                if (keyVal == "")
                {
                    break;
                }

                if (keyDict.ContainsKey(keyVal))
                {
                    lastError = string.Format("包含重复的key定义：{0}", keyVal);
                    return false;
                }
                keyDict.Add(keyVal, 1);

                string typeVal = getCellValue(range, typeRow, j);
                if (!isValidType(typeVal))
                {
                    lastError = string.Format("第{0}字段的类型{1}不合法", j, typeVal);
                    return false;
                }

                string scopeVal = getCellValue(range, scopeRow, j);
                string comment = getCellValue(range, commentRow, j);

                keys.Add(keyVal);
                types.Add(typeVal);
                scopes.Add(scopeVal);
                comments.Add(escapeString(comment));
            }


            return true;
        }

        public static bool readPlainData(Excel.Worksheet workSheet, int colCount, List<string[]> datas)
        {
            Excel.Range range = workSheet.UsedRange;
            // 加速数据读取
            // 如果通过range.
            object[,] values = range.Value2;

            Dictionary<string, int> privateKeys = new Dictionary<string, int>();
            int rowCount = range.Rows.Count;
            for (int i = 6; i <= rowCount; i++)
            {
                string privateVal = getCellValue(values, i, 1);
                if (privateVal == "" || privateVal == null)
                {
                    continue;
                }

                if (privateKeys.ContainsKey(privateVal))
                {
                    lastError = string.Format("出现重复的id：{0}", privateVal);
                    return false;
                }
                privateKeys.Add(privateVal, 1);

                List<string> rowCells = new List<string>();
                rowCells.Add(privateVal);
                for (int j = 2; j <= colCount; j++)
                {
                    rowCells.Add(getCellValue(values, i, j));

                }
                datas.Add(rowCells.ToArray());
            }

            return true;
        }

        private static string getCellValue(Excel.Range cell)
        {
            string cellData = Convert.ToString(cell.Value);
            return cellData;
        }

        // 读取数据非常慢
        private static string getCellValue(Excel.Range range, int row, int col)
        {
            Excel.Range cell = range.Cells[row, col] as Excel.Range;
            if (cell.Value == null)
            {
                return "";
            }
            string cellData = Convert.ToString(cell.Value);
            return cellData;
        }

        // 快速读取数据
        private static string getCellValue(object[,] cells, int row, int col)
        {
            object cellVal = cells[row, col];
            if (cellVal == null)
            {
                return "";
            }
            else
            {
                return cellVal.ToString();
            }
        }

    }
}
