using Microsoft.InformationProtection;

namespace GreetingsLib
{
    public class Greetings
    {

        private ApplicationInfo _appInfo = new ApplicationInfo()
        {
            ApplicationName = "Application Asif Hussain"
        };

        public string SayHello(string userName)
        {

            return $"Hello, {userName}. {_appInfo.ApplicationName}";
        }
    }
}