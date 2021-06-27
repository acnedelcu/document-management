using DocumentManagement.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing;

namespace DocumentManagement
{
    public static class Helper
    {
        public static List<FileRequest> FileRequests = new List<FileRequest>();
        private const string PdfExtension = ".pdf";
        private const string DocExtension = ".doc";
        private const string DocxExtension = ".docx";
        private const string XlsExtension = ".xls";
        private const string XlsxExtension = ".xlsx";
        private const string PptExtension = ".ppt";
        private const string PptxExtension = ".pptx";
        private const string TxtFileExtension = ".txt";

        //Qr codes constants
        private const int QrWidthInPixels = 400, QrHeightInPixels = 400;

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

        /// <summary>
        /// Generates a QR code for a given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Bitmap GenerateQrCodeForUrl(string url)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions { Width = QrWidthInPixels, Height = QrHeightInPixels }
            };

            Bitmap resultedQrCode = new Bitmap(writer.Write(url));

            return resultedQrCode;
        }

        /// <summary>
        /// Reads user information from an Excel file and returns a list of ApplicationUser
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<ApplicationUser> ReadExcelData(string filePath)
        {
            
            FileInfo fileToRead = new FileInfo(filePath);
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (ExcelPackage package = new ExcelPackage(fileToRead))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; //get the first worksheet
                int colCount = worksheet.Dimension.End.Column;  //get nr of columns
                int rowCount = worksheet.Dimension.End.Row;     //get nr of rows

                for (int row = 2; row <= rowCount; row++)
                {
                    string lastName = worksheet.Cells[row, 1].Value?.ToString();
                    string dadNameInitial = worksheet.Cells[row, 2].Value?.ToString();
                    string firstName = worksheet.Cells[row, 3].Value?.ToString();
                    string enrollmentNumber = worksheet.Cells[row, 4].Value?.ToString();
                    string userName = worksheet.Cells[row, 5].Value?.ToString();
                    string email = userName;

                    applicationUsers.Add(new ApplicationUser
                    {
                        LastName = lastName,
                        DadFirstNameInitial = dadNameInitial,
                        FirstName = firstName,
                        UserName = userName,
                        Email = email
                    });
                    System.Diagnostics.Debug.WriteLine(applicationUsers);
                }
            }

            return applicationUsers;

        }
    }
}
