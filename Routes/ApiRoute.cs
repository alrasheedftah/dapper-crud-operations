namespace EmployeeTasks.Routes;
    public static class ApiRoutes
    {
        public const string BaseApiUrl = "api";
        public const string BaseAuthUrl = "auth";

        public static class AuthRoute
        {
            public const string Login = BaseAuthUrl + "/login";
            public const string Register = BaseAuthUrl + "/register";
            public const string Verify = BaseAuthUrl + "/verify";
            public const string RefreshToen = BaseAuthUrl + "/refresh-token";
        }

        public static class EmployRoute
        {
            public const string Employees = BaseApiUrl + "/employees";

        }

    }
