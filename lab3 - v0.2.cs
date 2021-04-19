using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WithListTypeVersion1
{
    public class Data
    {
        string name, surname;
        int exam, h1, h2, h3, h4, h5;
        Data() { }
        Data(string[] data)
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
        class Lab3
        {
            static private double CalculateAvg(int[] arr)
            {
                double avg = Queryable.Average(arr.AsQueryable());
                //Console.WriteLine(avg);
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

            static private int[] ReturnArrInt(Data data)
            {
                //get int array for all hw points 
                int[] hws = new int[5] { data.h1, data.h2, data.h3, data.h4, data.h5 };
                /*
                Console.WriteLine("Array");
                foreach (int i in hws) {
                    Console.WriteLine(i);
                }
                Console.WriteLine("------");
                */
                return hws;
            }

            //sort Lab3[] by name
            static private Lab3[] SortObjects(Lab3[] data) {
                Array.Sort(data, delegate (Lab3 data1, Lab3 data2)
                {
                    return data1.name.CompareTo(data2.name);
                });
                return data;
             }

            string name, surname;
            double result, resultMid;
            public Lab3() { }
            public Lab3(string name_, string surname_, double result_, double resultMid_)
            {
                this.name = name_;
                this.surname = surname_;
                this.result = result_;
                this.resultMid = resultMid_;
            }

            static void Main(string[] args)
            {
                Console.WriteLine("Read data from txt file");
                Console.WriteLine("--------------------------------------------");
                // Read each line of the file into a string array. Each element
                // of the array is one line of the file.
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\reiya\OneDrive\デスクトップ\Integrated System Development\lab3\lab3v2.txt");
                //skip the first line as it is the name of column
                lines = lines.Skip(1).ToArray();
                //Create Data class object to store each value
                Data[] data = new Data[lines.Length];

                //loop for storing splitted data
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] words = lines[i].Split(' ');
                    data[i] = new Data(words);
                }

                //public Lab3(string name_, string surname_, double result_, double resultMid_)
                Lab3[] finalResult = new Lab3[data.Length];
                double res_avg, res_mid;
                //Final_points = 0.3 * average_of_hw + 0.7 * exam
                for (int i = 0; i < data.Length; i++)
                {
                    res_avg = Math.Round(0.3 * (CalculateAvg(ReturnArrInt(data[i]))) + 0.7 * data[i].exam, 3);
                    res_mid = Math.Round(0.3 * (CalculateMid(ReturnArrInt(data[i]))) + 0.7 * data[i].exam, 3);

                    finalResult[i] = new Lab3(data[i].surname, data[i].name, res_avg, res_mid);
                }
                ShowResultWithMid(SortObjects(finalResult));

                Console.WriteLine("Completed Version0.2 !");
                // Keep the console window open in debug mode.
                Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey();
            }
        }
    }
}
