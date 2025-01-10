using Microsoft.AspNetCore.Mvc;

namespace AdoDotNetProject.ViewComponents.LayoutComponents
{
	public class ScriptViewComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
