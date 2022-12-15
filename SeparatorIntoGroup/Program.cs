using SeparatorIntoGroup;

Teacher teacher1 = new Teacher(1111, "Maxim", "@Maxim");
Student student1 = new Student(2222, "Tom", "@Tom");
Student student2 = new Student(3333, "Bob", "@Bob");
Student student3 = new Student(4444, "Alex", "@Alex");

Group testGroup = new Group(1, "TestGroup");


Team testTeam = new Team(2, "TestTeam");

testTeam.AddStudentToTeam(student3);
testTeam.AddStudentToTeam(student3);
testTeam.AddStudentToTeam(student3);

testTeam.WriteInfoTeam();

testGroup.AddStudentToGroup(student1);
testGroup.AddStudentToGroup(student2);

//testGroup.WriteInfoGroup();

Questionnaire qqq = new Questionnaire();
qqq.QuestionAboutTime("18:50", "2");

BotManager manager = new BotManager();
Console.ReadLine();