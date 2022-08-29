// Крылов Роман 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace Last_last_version_6_lesson
{




    
    public delegate double Fun(double x);
    public delegate double Func(double a, double x);
    
    class Program
    {
        class Student
        {
            /* 3.Переделать программу Пример использования коллекций для решения следующих задач:
            а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
            б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(*частотный массив);
            в) отсортировать список по возрасту студента;
            г) *отсортировать список по курсу и возрасту студента;*/
           
            // Тут мы просто задаем значения
            public string lastName;
            public string firstName;
            public string university;
            public string faculty;
            public int course;
            public string department;
            public int group;
            public string city;
            public int age;
            // Создаем конструктор
            public Student(string firstName, string lastName, string university, string faculty, string department, int age, int course, int group, string city)
            {
                this.lastName = lastName;
                this.firstName = firstName;
                this.university = university;
                this.faculty = faculty;
                this.department = department;
                this.course = course;
                this.age = age;
                this.group = group;
                this.city = city;
            }
        }
        // Дальше идет задание 1 
        public static void Table(Func F, double a, double x, double b)
        {
            Console.WriteLine("----- A ------- X -------- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} | {2,8:0.000} |", a, x, F(a, x));
                x += 1;
            }
            Console.WriteLine("-----------------------------------");
        }
        // Основная инновация в задание 1 вот эти 2 строчки
        public static double MyFunc(double a, double x)
        {
            return a * x * x;
        }
        public static double MySin(double a, double x)
        {
            return a * Math.Sin(x);
        }
 
        public static void SaveFunc(string fileName, double start, double end, double step, Fun F)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            while (start <= end)
            {
                bw.Write(F(start));
                start += step;
            }
            bw.Close();
            fs.Close();
        }

        /*2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
        а) Сделать меню с различными функциями и представить пользователю выбор, для какой функции и на каком отрезке находить минимум. Использовать массив (или список) делегатов, в котором хранятся различные функции.
        б) *Переделать функцию Load, чтобы она возвращала массив считанных значений. Пусть она возвращает минимум через параметр (с использованием модификатора out).*/
        public static double[] Load(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double[] array = new double[fs.Length / sizeof(double)];
            min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                array[i] = d;
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return array;
        }

        static double secondDegree(double x)
        {
            return x * x;
        }

        static double thirdDegree(double x)
        {
            return x * x * x;
        }


        static double mySqrt(double x)
        {
            return Math.Sqrt(x);
        }


        static double Sin(double x)
        {
            return Math.Sin(x);
        }


        static int GetInt(int max)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x) || x > max)
                    Console.Write("Неверный ввод (требуется числовое значение от 0 до {0}).\nПожалуйста повторите ввод: ", max);
                else return x;
        }

   
        static void GetInterval(out double start, out double end)
        {
            string[] interval = Console.ReadLine().Split(' ');
            start = double.Parse(interval[0], CultureInfo.InvariantCulture);
            end = double.Parse(interval[1], CultureInfo.InvariantCulture);
        }


        static void PrintResults(double start, double end, double step, double[] values)
        {
            Console.WriteLine("------- X ------- Y -----");
            int index = 0;
            while (start <= end)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} ", start, values[index]);
                start += step;
                index++;
            }
           
            Console.WriteLine("--------------------------");
        }
        /*Задание 3  ** Переделать программу Пример использования коллекций для решения следующих задач:
       а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
       б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (*частотный массив);
       в) отсортировать список по возрасту студента;
       г) *отсортировать список по курсу и возрасту студента;*/

        static int AgeCompare(Student st1, Student st2)          // Создаем метод для сравнения для экземпляров
        {
            return String.Compare(st1.age.ToString(), st2.age.ToString());          // Сравниваем две строки
        }

        static int CourceAndAgeCompare(Student st1, Student st2)
        {
            if (st1.course > st2.course)
                return 1;
            if (st1.course < st2.course)
                return -1;
            if (st1.age > st2.age)
                return 1;
            if (st1.age < st2.age)
                return -1;
            return 0;
        }
        // Далее идет мейн с вводом и выводом всех программ
        static void Main(string[] args)
        {

            Console.WriteLine("Вас приветсвует программа нахождения минимума функции!");
            List<Fun> functions = new List<Fun> { new Fun(secondDegree), new Fun(thirdDegree), new Fun(mySqrt), new Fun(Sin) };
            Console.WriteLine("Выберите функцию для дальнейшей работы.");
            Console.WriteLine("1) f(x)=y^2");
            Console.WriteLine("2) f(x)=y^3");
            Console.WriteLine("3) f(x)=y^1/2");
            Console.WriteLine("4) f(x)=Sin(y)");
            int userChoose = GetInt(functions.Count);

            Console.WriteLine("Задайте отрезок для нахождения минимума в формате 'х1 х2':");

            double start = 0;
            double end = 0;
            GetInterval(out start, out end);

            Console.WriteLine("Задайте величинау шага дискредитирования:");
            double step = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            SaveFunc("data.bin", start, end, step, functions[userChoose - 1]);
            double min = double.MaxValue;
            Console.WriteLine("Получены следующие значения функции: ");
            PrintResults(start, end, step, Load("data.bin", out min));
            Console.WriteLine("Минимальное значение функции равняется: {0:0.00}", min);

            Console.ReadKey();
            // Создаем новый делегат и передаем ссылку на него в метод Table
            Console.WriteLine("Таблица функции a*x^2:");
            // Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
            Table(new Func(MyFunc), -1.5, -2, 2);

            Console.WriteLine("Таблица функции a*sin(x):");
            Table(new Func(MyFunc), 3, -2, 2);

            Console.ReadKey();

            int magistr1 = 0;
            int magistr2 = 0;
            List<Student> list = new List<Student>();                             // Создаем список студентов
            DateTime dt = DateTime.Now;
            Dictionary<int, int> cousreFrequency = new Dictionary<int, int>();
            StreamReader sr = new StreamReader("..\\..\\students_6.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    // Добавляем в список новый экземпляр класса Student
                    list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                    // Одновременно подсчитываем количество магистров певрого и второго курсов
                    if (int.Parse(s[6]) == 5) magistr1++; else if (int.Parse(s[6]) == 6) magistr2++;
                    if (int.Parse(s[5]) > 17 && int.Parse(s[5]) < 21)
                    {
                        if (cousreFrequency.ContainsKey(int.Parse(s[6])))
                            cousreFrequency[int.Parse(s[6])] += 1;
                        else
                            cousreFrequency.Add(int.Parse(s[6]), 1);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Ошибка!ESC - прекратить выполнение программы");
                    // Выход из Main
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();
            Console.WriteLine("Всего студентов:" + list.Count);
            Console.WriteLine("Магистров первого курса:{0}", magistr1);
            Console.WriteLine("Магистров второго курса:{0}", magistr2);
            Console.WriteLine("\nПокажем сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся.");
            ICollection<int> keys = cousreFrequency.Keys;
            String result = String.Format("{0,-10} {1,-10}\n", "Курс", "Количество студентов");
            foreach (int key in keys)
                result += String.Format("{0,-10} {1,-10:N0}\n",
                                   key, cousreFrequency[key]);
            Console.WriteLine($"\n{result}");

            list.Sort(new Comparison<Student>(AgeCompare));
            Console.WriteLine("Отсортируем студентов по возрасту: ");
            foreach (var v in list) Console.WriteLine($"{v.firstName} {v.age}");

            list.Sort(new Comparison<Student>(CourceAndAgeCompare));
            Console.WriteLine("\nОтсортируем студентов по курсу и возрасту возрасту: ");
            foreach (var v in list) Console.WriteLine($"{v.firstName}, курс {v.course}, возраст {v.age}");

            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }
    }



           

        }
    


