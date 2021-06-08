using Microsoft.AspNetCore.Mvc;

namespace ooohCar.Server.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        //private readonly IServerPreferenceManager _serverPreferenceManager;

        public PreferencesController(
            //IServerPreferenceManager serverPreferenceManager
            )
        {
            //_serverPreferenceManager = serverPreferenceManager;
        }

        //TODO - add actions
    }
}
