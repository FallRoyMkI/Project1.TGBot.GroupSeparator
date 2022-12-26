using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using SeparatorIntoGroup.Options;
using Telegram.Bot;
using Microsoft.VisualBasic;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
{
    public class StateIntoGroup : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.GroupMenu;

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "goToQuestionnaire":
                            BotManager.DeleteOldReplyMarkup(update);
                            controller.State = new StateFillingQuestionsAboutTime();
                            result = StudentMessageGenerator.QuestionAboutFreeDays;
                            break;

                        case "groupMembers":
                            BotManager.DeleteOldReplyMarkup(update);
                            string text = StringBuilder(update);
                            controller.State = new StateIntoGroup();
                            result = StudentMessageGenerator.GroupMembers(update, text);
                            break;

                        case "status":
                            BotManager.DeleteOldReplyMarkup(update);
                            StatusType status = _projectCore.Students.Find(x => x.Id == update.CallbackQuery.From.Id).Status;
                            result = StudentMessageGenerator.StudentStatusMessage(update, status);
                            break;
                    }
                    break;
            }
            return result;
        }

        public string StringBuilder(Update update)
        {
            ProjectCore pc = ProjectCore.GetProjectCore();
            List<Student> students = new List<Student>();

            long groupId = pc.Students.Find(x => x.Id == update.CallbackQuery.Message.Chat.Id).GroupId;

            students.AddRange(pc.Students.FindAll(x => x.GroupId == groupId));
            students.Remove(pc.Students.Find(x => x.Id == update.CallbackQuery.Message.Chat.Id));
            string studentInfo = "";
            if (students.Count > 0)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    studentInfo += $"{i + 1} {students[i].PersonName} {students[i].AccountName}" + Environment.NewLine;
                }
            }
            else
            {
                studentInfo = "На данный момент в группе находитесь только вы";
            }

            return studentInfo;
        }
    }
}
