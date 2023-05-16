using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    internal static class Config
    {
        public static string IpAddr { get; } = "127.0.0.1";
        public static int Port { get; } = 13000;

        public static string MongoConnectionString { get; } = "mongodb://localhost:27017";
        public static string MongoDatabaseName { get; } = "shop";

        public static string PostgresConnectionString { get; } = "User ID=postgres;Password=nioder125;Host=localhost;Port=5432;Database=ServerDatabase";

        public static string ITEMS_COLLECTION_NAME = "items";
        public static string CATEGORIES_COLLECTION_NAME = "categories";
        public static string EMPLOYEE_COLLECTION_NAME = "employee";
        public static string ORDER_COLLECTION_NAME = "order";

        public static string Exception1Message { get; } = "Invalid id. Id is not specified or has invalid format";
        public static string Exception2Message { get; } = "Page number out of range";
        public static string Exception3Message { get; } = "Size of page is not specified";
        public static string Exception4Message { get; } = "Invalid data type of parameter";
        public static string Exception5Message { get; } = "Something went wrong";
        public static string Exception6Message { get; } = "You have no access to do that";
        public static string Exception7Message { get; } = "Request body cannot be empty";
        public static string Exception8Message { get; } = "Invalid request body format";

        public static string PriceFilterProperty { get; } = "price_filter";
        public static string CountFilterProperty { get; } = "count_filter";
        public static string PageNumberProperty { get; } = "page";
        public static string PageSizeProperty { get; } = "page_size";
    }
}
