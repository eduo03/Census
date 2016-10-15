using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Test.Models;
using Test.Connection;
using Test;

namespace Test.Adapter
{
	public class AdpMemberList : BaseAdapter
	{
		private List<tblmember> _items;
		private Activity _context;

		public AdpMemberList(Activity context, List<tblmember> items)
		{
			_context = context;
			_items = items;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			LinearLayout view;
			tblmember item;
			item = _items.ElementAt (position);

			view = (convertView
				?? _context.LayoutInflater.Inflate (Resource.Layout.LayNameLists, parent, false)
			) as LinearLayout;

			view.FindViewById<TextView>(Resource.Id.lvlfullname ).Text = item.fname +" "+ item.mname +". "+ item.lname +" "+  item.ename +".";
			view.FindViewById<TextView>(Resource.Id.lvlageatdeath ).Text = item.rhousehold;
			view.FindViewById<TextView>(Resource.Id.lvlgender).Text = item.gender;

			return view;
		}

		public override int Count
		{
			get 
			{ 
				return _items.Count();
			}
		}

		public tblmember GetItemDetail(int position)
		{
			return _items.ElementAt (position);
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;

		}

		public override long GetItemId(int position)
		{
			return position;
		}
	}
}