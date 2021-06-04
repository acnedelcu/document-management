using DocumentManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public static class Helper
    {
        private const string PdfExtension = ".pdf";
        private const string DocExtension = ".doc";
        private const string DocxExtension = ".docx";
        private const string XlsExtension = ".xls";
        private const string XlsxExtension = ".xlsx";
        private const string PptExtension = ".ppt";
        private const string PptxExtension = ".pptx";
        private const string TxtFileExtension = ".txt";
        public static string GetFileType(string filepath)
        {
            string fileExtension = Path.GetExtension(filepath);

            switch (fileExtension)
            {
                case PdfExtension:
                    return "application/pdf";
                case DocExtension:
                    return "application/msword";
                case DocxExtension:
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case XlsExtension:
                    return "application/vnd.ms-excel";
                case XlsxExtension:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case PptExtension:
                    return "application/vnd.ms-powerpoint";
                case PptxExtension:
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case TxtFileExtension:
                    return "text/plain";
                default:
                    return "application/octet-stream";
            }
        }

        public static string GetBlobSasUrl(string accountName, string containerName, string fileName, string sasToken)
        {
            string blobSasUrl = "https://" + accountName + ".blob.core.windows.net/" + containerName + "/" + fileName + "?" + sasToken;
            return blobSasUrl;
        }
    }
}
