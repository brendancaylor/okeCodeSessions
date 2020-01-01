using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Projections;
using College.Api.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;
using System.Reflection;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Authorize(Roles = "AdminisiterColleges")]
    //[AllowAnonymous]
    public class StandardListController : BaseController
    {
        private readonly IStandardListRepository _standardListRepository;
        private readonly IAsyncRepository<StandardListItem> _standardListItemRepository;

        public StandardListController(
            IStandardListRepository standardListRepository,
            IAsyncRepository<StandardListItem> standardListItemRepository)
        {
            _standardListRepository = standardListRepository;
            _standardListItemRepository = standardListItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<StandardListDto>>> GetAllStandardListsAsync()
        {
            var data = await _standardListRepository.ListAllAsync();
            return data.Select(o => StandardListDto.From(o)).OrderBy(o => o.StandardListName).ToList();
        }

        [HttpGet("{standardListId}")]
        public async Task<ActionResult<StandardListDto>> GetStandardListAsync([FromRoute] Guid standardListId)
        {
            var domainObject = await _standardListRepository.GetStandardListWithChildrenAsync(standardListId);
            return StandardListDto.From(domainObject);
        }

        [HttpPost]
        public async Task<ActionResult<SimpleUpsertDto>> AddStandardListAsync([FromBody] StandardListDto dto)
        {
            var domainObject = new StandardList(); 
            StandardListDto.From(dto, domainObject);
            domainObject = await _standardListRepository.AddAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpPut]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateStandardListAsync([FromBody] StandardListDto dto)
        {
            var domainObject = await _standardListRepository.GetByIdAsync(dto.Id);
            StandardListDto.From(dto, domainObject);
            domainObject = await _standardListRepository.UpdateAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpPost("add-standard-list-item")]
        public async Task<ActionResult<SimpleUpsertDto>> AddStandardListItemAsync([FromBody] StandardListItemDto dto)
        {
            var domainObject = new StandardListItem();
            StandardListItemDto.From(dto, domainObject);
            domainObject.SpokenSentenceAsMp3 = this.GetGoogleSpeech(domainObject.Sentence, domainObject.SentenceLanguage);
            domainObject.SpokenWordAsMp3 = this.GetGoogleSpeech(domainObject.Word, domainObject.WordLanguage);
            domainObject = await _standardListItemRepository.AddAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpPut("update-standard-list-item")]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateStandardListItemAsync([FromBody] StandardListItemDto dto)
        {
            var domainObject = await _standardListItemRepository.GetByIdAsync(dto.Id);
            StandardListItemDto.From(dto, domainObject);
            domainObject.SpokenSentenceAsMp3 = this.GetGoogleSpeech(domainObject.Sentence, domainObject.SentenceLanguage);
            domainObject.SpokenWordAsMp3 = this.GetGoogleSpeech(domainObject.Word, domainObject.WordLanguage);
            domainObject = await _standardListItemRepository.UpdateAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpDelete("delete-standard-list-item")]
        public async Task DeleteStandardListItemAsync([FromQuery] Guid standardListItemId)
        {
            var domainObject = await _standardListItemRepository.GetByIdAsync(standardListItemId);
            await _standardListItemRepository.DeleteAsync(domainObject);
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
