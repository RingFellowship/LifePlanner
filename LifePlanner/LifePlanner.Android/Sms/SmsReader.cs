using System.Collections.Generic;
using Android.App;
using LifePlanner.Sms;

namespace LifePlanner.Droid.Sms
{
    public class SmsReader : ISmsReader
    {
        public IEnumerable<string> ReadSms() // todo добавить string address
        {
            //var SMS_URI_INBOX = "content://sms/inbox";
            var SMS_URI_ALL = "content://sms/";

            var uri = Android.Net.Uri.Parse(SMS_URI_ALL);
            var projection = new[] { "_id", "address", "person", "body", "date", "type" };
            using (var cur = Application.Context.ContentResolver.Query(uri, projection, /*"address='123456789'"*/default, default, "date desc"))
            {
                if (cur.MoveToFirst())
                {
                    var address = cur.GetColumnIndex("address");
                    var person = cur.GetColumnIndex("person");
                    var body = cur.GetColumnIndex("body");
                    var date = cur.GetColumnIndex("date");
                    var type = cur.GetColumnIndex("type");
                    do
                    {
                        var strAddress = cur.GetString(address);
                        var intPerson = cur.GetInt(person);
                        var strbody = cur.GetString(body);
                        var longDate = cur.GetLong(date);
                        var int_Type = cur.GetInt(type);

                        yield return $"[ {strAddress}, {intPerson}, {strbody}, {longDate}, {int_Type} ]"; 
                        // todo Заменить на класс описывающий смс
                        // todo Так же добавить класс описывающий смс в smsreceiver
                        // todo И вынести в сборку LifePlanner.Core и интерфейс тоже

                    } while (cur.MoveToNext());
                }
            }
        }
    }
}