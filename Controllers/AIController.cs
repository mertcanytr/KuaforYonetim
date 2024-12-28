using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text;
using System.Text.Json;

namespace KuaforWebSitesi.Controllers
{
    public class AIController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> ApplyHairStyle(IFormFile imageFile, string hairStyle)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Lütfen geçerli bir görsel yükleyin.");
            }

            var tempFilePath = Path.GetTempFileName();
            try
            {
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var client = new RestClient(new RestClientOptions("https://www.ailabapi.com") { MaxTimeout = -1 });
                var request = new RestRequest("/api/portrait/effects/hairstyle-editor", Method.Post);
                request.AddHeader("ailabapi-api-key", "xb632YQVlASupwQUho4cztHqO3grRka98z7iLRn0Jm1gu5ml7Z1AaofHF6nxFLvP");
                request.AlwaysMultipartFormData = true;
                request.AddFile("image_target", tempFilePath);
                request.AddParameter("hair_type", hairStyle);
                request.AddParameter("task_type", "hairstyle");

                var response = await client.ExecuteAsync(request);
                Console.WriteLine("API Yanıtı: " + response.Content);

                if (response.IsSuccessful)
                {
                    try
                    {
                        var jsonResponse = System.Text.Json.JsonDocument.Parse(response.Content);
                        var base64Image = jsonResponse.RootElement.GetProperty("data").GetProperty("image").GetString();

                        if (string.IsNullOrEmpty(base64Image))
                        {
                            return StatusCode(500, "API'den boş bir görsel verisi döndü.");
                        }

                        var imageDataUrl = $"data:image/png;base64,{base64Image}";
                        return View("Result", imageDataUrl);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine("JSON Ayrıştırma Hatası: " + ex.Message);
                        return StatusCode(500, "Yanıt JSON formatında değil.");
                    }
                }
                else
                {
                    Console.WriteLine("API Yanıtı: " + response.Content);
                    return StatusCode((int)response.StatusCode, "API isteği başarısız oldu: " + response.Content);
                }
            }
            finally
            {
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }
            }
        }
    }
}