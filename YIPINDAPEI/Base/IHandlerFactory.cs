using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity;

namespace Base
{
    public interface IHandlerFactory
    {
        void Page_Load(ref VelocityContext context);
        void Page_PostBack(ref VelocityContext context);
    }
}
