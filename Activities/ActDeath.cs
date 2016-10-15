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

using Test.Adapter;
using Test.Connection;

namespace Test.Activities
{
	[Activity (Label = "ActMember",Theme = "@android:style/Theme.Holo.Light.NoActionBar")]			
	public class ActDeath : Activity
	{
		private EditText txtsearch,txtfulladdress;
		private ListView lvlist;
		private Button btnAddmember,btnSync;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayDeathList);

			lvlist = FindViewById<ListView>(Resource.Id.lvlist);
			lvlist.ItemClick += new System.EventHandler<AdapterView.ItemClickEventArgs>(lvlist_ItemClicked);

			txtfulladdress = FindViewById<EditText>(Resource.Id.txtfulladdress);
			txtfulladdress.Text = Intent.GetStringExtra ("fulladdress");

			txtsearch = FindViewById<EditText>(Resource.Id.txtsearch);
			txtsearch.AfterTextChanged += delegate 
			{
				refreshItems();
			};

			btnSync = FindViewById<Button> (Resource.Id.btnSync);
			btnSync.Click += delegate {
				//ApiRPolist();
			};

			btnAddmember = FindViewById<Button> (Resource.Id.btnAddmember);
			btnAddmember.Click += delegate {
				var intent = new Intent ();
				intent.PutExtra("address_id",Intent.GetStringExtra ("address_id"));
				intent.PutExtra("fulladdress",txtfulladdress.Text);
				intent.SetClass (this, typeof(ActDeathAdd));
				StartActivity (intent);
			};

			refreshItems ();
		}

		public void refreshItems()
		{
			var items = ConDeath.GetList(Intent.GetStringExtra ("address_id"),txtsearch.Text);
			lvlist.Adapter = new AdpDeathList(this, items);
		}

		protected override void OnResume()
		{
			base.OnResume ();
			refreshItems ();
		}

		public override void OnBackPressed()
		{
			var intent = new Intent ();
			intent.SetClass (this, typeof(ActAddress));
			StartActivity (intent);
		}

		private void lvlist_ItemClicked(object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = ((AdpDeathList)lvlist.Adapter).GetItemDetail(e.Position);
			var intent = new Intent();
			intent.PutExtra("death_id",item.id.ToString());
			intent.PutExtra ("fullname", item.fname + ", " + item.lname + ", " + item.ename);
			intent.PutExtra("address_id",Intent.GetStringExtra ("address_id"));
			intent.PutExtra("fulladdress",txtfulladdress.Text);

			var builder1 = new AlertDialog.Builder(this);
			builder1.SetTitle ("Census");
			builder1.SetMessage (item.fname + ", " + item.lname + ", " + item.ename);
			builder1.SetPositiveButton("View",delegate 
				{ 
					intent.SetClass (this, typeof(ActDeathView));
					StartActivity (intent);
				});
			builder1.SetNeutralButton("Edit",delegate
				{ 
					intent.SetClass (this, typeof(ActDeathEdit));
					StartActivity (intent);
				});
			builder1.SetNegativeButton("Delete",delegate
				{
					var builderdel = new AlertDialog.Builder(this);
					builderdel.SetTitle ("Census");
					builderdel.SetMessage ("Are You Sure You Want To Delete Selected Death Information?\n"+ item.fname + ", " + item.lname + ", " + item.ename);
					builderdel.SetPositiveButton("Delete", delegate 
						{
							ConDeath.DeleteList(item.id);
							var builder2 = new AlertDialog.Builder(this);
							builder2.SetTitle ("Census");
							builder2.SetTitle("Death Information Successfully Delete");
							builder2.SetPositiveButton("Ok", delegate {
								refreshItems();
							});
							builder2.Show ();
						});
					builderdel.SetNegativeButton("Cancel",delegate {builderdel.Dispose();});
					builderdel.Show();
				});

			builder1.Show ();
			
		}
			
		/*
		private void Delete_Address(object sender, DialogClickEventArgs args)
		{
			ConAddress.AddAddressList (txtProvince.Text, txtCity.Text, txtBarangay.Text, txtNumber.Text, txtFname.Text, txtLname.Text, txtContact.Text);

			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Saving Address Successfully");
			builder.SetPositiveButton("Ok",delegate {
				var intent = new Intent ();
				intent.SetClass (this, typeof(ActAddress));
				StartActivity (intent);
			});
			builder.Show ();
		}
		*/
	}
}

