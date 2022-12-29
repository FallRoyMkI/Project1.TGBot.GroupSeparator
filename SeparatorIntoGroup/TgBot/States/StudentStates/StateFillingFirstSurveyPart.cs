using SeparatorIntoGroup.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
{
    public class StateFillingFirstSurveyPart : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();

        private Dictionary<TimeDictionaryKeys, bool> _dayList = new Dictionary<TimeDictionaryKeys, bool>()
        {
            { TimeDictionaryKeys.Понедельник, false },
            { TimeDictionaryKeys.Вторник, false },
            { TimeDictionaryKeys.Среда, false },
            { TimeDictionaryKeys.Четверг, false },
            { TimeDictionaryKeys.Пятница, false },
            { TimeDictionaryKeys.Суббота, false },
            { TimeDictionaryKeys.Воскресенье, false },
        };

        private Dictionary<TimeDictionaryValues, bool> _timeList = new Dictionary<TimeDictionaryValues, bool>()
        {
            { TimeDictionaryValues.EarlyMorning, false },
            { TimeDictionaryValues.Morning, false },
            { TimeDictionaryValues.FirstPartOfDay, false },
            { TimeDictionaryValues.SecondPartOfDay, false },
            { TimeDictionaryValues.Evening, false },
            { TimeDictionaryValues.LateEvening, false },
        };

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
                                case "done":
                                    BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                                    _isAnsweredOnDayQuestion = true;
                                    result = StudentMessageGenerator.QuestionAboutFreeTime;
                                    break;

                                default:
                                    var key = (TimeDictionaryKeys) Convert.ToInt32(update.CallbackQuery.Data);
                                    if (_dayList.ContainsKey(key))
                                    {
                                        _dayList[key] = !_dayList[key];
                                    }
                                    result = StudentMessageGenerator.SelectFreeDays(update, _dayList);
                                    break;
                            }
                            break;
                        default:
                            BotManager.DeleteActualMessage(update);
                            result = StudentMessageGenerator.StubMessage;
                            break;
                    }
                    break;

                case true:
                    switch (update.Type)
                    {
                        case UpdateType.CallbackQuery:
                            switch (update.CallbackQuery.Data)
                            {
                                case "done":
                                    BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                                    result = StudentMessageGenerator.SelectWishStudents;
                                    FillStudentSurvayInfo(update);
                                    controller.State = new StateFillingSecondSurveyPart();
                                    break;

                                default:
                                    var key = (TimeDictionaryValues)Convert.ToInt32(update.CallbackQuery.Data);
                                    if (_timeList.ContainsKey(key))
                                    {
                                        _timeList[key] = !_timeList[key];
                                    }
                                    result = StudentMessageGenerator.SelectFreeTime(update, _timeList);
                                    break;
                            }
                            break;
                        default:
                            BotManager.DeleteActualMessage(update);
                            result = StudentMessageGenerator.StubMessage;
                            break;

                    }
                    break;
            }

            return result;
        }

        private void FillStudentSurvayInfo(Update update)
        {
            List<TimeDictionaryKeys> days = new List<TimeDictionaryKeys>();
            foreach (var pair in _dayList)
            {
                if (pair.Value)
                {
                    days.Add(pair.Key);
                }
            }
            List<TimeDictionaryValues> time = new List<TimeDictionaryValues>();
            foreach (var pair in _timeList)
            {
                if (pair.Value)
                {
                    time.Add(pair.Key);
                }
            }

            _projectCore.Students.Find(x => x.Id == update.CallbackQuery.From.Id).AnswersToQuestionnaire.QuestionAboutFreeTime(days,time);
            _projectCore.SaveAll();
        }
    }
}
