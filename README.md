# File Management App
![fileMgmtDemo](https://user-images.githubusercontent.com/34210823/86528695-1bf50300-bec8-11ea-96f2-ee24a3fd8f33.gif)

# File Managment App
User can able to upload and download files.This  app contains two projects server and client project
# Server Installation
Server App to manage files built on Asp.net Core Wen Api.User uploaded files store on azure blob
 
 ### Congiure Azure Blob:
 change in appsettings.json
```sh
AzureBlobSettings": {
    "BlobUrl": "blob url",
    "BlobStorageKey": "provide storage account key",
  } 
```
Enable Service in StartUp.cs file
```sh
 services.AddScoped<IFileService, AzureBlobFileSystemService>();
```
 ### Congiure Local File Syatem:
Enable Service in StartUp.cs file
```sh
 services.AddScoped<IFileService, LocalFileSystemService>();
```

# Client Installation
switch to root directory
```sh
 npm install
 ng serve
```
Verify the deployment by navigating to your client address in your preferred browser.
```sh
localhost:4200
```
# Application Users
| Email | Password |
| ------ | ------ |
| ks@gmail.com | test@123 |
| raj@gmail.com | test@123 |
| nirmala@gmail.com | test@123 |
| user@gmail.com | test@123 |


