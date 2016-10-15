
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
	public class ActMemberView : Activity
	{
		private EditText txtfname,txtmname,txtlname,txtexname,txtbday,txtreligion,txtoccupation;
		private Button btnAddaddress;
		private EditText txtgender,txtrhouse,txtbirthcert,txtmarital,txtschoolhigh,txtschoolattendance,txtliteracy,txtworkstatus;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.LayNameView);


			txtfname = FindViewById<EditText> (Resource.Id.txtfname);
			txtmname= FindViewById<EditText> (Resource.Id.txtmname);
			txtlname= FindViewById<EditText> (Resource.Id.txtlname);
			txtexname= FindViewById<EditText> (Resource.Id.txtexname);
			txtbday= FindViewById<EditText> (Resource.Id.txtbday);
			txtreligion= FindViewById<EditText> (Resource.Id.txtreligion);
			txtoccupation= FindViewById<EditText> (Resource.Id.txtoccupation);
			txtgender = FindViewById<EditText> (Resource.Id.txtgender);
			txtrhouse= FindViewById<EditText> (Resource.Id.txtrhouse);
			txtbirthcert= FindViewById<EditText> (Resource.Id.txtbirthcert);
			txtmarital= FindViewById<EditText> (Resource.Id.txtmarital);
			txtschoolhigh= FindViewById<EditText> (Resource.Id.txtschoolhigh);
			txtschoolattendance= FindViewById<EditText> (Resource.Id.txtschoolattendance);
			txtliteracy= FindViewById<EditText> (Resource.Id.txtliteracy);
			txtworkstatus= FindViewById<EditText> (Resource.Id.txtworkstatus);

			var items=ConMember.GetListSingle ("select * from tblmember where id='"+ Intent.GetStringExtra("member_id") +"'");

			txtfname.Text = items.fname;
			txtmname.Text = items.mname;
			txtlname.Text = items.lname;
			txtexname.Text = items.ename;
			txtbday.Text = items.birthday;
			txtreligion.Text = items.religion;
			txtoccupation.Text = items.occupation;
			txtgender.Text = items.gender;
			txtrhouse.Text = items.rhousehold;
			txtbirthcert.Text = items.birth_certificate;
			txtmarital.Text = items.marital_status;
			txtschoolhigh.Text = items.highest_grade;
			txtschoolattendance.Text = items.school_attendance;
			txtliteracy.Text = items.literacy;
			txtworkstatus.Text = items.work_status;
		}
	}
}

