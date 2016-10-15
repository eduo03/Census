
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
	public class ActMemberAdd : Activity
	{
		private EditText txtfname,txtmname,txtlname,txtexname,txtbday,txtreligion,txtoccupation;
		private Button btnAddaddress;
		private Spinner spngender,spnrhouse,spnbirthcert,spnmarital,spnschoolhigh,spnschoolattendance,spnliteracy,spnworkstatus;
		private Boolean dialogdate=true; 

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayNameAdd);


			txtfname = FindViewById<EditText> (Resource.Id.txtfname);
			txtmname= FindViewById<EditText> (Resource.Id.txtmname);
			txtlname= FindViewById<EditText> (Resource.Id.txtlname);
			txtexname= FindViewById<EditText> (Resource.Id.txtexname);
			txtbday= FindViewById<EditText> (Resource.Id.txtbday);
			txtreligion= FindViewById<EditText> (Resource.Id.txtreligion);
			txtoccupation= FindViewById<EditText> (Resource.Id.txtoccupation);

			spngender= FindViewById<Spinner> (Resource.Id.spngender);
			spnrhouse= FindViewById<Spinner> (Resource.Id.spnrhouse);
			spnbirthcert= FindViewById<Spinner> (Resource.Id.spnbirthcert);
			spnmarital= FindViewById<Spinner> (Resource.Id.spnmarital);
			spnschoolattendance= FindViewById<Spinner> (Resource.Id.spnschoolattendance);
			spnschoolhigh= FindViewById<Spinner> (Resource.Id.spnschoolhigh);
			spnliteracy= FindViewById<Spinner> (Resource.Id.spnliteracy);
			spnworkstatus= FindViewById<Spinner> (Resource.Id.spnworkstatus);

			var adaptergender = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Gender_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adaptergender.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spngender.Adapter = adaptergender;

			var adapterhouse = ArrayAdapter.CreateFromResource(
				this, Resource.Array.RHOUSE_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adapterhouse.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnrhouse.Adapter = adapterhouse;

			var adpbirthcert = ArrayAdapter.CreateFromResource(
				this, Resource.Array.BirthCertCopy_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpbirthcert.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnbirthcert.Adapter = adpbirthcert;

			var adpmarital = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Marital_Spinner , Android.Resource.Layout.SimpleSpinnerItem);
			adpmarital.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnmarital.Adapter = adpmarital;

			var adpschoolattendance = ArrayAdapter.CreateFromResource(
				this, Resource.Array.SchoolAttend_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpschoolattendance.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnschoolattendance.Adapter = adpschoolattendance;

			var adpschoolhigh = ArrayAdapter.CreateFromResource(
				this, Resource.Array.SchoolHigh_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpschoolhigh.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnschoolhigh.Adapter = adpschoolhigh;

			var adpliteracy = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Literacy_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpliteracy.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnliteracy.Adapter = adpliteracy;

			var adpworkstatus = ArrayAdapter.CreateFromResource(
				this, Resource.Array.WorkStatus_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpworkstatus.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnworkstatus.Adapter = adpworkstatus;

			txtbday.Touch += (sender, e) => {
				if (dialogdate == true) {
					dialogdate = false;
					var inputView = LayoutInflater.Inflate (Resource.Layout.LayInputDATE, null);
					var builder = new AlertDialog.Builder (this);
					var datep = (DatePicker)inputView.FindViewById (Resource.Id.datepicker);
					builder.SetTitle ("Cencus");
					builder.SetMessage ("Select Birthday : ");
					builder.SetView (inputView);
					builder.SetPositiveButton ("Change", OkDialog_Clicked);
					builder.SetNegativeButton ("Cancel", delegate {
						builder.Dispose ();
					});
					builder.Show ();
				} else {
					dialogdate = true;
				}
			};

			btnAddaddress = FindViewById<Button> (Resource.Id.btnAddAddress);
			btnAddaddress.Click += new EventHandler (btnAddaddress_Clicked);

		}

		private void OkDialog_Clicked(object sender,DialogClickEventArgs args)
		{
			var dialog = (AlertDialog)sender;
			var datepicker = (DatePicker)dialog.FindViewById (Resource.Id.datepicker);
			txtbday.Text = datepicker.DateTime.ToString ("yyyy-MM-dd");
		}

		private void btnAddaddress_Clicked(object sender, EventArgs e)
		{ 																																																																																					
			if (  txtfname.Text != "" && txtlname.Text != "" && txtmname.Text != "" && txtbday.Text != "" && txtreligion.Text != "" && txtoccupation.Text != "" && spngender.SelectedItem.ToString() != "Select Gender" &&  spnrhouse.SelectedItem.ToString() != "Select Relation To HouseHold Owner" && spnbirthcert.SelectedItem.ToString() != "Birth Certificate Copy" && spnmarital.SelectedItem.ToString() != "Select Marital Status" && spnschoolattendance.SelectedItem.ToString() != "Select School Attendance" && spnliteracy.SelectedItem.ToString() != "Select Literacy" && spnworkstatus.SelectedItem.ToString() !=  "Select Employment Status"  && spnschoolhigh.SelectedItem.ToString() != "Select Highest School Attend") 
			{

				var builder = new AlertDialog.Builder(this);
				builder.SetTitle ("Census");
				builder.SetMessage ("Are You Sure You Want To Save Member?");
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
			ConMember.AddMember (Intent.GetStringExtra("address_id").ToString(),txtfname.Text,txtmname.Text,txtlname.Text,txtexname.Text,spngender.SelectedItem.ToString(),txtbday.Text,spnbirthcert.SelectedItem.ToString(),spnmarital.SelectedItem.ToString(),txtreligion.Text,spnschoolattendance.SelectedItem.ToString(),spnliteracy.SelectedItem.ToString(),spnschoolhigh.SelectedItem.ToString(),txtoccupation.Text,spnrhouse.SelectedItem.ToString(),spnworkstatus.SelectedItem.ToString());

			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Saving Address Successful");
			builder.SetPositiveButton ("Ok", Save_success);
				//delegate {
				//var intent = new Intent ();
				//intent.SetClass (this, typeof(ActMember));
				//StartActivity (intent);
			//});
			builder.Show ();
		}
		private void Save_success(object sender, DialogClickEventArgs args)
		{
			var intent = new Intent ();
			intent.PutExtra("address_id",Intent.GetStringExtra ("address_id"));
			intent.PutExtra("fulladdress",Intent.GetStringExtra ("fulladdress"));
			intent.SetClass(this,typeof(ActMember));
			StartActivity(intent);
		}

	}
}

