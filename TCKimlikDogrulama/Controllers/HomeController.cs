using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Mernis.KPSPublicSoapClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using TCKimlikDogrulama.Models;
using Mernis;

namespace TCKimlikDogrulama.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(TCModel model)
        {
            bool? durum = false;

            try
            {

                var mernisClient = new KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
                var tcKimlikDogrulamaServisResponse = mernisClient.TCKimlikNoDogrulaAsync(model.TCKimlikNo, model.Ad, model.Soyad, model.DogumYili.Year).GetAwaiter().GetResult();
                durum = tcKimlikDogrulamaServisResponse.Body.TCKimlikNoDogrulaResult;
                TempData["durum"] = durum;
                return View();
               
            }
            catch (Exception)
            {

                throw;
            }
        }

    
    }
}
