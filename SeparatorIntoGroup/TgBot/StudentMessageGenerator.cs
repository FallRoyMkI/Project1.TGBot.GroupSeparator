using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeparatorIntoGroup.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
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

        public static MessageModel GroupAuthorization
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

        public static MessageModel WrongGroupAuthorizationKey
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
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("Пн") { CallbackData = "monday" },
                                new InlineKeyboardButton("Вт") { CallbackData = "tuesday" },
                                new InlineKeyboardButton("Ср") { CallbackData = "wednesday" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Чт") { CallbackData = "thursday" },
                                new InlineKeyboardButton("Пт") { CallbackData = "friday" },
                                new InlineKeyboardButton("Сб") { CallbackData = "saturday" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Вс") { CallbackData = "sunday" },
                                new InlineKeyboardButton("Готово") { CallbackData = "done" }
                               
                            },
                        })
                };
            }
        }
        public static MessageModel FreeDays(Update update)
        {
            return new MessageModel()
            {
                Text = update.CallbackQuery.Message.Text,
                Keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            new InlineKeyboardButton("Пн") { CallbackData = "monday" },
                            new InlineKeyboardButton("Вт") { CallbackData = "tuesday" },
                            new InlineKeyboardButton("Ср") { CallbackData = "wednesday" }
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Чт") { CallbackData = "thursday" },
                            new InlineKeyboardButton("Пт") { CallbackData = "friday" },
                            new InlineKeyboardButton("Сб") { CallbackData = "saturday" }
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Вс") { CallbackData = "sunday" },
                            new InlineKeyboardButton("Готово") { CallbackData = "done" }

                        },
                    })
            };
        }
        public static MessageModel QuestionAboutFreeTime
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Укажите в какие дни вы свободны",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("6:00-9:00") { CallbackData = "early morning" },
                                new InlineKeyboardButton("9:00-12:00") { CallbackData = "morning" },
                                new InlineKeyboardButton("12:00-15:00") { CallbackData = "early day" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("15:00-18:00") { CallbackData = "day" },
                                new InlineKeyboardButton("18:00-21:00") { CallbackData = "early evening" },
                                new InlineKeyboardButton("21:00-24:00") { CallbackData = "evening" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Готово") { CallbackData = "done" }
                            },
                        })
                };
            }
        }
        public static MessageModel FreeTime(Update update)
        {
            return new MessageModel()
            {
                Text = update.CallbackQuery.Message.Text,
                Keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            new InlineKeyboardButton("6:00-9:00") { CallbackData = "early morning" },
                            new InlineKeyboardButton("9:00-12:00") { CallbackData = "morning" },
                            new InlineKeyboardButton("12:00-15:00") { CallbackData = "early day" }
                        },
                        new[]
                        {
                            new InlineKeyboardButton("15:00-18:00") { CallbackData = "day" },
                            new InlineKeyboardButton("18:00-21:00") { CallbackData = "early evening" },
                            new InlineKeyboardButton("21:00-24:00") { CallbackData = "evening" }
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Готово") { CallbackData = "done" }
                        },
                    })
            };
        }
        public static MessageModel QuestionAboutWishStudents
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Напишите через пробел @username студентов, с которым вы хотите быть в команде",
                    Keyboard = null 
                };
            }
        }
        public static MessageModel QuestionAboutNotWishStudents
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Напишите через пробел @username студентов, с которым вы не хотите быть в команде",
                    Keyboard = null // добавить кнопки
                };
            }
        }

        public static MessageModel StudentStatusMessage(Update update,StatusType status)
        {
            switch (status)
            {
                case StatusType.InGroup:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент в группе",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup
                    };
                case StatusType.PassedSurvey:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент прошедший опрос",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup
                    };
                case StatusType.InTeam:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент в команде",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup
                    };
                default:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент не в группе",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup
                    };
            }
        }

        public static MessageModel GroupMembers(Update update, string studentInfo)
        {
            return new MessageModel()
                {
                    Text = studentInfo,
                    Keyboard = update.CallbackQuery.Message.ReplyMarkup
                };
            
        }
    }

}
