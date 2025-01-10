using Microsoft.AspNetCore.Mvc;

namespace AdoDotNetProject.ViewComponents.LayoutComponents
{
	public class NavbarViewComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
