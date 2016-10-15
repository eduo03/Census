using System;
using System.IO;
using SQLite;
using Test.Connection;

namespace Test.Connection
{
	public class Database: SQLiteConnection 
	{
		protected static string dbLocation;

		public Database (string path): base (path)
		{
		}

		public static Database Connection ()
		{
			return new Database (DatabaseFilePath);
		}

		public static string DatabaseFilePath {
			get 
			{ 
				string result = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Census.db");
				return result;	
			}
		}
	}
}

