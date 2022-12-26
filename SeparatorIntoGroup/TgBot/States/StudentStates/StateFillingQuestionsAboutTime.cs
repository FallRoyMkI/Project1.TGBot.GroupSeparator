using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
{
    public class StateFillingQuestionsAboutTime : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        private bool _isAnsweredOnDayQuestion = false;

        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.QuestionAboutFreeDays;
            switch (_isAnsweredOnDayQuestion)
            {
                case false:
                    switch (update.Type)
                    {
                        case UpdateType.CallbackQuery:
                            switch (update.CallbackQuery.Data)
                            {
                                case "monday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "tuesday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "wednesday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "thursday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "friday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "saturday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "sunday":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeDays(update);
                                    break;

                                case "done":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    _isAnsweredOnDayQuestion = true;
                                    result = StudentMessageGenerator.QuestionAboutFreeTime;
                                    break;
                            }
                            break;
                    }
                    break;

                case true:
                    switch (update.Type)
                    {
                        case UpdateType.CallbackQuery:
                            switch (update.CallbackQuery.Data)
                            {
                                case "early morning":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeTime(update);
                                    break;

                                case "morning":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeTime(update);
                                    break;

                                case "early day":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeTime(update);
                                    break;

                                case "day":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeTime(update);
                                    break;

                                case "early evening":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeTime(update);
                                    break;

                                case "evening":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.FreeTime(update);
                                    break;

                                case "done":
                                    BotManager.DeleteOldReplyMarkup(update);
                                    result = StudentMessageGenerator.QuestionAboutWishStudents;
                                    controller.State = new StateFillingQuestionsAboutStudents();
                                    break;
                            }

                            break;
                    }

                    break;
            }

            return result;
        }

        private void FillStudentQuestionareInfo(Update update)
        {
            ProjectCore pc = ProjectCore.GetProjectCore();

            // pc.Students.Find(x => x.Id == update.CallbackQuery.Message.Chat.Id).AnswersToQuestionnaire.StudentFreeTime;
            pc.SaveAll();
        }
    }
}
