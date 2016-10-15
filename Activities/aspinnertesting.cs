
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Test.Models;
using Test.Adapter;
using Test.Connection;

namespace Test.Activities
{
	[Activity (Label = "aspinnertesting")]			
	public class aspinnertesting : Activity
	{
		private Spinner spntest;
		private List<tblAddressList> _address;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.aspinnertest);

			var spinner = FindViewById<Spinner>(Resource.Id.spntest);
			_address = ConAddress.GetList ("");
			var adapter = new CustomSpinnerAdapter(this,_address);
			spinner.Adapter = adapter;
			spinner.ItemSelected += SpinnerItemSelected;
		}

		private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Toast.MakeText(this, "Id:"+_address[e.Position].id +" Name"+_address[e.Position].barangay, ToastLength.Long).Show();
		}
	}
}

