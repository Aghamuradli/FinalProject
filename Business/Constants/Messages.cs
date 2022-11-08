using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product added ."; //public oldugu ucun PascalCase
        public static string ProductNameInvalid = "Product name is invalid .";
        public static string MaintenanceTime = "System maintenance .";
        public static string ProductsListed = "Products listed .";
        public static string ProductCountOfCategoryError = "A category can have up to 10 items .";
        public static string ProductNameAlreadyExists = "Product name already exists .";
        public static string CategoryLimitExceeded = "Unable to add new product because category limit is exceeded .";
    }
}
