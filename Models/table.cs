using SQLite;

namespace Test.Models
{
	public class tblConnection
	{
		[PrimaryKey, AutoIncrement]
		public long id { get; set; }
		public string url { get; set; }
	}

	public class tblAddressList
	{
		[PrimaryKey, AutoIncrement]
		public long id { get; set; }
		public string province { get; set; }
		public string city { get; set; }
		public string barangay { get; set; }
		public string building_no { get; set; }
		public string fname { get; set; }
		public string lname { get; set; }
		public string contact { get; set; }
		public string year { get; set; }
		public string status { get; set; }
		public string sync { get; set; }
	}

	public class tblmember
	{
		[PrimaryKey, AutoIncrement]
		public long id { get; set; }
		public string address_id { get; set; }
		public string fname { get; set; }
		public string mname { get; set; }
		public string lname { get; set; }
		public string ename { get; set; }
		public string rhousehold { get; set; }
		public string gender { get; set; }
		public string birthday { get; set; }
		public string birth_certificate { get; set; }
		public string marital_status { get; set; }
		public string religion { get; set; }
		public string school_attendance { get; set; }
		public string literacy { get; set; }
		public string highest_grade { get; set; }
		public string work_status { get; set; }
		public string occupation { get; set; }
	}

	public class tblHousing
	{
		[PrimaryKey, AutoIncrement]
		public long id { get; set; }
		public string address_id { get; set; }
		public string building { get; set; }
		public string roof { get; set; }
		public string wall { get; set; }
		public string light { get; set; }
		public string drink { get; set; }
		public string cooking { get; set; }
		public string tenure { get; set; }
	}

	public class tblDeath
	{
		[PrimaryKey, AutoIncrement]
		public long id { get; set; }
		public string address_id { get; set; }
		public string fname { get; set; }
		public string lname { get; set; }
		public string ename { get; set; }
		public string gender { get; set; }
		public string age_death { get; set; }
		public string register { get; set; }
		public string birthcertificate { get; set; }
	}

	public class tblUser
	{
		[PrimaryKey, AutoIncrement]
		public long id { get; set; }
		public string user_id { get; set; }
		public string fname { get; set; }
		public string lname { get; set; }
		public string username { get; set; }
		public string password { get; set; }
	}
}

