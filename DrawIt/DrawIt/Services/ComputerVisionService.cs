using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace DrawIt.Services
{
    public class ComputerVisionService
    {
        private const string API_Key = "";

        public ComputerVisionService()
        {
        }

        public async Task<AnalysisResult> GetAnalysisResult(Stream stream)
        {
            try
            {
                // If you're using a trial, make sure to pass the westcentralus api base URL
                var visionClient = new VisionServiceClient(API_Key, "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0");

                VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description, VisualFeature.Color, VisualFeature.ImageType };
                return await visionClient.AnalyzeImageAsync(stream, features.ToList(), null);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
