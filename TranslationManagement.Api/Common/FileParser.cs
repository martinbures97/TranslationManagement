using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Common;

public static class FileParser
{
    public static async Task<TranslationModel?> GetDataFromFile(IFormFile file, string? customer)
    {
        try
        {
            var reader = new StreamReader(file.OpenReadStream());

            if (file.FileName.EndsWith(".txt"))
            {
                return new TranslationModel(customer, await reader.ReadToEndAsync());
            }

            if (file.FileName.EndsWith(".xml"))
            {
                var root = new XmlRootAttribute
                {
                    ElementName = "root",
                    IsNullable = true
                };
                return new XmlSerializer(typeof(TranslationModel), root)
                    .Deserialize(reader) as TranslationModel;
            }

            if (file.FileName.EndsWith(".json"))
            {
                return JsonSerializer.Deserialize<TranslationModel>(await reader.ReadToEndAsync());
            }

            throw new NotSupportedException($"Unsupported file: {file.FileName}");
        }
        catch (Exception e)
        {
            throw new ApiLayerException(e.Message, e);
        }
    }
}