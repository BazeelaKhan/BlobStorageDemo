using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
namespace BlobStorageDemo
{
    /// <summary>
    /// Class to connect to the Storage Account 
    /// StorageAccountName - key in web.config
    /// StorageAccountKey - key in web.config
    /// </summary>
    public static class ConnectionString
    {
        static string account = CloudConfigurationManager.GetSetting("StorageAccountName");
        static string key = CloudConfigurationManager.GetSetting("StorageAccountKey");
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}