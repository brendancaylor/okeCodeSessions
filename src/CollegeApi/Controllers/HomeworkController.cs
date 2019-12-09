using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using College.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Authorize(Roles = "AdminisiterHomework")]
    public class HomeworkController : BaseController
    {
        private readonly IHomeWorkRepository _homeWorkRepository;
        private readonly IAsyncRepository<HomeWorkAssignment> _homeWorkAssignmentRepository;
        private readonly IAsyncRepository<HomeWorkAssignmentItem> _homeWorkAssignmentItemRepository;
        private readonly IAsyncRepository<SubmittedHomeWork> _submittedHomeWorkRepository;
        private readonly ICollegeRepository _collegeRepository;
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

        [HttpGet("get-homeWork-assignment")]
        public async Task<ActionResult<HomeWorkAssignmentDto>> GetHomeWorkAssignmentAsync([FromQuery] Guid homeWorkAssignmentId)
        {
            var data = await _homeWorkRepository.GetHomeWorkAssignmentWithChildren(homeWorkAssignmentId);
            return HomeWorkAssignmentDto.From(data);
        }

        
        [HttpPost("add-homeWork-assignment")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<SimpleUpsertDto> AddHomeWorkAssignmentAsync([FromBody] HomeWorkAssignmentAddDto dto)
        {
            var homeWorkAssignment = HomeWorkAssignmentAddDto.GetDomainObjectFrom(dto);
            homeWorkAssignment = await _homeWorkAssignmentRepository.AddAsync(homeWorkAssignment, this.AppUserId.Value);
            return SimpleUpsertDto.From(homeWorkAssignment);
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
            var homeWorkAssignmentItem = HomeWorkAssignmentItemAddDto.GetDomainObjectFrom(dto);
            homeWorkAssignmentItem = await _homeWorkAssignmentItemRepository.AddAsync(homeWorkAssignmentItem, this.AppUserId.Value);
            return SimpleUpsertDto.From(homeWorkAssignmentItem);
        }

        [HttpPut("update-homeWork-assignment-item")]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateHomeWorkAssignmentItemAsync([FromBody] HomeWorkAssignmentItemUpdateDto dto)
        {
            var domainObject = await _homeWorkAssignmentItemRepository.GetByIdAsync(dto.Id);
            _homeWorkAssignmentItemRepository.SetRowVersion(domainObject, dto.RowVersion);
            HomeWorkAssignmentItemUpdateDto.SetDomainObjectFrom(dto, domainObject);
            await _homeWorkAssignmentItemRepository.UpdateAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }

    }
}