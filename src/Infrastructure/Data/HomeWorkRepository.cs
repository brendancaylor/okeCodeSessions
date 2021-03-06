﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Projections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class HomeWorkRepository : EfRepository<HomeWorkAssignment>, IHomeWorkRepository
    {
        public HomeWorkRepository(CollegeContext dbContext) : base(dbContext)
        {
        }

        public Task<List<College>> GetCollegesFromNonAdmin(Guid appUserId)
        {
            return _dbContext
                .CollegeAppUsers
                    .Include(o => o.College)
                .Where(o => o.AppUserId == appUserId)
                .Select(s => s.College)
                .ToListAsync();
        }

        public async Task<List<HomeWorkAssignment>> GetHomeWorkAssignmentsWithChildrenAsync(Guid yearClassId)
        {
            return await _dbContext
                .HomeWorkAssignments
                    .Include(o => o.YearClass)
                    .Include(o => o.HomeWorkAssignmentItems)
                    .Include(o => o.SubmittedHomeWorks)
                .Where(o => o.YearClassId == yearClassId)
                .ToListAsync();
        }

        public async Task<HomeWorkAssignment> GetHomeWorkAssignmentWithChildrenAsync(Guid homeWorkAssignmentId)
        {
            return await _dbContext
                .HomeWorkAssignments
                    .Include(o => o.YearClass)
                    .Include(o => o.HomeWorkAssignmentItems)
                    .Include(o => o.SubmittedHomeWorks)
                .SingleAsync(o => o.Id == homeWorkAssignmentId);
        }

        public async Task AddHomeworkFromListAsync(AddHomeworkFromList addHomeworkFromList, Guid appUserId)
        {
            var standardList = await _dbContext.StandardLists
                    .Include(o => o.StandardListItems)
                .SingleAsync(o => o.Id == addHomeworkFromList.StandardListId);
            foreach (var addHomeworkAssignment in addHomeworkFromList.AddHomeworkAssignments)
            {
                var homeWorkAssignment = new HomeWorkAssignment();
                homeWorkAssignment.DueDate = addHomeworkAssignment.DueDate;
                homeWorkAssignment.YearClassId = addHomeworkFromList.YearClassId;

                foreach (var standardListItemId in addHomeworkAssignment.StandardListItemIds)
                {
                    var standardListItem = standardList.StandardListItems.Single(o => o.Id == standardListItemId);
                    var homeWorkAssignmentItem = new HomeWorkAssignmentItem
                    {
                        Sentence = standardListItem.Sentence,
                        SentenceLanguage = standardListItem.SentenceLanguage,
                        SpokenSentenceAsMp3 = standardListItem.SpokenSentenceAsMp3,

                        Word = standardListItem.Word,
                        WordLanguage = standardListItem.WordLanguage,
                        SpokenWordAsMp3 = standardListItem.SpokenWordAsMp3
                    };
                    homeWorkAssignmentItem.SetUserAddProperties(appUserId);
                    homeWorkAssignmentItem.SetDateAddProperties();
                    homeWorkAssignmentItem.Id = Guid.Empty;
                    homeWorkAssignment.AddHomeWorkAssignmentItem(homeWorkAssignmentItem);
                }
                try
                {
                    await this.AddAsync(homeWorkAssignment, appUserId);
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }

        }

        public async Task DeleteHomeWorkAssignmentItemAsync(Guid homeWorkAssignmentItemId)
        {
            var homeWorkAssignmentItem = await _dbContext.HomeWorkAssignmentItems
                    .Include(o => o.GoogleSpeechApiRequests)
                .SingleAsync(o => o.Id == homeWorkAssignmentItemId);
            foreach (var googleSpeechApiRequest in homeWorkAssignmentItem.GoogleSpeechApiRequests)
            {
                _dbContext.Entry(googleSpeechApiRequest).State = EntityState.Deleted;
            }
            _dbContext.Entry(homeWorkAssignmentItem).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteHomeWorkAssignmentAsync(Guid homeWorkAssignmentId)
        {
            var homeWorkAssignment = await _dbContext.HomeWorkAssignments
                .Include(o => o.SubmittedHomeWorks)
                .Include(o => o.HomeWorkAssignmentItems)
                    .ThenInclude(o => o.GoogleSpeechApiRequests)
                .SingleAsync(o => o.Id == homeWorkAssignmentId);

            foreach (var homeWorkAssignmentItem in homeWorkAssignment.HomeWorkAssignmentItems)
            {
                foreach (var googleSpeechApiRequest in homeWorkAssignmentItem.GoogleSpeechApiRequests)
                {
                    _dbContext.Entry(googleSpeechApiRequest).State = EntityState.Deleted;
                }
                _dbContext.Entry(homeWorkAssignmentItem).State = EntityState.Deleted;
            }
            foreach (var submittedHomeWork in homeWorkAssignment.SubmittedHomeWorks)
            {
                _dbContext.Entry(submittedHomeWork).State = EntityState.Deleted;
            }
            _dbContext.Entry(homeWorkAssignment).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
    }
}