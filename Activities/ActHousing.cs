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
	public class ActHousing : Activity
	{
		private Button btnAddaddress;
		private Spinner spnbuilding, spnroof, spnwall, spnlight, spndrink, spncook, spntenure;
		private EditText txtfulladdress;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayHousing);

			spnbuilding= FindViewById<Spinner> (Resource.Id.spnbuilding);
			spnroof= FindViewById<Spinner> (Resource.Id.spnroof);
			spnwall= FindViewById<Spinner> (Resource.Id.spnwall);
			spnlight= FindViewById<Spinner> (Resource.Id.spnlight);
			spndrink= FindViewById<Spinner> (Resource.Id.spndrink);
			spncook= FindViewById<Spinner> (Resource.Id.spncook);
			spntenure= FindViewById<Spinner> (Resource.Id.spntenure);

			btnAddaddress = FindViewById<Button> (Resource.Id.btnAddAddress);

			txtfulladdress = FindViewById<EditText> (Resource.Id.txtfulladdress);

			txtfulladdress.Text = Intent.GetStringExtra ("fulladdress");

			btnAddaddress.Click += new EventHandler (btnAddaddress_Clicked);

			var adapterbuilding = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Building_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adapterbuilding.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnbuilding.Adapter = adapterbuilding;

			var adapterroof = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Roof_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adapterroof.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnroof.Adapter = adapterroof;

			var adpwall = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Wall_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpwall.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnwall.Adapter = adpwall;

			var adplight = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Light_Spinner , Android.Resource.Layout.SimpleSpinnerItem);
			adplight.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spnlight.Adapter = adplight;

			var adpdrink = ArrayAdapter.CreateFromResource(
				this, Resource.Array.DRINK_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpdrink.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spndrink.Adapter = adpdrink;

			var adpcook = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Cook_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adpcook.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spncook.Adapter = adpcook;

			var adptenure = ArrayAdapter.CreateFromResource(
				this, Resource.Array.Tenure_Spinner, Android.Resource.Layout.SimpleSpinnerItem);
			adptenure.SetDropDownViewResource(Android.Resource.Layout.SimpleListItemActivated1);
			spntenure.Adapter = adptenure;


			string[] array;
			var items=ConHousing.GetListSingle ("select * from tblHousing where address_id='"+ Intent.GetStringExtra("address_id") +"'");
			if (items != null) 
			{
				array = Resources.GetStringArray (Resource.Array.Building_Spinner);
				fcnspinner (spnbuilding, array, items.building);
				array = Resources.GetStringArray (Resource.Array.Roof_Spinner);
				fcnspinner (spnroof, array, items.roof);
				array = Resources.GetStringArray (Resource.Array.Wall_Spinner);
				fcnspinner (spnwall, array, items.wall);
				array = Resources.GetStringArray (Resource.Array.Light_Spinner);
				fcnspinner (spnlight, array, items.light);
				array = Resources.GetStringArray (Resource.Array.DRINK_Spinner);
				fcnspinner (spndrink, array, items.drink);
				array = Resources.GetStringArray (Resource.Array.Cook_Spinner);
				fcnspinner (spncook, array, items.cooking);
				array = Resources.GetStringArray (Resource.Array.Tenure_Spinner);
				fcnspinner (spntenure, array, items.tenure);
			}
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
			
		public override void OnBackPressed()
		{
			var member = ConMember.GetListSingle ("select * from tblmember where address_id='" + Intent.GetStringExtra ("address_id") + "'");
			var house = ConHousing.GetListSingle ("select * from tblHousing where address_id='" + Intent.GetStringExtra ("address_id") + "'");
			if (member != null && house != null) 
			{
				ConAddress.GetListupdate ("update tblAddressList set status='Done' where id='" + Intent.GetStringExtra ("address_id") + "'");
			} 
			else
			{
				ConAddress.GetListupdate ("update tblAddressList set status='Open' where id='" + Intent.GetStringExtra ("address_id") + "'");
			}
			var intent = new Intent ();
			intent.SetClass (this, typeof(ActAddress));
			StartActivity (intent);
		}

		private void btnAddaddress_Clicked(object sender, EventArgs e)
		{ 	
			if (spnbuilding.SelectedItem.ToString() != "Select Building Type" &&  spnroof.SelectedItem.ToString() != "Select Roof Type" &&  spnwall.SelectedItem.ToString() != "Select Wall Type" &&  spnlight.SelectedItem.ToString() != "Select Source Of Light" &&  spndrink.SelectedItem.ToString() != "Select Source Of Drink" &&  spncook.SelectedItem.ToString() != "Select Use For Cooking" &&  spntenure.SelectedItem.ToString() != "Select Tenure Status")																																																																																		
			{
				var builder = new AlertDialog.Builder(this);
				builder.SetTitle ("Census");
				var items=ConHousing.GetListSingle ("select * from tblHousing where address_id='"+ Intent.GetStringExtra("address_id") +"'");
				if (items == null) 
				{
					builder.SetMessage ("Are You Sure You Want To Save Housing Question?");
					builder.SetPositiveButton ("Yes", Save_Address);
				}
				else 
				{
					builder.SetMessage ("Are You Sure You Want To Update Housing Question?");
					builder.SetPositiveButton ("Yes", delegate 
						{
							tblHousing updatehousing= new tblHousing();
							updatehousing.id=items.id;
							updatehousing.address_id=Intent.GetStringExtra("address_id");
							updatehousing.building=spnbuilding.SelectedItem.ToString();
							updatehousing.roof=spnroof.SelectedItem.ToString();
							updatehousing.wall=spnwall.SelectedItem.ToString();
							updatehousing.light=spnlight.SelectedItem.ToString();
							updatehousing.drink=spndrink.SelectedItem.ToString();
							updatehousing.cooking=spncook.SelectedItem.ToString();
							updatehousing.tenure=spntenure.SelectedItem.ToString();
							ConHousing.UpdateHousing(updatehousing);
							Save_Address1();
						});
				}
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
			ConHousing.AddHousing (Intent.GetStringExtra ("address_id").ToString (), spnbuilding.SelectedItem.ToString (), spnwall.SelectedItem.ToString (), spnroof.SelectedItem.ToString (), spnlight.SelectedItem.ToString (), spndrink.SelectedItem.ToString (), spncook.SelectedItem.ToString (), spntenure.SelectedItem.ToString ());
			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Saving Housing Question Successful");
			builder.SetPositiveButton ("Ok", Save_success);
			builder.SetCancelable(false);
			builder.Show ();
		}

		private void Save_Address1()
		{
			ConHousing.AddHousing (Intent.GetStringExtra ("address_id").ToString (), spnbuilding.SelectedItem.ToString (), spnwall.SelectedItem.ToString (), spnroof.SelectedItem.ToString (), spnlight.SelectedItem.ToString (), spndrink.SelectedItem.ToString (), spncook.SelectedItem.ToString (), spntenure.SelectedItem.ToString ());
			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Updating Housing Question Successful");
			builder.SetPositiveButton ("Ok", Save_success);
			builder.SetCancelable(false);
			builder.Show ();
		}

		private void Save_success(object sender, DialogClickEventArgs args)
		{
			var member = ConMember.GetListSingle ("select * from tblmember where address_id='" + Intent.GetStringExtra ("address_id") + "'");
			var house = ConHousing.GetListSingle ("select * from tblHousing where address_id='" + Intent.GetStringExtra ("address_id") + "'");
			if (member != null && house != null) 
			{
				ConAddress.GetListupdate ("update tblAddressList set status='Done' where id='" + Intent.GetStringExtra ("address_id") + "'");
			} 
			else
			{
				ConAddress.GetListupdate ("update tblAddressList set status='Open',sync='0' where id='" + Intent.GetStringExtra ("address_id") + "'");
			}
			var intent = new Intent ();
			intent.PutExtra("address_id",Intent.GetStringExtra ("address_id"));
			intent.PutExtra("fulladdress",Intent.GetStringExtra ("fulladdress"));
			intent.SetClass(this,typeof(ActAddress));
			StartActivity(intent);
		}
	}
}

