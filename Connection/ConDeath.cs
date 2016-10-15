
using System;
using Test.Models;
using SQLite;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;

namespace Test.Connection
{
	public class ConDeath
	{

		public ConDeath ()
		{
		}

		public static List<tblDeath> GetList(string address_id,string fullname)
		{
			using (var database = Database.Connection()) 
			{
				return database.Query<tblDeath> ("select * from tblDeath " +
					"where address_id='"+ address_id +"' and fname || ' ' || lname || ' ' || ename like '%"+ fullname +"%'").ToList ();
			}
		}
		public static long AddDeath(string address_id, string fname, string lname,string ename, string gender, string age_death,string register,string birthcertificate)
		{			
			using (var database = Database.Connection()) 
			{
				return database.Insert
					(new tblDeath
						{
							address_id = address_id,
							fname = fname,
							lname = lname,
							ename = ename,
							gender = gender,
							age_death = age_death,
							register = register,
							birthcertificate=birthcertificate,
						}
					);
			}
		}

		public static tblDeath GetListSingle (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblDeath>(query).FirstOrDefault ();
			}
		}

		public static void DeleteDeathQuery(string query)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblDeath>(query);
			}
		}

		public static tblDeath[] GetListArray1 (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblDeath>(query ).ToArray ();
			}
		}

		public static long UpdateList(tblDeath item)
		{
			using (var database = Database.Connection ())
			{
				database.Update(item);
				return item.id;
			}
		}

		public static void DeleteList(long id)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblDeath>("delete from tblDeath where id='"+ id +"'");
			}
		}

	}
}