using Google.Apis.Drive.v3;

namespace DocumentManagementSystem
{
    public static class Constants
    {
        public static string AppName = "Drive Management";

        public const string AndroidClientId = "665218630602-nm19vj78qnvamtji6pnb3b2fosnac4bf.apps.googleusercontent.com";
        public static string iOSClientId = "665218630602-ue3pqk0lg6mb9brgt1tmsfaqipnu6hfl.apps.googleusercontent.com";
        
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.665218630602-nm19vj78qnvamtji6pnb3b2fosnac4bf:/oauth2redirect";
        public static string iOSRedirectUrl = "com.googleusercontent.apps.665218630602-ue3pqk0lg6mb9brgt1tmsfaqipnu6hfl:/oauth2redirect";

        public const string AndroidRedirectUrlScheme = "com.googleusercontent.apps.665218630602-nm19vj78qnvamtji6pnb3b2fosnac4bf";

        //public static string Scope = "https://www.googleapis.com/auth/drive.file";
        public static string Scope = DriveService.Scope.DriveFile + " " + DriveService.Scope.DriveReadonly + " https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        public static string driveFilesUrl = "https://www.googleapis.com/drive/v3/files";

        public static string UnAuthorizedRequest = "UnAuthorizedRequest";
        public static string IncorrectRequest = "IncorrectRequest";
    }
}
