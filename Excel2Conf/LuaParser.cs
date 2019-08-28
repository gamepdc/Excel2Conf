using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using LitJson;

namespace Excel2Conf
{
    public class LuaParser
    {

        private static string escapeString(string raw)
        {
            return raw.Replace("\"", "\\\"");
        }


        private static string tranLuaTable(string jsonStr)
        {
            JsonReader reader = new JsonReader(jsonStr);
            JsonData jd = JsonMapper.ToObject(reader);

            return jsonObj2Lua(jd);
        }

        private static string jsonObj2Lua(JsonData jd)
        {
            if (jd.IsInt)
            {
                return jd.ToInt32().ToString();
            }
            else if (jd.IsLong)
            {
                return jd.ToLong().ToString();
            }
            else if (jd.IsDouble)
            {
                return jd.ToDouble().ToString();
            }
            else if (jd.IsString)
            {
                return "\"" + escapeString(jd.ToString()) + "\"";
            }
            else if (jd.IsArray)
            {
                return transArray(jd);
            }
            else if (jd.IsObject)
            {
                return transObject(jd);
            }
            else
            {
                return "nil";
            }
        }


        private static string transArray(JsonData jd)
        {
            string luaStr = "{ ";

            List<string> rows = new List<string>();
            for(int i = 0; i < jd.Count; i++)
            {
                JsonData subJd = jd[i];
                string valueStr = jsonObj2Lua(subJd);
                rows.Add(valueStr);
            }
            luaStr += String.Join(",", rows);
            
            luaStr += " }";

            return luaStr;
        }

        private static string transObject(JsonData jd)
        {
            string luaStr = "{ ";

            List<string> rows = new List<string>();
            Dictionary<string, JsonData> objs = jd.Inst_Object;
            foreach (var ele in objs)
            {
                JsonData subJd = ele.Value;
                string key = ele.Key;
                string valueStr = jsonObj2Lua(subJd);
                string keyValueStr = string.Format("{0} = {1}", key, valueStr);
                rows.Add(keyValueStr);
            }
            luaStr += String.Join(",", rows);

            luaStr += " }";

            return luaStr;
        }


        private static string parseValue4Lua(string val, string typeStr)
        {
            string outVal = "";
            if (typeStr == "string")
            {
                return "\"" + escapeString(val) + "\"";
            }
            else if (typeStr == "table")
            {
                return tranLuaTable(val);
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


        public static string ParseLua(List<string> keys, List<string> types, List<string> scopes, List<string> comments, List<string[]> data)
        {
            string luaStr = "local _M = {\r\n";

            List<string> rows = new List<string>();
            string[] typeArray = types.ToArray();
            string[] keyArray = keys.ToArray();

            foreach (string[] rowCells in data)
            {
                List<string> parseCells = new List<string>();

                string type0 = typeArray[0];
                string key0 = keyArray[0];

                string rowKeyStr = "";
                string rowKeyVal = rowCells[0];
                if (rowKeyVal == "" || rowKeyVal == null)
                {
                    continue;
                }

                if (type0 == "string")
                {
                    rowKeyStr = string.Format("\t[\"{0}\"] = ", rowKeyVal);
                }
                else if (type0 == "integer")
                {
                    rowKeyStr = string.Format("\t[{0}] = ", rowKeyVal);
                }

                string valueStr0 = parseValue4Lua(rowKeyVal, type0);
                string keyValStr0 = string.Format("{0} = {1}", key0, valueStr0);
                parseCells.Add(keyValStr0);

                for (int i = 1; i < typeArray.Length; i++)
                {
                    string typeStr = typeArray[i];
                    string keyStr = keyArray[i];
                    string rawValStr = rowCells[i];
                    if (rawValStr == null || rawValStr == "")
                    {
                        continue;
                    }
                    else
                    {
                        string valueStr = parseValue4Lua(rawValStr, typeStr);
                        string keyValStr = string.Format("{0} = {1}", keyStr, valueStr);
                        parseCells.Add(keyValStr);
                    }
                }
                string rowValueStr = String.Join(", ", parseCells);
                rows.Add(rowKeyStr + "{" + rowValueStr + "}");
            }
            luaStr += String.Join(",\r\n", rows);
            luaStr += "\r\n}\r\nreturn _M";
            return luaStr; 
        }

    }
}
