
using CloudinaryDotNet;

namespace Bloggie.Web.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly Account account;

		public ImageRepository(IConfiguration configuration)
		{
			this.account = new Account(configuration["Cloudinary:CloudName"], configuration["Cloudinary:ApiKey"], configuration["Cloudinary:ApiSecret"]);
		}
		public async Task<string> UploadAsync(IFormFile file)
		{
			var client = new Cloudinary(account);
			var uploadFileResult = await client.UploadAsync(new CloudinaryDotNet.Actions.ImageUploadParams
			{
				File = new FileDescription(file.FileName, file.OpenReadStream()),
				DisplayName = file.FileName
			});

			if (uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return uploadFileResult.SecureUri.ToString();
			}
			return null;
		}
	}
}
