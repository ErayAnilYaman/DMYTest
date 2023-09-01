using DMYTest.Data.Context;
using DMYTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMYTest.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (InternDBContext context = new InternDBContext())
            {
                context.Database.CreateIfNotExists();
                //var category = new Category()
                //{
                   
                //    //Description = "Apple",
                //    //ProductID = 1,
                //    //ProductName = "Iphone",
                //    //Stock = 15,
                //    //UnitPrice = 150,
                //    CategoryID = 1,
                //    CategoryName = "Test"
                   
                    

                //};
                //context..Add(category);

                context.SaveChanges();
               
            }
            Console.WriteLine("Hello World");
            Console.ReadLine();
        }
    }
}
