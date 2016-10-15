
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
	[Activity (Label = "ActMemberView",Theme = "@android:style/Theme.Holo.Light.NoActionBar")]			
	public class ActDeathView : Activity
	{
		private EditText txtfname,txtlname,txtexname,txtgender,txtageatdeath,txtcopydeathcert,txtdeathreg;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.LayDeathView);


			txtfname = FindViewById<EditText> (Resource.Id.txtfname);
			txtlname= FindViewById<EditText> (Resource.Id.txtlname);
			txtexname= FindViewById<EditText> (Resource.Id.txtexname);
			txtgender = FindViewById<EditText> (Resource.Id.txtgender);
			txtageatdeath= FindViewById<EditText> (Resource.Id.txtageatdeath);
			txtcopydeathcert= FindViewById<EditText> (Resource.Id.txtcopydeathcert);
			txtdeathreg= FindViewById<EditText> (Resource.Id.txtdeathreg);

			var items=ConDeath.GetListSingle ("select * from tblDeath where id='"+ Intent.GetStringExtra("death_id") +"'");

			txtfname.Text = items.fname;
			txtlname.Text = items.lname;
			txtexname.Text = items.ename;
			txtageatdeath.Text = items.age_death;
			txtgender.Text = items.gender;
			txtcopydeathcert.Text = items.birthcertificate;
			txtdeathreg.Text = items.register;
		}
	}
}

