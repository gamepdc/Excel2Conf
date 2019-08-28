using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Excel2Conf
{
    public class FindElem
    {
        public int elemType;
        public string elemName;
        public List<FindElem> elems;

        public FindElem(int eType, string name)
        {
            elemType = eType;
            elemName = name;
            if (elemType == 0)
            {
                elems = new List<FindElem>();
            }
            else
            {
                elems = null;
            }
        }
    }

    public class Util
    {
        

        public static string GetDirAbsPath(string dir)
        {
            var dirPath = "";
            if (Path.IsPathRooted(dir))
            {
                dirPath = dir;
            }
            else
            {
                var curDir = Directory.GetCurrentDirectory();
                dirPath = Path.Combine(curDir, dir);
            }
            return dirPath;
        }

        public static List<FindElem> ListExcelFilesEx(string dir)
        {
            List<FindElem> elemList = new List<FindElem>();

            var excelFiles = Directory.EnumerateFiles(dir, "*.xlsx");
            foreach (string currentFile in excelFiles)
            {
                //string fileName = currentFile.Substring(dirPath.Length + 1);
                if (currentFile.IndexOf("~") >= 0)
                {
                    continue;
                }
                //string fileName = currentFile.Replace(dir, "");
                //if (!dir.EndsWith("\\"))
                //{
                //    fileName = fileName.Substring(1);
                //}

                //if (searchCur == "" || fileName.IndexOf(searchCur, StringComparison.OrdinalIgnoreCase) >= 0)
                //{
                //    this.filelist.Items.Add(fileName);
                //}

                elemList.Add(new FindElem(0, currentFile));
            }

            var dirs = Directory.EnumerateDirectories(dir);
            foreach (string currentDir in dirs)
            {
                List<FindElem> subList = ListExcelFilesEx(currentDir);
                FindElem findElem = new FindElem(1, currentDir);
                findElem.elems = subList;
                elemList.Add(findElem);
            }


            return elemList;
        }

        public static List<string> ListExcelFiles(string dir, List<string> fileList)
        {
            if (fileList == null)
            {
                fileList = new List<string>();
            }

            if (!Directory.Exists(dir))
            {
                return fileList;
            }

            var excelFiles = Directory.EnumerateFiles(dir, "*.xlsx");
            foreach (string currentFile in excelFiles)
            {
                //string fileName = currentFile.Substring(dirPath.Length + 1);
                if (currentFile.IndexOf("~") >= 0)
                {
                    continue;
                }
                //string fileName = currentFile.Replace(dir, "");
                //if (!dir.EndsWith("\\"))
                //{
                //    fileName = fileName.Substring(1);
                //}

                //if (searchCur == "" || fileName.IndexOf(searchCur, StringComparison.OrdinalIgnoreCase) >= 0)
                //{
                //    this.filelist.Items.Add(fileName);
                //}

                fileList.Add(currentFile);
            }

            var dirs = Directory.EnumerateDirectories(dir);
            foreach(string currentDir in dirs)
            {
                ListExcelFiles(currentDir, fileList);
            }

            return fileList;
        }
    }
}
