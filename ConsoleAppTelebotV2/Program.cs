using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ConsoleAppTelebotV2
{
    class Program
    {
        static void Main()
        {
            Data _data = new Data();
            string token = _data.getToken();

            var bot = new TelegramBotClient(token); // Создаем экземпляр бота на основе токена

            Dictionary<long, UserInformation> dbUsers = new Dictionary<long, UserInformation>();

            bot.OnMessage += (sender, args) =>
            {
                // создание данных
                string msg = $"{DateTime.Now}: {args.Message.Chat.FirstName} {args.Message.Chat.Id} {args.Message.Text}";

                // Вывод на экран
                Console.WriteLine(msg);

                // Сохранение данных в лог
                using (var log = new StreamWriter(@"log.txt", true)) { log.WriteLine(msg); }

                if (!dbUsers.ContainsKey(args.Message.Chat.Id))
                {
                    dbUsers.Add(args.Message.Chat.Id, new UserInformation() { CurrentResponseIndex = 0 });
                }

                if (args.Message.Text == null) return;

                var messageText = args.Message.Text;

                if (messageText == Repository.BaseQuestions[dbUsers[args.Message.Chat.Id].CurrentResponseIndex].RightAnswer)
                {
                    dbUsers[args.Message.Chat.Id].TotalAnswer++;
                    bot.SendTextMessageAsync(args.Message.Chat.Id,
                        $"Молодец! Продолжай, попыток: {dbUsers[args.Message.Chat.Id].TotalAnswer}"
                        );
                    dbUsers[args.Message.Chat.Id].CurrentResponseIndex++;
                }
                else
                {
                    if (messageText != "/start")
                    {
                        dbUsers[args.Message.Chat.Id].TotalAnswer++;

                        bot.SendTextMessageAsync(
                            args.Message.Chat.Id,
                            $"Увы. Повтори, пожалуйста. Попыток: {dbUsers[args.Message.Chat.Id].TotalAnswer}");
                    }
                }

                string TextQuestion = string.Format(
                    "{0} \n{1} \n{2} \n{3} \n{4}",
                    Repository.BaseQuestions[dbUsers[args.Message.Chat.Id].CurrentResponseIndex].TextQuestion,
                    Repository.BaseQuestions[dbUsers[args.Message.Chat.Id].CurrentResponseIndex].Answer1,
                    Repository.BaseQuestions[dbUsers[args.Message.Chat.Id].CurrentResponseIndex].Answer2,
                    Repository.BaseQuestions[dbUsers[args.Message.Chat.Id].CurrentResponseIndex].Answer3,
                    Repository.BaseQuestions[dbUsers[args.Message.Chat.Id].CurrentResponseIndex].Answer4
                    );

                bot.SendTextMessageAsync(args.Message.Chat.Id,
                    $"{TextQuestion}"
                    );
            };

            bot.StartReceiving();

            Console.ReadKey();


        }
    }
}
