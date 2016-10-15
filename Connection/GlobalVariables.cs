using System;
using Android.App;
using Android.Runtime;
using Test.Connection;

namespace Test.Connection
{


	/*
	public class WMSApplication : Application
	{
		public IItemRepository ItemRepository { get; set; }

		public WMSApplication(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();
			ItemRepository = new OrmItemRepository(this);
		}
	}
	*/
	public class GlobalVariables
	{
		public static string GlobalUrl = "";
		public static string GlobalUserid = "";
		public static string GlobalFname = "Inter";
		public static string GlobalLname = "Viewer";
		public static string GlobalYear = "2016";
		public static string GlobalAddressId = "2016";
	}
}