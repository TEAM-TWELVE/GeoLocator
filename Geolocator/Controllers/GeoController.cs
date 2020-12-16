using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocator.Models;
using Geolocator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Geolocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private Logger _logger = new Logger("GeoController");
        public GeoController(IConfiguration configuration, IAddressService addressService)
        {
            Configuration = configuration;
            _addressService = addressService;
        }

        public IConfiguration Configuration { get; }
        public IAddressService _addressService { get; }

        // POST: api/Geo/
        [HttpGet]
        [Route("{address}")]
        public async Task<IActionResult> Get(string address)
        {
            _logger.Info("/address GET Get called.");
            var result = await _addressService.GetHotelCoordinate(address);
            _logger.Info("/address GET successfully finished.");
            return Ok(result);
        }

    }
}
