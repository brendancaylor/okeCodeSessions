using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly IAsyncRepository<College> _collegeRepository;

        public CollegeService(IAsyncRepository<College> collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }

        public Task AddCollege(string name)
        {
            return _collegeRepository.AddAsync(new College { CollegeName = name});
        }
    }
}
