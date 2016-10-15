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
	public class CustomSpinnerAdapter : BaseAdapter
	{

		private Activity _context;
		private List<tblAddressList> _items;
		public CustomSpinnerAdapter(Activity context, List<tblAddressList> listOfItems)
		{
			_context = context;
			_items = listOfItems;
		}

		public override int Count
		{
			get { return _items.Count; }
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return position;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = _items[position];
			var view = (convertView ?? _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem,
				parent,
				false));
			var name = view.FindViewById<TextView>(Android.Resource.Id.Text1);

			name.Text = item.barangay;
			return view;
		}

		public tblAddressList GetItemAtPosition(int position)
		{
			return _items[position];
		}

	}
}