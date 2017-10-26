using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq2Object
{
    public class LinqBasicTest
    {
        private static List<People> mPeoplesList = new List<People>()
        {
            new People("Lily", 10),
            new People("HanMeimei", 20),
            new People("Qarth", 1000),
            new People("Stanis", 25)
        };

        private static List<People> mTestPeoples = new List<People>();
        private static Random mRandom=new Random();
        private static int t1, t2,result;


        public static void Expression1()
        {
//            var result = from people in mPeoplesList where people.Age > 18 select people.Name;
//            Console.WriteLine(string.Concat(result));

//            Console.WriteLine(string.Join(" - ",mPeoplesList.Where(t=>t.Age>18).Select(t=>t.Name)));


            List<People> tmp = new List<People>();
            tmp.AddRange(mPeoplesList.Where(t => t.Age > 18));
            Console.WriteLine(string.Join("\n", tmp.Select(t => t.DebugMsg())));
        }

        public static void PerformanceTest()
        {
            for (int i = 0; i < 10000000; i++)
            {
                mTestPeoples.Add(new People("",mRandom.Next(1,200),mRandom.NextDouble()));
            }
            
            t1 = DateTime.Now.Millisecond;

            for (int i = 0; i < mTestPeoples.Count; i++)
            {
                if (mTestPeoples[i].Age < 100)
                {
                    result++;
                }
            }
            t2 = DateTime.Now.Millisecond;
            Console.WriteLine("For: Start Time:{0} End Time:{1} Runout Times:{2} Result:{3}",t1,t2,t2-t1,result);

            result = 0;
            t1 = DateTime.Now.Millisecond;
            
            foreach (People people in mTestPeoples)
            {
                if (people.Age < 100)
                {
                    result++;
                }
            }
            t2 = DateTime.Now.Millisecond;
            Console.WriteLine("Foreach: Start Time:{0} End Time:{1} Runout Times:{2} Result:{3}",t1,t2,t2-t1,result);
            
            t1 = DateTime.Now.Millisecond;
            result = mTestPeoples.Where(t => t.Age < 100).Count();
            t2 = DateTime.Now.Millisecond;
            Console.WriteLine("Linq: Start Time:{0} End Time:{1} Runout Times:{2} Result:{3}",t1,t2,t2-t1,result);
            
            //-----------------------Console-----------------------
            // For: Start Time:391 End Time:586 Runout Times:195 Result:4973166
            // Foreach: Start Time:607 End Time:737 Runout Times:130 Result:4973166
            // Linq: Start Time:737 End Time:965 Runout Times:228 Result:4973166
            //-----------------------------------------------------
        }
    }

    public class People
    {
        private string mName;
        private int mAge;
        private double mHairCount;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public int Age
        {
            get { return mAge; }
            set { mAge = value; }
        }
        
        public double HairCount
        {
            get { return mHairCount; }
            set { mHairCount = value; }
        }

        public People(string name, int age,double hairCount=1)
        {
            mName = name;
            mAge = age;
            mHairCount = hairCount;
        }

        public string DebugMsg()
        {
            return "SB ^v^ --> " + mName + " Look at SB's age -->" + mAge;
        }

        public override string ToString()
        {
            return string.Format("Name:{0} Age:{1}", mName, mAge);
        }
    }
}