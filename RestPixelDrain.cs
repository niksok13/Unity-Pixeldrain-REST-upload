using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PixelDrainApi
{
    public class ResponsePixelDrainUploadFile
    {
        public bool success;
        public string id;
    }
    
    public class RestPixelDrain
    {
        private const string UploadUrl = "https://pixeldrain.com/api/file/";
        private const string CapchaUrl = "https://pixeldrain.com/u/";
        
        public async Task<string> Upload(byte[] file, string fileName)
        {
            Debug.Log($"Uploading {fileName} to PixelDrain");
            var form = new WWWForm();
            form.AddField("name", fileName);
            form.AddField("anonymous", "false");
            form.AddBinaryData("file", file);
            var request = UnityWebRequest.Post(UploadUrl, form);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                Debug.Log($"Upload progress: {request.uploadProgress:P}");
                await Task.Delay(1000);
            }

            if (request.error != null)
            {
                request.Dispose();
                throw new Exception(request.error + request.downloadHandler.text);
            }

            var response = JsonUtility.FromJson<ResponsePixelDrainUploadFile>(request.downloadHandler.text);
            request.Dispose();
            if (response.success)
            {
                var message = $"`{fileName}`\n[DOWNLOAD]({UploadUrl+response.id})\n[CAPTCHA]({CapchaUrl+response.id})";
                Debug.Log(message);
                return message;
            }

            return "";
        }
    }
}
