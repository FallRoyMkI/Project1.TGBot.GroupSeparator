using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup
{
    public class StartState : IState
    {
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.StartMenu;

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "joinGroup":
                            controller.State = new StateJoinToGroup();
                            result = StudentMessageGenerator.GroupAvtorizationKey;
                            break;

                        case "status":
                            // добавить вывод инфы о студенте
                            break;
                    }
                    break;
            }
            return result;
        }
    }
}
