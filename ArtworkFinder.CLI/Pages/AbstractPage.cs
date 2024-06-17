using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkFinder.CLI.Pages
{
	internal abstract class AbstractPage : IPage
	{
		protected IPageHandler PageHandler;
        protected AbstractPage(IPageHandler pageHandler)
        {
            PageHandler = pageHandler;
        }
        public abstract void Display();
		protected void SetPage(IPage page)
		{
			PageHandler.SetPage(page);
		}
	}
}
