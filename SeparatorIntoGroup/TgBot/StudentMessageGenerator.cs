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
                    Text = "Здравствуйте студент",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("Статус") { CallbackData = "status" },
                                new InlineKeyboardButton("Войти в группу") { CallbackData = "joinGroup" }
                            },
                        }),
                    IsNeedToBeEdited = false
                };
            }
        }

        public static MessageModel GroupMenu
        {
            get
            {
                return new MessageModel()
                {
                    Text = "На данный момент вы находитесь в групповом меню:)",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("Статус") { CallbackData = "status" },
                                new InlineKeyboardButton("Пройти опрос") { CallbackData = "takeASurvey" },
                            },
                            new[]
                            {
                            new InlineKeyboardButton("Посмотреть участников группы") { CallbackData = "groupMembers" } 
                            }
                        }),
                    IsNeedToBeEdited = false
                };
            }
        }

        public static MessageModel AuthorizationInGroup
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Введите id группы, по полученному от преподавателя ключу",
                    Keyboard = null,
                    IsNeedToBeEdited = false
                };
            }
        }

        public static MessageModel WrongGroupAuthorizationKey
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Такого ключа нет, уточните ключ у преподавателя и попробуйте еще раз",
                    Keyboard = null,
                    IsNeedToBeEdited = false
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
                                new InlineKeyboardButton("Понедельник") { CallbackData = $"{0}"},
                                new InlineKeyboardButton("Вторник") { CallbackData = $"{1}"},
                                new InlineKeyboardButton("Среда") { CallbackData = $"{2}"}
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Четверг") { CallbackData = $"{3}"},
                                new InlineKeyboardButton("Пятница") { CallbackData = $"{4}"},
                                new InlineKeyboardButton("Суббота") { CallbackData = $"{5}"}
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Воскресенье") { CallbackData = $"{6}"},
                                new InlineKeyboardButton("Готово") { CallbackData = "done" }

                            },
                        }),
                    IsNeedToBeEdited = false
                };
            }
        }
        public static MessageModel SelectFreeDays(Update update, Dictionary<TimeDictionaryKeys,bool> isSelected)
        {
            var keyboard = new List<List<InlineKeyboardButton>>()
            {
                new List<InlineKeyboardButton>(),
                new List<InlineKeyboardButton>(),
                new List<InlineKeyboardButton>()
            };
            for (int i = 0; i < 3; i++)
            {
                if (isSelected[(TimeDictionaryKeys)i] == true)
                {
                    keyboard[0].Add( new InlineKeyboardButton($"+ {((TimeDictionaryKeys) i).ToString()}") { CallbackData = $"{i}" } );
                }
                else
                {
                    keyboard[0].Add(new InlineKeyboardButton($"{((TimeDictionaryKeys)i).ToString()}") { CallbackData = $"{i}" });
                }
            }
            for (int i = 3; i < 6; i++)
            {
                if (isSelected[(TimeDictionaryKeys)i] == true)
                {
                    keyboard[1].Add(new InlineKeyboardButton($"+ {((TimeDictionaryKeys)i).ToString()}") { CallbackData = $"{i}" });
                }
                else
                {
                    keyboard[1].Add(new InlineKeyboardButton($"{((TimeDictionaryKeys)i).ToString()}") { CallbackData = $"{i}" });
                }
            }
            if (isSelected[(TimeDictionaryKeys)6] == true)
            {
                keyboard[2].Add(new InlineKeyboardButton($"+ {((TimeDictionaryKeys)6).ToString()}") { CallbackData = $"{6}" });
            }
            else
            {
                keyboard[2].Add(new InlineKeyboardButton($"{((TimeDictionaryKeys)6).ToString()}") { CallbackData = $"{6}" });
            }
            keyboard[2].Add( new InlineKeyboardButton("Готово") { CallbackData = "done" } );
            
            return new MessageModel()
            {
                Text = update.CallbackQuery.Message.Text,
                Keyboard = new InlineKeyboardMarkup(keyboard),
                IsNeedToBeEdited = true
            };
        }
        public static MessageModel QuestionAboutFreeTime
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Укажите в какое время дня вы свободны",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("6:00-9:00") { CallbackData = $"{0}"},
                                new InlineKeyboardButton("9:00-12:00") { CallbackData = $"{1}"},
                                new InlineKeyboardButton("12:00-15:00") { CallbackData = $"{2}"}
                            },
                            new[]
                            {
                                new InlineKeyboardButton("15:00-18:00") { CallbackData = $"{3}"},
                                new InlineKeyboardButton("18:00-21:00") { CallbackData = $"{4}"},
                                new InlineKeyboardButton("21:00-24:00") { CallbackData = $"{5}"}
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Готово") { CallbackData = "done" }
                            },
                        }),
                    IsNeedToBeEdited = false
                };
            }
        }
        public static MessageModel SelectFreeTime(Update update, Dictionary<TimeDictionaryValues, bool> isSelected)
        {
            var keyboard = new List<List<InlineKeyboardButton>>()
            {
                new List<InlineKeyboardButton>(),
                new List<InlineKeyboardButton>(),
                new List<InlineKeyboardButton>()
            };
            if (isSelected[(TimeDictionaryValues)0] == true)
            {
                keyboard[0].Add(new InlineKeyboardButton($"+ 6:00 - 9:00") { CallbackData = $"{0}" });
            }
            else
            {
                keyboard[0].Add(new InlineKeyboardButton($"6:00 - 9:00") { CallbackData = $"{0}" });
            }
            if (isSelected[(TimeDictionaryValues)1] == true)
            {
                keyboard[0].Add(new InlineKeyboardButton($"+ 9:00-12:00") { CallbackData = $"{1}" });
            }
            else
            {
                keyboard[0].Add(new InlineKeyboardButton($"9:00-12:00") { CallbackData = $"{1}" });
            }
            if (isSelected[(TimeDictionaryValues)2] == true)
            {
                keyboard[0].Add(new InlineKeyboardButton($"+ 12:00-15:00") { CallbackData = $"{2}" });
            }
            else
            {
                keyboard[0].Add(new InlineKeyboardButton($"12:00-15:00") { CallbackData = $"{2}" });
            }

            if (isSelected[(TimeDictionaryValues)3] == true)
            {
                keyboard[1].Add(new InlineKeyboardButton($"+ 15:00-18:00") { CallbackData = $"{3}" });
            }
            else
            {
                keyboard[1].Add(new InlineKeyboardButton($"15:00-18:00") { CallbackData = $"{3}" });
            }
            if (isSelected[(TimeDictionaryValues)4] == true)
            {
                keyboard[1].Add(new InlineKeyboardButton($"+ 18:00-21:00") { CallbackData = $"{4}" });
            }
            else
            {
                keyboard[1].Add(new InlineKeyboardButton($"18:00-21:00") { CallbackData = $"{4}" });
            }
            if (isSelected[(TimeDictionaryValues)5] == true)
            {
                keyboard[1].Add(new InlineKeyboardButton($"+ 21:00-24:00") { CallbackData = $"{5}" });
            }
            else
            {
                keyboard[1].Add(new InlineKeyboardButton($"21:00-24:00") { CallbackData = $"{5}" });
            }

            keyboard[2].Add(new InlineKeyboardButton("Готово") { CallbackData = "done" });

            return new MessageModel()
            {
                Text = update.CallbackQuery.Message.Text,
                Keyboard = new InlineKeyboardMarkup(keyboard),
                IsNeedToBeEdited = true
            };
        }
        public static MessageModel SelectWishStudents
        {
            get
            {
                string text = "Напишите через пробел @username студентов, с которым вы ХОТИТЕ быть в команде" + Environment.NewLine;
                text += "Если не планируете никого выбирать введите \"ДАЛЕЕ\"";
                return new MessageModel()
                {
                    Text = text,
                    Keyboard = null,
                    IsNeedToBeEdited = false
                };
            }
        }
        public static MessageModel SelectNotWishStudents
        {
            get
            {
                string text = "Напишите через пробел @username студентов, с которым вы НЕ ХОТИТЕ быть в команде" + Environment.NewLine;
                text += "Если не планируете никого выбирать введите \"ДАЛЕЕ\"";
                return new MessageModel()
                {
                    Text = text,
                    Keyboard = null,
                    IsNeedToBeEdited = false
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
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup,
                        IsNeedToBeEdited = false
                    };
                case StatusType.PassedSurvey:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент прошедший опрос",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup,
                        IsNeedToBeEdited = false
                    };
                case StatusType.InTeam:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент в команде",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup,
                        IsNeedToBeEdited = false
                    };
                default:
                    return new MessageModel()
                    {
                        Text = "На данный момент ваш статус: Студент не в группе",
                        Keyboard = update.CallbackQuery.Message.ReplyMarkup,
                        IsNeedToBeEdited = false
                    };
            }
        }
        public static MessageModel GroupMembers(Update update, string groupMembers)
        {
            return new MessageModel()
                {
                    Text = groupMembers,
                    Keyboard = update.CallbackQuery.Message.ReplyMarkup,
                    IsNeedToBeEdited = false
            };
            
        }
        public static MessageModel StubMessage
        {
            get
            {
                return new MessageModel()
                {
                    Text = "STUBMESSAGE!!!",
                    Keyboard = null,
                    IsNeedToBeEdited = false
                };
            }
        }
    }

}
