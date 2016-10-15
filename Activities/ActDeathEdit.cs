
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
	public class ActDeathEdit : Activity
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

			var items=ConDeath.GetListSingle ("select * from tblDeath where id='"+ Intent.GetStringExtra("death_id") +"'");

			txtfname.Text = items.fname;
			txtlname.Text = items.lname;
			txtexname.Text = items.ename;
			txtbday.Text = items.age_death;

			string[] array;
			array = Resources.GetStringArray (Resource.Array.Gender_Spinner);
			fcnspinner (spngender, array, items.gender);

			array = Resources.GetStringArray (Resource.Array.Death_certificate);
			fcnspinner (spndeathcert, array, items.birthcertificate);
			array = Resources.GetStringArray (Resource.Array.Death_register);
			fcnspinner (spnregisterdeath, array, items.register);

			btnAddaddress = FindViewById<Button> (Resource.Id.btnAddAddress);
			btnAddaddress.Click += new EventHandler (btnAddaddress_Clicked);

		}

		private void fcnspinner(Spinner spn,Array arr,string val)
		{
			for (int x = 0; x < arr.Length; x++) 
			{
				if (arr.GetValue (x).ToString () == val) 
				{
					spn.SetSelection (x);
					break;
				}
			}

		}
			
		private void btnAddaddress_Clicked(object sender, EventArgs e)
		{ 																																																																																					
			if (txtfname.Text != "" && txtlname.Text != "" && txtexname.Text != "" && txtbday.Text != "" && spngender.SelectedItem.ToString() != "Select Gender" &&  spndeathcert.SelectedItem.ToString() != "Copy Of Death Certificate" && spnregisterdeath.SelectedItem.ToString() != "Death Registration") 
			{

				var builder = new AlertDialog.Builder(this);
				builder.SetTitle ("Census");
				builder.SetMessage ("Are You Sure You Want To Update Information Of Death?");
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
			tblDeath updatdeath = new tblDeath ();
			updatdeath.id=Convert.ToInt32(Intent.GetStringExtra("death_id"));
			updatdeath.address_id = Intent.GetStringExtra ("address_id");
			updatdeath.fname=txtfname.Text;
			updatdeath.lname=txtlname.Text;
			updatdeath.ename=txtexname.Text;
			updatdeath.gender=spngender.SelectedItem.ToString();
			updatdeath.age_death=txtbday.Text;
			updatdeath.birthcertificate=spndeathcert.SelectedItem.ToString();
			updatdeath.register=spnregisterdeath.SelectedItem.ToString();

			ConDeath.UpdateList (updatdeath);

			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Updating Death Information Successful");
			builder.SetCancelable(false);
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

