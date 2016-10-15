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
	public class AdpAddressList : BaseAdapter
	{
		private List<tblAddressList> _items;
		private Activity _context;

		public AdpAddressList(Activity context, List<tblAddressList> items)
		{
			_context = context;
			_items = items;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			LinearLayout view;
			tblAddressList item;
			CheckBox chkbox;
			item = _items.ElementAt (position);

			view = (convertView
				?? _context.LayoutInflater.Inflate (Resource.Layout.LayAddressLists, parent, false)
			) as LinearLayout;

			view.FindViewById<TextView>(Resource.Id.lvladdress ).Text = item.building_no +", "+ item.barangay +", "+  item.city +", "+  item.province+".";
			view.FindViewById<TextView>(Resource.Id.lvlfullname ).Text = item.fname +" "+item.lname ;
			view.FindViewById<TextView>(Resource.Id.lvlstatus).Text = item.status;
			chkbox = view.FindViewById<CheckBox> (Resource.Id.checkBox1);
			if (item.status != "Done") 
			{
				chkbox.Visibility = ViewStates.Invisible;
			}
			if (item.sync == "1") {
				chkbox.Checked = true;
			} 
			chkbox.CheckedChange += (object sender, CompoundButton.CheckedChangeEventArgs e) => 
			{

				if(e.IsChecked.Equals(true))
				{
					ConAddress.GetListupdate ("update tblAddressList set sync='1' where id='" + item.id + "'");
				}
				else
				{
					ConAddress.GetListupdate ("update tblAddressList set sync='0' where id='" + item.id + "'");
				}
			};

			return view;
		}

		public override int Count
		{
			get 
			{ 
				return _items.Count();
			}
		}

		public tblAddressList GetItemDetail(int position)
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