using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    internal static class Config
    {
        public static string IpAddr { get; private set; } = "127.0.0.1";
        public static int Port { get; set; } = 13000;

        public static string MongoConnectionString { get; set; } = "mongodb://localhost:27017";
        public static string MongoDatabaseName { get; set; } = "shop";

        public static string ITEMS_COLLECTION_NAME = "items";
        public static string CATEGORIES_COLLECTION_NAME = "categories";
        public static string EMPLOYEE_COLLECTION_NAME = "employee";
        public static string ORDER_COLLECTION_NAME = "order";

        public static string Exception1Message { get; set; } = "Invalid id. Id is not specified or has invalid format";
        public static string Exception2Message { get; set; } = "Page number out of range";
        public static string Exception3Message { get; set; } = "Size of page is not specified";
        public static string Exception4Message { get; set; } = "Invalid data type of parameter";
        public static string Exception5Message { get; set; } = "Something went wrong";
        public static string Exception6Message { get; set; } = "You have no access to do that";
    }
}
