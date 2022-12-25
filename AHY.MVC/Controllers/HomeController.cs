using AHY.MVC.Models;
using AHY.MVC.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AHY.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(@"http://localhost:5115/api/products/GetProducts");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewBag.ResponseMessage = "Başarılı";
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ProductResponseModel>>(jsonData);
                return View(list);
            }
            else
            {
                ViewBag.ResponseMessage = "Başarısız";
            }

            return View();
        }

        public IActionResult Create()
        {
            return View(new ProductCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent Hcontent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(@"http://localhost:5115/api/products",Hcontent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu {(int)responseMessage.StatusCode}";
                return View(model);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5115/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create","Home");
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5115/api/Products/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData =await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ProductResponseModel>(jsonData);
                return View(data);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductResponseModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent hContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5115/api/products",hContent);

            if (responseMessage.StatusCode==System.Net.HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["errorMessage"] = $"Bir hata ile karşılaşıldı. Hata kodu {(int)responseMessage.StatusCode}";

            return View(model);
        }

        [HttpGet]
        public  IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile formFile)
        {
            var client = _httpClientFactory.CreateClient();

            var stream = new MemoryStream();
            formFile.CopyTo(stream);
            var bytes = stream.ToArray();
            ByteArrayContent content = new(bytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(formFile.ContentType);
            MultipartFormDataContent formData = new();
         
            formData.Add(content,"formFile",formFile.FileName);

            var responseMessage = await client.PostAsync("http://localhost:5115/api/products/upload",formData);
            return RedirectToAction("Index");
        }
    }
}