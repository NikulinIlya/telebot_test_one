using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ConsoleAppTelebot
{
    class Program
    {
        static void Main()
        {
            Data _data = new Data();
            string token = _data.getToken();

            var bot = new TelegramBotClient(token); // Создаем экземпляр бота на основе токена

            bot.OnMessage += (sender, args) => // обрабатывает получение сообщений
            {
                // создание данных
                string msg = $"{DateTime.Now}: {args.Message.Chat.FirstName} {args.Message.Chat.Id} {args.Message.Text}";

                // Вывод на экран
                Console.WriteLine(msg);

                // Сохранение данных в лог
                using (var log = new StreamWriter(@"log.txt", true)) { log.WriteLine(msg); }

                // Отправка сообщение
                bot.SendTextMessageAsync(args.Message.Chat.Id, $"Вы прислали: {args.Message.Text}");
            };

            bot.StartReceiving(); // Запускаем процесс обновлений 

            Console.ReadKey();
        }
    }
}
