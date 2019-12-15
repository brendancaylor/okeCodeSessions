using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using College.Api.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [AllowAnonymous]
    public class HomeworkController : BaseController
    {
        private readonly IHomeWorkRepository _homeWorkRepository;
        private readonly IAsyncRepository<HomeWorkAssignment> _homeWorkAssignmentRepository;
        private readonly IAsyncRepository<HomeWorkAssignmentItem> _homeWorkAssignmentItemRepository;
        private readonly IAsyncRepository<SubmittedHomeWork> _submittedHomeWorkRepository;
        public HomeworkController(
            IHomeWorkRepository homeWorkRepository,
            IAsyncRepository<HomeWorkAssignment> homeWorkAssignmentRepository,
            IAsyncRepository<HomeWorkAssignmentItem> homeWorkAssignmentItemRepository,
            IAsyncRepository<SubmittedHomeWork> submittedHomeWorkRepository)
        {
            _homeWorkRepository = homeWorkRepository;
            _homeWorkAssignmentRepository = homeWorkAssignmentRepository;
            _homeWorkAssignmentItemRepository = homeWorkAssignmentItemRepository;
            _submittedHomeWorkRepository = submittedHomeWorkRepository;
        }

        [HttpGet("get-home-work-assignment")]
        public async Task<HomeWorkAssignmentDto> GetHomeWorkAssignmentAsync([FromQuery] Guid homeWorkAssignmentId)
        {
            var homeWorkAssignment = await _homeWorkRepository.GetHomeWorkAssignmentWithChildren(homeWorkAssignmentId);
            var dto = HomeWorkAssignmentDto.From(homeWorkAssignment);
            return dto;
        }

        [HttpGet("get-home-work-assignments")]
        public async Task<ActionResult<List<HomeWorkAssignmentDto>>> GetHomeWorkAssignmentsAsync([FromQuery] Guid yearClassId)
        {
            var data = await _homeWorkAssignmentRepository.ListAsync(o => o.YearClassId == yearClassId);
            return data.Select(s => HomeWorkAssignmentDto.From(s)).ToList();
        }

        [HttpPost("add-homeWork-assignment")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<SimpleUpsertDto> AddHomeWorkAssignmentAsync([FromBody] HomeWorkAssignmentAddDto dto)
        {
            var homeWorkAssignment = HomeWorkAssignmentAddDto.GetDomainObjectFrom(dto);
            homeWorkAssignment = await _homeWorkAssignmentRepository.AddAsync(homeWorkAssignment, this.AppUserId.Value);
            return SimpleUpsertDto.From(homeWorkAssignment);
        }

        [HttpPost("add-submitted-homeWork")]
        public Task AddSubmittedHomeWorkAsync([FromBody] SubmittedHomeWorkAddDto dto)
        {
            var submittedHomeWork = SubmittedHomeWorkAddDto.GetDomainObjectFrom(dto);
            return _submittedHomeWorkRepository.AddAsync(submittedHomeWork, Guid.Empty);
        }

        [HttpPut("update-homeWork-assignment")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateHomeWorkAssignmentAsync([FromBody] HomeWorkAssignmentUpdateDto dto)
        {
            var domainObject = await _homeWorkAssignmentRepository.GetByIdAsync(dto.Id);
            _homeWorkAssignmentRepository.SetRowVersion(domainObject, dto.RowVersion);
            HomeWorkAssignmentUpdateDto.SetDomainObjectFrom(dto, domainObject);
            await _homeWorkAssignmentRepository.UpdateAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpPost("add-homeWork-assignment-item")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<SimpleUpsertDto> AddHomeWorkAssignmentItemAsync([FromBody] HomeWorkAssignmentItemAddDto dto)
        {
            var domainObject = HomeWorkAssignmentItemAddDto.GetDomainObjectFrom(dto);
            //homeWorkAssignmentItem.SpokenWordAsMp3 = this.GetGoogleSpeech(homeWorkAssignmentItem.Word, homeWorkAssignmentItem.WordLanguage);
            //homeWorkAssignmentItem.SpokenSentenceAsMp3 = this.GetGoogleSpeech(homeWorkAssignmentItem.Sentence, homeWorkAssignmentItem.SentenceLanguage);
            domainObject.AddGoogleSpeechApiRequest();
            domainObject = await _homeWorkAssignmentItemRepository.AddAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpPut("update-homeWork-assignment-item")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateHomeWorkAssignmentItemAsync([FromBody] HomeWorkAssignmentItemUpdateDto dto)
        {
            var domainObject = await _homeWorkAssignmentItemRepository.GetByIdAsync(dto.Id);
            _homeWorkAssignmentItemRepository.SetRowVersion(domainObject, dto.RowVersion);

            if (domainObject.Word != dto.Word)
            {
                //domainObject.SpokenWordAsMp3 = this.GetGoogleSpeech(dto.Word, dto.WordLanguage);
            }

            if (domainObject.Sentence != dto.Sentence)
            {
                //domainObject.SpokenSentenceAsMp3 = this.GetGoogleSpeech(dto.Sentence, dto.SentenceLanguage);
            }

            domainObject.AddGoogleSpeechApiRequest();

            HomeWorkAssignmentItemUpdateDto.SetDomainObjectFrom(dto, domainObject);
            await _homeWorkAssignmentItemRepository.UpdateAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

        [HttpDelete("delete-homeWork-assignment-item")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task DeleteHomeWorkAssignmentItemAsync([FromQuery] Guid HomeWorkAssignmentItemUpdateId)
        {
            var domainObject = await _homeWorkAssignmentItemRepository.GetByIdAsync(HomeWorkAssignmentItemUpdateId);
            await _homeWorkAssignmentItemRepository.DeleteAsync(domainObject);
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