using Android.App;
using Android.Content;
using Android.Provider;
using System;

namespace LifePlanner.Droid.Sms
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SmsReceiver : BroadcastReceiver
    {
        public static event Action<string> Sms;

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(Telephony.Sms.Intents.SmsReceivedAction))
            {
                var msgs = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
                foreach (var msg in msgs)
                {
                    //Log.Debug(TAG, $" MessageBody {msg.MessageBody}");
                    //Log.Debug(TAG, $"DisplayOriginatingAddress {msg.DisplayOriginatingAddress}");
                    //Log.Debug(TAG, $"OriginatingAddress {msg.OriginatingAddress}");

                    Sms?.Invoke(msg.MessageBody);
                }
            }
        }
    }
}