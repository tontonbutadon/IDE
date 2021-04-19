using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace version3
{
    public struct DataStruct {
        public string name, surname;
        public int h1, h2, h3, h4, h5, exam;
    }
    public class Data
    {
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
                //Console.WriteLine("mid - " + mid);
                return mid;
            }

            static private void ShowResult(List<Lab3> data)
            {
                //Console.WriteLine("First Name | Last Name  |   Age")
                Console.WriteLine("Surname      Name           Final points(Avg.)");
                Console.WriteLine("------------------------------------------------");
                for (int i = 0; i < data.Count; i++)
                {
                    Console.WriteLine(String.Format("{0,-10}   {1,-10}   {2,5}", data[i].surname, data[i].name, data[i].result));
                }
            }
            static private void ShowResultWithMid(List<Lab3StructResult> data)
            {
                //Console.WriteLine("First Name | Last Name  |   Age")
                Console.WriteLine("Surname      Name           Final points(Avg.) /     Final points(Mid.)");
                Console.WriteLine("----------------------------------------------------------------------------------");
                for (int i = 0; i < data.Count; i++)
                {
                    Console.WriteLine(String.Format("{0,-10}   {1,-10}   {2,19}   {3,24}", data[i].surname, data[i].name, data[i].result, data[i].resultMid));
                }
            }
            //sort Lab3[] by name
            static private List<Lab3StructResult> SortObjects(List<Lab3StructResult> data)
            {
                data.Sort(delegate (Lab3StructResult data1, Lab3StructResult data2)
                {
                    if (data1.name == null && data2.name == null) return 0;
                    else if (data1.name == null) return 1;
                    else if (data2.name == null) return -1;
                    else return data1.name.CompareTo(data2.name);
                });
                return data;
            }
 
            string name, surname;
            double result, resultMid;             
            public struct Lab3StructResult
            {
                public string name, surname;
                public double result, resultMid;
            }

            static void Main(string[] args)
            {
                Console.WriteLine("Read data from txt file");
                Console.WriteLine("--------------------------------------------");
                // Read each line of the file into a string array. Each element
                // of the array is one line of the file.
                try {
                    string[] lines = System.IO.File.ReadAllLines(@"C:\Users\reiya\OneDrive\デスクトップ\Integrated System Development\lab3\lab3v2.txt");
                    //skip the first line as it is the name of column
                    lines = lines.Skip(1).ToArray();
                    //Create  list of Data structures  and a data structure to store each value
                    var dataStructList = new List<DataStruct>();
                    var item = new DataStruct();
                    //split lines and store string array 
                    //store a word to structure
                    for (int i = 0; i < lines.Length; i++) //lines.length = 4
                    {
                        string[] words = lines[i].Split(' ');

                        item.surname = words[0];
                        item.name = words[1];
                        item.h1 = Int32.Parse(words[2]);
                        item.h2 = Int32.Parse(words[3]);
                        item.h3 = Int32.Parse(words[4]);
                        item.h4 = Int32.Parse(words[5]);
                        item.h5 = Int32.Parse(words[6]);
                        item.exam = Int32.Parse(words[7]);
                        dataStructList.Add(item);
                        item = new DataStruct();
                    }

                    double res_avg, res_mid;
                    var lab3StructResultList = new List<Lab3StructResult>();
                    var lab3StructResultItem = new Lab3StructResult();
                    for (int i = 0; i < dataStructList.Count; i++)
                    {
                        int[] intArr = { dataStructList[i].h1, dataStructList[i].h2, dataStructList[i].h3, dataStructList[i].h4, dataStructList[i].h5 };
                        res_avg = Math.Round(0.3 * (CalculateAvg(intArr)) + 0.7 * dataStructList[i].exam, 3);
                        res_mid = Math.Round(0.3 * (CalculateMid(intArr)) + 0.7 * dataStructList[i].exam, 3);
                        lab3StructResultItem.name = dataStructList[i].name;
                        lab3StructResultItem.surname = dataStructList[i].surname;
                        lab3StructResultItem.result = res_avg;
                        lab3StructResultItem.resultMid = res_mid;
                        lab3StructResultList.Add(lab3StructResultItem);
                        lab3StructResultItem = new Lab3StructResult();
                    }

                    Console.WriteLine("Sort by Name (not surname)");
                    ShowResultWithMid(SortObjects(lab3StructResultList));

                    Console.WriteLine("Completed Version0.3 !");
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Please check file is existing and file pass.");
                }

                // Keep the console window open in debug mode.
                Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey();
            }
        }
    }
}
