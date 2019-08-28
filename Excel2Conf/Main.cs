using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using Excel = Microsoft.Office.Interop.Excel;

namespace Excel2Conf
{
    public partial class Main : Form
    {
        private string dir;
        private string sOut;
        private string cOut;
        private string searchCur;
        private bool checkingChildren;

        public Main()
        {
            InitializeComponent();

            Config.ReadConfig();

            dir = Util.GetDirAbsPath(Config.DesignDir);
            sOut = Util.GetDirAbsPath(Config.ServerDir);
            cOut = Util.GetDirAbsPath(Config.ClientDir);
            searchCur = "";

            designIn.Text = dir;
            serverOut.Text = sOut;
            clientOut.Text = cOut;

            refreshExcels();

            this.listBox1.DrawItem += new DrawItemEventHandler(drawLog);
        }

        private void drawLog(Object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            string str = this.listBox1.Items[e.Index].ToString();

            Color foreColor = Color.Black;
            if (str.StartsWith("Error"))
            {
                foreColor = Color.Red;
            }

            e.Graphics.DrawString(str, e.Font, new SolidBrush(foreColor), e.Bounds);
        }

        private void addTips(string tipMessage)
        {
            this.listBox1.Items.Add(tipMessage);
        }

        private void addError(string errMessage)
        {
            this.listBox1.Items.Add("Error：" + errMessage);
        }

        private void refreshExcels()
        {
            //this.filelist.Items.Clear();
            this.treeView1.Nodes.Clear();
            if (!Directory.Exists(dir))
            {
                return;
            }

            /*List<string> fileList = Util.ListExcelFiles(dir, null);
            foreach (string currentFile in fileList)
            {
                //string fileName = currentFile.Substring(dirPath.Length + 1);
                string fileName = currentFile.Replace(dir, "");
                if (!dir.EndsWith("\\"))
                {
                    fileName = fileName.Substring(1);
                }

                if (searchCur == "" || fileName.IndexOf(searchCur, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.filelist.Items.Add(fileName);
                }
            }*/

            List<FindElem> elemList = Util.ListExcelFilesEx(dir);
            foreach(FindElem findElem in elemList)
            {
                string fileName = findElem.elemName;
                if (searchCur == "" || fileName.IndexOf(searchCur, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.treeView1.Nodes.Add(createNode(findElem));
                }
            }
            this.treeView1.ExpandAll();
        }

        private TreeNode createNode(FindElem findElem)
        {
            string name = findElem.elemName;
            name = name.Replace(dir, ""); 
            if (!dir.EndsWith("\\"))
            {
                name = name.Substring(1);
            }

            TreeNode node = new TreeNode(name);
            if (findElem.elemType == 1)
            {
                foreach(FindElem elem in findElem.elems)
                {
                    node.Nodes.Add(createNode(elem));
                }
            }

            return node;
        }

        private List<string> getAllSelectFiles()
        {
            List<string> files = new List<string>();
            /*foreach (var item in this.filelist.CheckedItems)
            {
                files.Add(item.ToString());
            }*/
            return files;
        }

        private void getSelectedSubNodes(TreeNode node, List<string> fileList)
        {
            foreach(TreeNode child in node.Nodes)
            {
                if (child.Nodes.Count > 0)
                {
                    getSelectedSubNodes(child, fileList);
                }
                else
                {
                    if (child.Checked)
                    {
                        fileList.Add(child.Text);
                    }
                }
            }
        }

        private List<string> getAllSelectFilesEx()
        {
            List<string> files = new List<string>();
            foreach (TreeNode item in this.treeView1.Nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    getSelectedSubNodes(item, files);
                }
                else
                {
                    if (item.Checked)
                    {

                        files.Add(item.Text);
                    }
                }

            }
            return files;
        }


        private void exportSelect_Click(object sender, EventArgs e)
        {
            List<string> files = getAllSelectFilesEx();
            this.listBox1.Items.Clear();

            if (!Directory.Exists(this.clientOut.Text))
            {
                addError("客户端目录不存在，导出失败!");
                return;
            }

            if (!Directory.Exists(this.serverOut.Text))
            {
                addError("服务端目录不存在，导出失败!");
                return;
            }

            dir = this.designIn.Text;
            cOut = this.clientOut.Text;
            sOut = this.serverOut.Text;

            Exporter.LastErr = "";
            exportExcels(files.ToArray());
            addTips("导出完成");

            Config.ClientDir = cOut;
            Config.ServerDir = sOut;
            Config.DesignDir = dir;

            Config.WriteConfig();
        }

        private void exportExcels(string[] files)
        {
            Excel.Application excelApp = new Excel.Application();
            foreach(string file in files)
            {
                addTips("开始导出" + file);
                Thread.Sleep(100);
                if (!exportExcel(excelApp, file))
                {
                    addError("导出文件" + file + "失败");
                }
                addTips("");
            }
            excelApp.Quit();
        }

        private bool exportExcel(Excel.Application excelApp, string filePath)
        {
            string path = Directory.GetCurrentDirectory();
            //string filePath = Path.Combine(path, dir, file);
            filePath = Path.Combine(dir, filePath);
            Excel.Workbook workBook = excelApp.Workbooks.Open(filePath);
            //string file = Path.GetFileName(filePath);

            int sheetCount = workBook.Worksheets.Count;
            bool exportSuccess = false;
            if (sheetCount > 0)
            {
                Excel.Worksheet workSheet = workBook.Worksheets.Item[1];
                try
                {
                    string csvText = "";
                    string luaText = "";
                    if (Exporter.ParseConfig(workSheet, ref csvText, ref luaText))
                    {
                        string sheetName = workSheet.Name;
                        int namePos = sheetName.LastIndexOf('_');
                        if (namePos >= 0)
                        {
                            string fileName = sheetName.Substring(namePos + 1);

                            string csvPath = Path.Combine(sOut, fileName + ".csv");
                            writeUtf8(csvPath, csvText);
                            addTips("导出服务端配置" + csvPath);

                            string luaPath = Path.Combine(cOut, fileName + ".lua");
                            writeUtf8(luaPath, luaText);
                            addTips("导出客户端端配置" + luaPath);

                            exportSuccess = true;
                        }
                    }
                    else
                    {
                        addError(Exporter.LastErr);
                    }
                }
                catch(Exception e)
                {
                    addError("未知错误" + e.Message);
                }
            }

            workBook.Close();
            return exportSuccess;
        }

        private static void writeUtf8(string filePath, string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('\uFEFF');
            sb.Append(text);
            Encoding utf8Encoding = Encoding.UTF8;
            byte[] bt1 = utf8Encoding.GetBytes(sb.ToString());
            File.WriteAllBytes(filePath, bt1);
        }

        private void selectall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*for (int i = 0; i < this.treeView1.Nodes.Count; i++)
            {
                this.treeView1. .SetItemChecked(i, true);
            }*/

            foreach(TreeNode node in this.treeView1.Nodes)
            {
                node.Checked = true;
            }
        }

        private void unselectall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            /*for (int i = 0; i < this.filelist.Items.Count; i++)
            {
                this.filelist.SetItemChecked(i, false);
            }*/

            foreach (TreeNode node in this.treeView1.Nodes)
            {
                node.Checked = false;
            }
        }

        private void selectDesignIn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                this.designIn.Text = dialog.SelectedPath;
            }
        }

        private void selectServerOut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                this.serverOut.Text = dialog.SelectedPath;
            }
        }

        private void selectClientOut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                this.clientOut.Text = dialog.SelectedPath;
            }
        }

        private void designIn_TextChanged(object sender, EventArgs e)
        {
            dir = this.designIn.Text;
            refreshExcels();
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            string searchNew = searchText.Text;
            if (searchNew == searchCur)
            {
                return;
            }

            searchCur = searchNew;
            refreshExcels();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (checkingChildren)
            {
                return;
            }
            checkingChildren = true;
            childChildren(e.Node, e.Node.Checked);
            checkingChildren = false;
        }

        private void childChildren(TreeNode node, bool check)
        {
            foreach(TreeNode child in node.Nodes)
            {
                child.Checked = check;
                childChildren(child, check);
            }
        }
    }
}
