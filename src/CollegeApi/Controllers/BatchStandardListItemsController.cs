using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Projections;
using College.Api.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [OpenApiIgnore]
    [ApiController]
    [AllowAnonymous]
    public class BatchStandardListItemsController : BaseController
    {
        private readonly IStandardListRepository _standardListRepository;
        private readonly IOxfordDictionaryService _oxfordDictionaryService;

        public BatchStandardListItemsController(
            IStandardListRepository standardListRepository,
            IOxfordDictionaryService oxfordDictionaryService)
        {
            _standardListRepository = standardListRepository;
            _oxfordDictionaryService = oxfordDictionaryService;
        }


        [HttpPost("{standardListId}")]
        public async Task<bool> UploadBatchStandardListItemAsync([FromForm] IFormFile file, [FromRoute] Guid standardListId)
        {
            var standardListItems = new List<StandardListItem>();
            
            if (file.Length > 0)
            {
                
                var csvUtils = new CsvUtils();
                var wordItems = csvUtils.ReadWordItemCsv(file);
                foreach (var wordItem in wordItems)
                {
                    var standardListItem = new StandardListItem
                    {
                        Sentence = wordItem.Sentence,
                        Word = wordItem.Word,
                        SentenceLanguage = wordItem.SentenceLanguage,
                        WordLanguage = wordItem.WordLanguage
                    };
                    
                    System.Threading.Thread.Sleep(1000);
                    standardListItem.SpokenWordAsMp3 = GetGoogleSpeech(wordItem.Word, wordItem.WordLanguage);
                    System.Threading.Thread.Sleep(1000);
                    standardListItem.SpokenSentenceAsMp3 = GetGoogleSpeech(wordItem.Sentence, wordItem.SentenceLanguage);
                   

                    standardListItems.Add(standardListItem);
                }
                
                var worked = await _standardListRepository.ReplaceStandardListItemsAsync(standardListId, standardListItems);
            }
            return true;
        }

        [HttpPost("words-only/{standardListId}")]
        public async Task<bool> UploadBatchWordsAsync([FromForm] IFormFile file, [FromRoute] Guid standardListId)
        {
            var standardListItems = new List<StandardListItem>();

            if (file.Length > 0)
            {

                var csvUtils = new CsvUtils();
                var words = csvUtils.ReadWordOnlyCsv(file);
                foreach (var word in words)
                {
                    var standardListItem = new StandardListItem
                    {
                        Word = word.Word,
                        SentenceLanguage = "en-GB",
                        WordLanguage = "en-GB"
                    };

                    try
                    {
                        var defintion = await _oxfordDictionaryService.GetDefinitionAsync(standardListItem.Word);
                        standardListItem.Sentence = defintion
                            .results.FirstOrDefault()?
                            .lexicalEntries.FirstOrDefault()?
                            .entries.FirstOrDefault()?
                            .senses.FirstOrDefault()?
                            .shortDefinitions.FirstOrDefault();

                    }
                    catch (Exception ex)
                    {
                        standardListItem.Sentence = $"No entry found for {standardListItem.Word}";
                    }
                    

                    System.Threading.Thread.Sleep(1000);
                    standardListItem.SpokenWordAsMp3 = GetGoogleSpeech(standardListItem.Word, standardListItem.WordLanguage);
                    System.Threading.Thread.Sleep(1000);
                    standardListItem.SpokenSentenceAsMp3 = GetGoogleSpeech(standardListItem.Sentence, standardListItem.SentenceLanguage);


                    standardListItems.Add(standardListItem);
                }

                var worked = await _standardListRepository.ReplaceStandardListItemsAsync(standardListId, standardListItems);
            }
            return true;
        }


        private byte[] GetGoogleSpeech(string speechText, string languageCode)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SpellingApp-7fc0cf8b5885.json");
            var credential = GoogleCredential.FromFile(path);
            var channel = new Grpc.Core.Channel(TextToSpeechClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());
            TextToSpeechClient client = TextToSpeechClient.Create(channel);

            // Set the text input to be synthesized.
            SynthesisInput input = new SynthesisInput
            {
                Text = speechText
            };

            // Build the voice request, select the language code ("en-US"),
            // and the SSML voice gender ("neutral").
            VoiceSelectionParams voice = new VoiceSelectionParams
            {
                LanguageCode = languageCode,
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

            return response.AudioContent.ToByteArray();
        }

    }
}
