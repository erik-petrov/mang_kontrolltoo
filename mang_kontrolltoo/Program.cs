using mang_kontrolltoo;
using System.Text;
/*TODO: 
Можно сделать возможность дополнять список предметов.
Можно сделать возможность добавлять к предметам спрайты(картиночки).
Можно сделать возможность играть в игру самостоятельно или с друзьями, поочереди бросая кубик и получая предметы.
 */
Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Добро пожаловать в игру?! Для вывода топ игроков за все время, напишите l.\n" +
    "Для игры нажмите любую другую кнопку(если это буква то Enter тоже)");
switch (Console.ReadLine())
{
    case "l":
        Console.Write("Сколько строчек хотите видеть?: ");
        int rows = 0;
        try
        {
            rows = Int32.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            Console.WriteLine("Вы ввели неверное значение, программа закрывается..");
        }
        Peaklass.PrintLeaderboard(rows);
        break;
    default:
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
        break;
}
Console.ReadLine();
