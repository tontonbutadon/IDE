using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3_v04
{
    class Student
    {
        String surname, name;
        int h1, h2, h3, h4, h5, exam;
        int[] intarr;
        public Student() { }
        public Student(string[] data)
        {
            this.surname = data[0];
            this.name = data[1];
            this.h1 = Int32.Parse(data[2]);
            this.h2 = Int32.Parse(data[3]);
            this.h3 = Int32.Parse(data[4]);
            this.h4 = Int32.Parse(data[5]);
            this.h5 = Int32.Parse(data[6]);
            this.exam = Int32.Parse(data[7]);
        }
        public Student(String surname, String name, int h1, int h2, int h3, int h4, int h5, int exam)
        {
            this.surname = surname;
            this.name = name;
            this.h1 = h1;
            this.h2 = h2;
            this.h3 = h3;
            this.h4 = h4;
            this.h5 = h5;
            this.exam = exam;
        }

        public String getSurname() { return surname; }
        public String getName() { return name; }
        public int getH1() { return h1; }
        public int get2() { return h2; }
        public int get3() { return h3; }
        public int get4() { return h4; }
        public int get5() { return h5; }
        public int getExam() { return exam; }
        class Lab3
        {
            string name, surname;
            double result, resultMid;
            public Lab3() { }
            //with Finalresult of midian
            public Lab3(string name_, string surname_, double result_, double resultMid_)
            {
                this.name = name_;
                this.surname = surname_;
                this.result = result_;
                this.resultMid= resultMid_;
            }
            public Lab3(string name_, string surname_, double result_)
            {
                this.name = name_;
                this.surname = surname_;
                this.result = result_;
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
                Console.WriteLine("mid - " + mid);
                return mid;
            }

            static private void ShowResult(Lab3[] data)
            {
                //Console.WriteLine("First Name | Last Name  |   Age")
                Console.WriteLine("Surname      Name           Final points(Avg.)");
                Console.WriteLine("------------------------------------------------");
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine(String.Format("{0,-10}   {1,-10}   {2,5}", data[i].surname, data[i].name, data[i].result));
                }
            }

            static private Lab3[][] PassOrFail(Lab3[] data)
            {
                Lab3[][] divided = new Lab3[2][];
                var pass = new List<Lab3>();
                var fail = new List<Lab3>();
                foreach (Lab3 obj in data)
                {
                    //pass if data.res_avg >= 5.0
                    if (obj.result >= 5.0)
                    {
                        pass.Add(obj);
                    }
                    //fail res < 5.0
                    else
                    {
                        fail.Add(obj);
                    }
                }
                divided[0] = pass.ToArray();
                divided[1] = fail.ToArray();
                return divided;
            }

            static private void ShowResultWithMid(Lab3[] data)
            {
                //Console.WriteLine("First Name | Last Name  |   Age")
                Console.WriteLine("Surname      Name           Final points(Avg.) /     Final points(Mid.)");
                Console.WriteLine("----------------------------------------------------------------------------------");


                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine(String.Format("{0,-10}   {1,-10}   {2,19}   {3,24}", data[i].surname, data[i].name, data[i].result, data[i].resultMid));
                }
            }

            static private int[] ReturnArrInt(Student data)
            {
                //get int array for all hw points 
                int[] hws = new int[5] { data.h1, data.h2, data.h3, data.h4, data.h5 };
                return hws;
            }

            //sort Lab3[] by name
            static private Lab3[] SortObjects(Lab3[] data)
            {
                Array.Sort(data, delegate (Lab3 data1, Lab3 data2)
                {
                    return data1.name.CompareTo(data2.name);
                });
                return data;
            }

            private static void WriteResultToFile(Lab3[] data, String pass)
            {
                using (var sw = new StreamWriter(pass))
                {
                    foreach (Lab3 obj in data)
                    {
                        sw.WriteLine("{0} {1} {2}", obj.surname, obj.name, obj.result);
                    }
                }
            }

            static void Main(string[] args)
            {
                //default size of list
                int sizeOfList = 1000;
                Console.WriteLine("Choose the size of student list from the list below(default is 1000 size)↓");
                Console.WriteLine("\t1 - 1000" +
                    "\n\t2 - 10000" +
                    "\n\t3 - 100000" +
                    "\n\t4 - 1000000" +
                    "\n\t5 - 10000000");
                Console.Write("Your choice = ");
                try { 
                    int choice = Int32.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            sizeOfList = 1000;
                            break;
                        case 2:
                            sizeOfList = 10000;
                            break;
                        case 3:
                            sizeOfList = 100000;
                            break;
                        case 4:
                            sizeOfList = 1000000;
                            break;
                        case 5:
                            sizeOfList = 10000000;
                            break;
                        default:
                            Console.WriteLine("You input invlid value.");
                            Console.WriteLine("The size will be set 1000");
                            sizeOfList = 1000;
                            break;
                    }
                } catch (FormatException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Only integer values accepted");
                    Console.WriteLine("You should choose the integer from the list.");
                }

                Console.WriteLine("The list will contains {0} records", sizeOfList);

                //start timer
                var watch = System.Diagnostics.Stopwatch.StartNew();

                //variables for creating file
                Random random = new Random();

                int in1, in2, in3, in4, in5, in6;
                String name_ = "name";
                String surname_ = "surname";
                String surname2, name2;
                String path = @"C:\Users\reiya\OneDrive\デスクトップ\Integrated System Development\lab3\v4-";
                String fullpath = @String.Concat(path, sizeOfList.ToString(),"size.txt");
                //create student list 
                using (StreamWriter sw = new StreamWriter(fullpath))
                {
                    //Generate students list; 
                    for (int i = 0; i < sizeOfList; i++)
                    {
                        surname2 = string.Format("{0}{1}", surname_, i.ToString()); 
                        name2 = String.Concat(name_, i.ToString()); //same result with surname2
                        //random integer for 5 hw and 1 exam 
                        //min - 0, max - 10
                        in1 = random.Next(11);
                        in2 = random.Next(11);
                        in3 = random.Next(11);
                        in4 = random.Next(11);
                        in5 = random.Next(11);
                        in6 = random.Next(11); //exam
                        sw.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", surname2, name2, in1, in2, in3, in4, in5, in6);
                    }
                }
                Console.WriteLine("Created a student file.");

                //here read txt file
                Console.WriteLine("Read student data from txt file");
                Console.WriteLine("--------------------------------------------");
                try {
                    // Read each line of the file into a string array. Each element
                    // of the array is one line of the file.
                    string[] lines = System.IO.File.ReadAllLines(fullpath);
                    //Create Data class object to store each value
                    Student stu;
                    Student[] stuArr = new Student[lines.Length];
                    List<Student> stuList = new List<Student>();
                    //loop for storing splitted data
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] words = lines[i].Split(' ');
                        stuArr[i] = new Student(words);
                        stuList.Add(new Student(words));
                    }

                    //final result calculation
                    Lab3[] finalResult = new Lab3[stuArr.Length];
                    double res_avg;
                    //Final_points = 0.3 * average_of_hw + 0.7 * exam
                    for (int i = 0; i < stuArr.Length; i++)
                    {
                        res_avg = Math.Round(0.3 * (CalculateAvg(ReturnArrInt(stuArr[i]))) + 0.7 * stuArr[i].exam, 3);
                        //without final result calculated by using midian
                        //res_mid = Math.Round(0.3 * (CalculateMid(ReturnArrInt(stuArr[i]))) + 0.7 * stuArr[i].exam, 3);

                        //finalResult[i] = new Lab3(stuArr[i].surname, stuArr[i].name, res_avg, res_mid);
                        finalResult[i] = new Lab3(stuArr[i].surname, stuArr[i].name, res_avg);
                    }

                    //pass the sorted result to the func which divides into 2 groups - pass, fail
                    // and it returns lab3[][]
                    // Lab3[0][] - students who passed 
                    // Lab3[1][] - stidents who failed 
                    Lab3[][] pass_fail = PassOrFail(SortObjects(finalResult));
                    int pass_arr_length = pass_fail[0].Length;
                    int fail_arr_length = pass_fail[1].Length;
                    //declare 2 arrays of Lab3 object with the length of pass_fail[]
                    Lab3[] pass = new Lab3[pass_arr_length] ; 
                    Lab3[] fail = new Lab3 [fail_arr_length];
                    for (int i = 0; i < pass.Length; i++) {
                        pass[i] = pass_fail[0][i];
                    }
                    for (int i = 0; i < fail.Length; i++)
                    {
                        fail[i] = pass_fail[1][i];
                    }
                    //here write the result into 2 separated file. 
                    //declare file pass 
                    String pass_file = @"C:\Users\reiya\OneDrive\デスクトップ\Integrated System Development\lab3\v4-pass.txt";
                    String fail_file = @"C:\Users\reiya\OneDrive\デスクトップ\Integrated System Development\lab3\v4-fail.txt";
                    //exception handler 
                    if (pass.Length == null)
                    {
                        Console.WriteLine("All students failed exam.");
                        Console.WriteLine("Students list is sorted by NAME.");
                        WriteResultToFile(fail, fail_file);
                        Console.WriteLine("FAIL Students list has been published.");

                    }
                    else if (fail.Length == null) {
                        Console.WriteLine("All students passed exam.");
                        Console.WriteLine("Students list is sorted by NAME.");
                        WriteResultToFile(pass, pass_file);
                        Console.WriteLine("PASS Students list has been published.");
                    }
                    else if (pass.Length == null && fail.Length == null){
                        Console.WriteLine("The program has an error. Please contact the admin");
                    }
                    else
                    {
                        Console.WriteLine("Students list is sorted by NAME.");
                        WriteResultToFile(fail, fail_file);
                        Console.WriteLine("FAIL Students list has been published.");
                        WriteResultToFile(pass, pass_file);
                        Console.WriteLine("PASS Students list has been published.");

                    }
                    Console.WriteLine("Completed Version0.4 !");
                } 
                catch (FileNotFoundException e) {
                    Console.WriteLine("Please check the file exist and file name pass is correct.");
                }
                //the code that you want to measure comes here
                watch.Stop();
                String path_time = @"C:\Users\reiya\OneDrive\デスクトップ\Integrated System Development\lab3\v4-time-";
                String fullpath_time = @String.Concat(path_time, sizeOfList.ToString(),"size.txt");
                using (StreamWriter sw = new StreamWriter(fullpath_time)) {
                    sw.WriteLine("The execution time of {0} size list = {1} ms", sizeOfList, watch.ElapsedMilliseconds);
                }

                Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey();
            }
        }

    }
}