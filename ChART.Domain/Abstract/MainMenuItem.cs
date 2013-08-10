using System;

namespace ChART.Domain.iOS
{
	public enum MainMenuItem
	{
		Map,
		ClosestStation,
		FAQ,
		About,
		None
	}

	public static class MainMenuItemExtensions
	{
		public static MainMenuItem FromOrder(this MainMenuItem mainMenuItem, int order)
		{
			switch (order) 
			{
				case 0:
					return MainMenuItem.Map;
				case 1:
					return MainMenuItem.ClosestStation;
				case 2:
					return MainMenuItem.FAQ;
				case 3:
					return MainMenuItem.About;
				default:
					return MainMenuItem.About;
			}
		}

		public static string Title(this MainMenuItem mainMenuItem)
		{
			switch (mainMenuItem) 
			{
				case MainMenuItem.Map:
				return "Mapa ViveBus";
				case MainMenuItem.ClosestStation:
				return "Estación más Cercana";
				case MainMenuItem.FAQ:
				return "FAQS";
				case MainMenuItem.About:
				return "Acerca de";
				default:
				return "Acerca de";
			}
		}
	}
}

