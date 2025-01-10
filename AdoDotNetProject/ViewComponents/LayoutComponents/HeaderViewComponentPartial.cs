using Microsoft.AspNetCore.Mvc;

namespace AdoDotNetProject.ViewComponents.LayoutComponents
{
	public class HeaderViewComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
