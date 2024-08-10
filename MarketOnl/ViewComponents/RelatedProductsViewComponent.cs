using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MarketOnl.ViewComponents
{
	public class RelatedProductsViewComponent : ViewComponent
	{

		private readonly BanHangOnlContext _context;

		public RelatedProductsViewComponent(BanHangOnlContext context)
		{
			_context = context;

		}

		public IViewComponentResult Invoke()
		{
			var relatedPro = _context.Products.Take(9).ToList();
			return View(relatedPro);
		}


	}
}
