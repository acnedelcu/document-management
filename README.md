---------- AzureDocs ----------

This repository contains the source code of AzureDocs, a web app designed for managing the documents of a university.
The users can access, upload and request documents, generate QR codes to access them easier and many more.

Technologies & services used:

- .NET 5 application
- Entity Framework Core (Code First database)
- ASP.NET Core Identity
- DevExpress UI Components (uploader, data grids)
- Bootstrap
- ZXing (QR codes)

Cloud Services
- Database stored in Azure
- Azure Blob Storage 

Prerequisites for running the application
Azure Database and Azure BlobStorage

The user secrets file should have the next structure:
{
  "ConnectionStrings": {
    "DbConnectionString": "InsertYourDatabaseConnectionString",
    "StorageConnectionString": "InsertYourStorageAccountConnectionString",
    "AccountKey": "InsertYourAccountKey"
  }
}

Restoring the Database

Using the Package Manager Console, run the Update-Database command

Enjoy using the app

The illustrations were created using InVision Studio and the open-source design library, Humaaans.
See more details at https://www.humaaans.com/
