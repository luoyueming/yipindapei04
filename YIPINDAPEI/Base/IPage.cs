using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
    public interface IPage
    {
        void Page_Load(System.Web.HttpContext context);
    }
}
