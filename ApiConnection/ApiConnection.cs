using System;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

using Android.Widget;
using Android.Graphics;
using Android.App;


using SQLite;

using Test.Activities;
using Test.Connection;
using Test.Models;
using Test;

namespace Test.ApiConnection
{
	public class ApiConnection1
	{
		tblUser user1 = new tblUser();

		public ApiConnection1 ()
		{
		
		}

		public static async Task<bool> UserLogin(string url)
		{
			bool result = false;
			var request = System.Net.WebRequest.Create(url) as HttpWebRequest;
			if (request != null)
			{
				request.Method = "GET";
				request.ServicePoint.Expect100Continue = false;
				request.Timeout = 20000;
				request.ContentType = "application/json";
				using (WebResponse response = await request.GetResponseAsync ()) 
				{
					using (Stream stream = response.GetResponseStream ()) 
					{
						string x = JsonObject.Load(stream).ToString();
						JsonObject jObj = (JsonObject)JsonObject.Parse(x);
						JsonArray jArr = (JsonArray)jObj["result"];
						foreach (var item in jArr)
						{
							tblUser user = new tblUser ();
							user.id = 1;
							user.user_id=Convert.ToString ((Int32)item ["number"]);
							user.fname=Convert.ToString ((string)item ["fname"]);
							user.lname=Convert.ToString ((string)item ["lname"]);
							user.username=Convert.ToString ((string)item ["user"]);
							user.password=Convert.ToString ((string)item ["password"]);
							ConAddress.UpdateUser (user);
						}
					}
				}
			}
			return result;
		}

		public static async Task<bool> AddressId(string url)
		{
			bool result = false;
			var request = System.Net.WebRequest.Create(url) as HttpWebRequest;
			if (request != null)
			{
				request.Method = "GET";
				request.ServicePoint.Expect100Continue = false;
				request.Timeout = 20000;
				request.ContentType = "application/json";
				using (WebResponse response = await request.GetResponseAsync ()) 
				{
					using (Stream stream = response.GetResponseStream ()) 
					{
						string x = JsonObject.Load(stream).ToString();
						JsonObject jObj = (JsonObject)JsonObject.Parse(x);
						JsonArray jArr = (JsonArray)jObj["result"];
						foreach (var item in jArr)
						{
							int address_id=(Int32)item ["addressid"]+1;
							GlobalVariables.GlobalAddressId = Convert.ToString (address_id);
						}
					}
				}
			}
			return result;
		}

		public static async Task<bool> Updatedata(string url)
		{
			bool result = false;
			var request = System.Net.WebRequest.Create(url) as HttpWebRequest;
			if (request != null)
			{
				request.Method = "GET";
				request.ServicePoint.Expect100Continue = false;
				request.Timeout = 20000;
				request.ContentType = "application/json";
				using (WebResponse response = await request.GetResponseAsync ()) 
				{
					/*
					using (Stream stream = response.GetResponseStream ()) 
					{

						string x = JsonObject.Load(stream).ToString();
						JsonObject jObj = (JsonObject)JsonObject.Parse(x);
						JsonArray jArr = (JsonArray)jObj["result"];
						foreach (var item in jArr)
						{

						}

					}*/
				}
			}
			return result;
		}

	}
}

