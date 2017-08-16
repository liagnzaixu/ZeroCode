using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroCode.ConTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = { "Albert was here", "Burke slept late", "Connor is happy" };
            var tokens = text.Select(s => s.Split(' '));
            foreach (string[] line in tokens)
                foreach (string token in line)
                    Console.Write("{0}.", token);

            Console.WriteLine();


            string[] text2 = { "Albert was here", "Burke slept late", "Connor is happy" };
            var tokens2 = text2.SelectMany(s => s.Split(' '));
            foreach (string token in tokens2)
                Console.Write("{0}.", token);

            Console.ReadKey();

            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher("a",new List<Student>{ new Student(100),new Student(90),new Student(30) }),
                new Teacher("b",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("c",new List<Student>{ new Student(100),new Student(90),new Student(40) }),
                new Teacher("d",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("e",new List<Student>{ new Student(100),new Student(90),new Student(50) }),
                new Teacher("f",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("g",new List<Student>{ new Student(100),new Student(90),new Student(60) })
            };

            var list3 = teachers.SelectMany(t => t.Students).Where(s => s.Score > 30);
            var list4 = teachers.SelectMany(t => t.Students);
        }
    }

    public class Student
    {
        public int Score { get; set; }

        public Student(int score)
        {
            this.Score = score;
        }
    }

    public class Teacher
    {
        public string Name { get; set; }

        public List<Student> Students;

        public Teacher(string order, List<Student> students)
        {
            this.Name = order;

            this.Students = students;
        }
    }
}
