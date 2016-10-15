
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
	public class ActMember : Activity
	{
		private EditText txtsearch,txtfulladdress;
		private ListView lvlist;
		private Button btnAddmember,btnSync;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayNameList);

			lvlist = FindViewById<ListView>(Resource.Id.lvlist);
			lvlist.ItemClick += new System.EventHandler<AdapterView.ItemClickEventArgs>(lvlist_ItemClicked);
			//lvlist.ItemLongClick += new	System.EventHandler<AdapterView.ItemLongClickEventArgs>(lvlist_ItemLongClick);

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
				intent.SetClass (this, typeof(ActMemberAdd));
				StartActivity (intent);
			};

			refreshItems ();
		}

		public void refreshItems()
		{
			var items = ConMember.GetList(Intent.GetStringExtra ("address_id"),txtsearch.Text);
			lvlist.Adapter = new AdpMemberList(this, items);
		}

		protected override void OnResume()
		{
			base.OnResume();
			refreshItems();
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
				ConAddress.GetListupdate ("update tblAddressList set status='Open',sync='0' where id='" + Intent.GetStringExtra ("address_id") + "'");
			}
			var intent = new Intent ();
			intent.SetClass (this, typeof(ActAddress));
			StartActivity (intent);
		}

		private void lvlist_ItemClicked(object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = ((AdpMemberList)lvlist.Adapter).GetItemDetail(e.Position);
			var intent = new Intent();
			intent.PutExtra("member_id",item.id.ToString());
			intent.PutExtra ("fullname", item.fname + ", " + item.lname + ", " + item.ename);
			intent.PutExtra("address_id",Intent.GetStringExtra ("address_id"));
			intent.PutExtra("fulladdress",txtfulladdress.Text);

			var builder1 = new AlertDialog.Builder(this);
			builder1.SetTitle ("Census");
			builder1.SetMessage (item.fname + ", " + item.lname + ", " + item.ename);
			builder1.SetPositiveButton("View",delegate 
				{ 
					intent.SetClass (this, typeof(ActMemberView));
					StartActivity (intent);
				});
			builder1.SetNeutralButton("Edit",delegate
				{ 
					//intent.PutExtra("id",item.id.ToString());
					//intent.PutExtra ("fullname", item.fname + ", " + item.lname + ", " + item.ename);
					intent.SetClass (this, typeof(ActMemberEdit));
					StartActivity (intent);
				});
			builder1.SetNegativeButton("Delete",delegate
				{
					var builderdel = new AlertDialog.Builder(this);
					builderdel.SetTitle ("Census");
					builderdel.SetMessage ("Are You Sure You Want To Delete Selected Member?\n"+ item.fname + ", " + item.lname + ", " + item.ename);
					builderdel.SetPositiveButton("Delete", delegate 
						{
							ConMember.DeleteMemberList(item.id);
							var builder2 = new AlertDialog.Builder(this);
							builder2.SetTitle ("Census");
							builder2.SetTitle("Member Successfully Delete");
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
		private void lvlist_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			var item = ((AdpAddressList)lvlist.Adapter).GetItemDetail(e.Position);
			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage ("Are You Sure You Want To Delete Address?");
			builder.SetPositiveButton("Yes", delegate {
				ConAddress.DeleteAddressList(item.id);
				var builder1 = new AlertDialog.Builder(this);
				builder1.SetTitle ("Census");
				builder1.SetMessage ("Deleting Address Successful");
				builder1.SetPositiveButton("Ok", delegate {
					refreshItems();
				});
				builder1.Show ();
			});
			builder.SetNegativeButton("Cancel",delegate { builder.Dispose(); });
			builder.Show ();
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

