﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dentist.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPatientPage : ContentPage
	{
		public AddPatientPage ()
		{
			InitializeComponent ();
		}

        private void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}