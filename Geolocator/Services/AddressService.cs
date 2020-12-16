using Geolocator.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Geolocator.Services
{
    public class AddressService : IAddressService
    {
        private Logger _logger = new Logger("AddressService");
        public IConfiguration Configuration { get; }

        public AddressService(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<Coordinates> GetHotelCoordinate(string address)
        {
            _logger.Info("getHotelCoordinate called.");
           
            string apiKey = Configuration["ExternalProviders:Google:ApiKey"];
            Coordinates coordinates = new Coordinates();

            
            using (HttpClient client = new HttpClient())
            {
                _logger.Info("Successfully entered using block with HttpClient initialization.");
                var response = await client.GetAsync(Configuration["ApiEndpoints:Google:GeoCode:LongLatDiscoverer"] + address + "&key=" + apiKey);
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info("Status code was " + response.StatusCode);
                    var content = await response.Content.ReadAsStringAsync();
                    coordinates = UnwrapCoordsFromJson(content);
                }
                else
                {
                    _logger.Error("Couldn't get response with address: " + address);
                    throw new HttpRequestException("Couldn't get response... StatusCode: " + response.StatusCode);
                }
            }

            _logger.Info("Exiting GetHotelCoordinate.");
            return coordinates;
        }

        private Coordinates UnwrapCoordsFromJson(string json)
        {
            _logger.Info("unwrapCoordsFromJson called.");
            JObject coords = JsonConvert.DeserializeObject<dynamic>(json);
            var lat = (double) coords.SelectToken("results[0].geometry.location.lat");
            var lng = (double) coords.SelectToken("results[0].geometry.location.lng");

            _logger.Info("Exiting unwrapCoordsFromJson.");
            return new Coordinates { Lat = lat, Long = lng };
        }
    }
}
