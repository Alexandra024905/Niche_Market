using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile formFile);
    }
}
