namespace AzureDocumentIntelligence.API.Utils
{
    public static class StreamUtil
    {
        public static async Task<MemoryStream> ToMemoryStreamAsync(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
