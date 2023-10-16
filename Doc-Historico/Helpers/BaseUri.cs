using System;
namespace Doc_Historico.Helpers
{
	public class BaseUri
	{
        private static BaseUri _instance;

        public static BaseUri Instance => _instance ??= new BaseUri();
        public string Uri
        {
            get; set;
        } = "http://10.0.2.2:5047";
    }
}

