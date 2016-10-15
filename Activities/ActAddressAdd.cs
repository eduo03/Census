
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
using Test.Connection;

namespace Test.Activities
{
	[Activity (Label = "ActAddAddress",Theme = "@android:style/Theme.Holo.Light.NoActionBar")]			
	public class ActAddressAdd : Activity
	{
		private EditText txtProvince,txtCity,txtBarangay,txtNumber,txtFname,txtLname,txtContact;
		private Button btnAddaddress;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayAddressAdd);


			txtProvince = FindViewById<EditText> (Resource.Id.txtProvince);
			txtCity= FindViewById<EditText> (Resource.Id.txtCity);
			txtBarangay= FindViewById<EditText> (Resource.Id.txtBarangay);
			txtNumber= FindViewById<EditText> (Resource.Id.txtBuilding_no);
			txtFname= FindViewById<EditText> (Resource.Id.txtFname);
			txtLname= FindViewById<EditText> (Resource.Id.txtLname);
			txtContact= FindViewById<EditText> (Resource.Id.txtContact);


			btnAddaddress = FindViewById<Button> (Resource.Id.btnAddAddress);

			btnAddaddress.Click += new EventHandler (btnAddaddress_Clicked);
		}

		private void btnAddaddress_Clicked(object sender, EventArgs e)
		{ 
			if (txtProvince.Text != "" && txtCity.Text != "" && txtBarangay.Text != "" && txtNumber.Text != "" && txtFname.Text != "" && txtLname.Text != "" && txtContact.Text != "") {

				var builder = new AlertDialog.Builder (this);
				builder.SetTitle ("Census");
				builder.SetMessage ("Are You Sure You Want To Save Address?");
				builder.SetPositiveButton ("Yes", Save_Address);
				builder.SetNegativeButton ("Cancel", delegate {
					builder.Dispose ();
				});
				builder.Show ();
			}
			else
			{
				Toast.MakeText (this, "Unable to continue.\n Please Complete The Field.", ToastLength.Long).Show ();
			}
		}

		private void Save_Address(object sender, DialogClickEventArgs args)
		{
			ConAddress.AddAddressList (txtProvince.Text, txtCity.Text, txtBarangay.Text, txtNumber.Text, txtFname.Text, txtLname.Text, txtContact.Text,GlobalVariables.GlobalYear);

			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Saving Address Successful");
			builder.SetPositiveButton("Ok",delegate {
				var intent = new Intent ();
				intent.SetClass (this, typeof(ActAddress));
				StartActivity (intent);
			});
			builder.Show ();
		}
	}
}

