
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

using Test.ApiConnection;
using Test.Adapter;
using Test.Connection;
using Test.Models;
namespace Test.Activities
{
	[Activity (Label = "ActAddress",Theme = "@android:style/Theme.Holo.Light.NoActionBar")]			
	public class ActAddress : Activity
	{
		private EditText txtsearch;
		private ListView lvlist;
		private Button btnAddaddress,btnSync;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.LayAddress);

			var menuButton = FindViewById (Resource.Id.menuHdrButton);
			menuButton.Click += (sender, e) => {
				leftdrawer();
			};

			var MenuUserName= FindViewById<TextView>(Resource.Id.MenuUserName);
			MenuUserName.Text=" "+GlobalVariables.GlobalFname+" "+GlobalVariables.GlobalLname;

			var MenuInterviewer = FindViewById (Resource.Id.MenuInterviewer);
			MenuInterviewer.Click += (sender, e) => {leftdrawer ();};

			var MenuLogout = FindViewById (Resource.Id.MenuLogout);
			MenuLogout.Click += (sender, e) => 
			{
				tblUser user = new tblUser();
				GlobalVariables.GlobalUserid = "";
				user.id = 1;
				user.user_id = "";
				user.username = "";
				user.password= "";
				user.fname = "";
				user.lname = "";
				ConAddress.UpdateUser (user);

				var intent = new Intent ();
				intent.SetClass (this, typeof(ActLogin));
				StartActivity (intent);
			};

			lvlist = FindViewById<ListView>(Resource.Id.lvlist);
			lvlist.ItemClick += new System.EventHandler<AdapterView.ItemClickEventArgs>(lvlist_ItemClicked);
			lvlist.ItemLongClick += new	System.EventHandler<AdapterView.ItemLongClickEventArgs>(lvlist_ItemLongClick);


			txtsearch = FindViewById<EditText>(Resource.Id.txtsearch);
			txtsearch.AfterTextChanged += delegate 
			{
				refreshItems();
			};

			btnSync = FindViewById<Button> (Resource.Id.btnSync);
			btnSync.Click += delegate 
			{
				ApiRSTlist();
			};

			btnAddaddress = FindViewById<Button> (Resource.Id.btnAddaddress);
			btnAddaddress.Click += delegate {
				var intent = new Intent ();
				intent.SetClass (this, typeof(ActAddressAdd));
				StartActivity (intent);
			};

			refreshItems ();
		}

		public async void ApiRSTlist()
		{
			var progressDialog = ProgressDialog.Show(this, "Please wait...", "Updating Data From Server...", true);
			try
			{

				var address = ConAddress.GetListArray1("select * from tblAddressList where sync='1'");
				foreach(var x in address)
				{
					await (ApiConnection1.AddressId (GlobalVariables.GlobalUrl + "/addressid"));
					await (ApiConnection1.Updatedata (GlobalVariables.GlobalUrl + "/address/" + GlobalVariables.GlobalUserid + "/" + x.province + "/" + x.city + "/" + x.barangay + "/" + x.building_no + "/" + x.fname + "/" + x.lname + "/" + x.contact));
					var member = ConMember.GetListArray1("select * from tblmember where address_id='"+ x.id +"'");
					foreach(var m in member)
					{
						var ename=m.ename;
						if(m.ename=="") ename="000";
						await (ApiConnection1.Updatedata (GlobalVariables.GlobalUrl + "/member/" + GlobalVariables.GlobalAddressId + "/" + m.fname + "/" + m.mname + "/" + m.lname + "/" + ename + "/" + m.rhousehold + "/" + m.gender + "/" + m.birthday + "/" + m.birth_certificate + "/" + m.marital_status + "/" + m.religion + "/" + m.school_attendance + "/" + m.literacy + "/" + m.highest_grade + "/" + m.work_status + "/" + m.occupation));
					}
					var housing = ConHousing.GetListSingle("select * from tblHousing where address_id='"+ x.id +"'");
					await (ApiConnection1.Updatedata (GlobalVariables.GlobalUrl + "/housing/"+ GlobalVariables.GlobalAddressId +"/"+ housing.building +"/"+ housing.roof +"/"+ housing.wall +"/"+ housing.light +"/"+ housing.drink +"/"+ housing.cooking +"/"+ housing.tenure));

					var death = ConDeath.GetListArray1("select * from tblDeath where address_id='"+ x.id +"'");
					foreach(var d in death)
					{
						await (ApiConnection1.Updatedata (GlobalVariables.GlobalUrl + "/death/" + GlobalVariables.GlobalAddressId + "/" + d.fname + "/" + d.lname + "/" + d.gender + "/" + d.age_death + "/" + d.register + "/" + d.birthcertificate));
					}

					ConAddress.DeleteAddressQuery("delete from tblAddressList where id='"+ housing.address_id +"'");
					ConMember.DeleteMemberQuery("delete from tblmember where address_id='"+ housing.address_id +"'");
					ConHousing.DeleteHousingQuery("delete from tblHousing where address_id='"+ housing.address_id +"'");
					ConDeath.DeleteDeathQuery("delete from tblDeath where address_id='"+ housing.address_id +"'");

					Toast.MakeText (this, "Saving Successful..", ToastLength.Long).Show ();
					refreshItems();
				}
			}
			catch(Exception ex) 
			{
				Toast.MakeText (this,"Unable To Update Data.\n" + ex.Message, ToastLength.Long).Show ();
			}

			progressDialog.Cancel();
		}
		public void refreshItems()
		{
			var items = ConAddress.GetList(txtsearch.Text);
			lvlist.Adapter = new AdpAddressList(this, items);
		}

		public override void OnBackPressed()
		{
			leftdrawer ();
		}

		private void leftdrawer()
		{
			var menu = FindViewById<ActLeftDrawer> (Resource.Id.ActLeftDrawer);
			menu.AnimatedOpened = !menu.AnimatedOpened;
		}

		private void lvlist_ItemClicked(object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = ((AdpAddressList)lvlist.Adapter).GetItemDetail(e.Position);
			var intent = new Intent();
			intent.PutExtra("address_id",item.id.ToString());
			intent.PutExtra ("fulladdress", item.building_no + ", " + item.barangay + ", " + item.city + ", " + item.province);


			var builder1 = new AlertDialog.Builder(this);
			builder1.SetTitle ("Census");
			builder1.SetMessage (item.building_no + ", " + item.barangay + ", " + item.city + ", " + item.province);
			builder1.SetPositiveButton("Death",delegate 
				{ 
					intent.SetClass(this, typeof(ActDeath));
					StartActivity(intent);
				});
			builder1.SetNeutralButton("Housing",delegate 
				{
					intent.SetClass(this, typeof(ActHousing));
					StartActivity(intent);
				});
			builder1.SetNegativeButton("Household Population",delegate
				{
					intent.SetClass(this, typeof(ActMember));
					StartActivity(intent);
				});
			builder1.Show ();
		

		}

		private void lvlist_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			var item = ((AdpAddressList)lvlist.Adapter).GetItemDetail(e.Position);
			var builder = new AlertDialog.Builder(this);
			builder.SetTitle ("Census");
			builder.SetMessage (item.building_no + ", " + item.barangay + ", " + item.city + ", " + item.province);
			builder.SetPositiveButton("Edit", delegate {
				var intent = new Intent();
				intent.PutExtra("id",item.id.ToString());
				intent.SetClass(this, typeof(ActAddressEdit));
				StartActivity(intent);
			});
			builder.SetNegativeButton("Delete",delegate 
				{
					var builderdel = new AlertDialog.Builder(this);
					builderdel.SetTitle ("Census");
					builderdel.SetMessage ("Are You Sure You Want To Delete Selected Address?\n"+ item.building_no + ", " + item.barangay + ", " + item.city + ", " + item.province);
					builderdel.SetPositiveButton("Delete", delegate 
						{
							ConAddress.DeleteAddressList(item.id);
							var builder1 = new AlertDialog.Builder(this);
							builder1.SetTitle ("Census");
							builder1.SetTitle("Address Successfully Delete");
							builder1.SetPositiveButton("Ok", delegate {
								refreshItems();
							});
							builder1.Show ();
						});
					builderdel.SetNegativeButton("Cancel",delegate {builderdel.Dispose();});
					builderdel.Show();
				});
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

