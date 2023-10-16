using System;
namespace Doc_Historico.Helpers
{
    public class BaseUri
    {
        private static readonly Lazy<BaseUri> _instance = new Lazy<BaseUri>(() => new BaseUri());

        public static BaseUri Instance => _instance.Value;
#if ANDROID
        public string Uri { get; } = "http://10.0.2.2:5047";
#endif
#if IOS
        public string Uri { get; } = "http://127.0.0.1:5047";
#endif
        public string GetPatientsUri => $"{Uri}/Patient";

        private BaseUri() { }
    }

}

