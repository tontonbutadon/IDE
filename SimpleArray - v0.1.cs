using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3
{
    class Program
    {
        //version 1
        //simple array
        //Student name and surname;
        //homeworks and exam results
        static private string EnterName()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            return name;
        }
        static private string EnterSurname()
        {
            Console.Write("Enter surname: ");
            string surname = Console.ReadLine();
            return surname;
        }

        static private int[] EnterHWs() {
            int n,hw;
            Console.WriteLine("How many homework data do you have?");
            Console.Write("Enter: ");
            n = Int32.Parse(Console.ReadLine());
            int[] hws = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write("Enter homework points: ");
                hw = Int32.Parse(Console.ReadLine());
                hws[i] = hw;
            }
            return hws;
         }
        static private List<int> EnterHWsList()
        {
            int n, hw;
            Console.WriteLine("How many homework data do you have?");
            Console.Write("Enter: ");
            n = Int32.Parse(Console.ReadLine());
            var hws = new List<int>();

            for (int i = 0; i < n; i++)
            {
                Console.Write("Enter homework points: ");
                hw = Int32.Parse(Console.ReadLine());
                hws.Add(hw);
            }
            return hws;
        }

        static private int EnterExam()
        {
            Console.Write("Enter exam points: ");
            int exam = Int32.Parse(Console.ReadLine());
            return exam;
        }

        static private double CalculateAvg(int[] arr)
        {
            double avg = Queryable.Average(arr.AsQueryable());
            return avg;
        }
        static private double CalculateMid(int[] arr) 
        {
            double mid;
            int numberCount = arr.Count();
            int halfIndex = arr.Count() / 2;
            var sortedNumbers = arr.OrderBy(n => n);
            if ((numberCount % 2) == 0)
            {
                mid = ((sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1))) / 2;
            }
            else
            {
                mid = sortedNumbers.ElementAt(halfIndex);
            }
            return mid;
        }

        static private void ShowResult(Program[] data)
        {
            //Console.WriteLine("First Name | Last Name  |   Age")
            Console.WriteLine("Surname      Name           Final points(Avg.)");
            Console.WriteLine("------------------------------------------------");
            for (int i=0; i < data.Length; i++)
            {
                Console.WriteLine(String.Format("{0,-10}   {1,-10}   {2,5}", data[i].surname, data[i].name, data[i].result));
            }
        }
        static private void ShowResultWithMid(Program[] data)
        {
            //Console.WriteLine("First Name | Last Name  |   Age")
            Console.WriteLine("Surname      Name           Final points(Avg.) /     Final points(Mid.)");
            Console.WriteLine("----------------------------------------------------------------------------------");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(String.Format("{0,-10}   {1,-10}   {2,18}   {3,23}", data[i].surname, data[i].name, data[i].result, data[i].resultMid));
            }
        }
        string name, surname;
        double result, resultMid;
        public Program() { }
        public Program(string name_, string surname_, double result_)
        {
            this.name = name_;
            this.surname = surname_;
            this.result = result_;
        }
        public Program(string name_, string surname_, double result_, double resultMid_)
        {
            this.name = name_;
            this.surname = surname_;
            this.result = result_;
            this.resultMid = resultMid_;
        }
        /*
        public Program(string name_ , string surname_, int[] hw_, int exam_) {
            this.name = name_;
            this.surname = surname_;
            this.hw = hw_;
            this.exam = exam_;
        }
        */
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the student name, surname and points");
            Console.WriteLine("If you want to finish,  choose 0");
            Console.WriteLine("--------------------------------------------");

            int n = 0;
            string name, surname;
            int[] hws;
            int exam, count;
            double result, resultMid;
            var hws_ListTypeT = new List<int>();
            Console.WriteLine("How many students do you have?");
            count = Int32.Parse(Console.ReadLine());
            Program[] allData = new Program[count];
            while (n < count) {
                Console.WriteLine("Enter {0}'s person data", n + 1);
                name = EnterName();
                surname = EnterSurname();
                hws = EnterHWs();
                //hws_ListTypeT = EnterHWsList();
                exam = EnterExam();
                result = Math.Round(0.3 * CalculateAvg(hws) + 0.7 + exam, 2);
                resultMid = Math.Round(0.3 * CalculateMid(hws) + 0.7 * exam, 2);
                allData[n] = new Program(name, surname, result, resultMid);
                //Final_points = 0.3 * average_of_hw + 0.7 * exam
                Console.WriteLine("\n");
                n++;
            }
            //show the result 
            ShowResultWithMid(allData);
            //put homework points to an simple array 
            Console.WriteLine("Completed Version0.1 !");
        }
    }
}
