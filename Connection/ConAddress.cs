
using System;
using Test.Models;
using SQLite;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;

namespace Test.Connection
{
	public class ConAddress
	{

		public ConAddress ()
		{
		}

		public static List<tblAddressList> GetList(string address)
		{
			tblAddressList itemlist = new tblAddressList ();
			using (var database = Database.Connection()) 
			{
				return database.Query<tblAddressList> ("select * from tblAddressList " +
					"where year='"+ GlobalVariables.GlobalYear +"' and building_no || ',' || barangay || ',' || city ||',' || province like '%"+ address +"%'").ToList ();
			}
		}

		public static tblAddressList GetListArray (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblAddressList>(query ).SingleOrDefault ();
			}
		}

		public static tblAddressList[] GetListArray1 (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblAddressList>(query ).ToArray ();
			}
		}

		public static long AddAddressList(string province, string city, string barangay,string numbers, string fname, string lname,string contact,string year)
		{
			using (var database = Database.Connection()) 
			{
				return database.Insert
					(new tblAddressList
						{
							province = province,
							city = city,
							barangay = barangay,
							building_no = numbers,
							fname = fname,
							lname = lname,
							contact = contact,
							year=year,
							status = "Open"

						}
					);
			}
		}

		public static long UpdateAddressList(tblAddressList item)
		{
			using (var database = Database.Connection ())
			{
				database.Update(item);
				return item.id;
			}
		}

		public static void GetListupdate (string query)
		{
			using (var database = Database.Connection ())
			{
			 database.Query<tblAddressList> (query);
			}
		}

		public static void DeleteAddressList(long id)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblAddressList>("delete from tblAddressList where id='"+ id +"'");
			}
		}

		public static void DeleteAddressQuery(string query)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblAddressList>(query);
			}
		}


		public static tblConnection GetConnectionURL()
		{
			using (var database = Database.Connection ()) 
			{
				return database
					.Table<tblConnection>().FirstOrDefault ();
			}
		}

		public static long UpdateConnectionURL(tblConnection item)
		{
			using (var database = Database.Connection ())
			{
				database.Update(item);
				return item.id;
			}
		}

		public static long UpdateUser(tblUser item)
		{
			using (var database = Database.Connection ())
			{
				database.Update(item);
				return item.id;
			}
		}

		public static tblUser GetUser ()
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblUser>("Select * from tblUser where id='1'").SingleOrDefault ();
			}
		}

	}
}