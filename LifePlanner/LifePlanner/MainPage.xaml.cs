using LifePlanner.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LifePlanner
{
    public partial class MainPage : ContentPage
    {
        private readonly ISmsReader smsReader;

        public MainPage(ISmsReader smsReader)
        {
            InitializeComponent();
            this.smsReader = smsReader;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            smsList.ItemsSource = smsReader.ReadSms();
        }
    }
}
