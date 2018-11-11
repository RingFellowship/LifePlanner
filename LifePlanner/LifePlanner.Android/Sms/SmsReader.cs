using System.Text;
using Android.App;

namespace LifePlanner.Droid.Sms
{
    public class SmsReader
    {
        public string ReadSms()
        {
            var smsBuilder = new StringBuilder();
            //var SMS_URI_INBOX = "content://sms/inbox";
            var SMS_URI_ALL = "content://sms/";

            var uri = Android.Net.Uri.Parse(SMS_URI_ALL);
            var projection = new[] { "_id", "address", "person", "body", "date", "type" };
            using (var cur = Application.Context.ContentResolver.Query(uri, projection, /*"address='123456789'"*/default, default, "date desc"))
            {
                if (cur.MoveToFirst())
                {
                    var index_Address = cur.GetColumnIndex("address");
                    var index_Person = cur.GetColumnIndex("person");
                    var index_Body = cur.GetColumnIndex("body");
                    var index_Date = cur.GetColumnIndex("date");
                    var index_Type = cur.GetColumnIndex("type");
                    do
                    {
                        var strAddress = cur.GetString(index_Address);
                        var intPerson = cur.GetInt(index_Person);
                        var strbody = cur.GetString(index_Body);
                        var longDate = cur.GetLong(index_Date);
                        var int_Type = cur.GetInt(index_Type);

                        smsBuilder.Append("[ ");
                        smsBuilder.Append(strAddress + ", ");
                        smsBuilder.Append(intPerson + ", ");
                        smsBuilder.Append(strbody + ", ");
                        smsBuilder.Append(longDate + ", ");
                        smsBuilder.Append(int_Type);
                        smsBuilder.Append(" ]\n\n");
                    } while (cur.MoveToNext());

                }
                else
                {
                    smsBuilder.Append("no result!");
                } // end if   

                return smsBuilder.ToString();
            }
        }
    }
}