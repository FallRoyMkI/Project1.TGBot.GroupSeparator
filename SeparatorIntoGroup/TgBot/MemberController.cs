using SeparatorIntoGroup.Options;
using SeparatorIntoGroup.TgBot.States;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup
{
    public class MemberController
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public long Id { get; set; }
        public IState State { get; set; }

        public MemberController(long id)
        {
            Id = id;

            StatusType status = _projectCore.Students.Find(x => x.Id == Id).Status;
            switch (status)
            {
                case StatusType.InGroup:
                    State = new StateIntoGroup();
                    break;
                case StatusType.PassedSurvey:
                    State = new StateIntoGroup();
                    break;
                default:
                    State = new StartState();
                    break;
            }

        }

        public MessageModel GetAnswer(Update update)
        {
            return State.HandleUpdate(update, this);
        }
    }
}
