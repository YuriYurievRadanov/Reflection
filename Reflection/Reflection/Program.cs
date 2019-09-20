using System;
using System.Reflection;

namespace Reflection
{
    //a custom attribute BugFix to be assigned to a class and its members
    [AttributeUsage(
       AttributeTargets.Class |
       AttributeTargets.Constructor |
       AttributeTargets.Field |
       AttributeTargets.Method |
       AttributeTargets.Property,
       AllowMultiple = true)]

    public class DeBugInfo : System.Attribute
    {
        public string message;

        public DeBugInfo(int bg, string dev, string d)
        {
            this.BugNo = bg;
            this.Developer = dev;
            this.LastReview = d;
        }
        public int BugNo { get; }

        public string Developer { get; }
        public string LastReview { get; }

        public string Message
        {
            get => message;
            set => message = value;
        }
    }
    [DeBugInfo(45, "Fizzy", "12/8/2019", Message = "Return type mismatch")]
    [DeBugInfo(49, "Buzzy", "10/10/2019", Message = "Unused variable")]

    class Rectangle
    {
        //member variables
        protected double length;
        protected double width;

        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }
        [DeBugInfo(55, "Fizzy", "19/10/2019", Message = "Return type mismatch")]
        public double GetArea()
        {
            return length * width;
        }
        [DeBugInfo(56, "Fizzy", "19/10/2019")]
        public void Display()
        {
            Console.WriteLine("Length: {0}", length);
            Console.WriteLine("Width: {0}", width);
            Console.WriteLine("Area: {0}", GetArea());
        }
    }//end class Rectangle

    class ExecuteRectangle
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle(4.5, 7.5);
            r.Display();
            Type type = typeof(Rectangle);

            //iterating through the attribtues of the Rectangle class
            foreach (Object attributes in type.GetCustomAttributes(false))
            {
                DeBugInfo dbi = (DeBugInfo)attributes;

                if (null != dbi)
                {
                    Console.WriteLine("Bug no: {0}", dbi.BugNo);
                    Console.WriteLine("Developer: {0}", dbi.Developer);
                    Console.WriteLine("Last Reviewed: {0}", dbi.LastReview);
                    Console.WriteLine("Remarks: {0}", dbi.Message);
                }
            }

            //iterating through the method attribtues
            foreach (MethodInfo m in type.GetMethods())
            {

                foreach (Attribute a in m.GetCustomAttributes(true))
                {
                    DeBugInfo dbi = (DeBugInfo)a;

                    if (null != dbi)
                    {
                        Console.WriteLine("Bug no: {0}, for Method: {1}", dbi.BugNo, m.Name);
                        Console.WriteLine("Developer: {0}", dbi.Developer);
                        Console.WriteLine("Last Reviewed: {0}", dbi.LastReview);
                        Console.WriteLine("Remarks: {0}", dbi.Message);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}