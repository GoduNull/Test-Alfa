using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Logic.Managers
{
    public class WriteManager
    {
        /// <summary>
        /// Write to text file. 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task WriteTxtAsync(List<Item> items)
        {
            if (items is not null)
            {
                using StreamWriter reader = new StreamWriter("text.txt", false);
                foreach (var item in items)
                {
                    await reader.WriteLineAsync(item.Title + "\n" +
                        item.Link + "\n" +
                        item.Description + "\n" +
                        item.Category + "\n" +
                        item.PubDate.ToString() + "\n");
                }
            }    
        }
        /// <summary>
        /// Write to text file.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static async Task WriteTxtAsync(string items)
        {
            if (items is not null)
            {
                using StreamWriter reader = new StreamWriter("text.txt", false);
                await reader.WriteLineAsync(items);
            }
        }
        /// <summary>
        /// Write to word file.
        /// </summary>
        /// <param name="items"></param>
        public static void WriteDocx(string items)
        {
            if (items is not null)
            {
                var application = new Word.Application();
                application.Visible = false;
                var OneDoc = application.Documents.Add();
                var paragraphone = OneDoc.Content.Paragraphs.Add();
                paragraphone.Range.Text = items;
                paragraphone.Range.Font.Name = "Times New Roman";
                paragraphone.Range.Font.Size = 14;
                OneDoc.SaveAs2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "text.docx"));
                application.Quit();
            }
        }
        /// <summary>
        /// Write to word file.
        /// </summary>
        /// <param name="items"></param>
        public static void WriteDocx(List<Item> items)
        {
            if (items is not null)
            {
                var application = new Word.Application();
                application.Visible = false;
                var OneDoc = application.Documents.Add();
                var paragraphone = OneDoc.Content.Paragraphs.Add();
                foreach (var item in items)
                {
                    paragraphone.Range.InsertBefore(item.Title + "\n" +
                        item.Link + "\n" +
                        item.Description + "\n" +
                        item.Category + "\n" +
                        item.PubDate.ToString() + "\n\n");
                    paragraphone.Range.Font.Name = "Times New Roman";
                    paragraphone.Range.Font.Size = 14;
                }
                OneDoc.SaveAs2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "text.docx"));
                application.Quit();
            }
        }
        /// <summary>
        /// Write to exel file.
        /// </summary>
        /// <param name="items"></param>
        public static void WriteExel(List<Item> items)
        {
            if (items is not null)
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.Workbooks.Add();
                Excel.Worksheet workSheet = excelApp.ActiveSheet;
                workSheet.Cells[1, "A"] = "Title";
                workSheet.Cells[1, "B"] = "Link";
                workSheet.Cells[1, "C"] = "Description";
                workSheet.Cells[1, "D"] = "Category";
                workSheet.Cells[1, "E"] = "PubDate";
                int count = 2;
                foreach (var item in items)
                {
                    workSheet.Cells[count, "A"].Value = item.Title;
                    workSheet.Cells[count, "B"].Value = item.Link;
                    workSheet.Cells[count, "C"].Value = item.Description;
                    workSheet.Cells[count, "D"].Value = item.Category;
                    workSheet.Cells[count, "E"].Value = item.PubDate;
                    count++;
                }
                workSheet.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "text.xlsx"));
                excelApp.Quit();
            }
        }
        /// <summary>
        /// Write to exel file.
        /// </summary>
        /// <param name="items"></param>
        public static void WriteExel(string items)
        {
            if (items is not null)
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.Workbooks.Add();
                Excel.Worksheet workSheet = excelApp.ActiveSheet;
                workSheet.Cells[1, "A"] = "Title";
                workSheet.Cells[1, "B"] = "Link";
                workSheet.Cells[1, "C"] = "Description";
                workSheet.Cells[1, "D"] = "Category";
                workSheet.Cells[1, "E"] = "PubDate";

                string[] sum = items.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 2;
                for (int i = 0; i < sum.Length; i += 5)
                {
                    workSheet.Cells[count, "A"].Value = sum[i];
                    workSheet.Cells[count, "B"].Value = sum[i + 1];
                    workSheet.Cells[count, "C"].Value = sum[i + 2];
                    workSheet.Cells[count, "D"].Value = sum[i + 3];
                    workSheet.Cells[count, "E"].Value = sum[i + 4];
                    count++;
                };
                workSheet.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "text.xlsx"));
                excelApp.Quit();
            }
        }
    }
}
