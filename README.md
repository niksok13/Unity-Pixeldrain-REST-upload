# Unity-Pixeldrain-REST-upload
Unity tiny editor script to upload files to Pixeldrain via REST API

## Usage:
```cs
var api = new RestPixelDrain()
var fileBytes = await File.ReadAllBytesAsync(path);
var message = await api.Upload(fileBytes, "build.apk")
Debug.Log(message)
```
Result message contains direct link to file download and second link to CAPTCHA page, if direct link is blocked.
