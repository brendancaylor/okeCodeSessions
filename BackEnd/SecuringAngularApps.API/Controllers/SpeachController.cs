using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Google.Cloud.TextToSpeech.V1;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;

namespace SecuringAngularApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SpeachController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Produces("audio/mpeg")]
        public FileStreamResult GetSpeach(string sentence)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SpellingApp-7fc0cf8b5885.json");
            var credential = GoogleCredential.FromFile(path);
            var channel = new Grpc.Core.Channel(TextToSpeechClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());
            TextToSpeechClient client = TextToSpeechClient.Create(channel);

            // Set the text input to be synthesized.
            SynthesisInput input = new SynthesisInput
            {
                Text = sentence
            };

            // Build the voice request, select the language code ("en-US"),
            // and the SSML voice gender ("neutral").
            VoiceSelectionParams voice = new VoiceSelectionParams
            {
                LanguageCode = "en-GB",
                SsmlGender = SsmlVoiceGender.Neutral
            };

            // Select the type of audio file you want returned.
            AudioConfig config = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3
            };

            // Perform the Text-to-Speech request, passing the text input
            // with the selected voice parameters and audio file type
            var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
            {
                Input = input,
                Voice = voice,
                AudioConfig = config
            });

            // Write the binary AudioContent of the response to an MP3 file.
            MemoryStream output = new MemoryStream();
            response.AudioContent.WriteTo(output);
            output.Seek(0, SeekOrigin.Begin);
            return File(output, "application/octet-stream");
        }
    }
}