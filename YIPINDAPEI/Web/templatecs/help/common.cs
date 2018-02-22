using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.templatecs.Help
{
    public class common:Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
        }

        /// <summary>
        /// 验证当前页面是否被选中，1：关于我们，2：报道红人，3：入驻红人爱品，4：联系我们，5：招募自媒体人，6：红人微吧招募兴趣吧吧主
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cur">1：关于我们，2：报道红人，3：入驻红人爱品，4：联系我们，5：招募自媒体人，6：红人微吧招募兴趣吧吧主</param>
        public void Init_Help(NVelocity.VelocityContext context, int cur)
        {
            context.Put("curi",cur);
        }
    }
}