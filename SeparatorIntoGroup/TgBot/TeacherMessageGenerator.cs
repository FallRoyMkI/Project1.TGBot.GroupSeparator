﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup
{
    public class TeacherMessageGenerator
    {
        public static MessageModel StartMenu
        {
            get
            {
                return new MessageModel()
                {
                    Text = $"Ку зябл {Emoji._markHand}",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._plusSign} Создать группу") { CallbackData = "createGroup" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._markQuestion} Посмотреть список групп") { CallbackData = "groupList" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Взаимодействовать с группой") { CallbackData = "goIntoGroup" }
                            }
                        })
                };
            }
        }
        public static MessageModel CreateGroupId
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Введите Id для группы, которую хотите создать",
                    Keyboard = null
                };
            }
        }
        public static MessageModel WrongGroupId
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Группа с таким Id уже создана, введите другой ещё раз",
                    Keyboard = null
                };
            }
        }
        public static MessageModel CreateGroupName
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Введите название для группы, которую хотите создать",
                    Keyboard = null
                };
            }
        }
        public static MessageModel GetGroupId
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Введите Id группы, с которой хотите взаимодействовать",
                    Keyboard = null
                };
            }
        }
        public static MessageModel GetWrongGroupId
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Группы с введенным id не существует",
                    Keyboard = null
                };
            }
        }
        public static MessageModel GroupMenu
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Вы попали в групповое меню, Вам доступны команды:",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._markQuestion} Список студентов") { CallbackData = "studentList" },
                                new InlineKeyboardButton($"{Emoji._minusSign} Выгнать студента") { CallbackData = "deleteStudent" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._markrecycling} Распределить студентов на команды") { CallbackData = "createTeams" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._minusSign} УДАЛИТЬ ГРУППУ") { CallbackData = "deleteGroup" }
                            }
                        })
                };
            }
        }
        public static MessageModel StringToBot(Update update, string text)
        {
            return new MessageModel()
            {
                Text = text,
                Keyboard = update.CallbackQuery.Message.ReplyMarkup
            };

        }
        public static MessageModel StringToBot(string text)
        {
            return new MessageModel()
            {
                Text = text,
                Keyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            new InlineKeyboardButton("Пересоздать") { CallbackData = "rebuild" },
                            new InlineKeyboardButton("Подтвердить") { CallbackData = "confirm" }
                        },
                        new[]
                        {
                            new InlineKeyboardButton("Назад") { CallbackData = "back" }
                        }
                    })
            };

        }
        public static MessageModel DeleteStudent
        {
            get
            {
                return new MessageModel()
                {
                    Text = $"{Emoji._minusSign} Введите Id или @username студента, которого хотите отчислить с данной группы (Если вы передумали введите \"НАЗАД\"" ,
                    Keyboard = null
                };
            }
        }
        public static MessageModel WrongStudentInfo
        {
            get
            {
                return new MessageModel()
                {
                    Text = $"{Emoji._warningSign} Студент с введенными параметрами не состоит в текущей группе",
                    Keyboard = null
                };
            }
        }
        public static MessageModel ConfirmationNotGet
        {
            get
            {
                return new MessageModel()
                {
                    Text = $"{Emoji._markExclamation} Подтверждение не получено",
                    Keyboard = null
                };
            }
        }
        public static MessageModel SuccessfulDeleting
        {
            get
            {
                return new MessageModel()
                {
                    Text = $"{Emoji._markCheck} Вы успешно удалили студента, каковы Ваши дальнейшие действия:",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("Список студентов") { CallbackData = "studentList" },
                                new InlineKeyboardButton($"{Emoji._minusSign} Выгнать студента") { CallbackData = "deleteStudent" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._markrecycling} Распределить студентов на команды") { CallbackData = "createTeams" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._minusSign} УДАЛИТЬ ГРУППУ") { CallbackData = "deleteGroup" }
                            }
                        })
                };
            }
        }
        public static MessageModel SuccessfulGroupDeleting
        {
            get
            {
                return new MessageModel()
                {
                    Text = $"{Emoji._markCheck} Вы успешно удалили группу, каковы Ваши дальнейшие действия:",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton($"{Emoji._plusSign} Создать группу") { CallbackData = "createGroup" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Посмотреть список групп") { CallbackData = "groupList" }
                            },
                            new[]
                            {
                                new InlineKeyboardButton("Взаимодействовать с группой") { CallbackData = "goIntoGroup" }
                            }
                        })
                };
            }
        }
        public static MessageModel ConfirmDeleting
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Для удаления группы введите \"ДА Я УВЕРЕН\"",
                    Keyboard = null
                };
            }
        }
        public static MessageModel CreateTeams
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Введите через пробел максимальное количество студентов в каждой команде",
                    Keyboard = null
                };
            }
        }
        public static MessageModel WrongInitialization
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Что-то пошло не так, введите другие размеры команд или уменьшите их количество",
                    Keyboard = null
                };
            }
        }
        public static MessageModel Rebuilder
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Нажмите для запуска.......",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("ЗАПУСК") {CallbackData = "initial"}
                            },
                        }),
                    IsNeedToBeEdited = false
                };
            }
        }
        public static MessageModel NotEnoughStudents
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Для составления команд недостаточно студентов прошло опрос",
                    Keyboard = null,
                    IsNeedToBeEdited = false
                };
            }
        }
    }
}
