
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
	public class ActDeathAdd : Activity
	{
		private EditText txtfname,txtlname,txtexname,txtbday;
		private Button btnAddaddress;
		private Spinner spngender,spndeathcert,spnregisterdeath; 

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayDeathAdd);


			txtfname = FindViewById<EditText> (Resource.Id.txtfname);
			txtlname= FindViewById<EditText> (Resource.Id.txtlname);
			txtexname= FindViewById<EditText> (Resource.Id.txtexname);
			txtbday= FindViewById<EditText> (Resource.Id.txtageatdeath);

			spngender= FindViewById<Spinner> (Resource.Id.spngender);
			spndeathcert= FindViewById<Spinner> (Resource.Id.spndeathcert);
			spnregisterdeath= FindViewById<Spinner> (Resource.Id.spnregisterdeath);


			var adaptergender = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Gender_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adaptergender.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spngender.Adapter = adaptergender;

			var adpdeathcert = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Death_certificate, Android.Resource.Layout.SimpleSpinnerItem);
			adpdeathcert.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spndeathcert.Adapter = adpdeathcert;

			var adpregisterdeath = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Death_register, Android.Resource.Layout.SimpleSpinnerItem);
			adpregisterdeath.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnregisterdeath.Adapter = adpregisterdeath;



			btnAddaddress = FindViewById<Button> (Resource.Id.btnAddAddress);
			btnAddaddress.Click += new EventHandler (btnAddaddress_Clicked);

		}

		private void btnAddaddress_Clicked(object sender, EventArgs e)
		{ 																																																																																					
			if (txtfname.Text != "" && txtlname.Text != "" && txtexname.Text != "" && txtbday.Text != "" && spngender.SelectedItem.ToString() != "Select Gender" &&  spndeathcert.SelectedItem.ToString() != "Copy Of Death Certificate" && spnregisterdeath.SelectedItem.ToString() != "Death Registration") 
			{

				var builder = new AlertDialog.Builder(this);
				builder.SetTitle ("Census");
				builder.SetMessage ("Are You Sure You Want To Save Information of Death?");
				builder.SetPositiveButton("Yes", Save_Address);
				builder.SetNegativeButton("Cancel",delegate { builder.Dispose(); });
				builder.Show ();
			}
			else
			{
				Toast.MakeText (this, "Unable to continue.\n Please Complete The Field.", ToastLength.Long).Show ();
			}
		}


		private void Save_Address(object sender, DialogClickEventArgs args)
		{
			ConDeath.AddDeath (Intent.GetStringExtra("address_id").ToString(),txtfname.Text,txtlname.Text,txtexname.Text,spngender.SelectedItem.ToString(),txtbday.Text,spnregisterdeath.SelectedItem.ToString(),spndeathcert.SelectedItem.ToString());

			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Saving Death Information Successful");
			builder.SetCancelable (false);
			builder.SetPositiveButton ("Ok", Save_success);
			builder.Show ();
		}
		private void Save_success(object sender, DialogClickEventArgs args)
		{
			var intent = new Intent ();
			intent.PutExtra("address_id",Intent.GetStringExtra ("address_id"));
			intent.PutExtra("fulladdress",Intent.GetStringExtra ("fulladdress"));
			intent.SetClass(this,typeof(ActDeath));
			StartActivity(intent);



		}
	
	}
}

