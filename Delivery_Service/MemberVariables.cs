﻿using Microsoft.VisualBasic;

namespace Delivery_Service
{
    public static class MemberVariables
    {
        public static string BASE_URL_RESTAURANT_SERVICE = "";
        public static string BASE_URL_ORDER_SERVICE = "";
        public static string BASE_URL_CUSTOMER_SERVICE = Environment.GetEnvironmentVariable("CUSTOMER_SERVICE");
        public static string ENDPOINT_CUSTOMER_SERVICE__CUSTOMER_ADDRESS = "CustomerAddress/#1";
    }
}