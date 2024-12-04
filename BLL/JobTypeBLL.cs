using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class JobTypeBLL
    {
        private readonly JobTypeService _jobTypeService;

        public JobTypeBLL()
        {
            _jobTypeService = new JobTypeService();
        }

        // Lấy danh sách Job Types
        public async Task<List<JobType>> GetJobTypes(
            Expression<Func<JobType, bool>> filter = null,
            Func<IQueryable<JobType>, IOrderedQueryable<JobType>> orderBy = null)
        {
            return await _jobTypeService.GetJobTypes(filter, orderBy);
        }

        // Thêm mới Job Type
        public async Task<JobType> CreateJobType(JobType jobType)
        {
            return await _jobTypeService.CreateJobType(jobType);
        }

        // Cập nhật Job Type
        public async Task<JobType> UpdateJobType(string id, JobType updatedJobType)
        {
            return await _jobTypeService.UpdateJobType(id, updatedJobType);
        }

        // Xóa Job Type
        public async Task<JobType> DeleteJobType(string id)
        {
            return await _jobTypeService.DeleteJobType(id);
        }
    }
}
