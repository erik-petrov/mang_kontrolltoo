using mang_kontrolltoo;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
try
{
    Peaklass.PlayGame(8);
}
catch (ArgumentOutOfRangeException)
{
    Console.WriteLine("Пожалуйста заполните файл items.txt на рабочем столе и попробуйте снова.");
}
catch (FileNotFoundException)
{
    Console.WriteLine("Пожалуйста создайте файл items.txt на рабочем столе и попробуйте снова.");
}