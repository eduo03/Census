
using System;
using Test.Models;
using SQLite;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;

namespace Test.Connection
{
	public class ConMember
	{

		public ConMember ()
		{
		}

		public static List<tblmember> GetList(string address_id,string fullname)
		{
			using (var database = Database.Connection()) 
			{
				return database.Query<tblmember> ("select * from tblmember " +
					"where address_id='"+ address_id +"' and fname || ' ' || mname  || ' ' || lname || ' ' || ename like '%"+ fullname +"%'").ToList ();
			}
		}


		public static long AddMember(string address_id, string fname, string mname, string lname,string ename, string gender, string bday,string birthcertificate,string marital_status, string religion, string school,string literacy,string highestgrade, string occupation, string rhousehold, string work_status)
		{
			using (var database = Database.Connection()) 
			{
				return database.Insert
					(new tblmember
						{
							address_id = address_id,
							fname = fname,
							mname = mname,
							lname = lname,
							ename = ename,
							rhousehold = rhousehold,
							gender = gender,
							birthday = bday,
							birth_certificate=birthcertificate,
							marital_status=marital_status,
							religion=religion,
							school_attendance=highestgrade,
							literacy=literacy,
							highest_grade=school,
							occupation=occupation,
							work_status=work_status,

						}
					);
			}
		}

		public static tblmember[] GetListArray1 (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblmember>(query ).ToArray ();
			}
		}

		public static tblmember GetListSingle (string query)
		{
			using (var database = Database.Connection ())
			{
				return database
					.Query<tblmember>(query).FirstOrDefault ();
			}
		}


		public static long UpdateMemberList(tblmember item)
		{
			using (var database = Database.Connection ())
			{
				database.Update(item);
				return item.id;
			}
		}


		public static void DeleteMemberQuery(string query)
		{
			using (var database = Database.Connection ())
			{
				database.Query<tblmember>(query);
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