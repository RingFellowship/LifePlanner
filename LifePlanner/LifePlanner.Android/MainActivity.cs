using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android;
using System.Linq;
using LifePlanner.Droid.Sms;

namespace LifePlanner.Droid
{
    [Activity(Label = "LifePlanner", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private readonly string[] PermissionsGroupLocation =
        {
            Manifest.Permission.ReadSms,
            Manifest.Permission.ReceiveSms,
        };

        private const int RequestLocationId = 0;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            ShowToast($"Special permissions {(grantResults[0] == (int)Permission.Granted ? "granted" : "denied")}");
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            TryToGetPermissions();
            LoadApplication(new App(new SmsReader()));
        }

        private void TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                GetPermissionsAsync();
            }
        }

        private void GetPermissionsAsync()
        {
            foreach (var permission in PermissionsGroupLocation.Where(permission => CheckSelfPermission(permission) != (int)Permission.Granted))
            {
                using (var alert = new AlertDialog.Builder(this))
                {
                    alert.SetTitle("Permissions Needed");
                    alert.SetMessage("The application need special permissions to continue");
                    alert.SetPositiveButton("Request Permissions", (senderAlert, args) => RequestPermissions(new[] { permission }, RequestLocationId));
                    alert.SetNegativeButton("Cancel", (senderAlert, args) => ShowToast("Cancelled!"));

                    using (var dialog = alert.Create())
                    {
                        dialog.Show();
                    }
                }
            }
        }

        private void ShowToast(string text, ToastLength duration = ToastLength.Short)
        {
            using (var toast = Toast.MakeText(this, text, duration))
            {
                toast.Show();
            }
        }
    }
}