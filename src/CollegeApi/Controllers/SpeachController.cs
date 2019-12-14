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
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using College.Api.Models;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SpeachController : ControllerBase
    {

        private readonly IAsyncRepository<HomeWorkAssignmentItem> _homeWorkAssignmentItemRepository;

        public SpeachController(IAsyncRepository<HomeWorkAssignmentItem> homeWorkAssignmentItemRepository)
        {
            _homeWorkAssignmentItemRepository = homeWorkAssignmentItemRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces("audio/mpeg")]
        public async Task<FileStreamResult> GetSpeachAsync(Guid homeWorkAssignmentItemId, SpeechType speechType)
        {
            var data = await _homeWorkAssignmentItemRepository.GetByIdAsync(homeWorkAssignmentItemId);
            MemoryStream output = new MemoryStream();
            switch (speechType)
            {
                case SpeechType.Word:
                    output = new MemoryStream(data.SpokenWordAsMp3);
                    break;
                case SpeechType.Sentence:
                    output = new MemoryStream(data.SpokenSentenceAsMp3);
                    break;
                default:
                    break;
            }
            output.Seek(0, SeekOrigin.Begin);
            return File(output, "application/octet-stream");
        }
    }
}
