
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Test.Models;
using Test.Connection;
using Test.Activities;
using Test.ApiConnection;
namespace Test.Activities
{
	//[Activity (Label = "Census",MainLauncher=true,Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
	[Activity (Label = "Census",Theme = "@android:style/Theme.Holo.Light.NoActionBar",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class ActLogin : Activity
	{
		private Button btnlogin;
		private EditText txtuser,txtpass;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.LayLogin);
			CopyExistingDB ();
			var url=ConAddress.GetConnectionURL ();
			GlobalVariables.GlobalUrl = url.url;
			txtuser = FindViewById<EditText> (Resource.Id.txtusername);
			txtpass = FindViewById<EditText> (Resource.Id.txtpassword);


			btnlogin= FindViewById<Button>(Resource.Id.btnlogin);
			btnlogin.Click+= new EventHandler(btnlogin_click);

			var users = ConAddress.GetUser();
			if(users.username != "" )
			{
				GlobalVariables.GlobalUserid=users.user_id;
				GlobalVariables.GlobalFname=users.fname;
				GlobalVariables.GlobalLname=users.lname;
				var intent = new Intent ();
				intent.SetClass (this, typeof(ActAddress));
				Toast.MakeText (this, "Login Successfully\n"+ users.fname +" "+ users.lname +".", ToastLength.Long).Show ();
				StartActivity (intent);
			} 

		}


		private async void btnlogin_click(object sender, EventArgs e)
		{	
			var url=ConAddress.GetConnectionURL ();
			GlobalVariables.GlobalUrl = url.url;

			if (txtuser.Text != "" && txtpass.Text != "") {
				var progressDialog = ProgressDialog.Show (this, "Please wait...", "Verifying Credentials...", true);
				try {

					await (ApiConnection1.UserLogin (GlobalVariables.GlobalUrl +"/login/"+ txtuser.Text	+"/"+ txtpass.Text));
					var users = ConAddress.GetUser();
					if(users.username != "")
					{
						GlobalVariables.GlobalUserid=users.user_id;
						GlobalVariables.GlobalFname=users.fname;
						GlobalVariables.GlobalLname=users.lname;
						var intent = new Intent ();
						intent.SetClass (this, typeof(ActAddress));
						Toast.MakeText (this, "Login Successfully\n"+ users.fname +" "+ users.lname +".", ToastLength.Long).Show ();
						StartActivity (intent);
					} 
					else 
					{
						Toast.MakeText (this, "Login Failed\nIncorect UserName Or Password..", ToastLength.Long).Show ();
					}
				} catch (Exception ex) {
					Toast.MakeText (this, "Unable To Connect To Server.\n" + ex.Message, ToastLength.Long).Show ();
				}
				progressDialog.Cancel ();
			} 
			else 
			{
				Toast.MakeText (this, "Unable To Login.\nPlease Complete Your Credential..", ToastLength.Long).Show ();
			}
			txtpass.Text = "";
		
		}
		private void CopyExistingDB()
		{
			string dbName = "Census.db";
			string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

			if (!File.Exists(dbPath))
			{
				using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, len);
						}
						bw.Flush();
						bw.Close();
					}
				}
			}

		}

		public override void OnBackPressed ()
		{
			this.FinishAffinity();
		}


		public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
		{
			if (keyCode == Keycode.Menu) {
				var inputView = LayoutInflater.Inflate(Resource.Layout.LayInputURL, null);
				var builder = new AlertDialog.Builder(this);
				builder.SetTitle("Change Server Address");
				builder.SetMessage("Server Address: "+ GlobalVariables.GlobalUrl);
				builder.SetView(inputView);
				builder.SetPositiveButton("Change", OkDialog_Clicked);
				builder.SetNegativeButton("Cancel", delegate { builder.Dispose(); });
				builder.Show();
			}
			return base.OnKeyUp (keyCode, e);
		}

		private void OkDialog_Clicked(object sender, DialogClickEventArgs args)
		{
			tblConnection connection = new tblConnection();
			var dialog = (AlertDialog)sender;
			var txtnewurl = (EditText)dialog.FindViewById (Resource.Id.txturl);
			connection.id=1;
			connection.url=txtnewurl.Text;
			ConAddress.UpdateConnectionURL(connection);
			var url=ConAddress.GetConnectionURL ();
			GlobalVariables.GlobalUrl = url.url;
		}
			
	}
}

