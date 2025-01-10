using Microsoft.AspNetCore.Mvc;

namespace AdoDotNetProject.ViewComponents.LayoutComponents
{
	public class FooterViewComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
