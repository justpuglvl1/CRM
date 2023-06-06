using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace BOT.Models
{
    internal class Buttons
    {
        public static IReplyMarkup? GetButtons()
        {
            return new ReplyKeyboardMarkup("Хз что тут писать")
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton("Блог"), new KeyboardButton("Проекты"), new KeyboardButton("Сервисы")},
                    new List<KeyboardButton>{ new KeyboardButton("Сделать заказ") }
                },
                ResizeKeyboard = true
            };
        }
        public static IReplyMarkup? GetButtons1()
        {
            return new ReplyKeyboardMarkup("Хз что тут писать")
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton("Отмена") }
                },
                ResizeKeyboard = true
            };
        }
        public static IReplyMarkup? GetButtons2()
        {
            return new ReplyKeyboardMarkup("Хз что тут писать")
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton("На галвную страницу") }
                },
                ResizeKeyboard = true
            };
        }
    }
}