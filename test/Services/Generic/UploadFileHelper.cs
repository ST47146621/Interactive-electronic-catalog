using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using test.Extend;
using test.Models;

namespace test.Services.Generic
{
    public class UploadFileHelper
    {
        private IUnitOfWork db = new UnitOfWork();
        private string _directoryPath = "File";

        //public UploadFileHelper(string program)
        //{
        //    this._directoryPath = "File";
        //}

        public string UploadFile(HttpPostedFileBase uploadFile, string fileId)
        {
            if (uploadFile.ContentLength == 0)
                return null;
            else if (uploadFile.ContentLength > 100000000)
                throw new MyException("檔案大小不能超過 100 MB");

            var fileName = Path.GetFileName(uploadFile.FileName);
            var extension = Path.GetExtension(fileName);
            var uploadFileName = String.Empty;

            var filePath = this.CheckDirectory();
            uploadFileName = fileId + extension;
            filePath = Path.Combine(filePath, uploadFileName);

            uploadFile.SaveAs(filePath);
            return  _directoryPath + "/" + uploadFileName;
        }

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFiles(string fileName)
        {
            try
            {
                var filePath = HttpContext.Current.Server.MapPath("~/" + fileName);

                if (System.IO.File.Exists(filePath))
                {
                    // 路徑如果存在，則刪除檔案
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception e)
            {

            }
        }

        public Tuple<string, string> GetDownloadFile<TEntity>(Guid fileId) where TEntity : class
        {
            TEntity data = db.Repository<TEntity>().GetById(fileId);
            string extension = data.GetType().GetProperty("Extension").GetValue(data, null).ToString();
            string fileName = fileId.ToString() + "." + extension;
            string downloadName = data.GetType().GetProperty("Name").GetValue(data, null).ToString() + "." + extension;
            return Tuple.Create(HttpContext.Current.Server.MapPath("~/" + this._directoryPath + "/" + fileName), downloadName);
        }

        private string CheckDirectory()
        {
            var fileDirectory = HttpContext.Current.Server.MapPath("~/" + this._directoryPath);

            if (Directory.Exists(fileDirectory) == false)
            {
                // 假如資料夾不存在，則建立它
                Directory.CreateDirectory(fileDirectory);
            }

            return fileDirectory;
        }
    }
}