using SeparatorIntoGroup.Options;
using SeparatorIntoGroup.TgBot.States;
using SeparatorIntoGroup.TgBot.States.StudentStates;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup
{
    public class MemberController
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public long Id { get; set; }
        public IState State { get; set; }
        public long ActualGroupId { get; set; }
        public int[] ActualTeams {get; set; }
        public List<List<Student>> ActualTeamList { get; set; }

        public MemberController(long id)
        {
            Id = id;
            StatusType status;

            if (_projectCore.Teachers.Contains(_projectCore.Teachers.Find(x => x.Id == Id)))
            {
                status = StatusType.IsTeacher;
            }
            else 
            {
                status = _projectCore.Students.Find(x => x.Id == Id).Status;
            }

            switch (status)
            {
                case StatusType.InGroup:
                    State = new StateIntoGroup();
                    break;
                case StatusType.PassedSurvey:
                    State = new StateIntoGroup();
                    break;
                case StatusType.IsTeacher:
                    State = new StartTeacherState();
                    break;
                default:
                    State = new StartStudentState();
                    break;
            }

        }

        public MessageModel GetAnswer(Update update)
        {
            return State.HandleUpdate(update, this);
        }
    }
}
