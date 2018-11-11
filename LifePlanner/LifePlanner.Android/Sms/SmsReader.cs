using System.Collections.Generic;
using System.Text;
using Android.App;
using LifePlanner.Sms;

namespace LifePlanner.Droid.Sms
{
    public class SmsReader : ISmsReader
    {
        public IEnumerable<string> ReadSms()
        {
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
                        var smsBuilder = new StringBuilder();

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

                        yield return smsBuilder.ToString();

                    } while (cur.MoveToNext());
                }
                else
                {
                    yield return "no result!";
                } // end if
            }
        }
    }
}