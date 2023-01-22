namespace Juan.Helpers
{
    public static class FileManager
    {
       public static string SaveFile(string rootpath,string foldername,IFormFile formFile)
        {
            var name = formFile.FileName;
            name= name.Length>64 ? name.Substring(name.Length-64, 64) : name;
            name=Guid.NewGuid().ToString()+name;
            string savepath= Path.Combine(rootpath,foldername,name);
            using (FileStream fs =new FileStream(savepath,FileMode.Create))
            {
                formFile.CopyTo(fs);
            }
            return name;
        }
        public static void DeleteFile(string rootpath, string foldername, string formFile)
        {
            string deletepath= Path.Combine(rootpath,foldername,formFile);
            if (System.IO.File.Exists(deletepath))
            {
                System.IO.File.Delete(deletepath);
            };
        }
    }
}
