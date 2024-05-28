using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Services
{
    //. Fotoğraflar Cloudinary üzerinde barındırılır ve yönetilir.
    // Bu sınıf, IPhotoService arayüzünü (interface) uygulayarak bağımlılıkların enjeksiyon yoluyla yönetilmesini de sağlar.
    //foto yukleme ve silme servisii. Cloudinary saglar bunu
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary( acc );
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)  //dosya alır ve Cloudinarya yukler
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length >0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams //ilk yükleme parametlereli belirlenir
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams); //UploadAsync metoduyla fotolar Cloudinarya yuklenır
            }
            return uploadResult;
        }
        //foto silme işlemi
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
