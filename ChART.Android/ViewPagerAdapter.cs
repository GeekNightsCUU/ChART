using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace ChART.Android
{
	public class ViewPagerAdapter : FragmentPagerAdapter
	{
		private readonly int Pages = 3;
		private MapsFragment mapFragment;
		private FAQSFragment faqsFragment;
		private AboutFragment aboutFragment;

		public ViewPagerAdapter(FragmentManager fm): base(fm)
		{
			mapFragment = new MapsFragment ();
			faqsFragment = new FAQSFragment ();
			aboutFragment = new AboutFragment ();
		}

		public override Fragment GetItem (int position)
		{
			switch (position) {
				case 0:
					return mapFragment;
				case 1:					
					return faqsFragment;
				case 2:					
					return aboutFragment;
			}
			return null;
		}

		public override int Count {
			get {
				return Pages;
			}
		}
	}
}

