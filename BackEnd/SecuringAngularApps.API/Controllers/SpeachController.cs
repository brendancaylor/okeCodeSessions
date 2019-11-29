﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Google.Cloud.TextToSpeech.V1;
using Microsoft.AspNetCore.Authorization;

namespace SecuringAngularApps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SpeachController : ControllerBase
    {
        [HttpGet]
        [Produces("audio/mpeg")]
        
        public ActionResult GetSpeach(string sentence)
        {

            // Instantiate a client
            TextToSpeechClient client = TextToSpeechClient.Create();

            // Set the text input to be synthesized.
            SynthesisInput input = new SynthesisInput
            {
                Text = sentence
            };

            // Build the voice request, select the language code ("en-US"),
            // and the SSML voice gender ("neutral").
            VoiceSelectionParams voice = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
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
            using (MemoryStream output = new MemoryStream())
            {
                response.AudioContent.WriteTo(output);
                return File(output, "application/octet-stream");
            }

        }
    }
}