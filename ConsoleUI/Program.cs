using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SOLID - O => Open Closed
            ProductTest();
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine("Order : " + category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            Console.WriteLine("ProductName | CategoryName");

            Console.WriteLine("");

            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine(product.ProductName + " /   " + product.CategoryName);
            }
        }
    }
}
