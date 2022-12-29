namespace SeparatorIntoGroup
{
    public class TmpUsersController
    {
        private Dictionary<long, MemberController> _studentMembers;
        private Dictionary<long, MemberController> _teachers;
        public MemberController this[long id]
        {
            get
            {
                if (_teachers.ContainsKey(id))
                {
                    return _teachers[id];
                }
                return _studentMembers[id];
                
            }
        }
        public TmpUsersController()
        {
            _studentMembers = new Dictionary<long, MemberController>();
            _teachers = new Dictionary<long, MemberController>();
        }

        public void AddUsers(long id)
        {
            _studentMembers.Add(id, new MemberController(id));
        }

        public void AddTeacher(long id)
        {
            _teachers.Add(id, new MemberController(id));
        }

        public bool IsContais(long id)
        {
            return _studentMembers.ContainsKey(id) || _teachers.ContainsKey(id);
        }
    }
}
