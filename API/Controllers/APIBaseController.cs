using API.data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ApiBaseController : ControllerBase
    {
        protected readonly DataContext DataContext;

        public ApiBaseController(DataContext dataContext)
        {
            DataContext = dataContext;
        }
        
    }
}