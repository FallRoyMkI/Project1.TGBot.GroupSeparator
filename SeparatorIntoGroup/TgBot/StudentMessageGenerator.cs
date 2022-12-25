using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace SeparatorIntoGroup
{
    public static class StudentMessageGenerator
    {
        public static MessageModel StartMenu
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Привет студент",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("Статус") { CallbackData = "status" },
                                new InlineKeyboardButton("Войти в группу") { CallbackData = "joinGroup" }
                            },
                        })
                };
            }
        }

        public static MessageModel GroupMenu
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Привет студент в группе",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("Статус") { CallbackData = "status" },
                                new InlineKeyboardButton("Пройти опрос") { CallbackData = "goToQuestionnaire" },
                            },
                            new[]
                            {
                            new InlineKeyboardButton("Посмотреть участников группы") { CallbackData = "groupMembers" } 
                            }
                        })
                };
            }
        }

        public static MessageModel GroupAvtorizationKey
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Введите id группы",
                    Keyboard = null
                };
            }
        }

        public static MessageModel WrongGroupAvtorizationKey
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Такого ключа нет, уточните ключ у преподавателя и введите еще раз",
                    Keyboard = null
                };
            }
        }

        public static MessageModel QuestionAboutFreeDays
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Укажите в какие дни вы свободны",
                    Keyboard = null // добавить кнопки
                };
            }
        }
        public static MessageModel QuestionAboutFreeTime
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Укажите в какое время дня вы свободны",
                    Keyboard = null // добавить кнопки
                };
            }
        }
        public static MessageModel QuestionAboutWishStudents
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Напишите студента, с которым вы хотите быть в команде",
                    Keyboard = null // добавить кнопки
                };
            }
        }
        public static MessageModel QuestionAboutNotWishStudents
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Напишите студента, с которым вы не хотите быть в команде",
                    Keyboard = null // добавить кнопки
                };
            }
        }
    }
}
