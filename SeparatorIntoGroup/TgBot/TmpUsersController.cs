namespace SeparatorIntoGroup
{
    public class TmpUsersController
    {
        private Dictionary<long, MemberController> _studentMembers;
        public MemberController this[long id]
        {
            get
            {
                return _studentMembers[id];
            }
        }
        public TmpUsersController()
        {
            _studentMembers = new Dictionary<long, MemberController>();
        }

        public void AddUsers(long id)
        {
            _studentMembers.Add(id, new MemberController(id));
        }
        public bool IsContais(long id)
        {
            return _studentMembers.ContainsKey(id);
        }
    }
}
