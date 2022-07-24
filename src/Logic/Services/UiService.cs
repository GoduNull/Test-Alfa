using Data.Models;
using Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class UiService
    {
        public static async Task RunAsync()
        {
            try
            {
                Console.WriteLine("Импорт данных:" +
                    "\n1: Стандартная библиотека" +
                    "\n2: XPath" +
                    "\n3: Regex и получить List" +
                    "\n4: Regex и получить String" +
                    "\n5: Выход");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    switch (input)
                    {
                        case 1:
                            await WriteAsync(ReadManager.ReadXml());
                            await RunAsync();
                            break;
                        case 2:
                            await WriteAsync(ReadManager.ReadXmlXPath());
                            await RunAsync();
                            break;
                        case 3:
                            await WriteAsync(await ReadManager.ReadXmlRegexModelsAsync());
                            await RunAsync();
                            break;
                        case 4:
                            await WriteAsync(await ReadManager.ReadXmlRegexStringAsync());
                            await RunAsync();
                            break;
                        case 5:
                            break;
                        default:
                            Console.WriteLine("Выберите вариант из списка");
                            await RunAsync();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private async static Task WriteAsync(List<Item> items)
        {
            Console.WriteLine("Экспорт данных:" +
            "\n1: Текстовый файл" +
            "\n2: Word" +
            "\n3: Exel");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                switch (input)
                {
                    case 1:
                        await WriteManager.WriteTxtAsync(items);
                        break;
                    case 2:
                        WriteManager.WriteDocx(items);
                        break;
                    case 3:
                        WriteManager.WriteExel(items);
                        break;
                    default:
                        Console.WriteLine("Выберите вариант из списка");
                        break;
                }
            }
        }
        private async static Task WriteAsync(string items)
        {
            Console.WriteLine("Экспорт данных:" +
            "\n1: Текстовый файл" +
            "\n2: Word" +
            "\n3: Exel");
            if (int.TryParse(Console.ReadLine(), out int input))
            {

                switch (input)
                {
                    case 1:
                        await WriteManager.WriteTxtAsync(items);
                        break;
                    case 2:
                        WriteManager.WriteDocx(items);
                        break;
                    case 3:
                        WriteManager.WriteExel(items);
                        break;
                    default:
                        Console.WriteLine("Выберите вариант из списка");
                        break;
                }
            }
        }
    }
}
