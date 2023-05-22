using System.Xml.Linq;
using ZstdSharp.Unsafe;

namespace ServerCore
{
    internal static class Config
    {
        private const string CONFIG_NAME_NODE = "config";
        private const string SERVER_NAME_NODE = "server";
        private const string MONGO_NAME_NODE = "mongo";
        private const string POSTGRES_NAME_NODE = "postgres";
        private const string ERROR_MESSAGES_NAME_NODE = "error_messages";
        private const string PARAMETERS_NAME_NODE = "parameters";
        //private const string METHODS_NAME_NODE = "methods";

        private const string IP_NAME_NODE = "ip";
        private const string PORT_NAME_NODE = "port";

        private const string MONGO_CONNECTION_STRING_NODE = "connection_string";
        private const string MONGO_DATABASE_NAME_NODE = "database";
        private const string MONGO_COLLECTIONS_NAME_NODE = "collections";

        private const string ITEMS_COLLECTION_NAME_NODE = "items_collection";
        private const string CATEGORIES_COLLECTION_NAME_NODE = "categories_collection";
        private const string EMPLOYEE_COLLECTION_NAME_NODE = "employee_collection";
        private const string ORDER_COLLECTION_NAME_NODE = "order_collection";

        private const string POSTGRES_CONNECTION_STRING_NODE = "connection_string";

        private const string EXCEPTION1_NODE = "exception1";
        private const string EXCEPTION2_NODE = "exception2";
        private const string EXCEPTION3_NODE = "exception3";
        private const string EXCEPTION4_NODE = "exception4";
        private const string EXCEPTION5_NODE = "exception5";
        private const string EXCEPTION6_NODE = "exception6";
        private const string EXCEPTION7_NODE = "exception7";
        private const string EXCEPTION8_NODE = "exception8";

        private const string PRICE_FILTER_NODE = "price_filter";
        private const string COUNT_FILTER_NODE = "count_filter";
        private const string PAGE_NUMBER_NODE = "page_number";
        private const string PAGE_SIZE_NODE = "page_size";
        private const string SALARY_FILTER_NODE = "salary_filter";

        //private const string ITEMS_COUNT_METHOD_NODE = "items_count";
        //private const string ITEMS_GET_METHOD_NODE = "items_get";
        //private const string ITEMS_CREATE_METHOD_NODE = "items_create";
        //private const string ITEMS_DELETE_METHOD_NODE = "items_delete";
        //private const string EMPLOYEE_COUNT_METHOD_NODE = "employee_count";
        //private const string EMPLOYEE_GET_METHOD_NODE = "employee_get";
        //private const string EMPLOYEE_CREATE_METHOD_NODE = "employee_create";
        //private const string EMPLOYEE_DELETE_METHOD_NODE = "employee_delete";

        private static XElement? _config;

        private static XElement? _server;
        private static XElement? _mongo;
        private static XElement? _postgres;
        private static XElement? _errorMessages;
        private static XElement? _parameters;
        private static XElement? _methods;

        private static void ValidateConfig()
        {
            ArgumentNullException.ThrowIfNull(_server, SERVER_NAME_NODE);
            ArgumentNullException.ThrowIfNull(_mongo, MONGO_NAME_NODE);
            ArgumentNullException.ThrowIfNull(_postgres, POSTGRES_NAME_NODE);
            ArgumentNullException.ThrowIfNull(_errorMessages, ERROR_MESSAGES_NAME_NODE);
            ArgumentNullException.ThrowIfNull(_parameters, PARAMETERS_NAME_NODE);
            //ArgumentNullException.ThrowIfNull(_methods, METHODS_NAME_NODE);
        }

        public static void Load(string path)
        {
            _config = XDocument.Load(path).Document?.Root;
            ArgumentNullException.ThrowIfNull(_config, CONFIG_NAME_NODE);
            _server = _config.Element(SERVER_NAME_NODE);
            _mongo = _config.Element(MONGO_NAME_NODE);
            _postgres = _config.Element(POSTGRES_NAME_NODE);
            _errorMessages = _config.Element(ERROR_MESSAGES_NAME_NODE);
            _parameters = _config.Element(PARAMETERS_NAME_NODE);
            //_methods = _config.Element(METHODS_NAME_NODE);
            ValidateConfig();
        }

        public static string IpAddr => _server?.Element(IP_NAME_NODE)?.Value ?? throw new ArgumentNullException(IP_NAME_NODE);
        public static string Port => _server?.Element(PORT_NAME_NODE)?.Value ?? throw new ArgumentNullException(PORT_NAME_NODE);

        public static string MongoConnectionString => _mongo?.Element(MONGO_CONNECTION_STRING_NODE)?.Value ?? throw new ArgumentNullException(MONGO_CONNECTION_STRING_NODE);
        public static string MongoDatabaseName => _mongo?.Element(MONGO_DATABASE_NAME_NODE)?.Value ?? throw new ArgumentNullException(MONGO_DATABASE_NAME_NODE);

        public static string PostgresConnectionString => _postgres?.Element(POSTGRES_CONNECTION_STRING_NODE)?.Value ?? throw new ArgumentNullException(POSTGRES_CONNECTION_STRING_NODE);
        public static string ItemsCollectionName => _mongo?.Element(MONGO_COLLECTIONS_NAME_NODE)
                ?.Element(ITEMS_COLLECTION_NAME_NODE)?.Value ?? throw new ArgumentNullException(ITEMS_COLLECTION_NAME_NODE);
        public static string CategoriesCollectionName => _mongo?.Element(MONGO_COLLECTIONS_NAME_NODE)
                ?.Element(CATEGORIES_COLLECTION_NAME_NODE)?.Value ?? throw new ArgumentNullException(CATEGORIES_COLLECTION_NAME_NODE);
        public static string EmployeeCollectionName => _mongo?.Element(MONGO_COLLECTIONS_NAME_NODE)
                ?.Element(EMPLOYEE_COLLECTION_NAME_NODE)?.Value ?? throw new ArgumentNullException(EMPLOYEE_COLLECTION_NAME_NODE);
        public static string OrderCollectionName => _mongo?.Element(MONGO_COLLECTIONS_NAME_NODE)
                ?.Element(ORDER_COLLECTION_NAME_NODE)?.Value ?? throw new ArgumentNullException(ORDER_COLLECTION_NAME_NODE);

        public static string Exception1Message => _errorMessages?.Element(EXCEPTION1_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION1_NODE);
        public static string Exception2Message => _errorMessages?.Element(EXCEPTION2_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION2_NODE);
        public static string Exception3Message => _errorMessages?.Element(EXCEPTION3_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION3_NODE);
        public static string Exception4Message => _errorMessages?.Element(EXCEPTION4_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION4_NODE);
        public static string Exception5Message => _errorMessages?.Element(EXCEPTION5_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION5_NODE);
        public static string Exception6Message => _errorMessages?.Element(EXCEPTION6_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION6_NODE);
        public static string Exception7Message => _errorMessages?.Element(EXCEPTION7_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION7_NODE);
        public static string Exception8Message => _errorMessages?.Element(EXCEPTION8_NODE)?.Value ?? throw new ArgumentNullException(EXCEPTION8_NODE);

        public static string PriceFilterProperty => _parameters?.Element(PRICE_FILTER_NODE)?.Value ?? throw new ArgumentNullException(PRICE_FILTER_NODE);
        public static string CountFilterProperty => _parameters?.Element(COUNT_FILTER_NODE)?.Value ?? throw new ArgumentNullException(COUNT_FILTER_NODE);
        public static string PageNumberProperty => _parameters?.Element(PAGE_NUMBER_NODE)?.Value ?? throw new ArgumentNullException(PAGE_NUMBER_NODE);
        public static string PageSizeProperty => _parameters?.Element(PAGE_SIZE_NODE)?.Value ?? throw new ArgumentNullException(PAGE_SIZE_NODE);

        public static string SalaryFilterProperty => _parameters?.Element(SALARY_FILTER_NODE)?.Value ?? throw new ArgumentNullException(SALARY_FILTER_NODE);

        //public static string ITEMS_COUNT_METHOD => _methods?.Element(ITEMS_COUNT_METHOD_NODE)?.Value ?? throw new ArgumentNullException(ITEMS_COUNT_METHOD_NODE);
        //public static string ITEMS_GET_METHOD => _methods?.Element(ITEMS_GET_METHOD_NODE)?.Value ?? throw new ArgumentNullException(ITEMS_GET_METHOD_NODE);
        //public static string ITEMS_CREATE_METHOD => _methods?.Element(ITEMS_CREATE_METHOD_NODE)?.Value ?? throw new ArgumentNullException(ITEMS_CREATE_METHOD_NODE);
        //public static string ITEMS_DELETE_METHOD => _methods?.Element(ITEMS_DELETE_METHOD_NODE)?.Value ?? throw new ArgumentNullException(ITEMS_DELETE_METHOD_NODE);
        //public static string EMPLOYEE_COUNT_METHOD => _methods?.Element(EMPLOYEE_COUNT_METHOD_NODE)?.Value ?? throw new ArgumentNullException(EMPLOYEE_COUNT_METHOD_NODE);
        //public static string EMPLOYEE_GET_METHOD => _methods?.Element(EMPLOYEE_GET_METHOD_NODE)?.Value ?? throw new ArgumentNullException(EMPLOYEE_GET_METHOD_NODE);
        //public static string EMPLOYEE_CREATE_METHOD => _methods?.Element(EMPLOYEE_CREATE_METHOD_NODE)?.Value ?? throw new ArgumentNullException(EMPLOYEE_CREATE_METHOD_NODE);
        //public static string EMPLOYEE_DELETE_METHOD => _methods?.Element(EMPLOYEE_DELETE_METHOD_NODE)?.Value ?? throw new ArgumentNullException(EMPLOYEE_DELETE_METHOD_NODE);
    }
}
