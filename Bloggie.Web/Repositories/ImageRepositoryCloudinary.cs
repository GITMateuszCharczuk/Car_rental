using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web.Repositories;

public class ImageRepositoryCloudinary : IImageRepository
{
    private readonly Account _account;

    public ImageRepositoryCloudinary(IConfiguration configuration)
    {
        _account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
            configuration.GetSection("Cloudinary")["ApiKey"],
            configuration.GetSection("Cloudinary")["ApiSecret"]);
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var client = new Cloudinary(_account);
        var uploadFileResult = await client.UploadAsync(
            new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            }
        );
        if (uploadFileResult != null && uploadFileResult.StatusCode == HttpStatusCode.OK)
            return uploadFileResult.SecureUrl.ToString();

        return null;
    }
}