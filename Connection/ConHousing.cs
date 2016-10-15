
using System;
using Test.Models;
using SQLite;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;

namespace Test.Connection
{
	public class ConHousing
	{

		public ConHousing ()
		{
		}

		public static List<tblmember> GetList(string address_id,string fullname)
		{
			using (var database = Database.Connection()) 
			{
				return database.Query<tblmember> ("select * from tblmember " +
					"where address_id='"+ address_id +"' and fname || ' ' || lname || ' ' || ename like '%"+ fullname +"%'").ToList ();
			}
		}
		public static long AddHousing(string address_id, string building, string wall,string roof, string light, string drink,string cook,string tenure)
		{
			using (var database = Database.Connection()) 
			{
				return database.Insert
					(new tblHousing
						{
							address_id = address_id,
							building = building,
							wall = wall,
							roof = roof,
							light = light,
							drink = drink,
							cooking = cook,
							tenure=tenure,
						}
					);
			}
		}

		public static tblHousing[] GetListArray1 (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblHousing>(query ).ToArray ();
			}
		}

		public static void DeleteHousingQuery(string query)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblHousing>(query);
			}
		}


		public static tblHousing GetListSingle (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblHousing>(query).FirstOrDefault ();
			}
		}

		public static long UpdateHousing(tblHousing item)
		{
			using (var database = Database.Connection ())
			{
				database.Update(item);
				return item.id;
			}
		}

		public static void DeleteMemberList(long id)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblmember>("delete from tblmember where id='"+ id +"'");
			}
		}

	}
}