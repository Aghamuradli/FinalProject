using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public static string AuthorizationDenied = "You are not authorized .";
        public static string UserRegistered = "Successfully registered .";
        public static string UserNotFound = "User not found .";
        public static string PasswordError = "password error .";
        public static string SuccessfulLogin = "successful login .";
        public static string UserAlreadyExists = "User already exists .";
        public static string AccessTokenCreated = "Token created .";
    }
}
