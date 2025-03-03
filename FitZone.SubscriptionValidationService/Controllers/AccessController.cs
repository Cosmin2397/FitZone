using FitZone.SubscriptionValidationService.DTOs;
using FitZone.SubscriptionValidationService.Models.Enums;
using FitZone.SubscriptionValidationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace FitZone.SubscriptionValidationService.Controllers
{
    [ApiController]
    [Route("api/access")]
    public class AccessController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccessController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("validate-image")]
        public async Task<IActionResult> ValidateAccessFromImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var access =  DecodeQrCodeFromImage(file);

            if (access == null)
            {
                return BadRequest("Invalid QR Code.");
            }

            var isValid = await ValidateSubscription(access);

            if (isValid)
            {
                return Ok(new { Message = "Access granted!" });
            }
            else
            {
                return NotFound("Access denied. Invalid or expired subscription.");
            }
        }


        [HttpGet("generate-test-qr")]
        public IActionResult GenerateTestQr()
        {
            // Creeaza obiectul ClientsAccess
            var access = new ClientsAccess
            {
                Id = Guid.NewGuid(),
                GymId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                SubscriptionId = Guid.NewGuid(),
                Role = Role.Client,
                ValidationType = ValidationType.Entry,
                DataValidare = DateTime.UtcNow
            };

            // Genereaza QR Code-ul în memorie
            byte[] qrCodeBytes = QRGenerator.GenerateQrCode(access);

            // Returneaza fisierul ca răspuns HTTP
            return File(qrCodeBytes, "image/png", "ClientsAccessQR.png");
        }



        private ClientsAccess DecodeQrCodeFromImage(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var bitmap = new Bitmap(stream);

            // Decodăm QR-ul
            var reader = new BarcodeReader();
            var result = reader.Decode(bitmap);

            if (result == null)
                return null;  // Daca nu exista niciun rezultat în QR

            // Deserializeaza textul JSON obtinut din QR Code în obiectul ClientsAccess
            try
            {
                var accessData = JsonConvert.DeserializeObject<ClientsAccess>(result.Text);
                return accessData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing QR code data: {ex.Message}");
                return null;
            }
        }


    private async Task<bool> ValidateSubscription(ClientsAccess access)
        {

            return false;
        }
    }

}
