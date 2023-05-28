using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class TopicController : BaseController<Topic>
    {
        public TopicController(ITopicBL topicBL) : base(topicBL)
        {
        }
    }
}
