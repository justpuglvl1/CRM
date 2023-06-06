using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Drawing;
using System.Drawing.Imaging;
using Plugin.Media.Abstractions;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Xamarin.Essentials;
using File = System.IO.File;
using Newtonsoft.Json;
using System.Net.Http;
using BOT.Data;

namespace BOT.Models
{
    internal class Up
    {
        public static int check = 0;
        public static Notes order = new Notes();
        public static DiaryApiStore diary = new DiaryApiStore();
        async public static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message? message = update.Message;

            if (message?.Text != null)
            {
                if (message.Text == "Блог")
                {
                    var a = await diary.AllBlog();
                    foreach (var b in a)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{b.Name} - {b.Text}");
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Все записи в блоге", replyMarkup: Buttons.GetButtons());
                }
                else if (message.Text == "Проекты")
                {
                    var a = await diary.AllWedo();
                    foreach (var b in a)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{b.Name} - {b.Text}");
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Все записи в проектах", replyMarkup: Buttons.GetButtons());
                }
                else if (message.Text == "Сервисы")
                {
                    var a = await diary.AllAbout();
                    foreach (var b in a)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{b.Name} - {b.Text}");
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Все записи в сервисах", replyMarkup: Buttons.GetButtons());
                }
                else if (message.Text == "Сделать заказ")
                {
                    check = 1;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ваше имя", replyMarkup: Buttons.GetButtons1());

                }
                else if (check == 1)
                {
                    order.Name = message.Text;
                    check = 2;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите описание", replyMarkup: Buttons.GetButtons1());

                }
                else if (check == 2)
                {
                    order.Description = message.Text;
                    check = 3;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ваши контакты", replyMarkup: Buttons.GetButtons1());

                }
                else if (check == 3)
                {
                    order.Address = message.Text;
                    order.Date = DateTime.Now.ToString();
                    order.Iban = "Ожидается";
                    await CreateNotes(order);
                    check = 0;
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Ваша заявка отправлена", replyMarkup: Buttons.GetButtons2());

                }
                else
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Что вы хотите посмотерь?", replyMarkup: Buttons.GetButtons());
            }
        }

        public static async Task CreateNotes(Notes model)
        {
            await diary.AddNoteAsync(model);
        }
    }
}
