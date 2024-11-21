namespace AdminDashBoard.Helper
{
	public class PictureSettings
	{
		public static string UploadFile(IFormFile file,string FolderName) 
		{
			//1-Get Folder Path 
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images" ,FolderName);

			//2- set File name unique
			
			var fileName =Guid.NewGuid()+file.FileName;

			//3- get file path 
			var filePath = Path.Combine(folderPath, fileName);

			//4- save file as stream 
			var fs = new FileStream(filePath, FileMode.Create);

			file.CopyTo(fs);

			return Path.Combine("images//products", fileName);

		}
		public static void DeleteFile(string FolderName, string filename) 
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", FolderName,filename);

			if (File.Exists(filePath)) 
			{
				File.Delete(filePath);
			}

		}
	}
}
