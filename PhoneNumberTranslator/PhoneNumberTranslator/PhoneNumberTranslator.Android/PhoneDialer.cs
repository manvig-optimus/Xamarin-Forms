using Android.Content;
using Android;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android.App;
using Android.Content.PM;
using System.Linq;
using Android.Telephony;
using Xamarin.Forms;
using PhoneNumberTranslator.Droid;
using Uri = Android.Net.Uri;
using PhoneNumberTranslator.Dialer;

[assembly: Dependency(typeof(PhoneDialer))]
namespace PhoneNumberTranslator.Droid
{
    public class PhoneDialer : IDialer
    {
        private static readonly int _requestCall = 1;

        public bool Dial(string number)
        {
            var context = MainActivity.Instance;
            if (context == null)
                return false;

            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Uri.Parse("tel:" + number));

            if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.CallPhone) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity)context, new string[] { Manifest.Permission.CallPhone }, _requestCall);
            }
            else if (IsIntentAvailable(context, intent))
            {
                App.PhoneNumbers.Add(number);
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));

            if (list.Any())
                return true;

            var manager = TelephonyManager.FromContext(context);
            return manager.PhoneType != PhoneType.None;
        }
    }
}
