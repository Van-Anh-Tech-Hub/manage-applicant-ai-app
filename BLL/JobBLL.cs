using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class JobBLL
    {
        private readonly JobService _jobService;

        public JobBLL()
        {
            _jobService = new JobService();
        }

        // Lấy danh sách Jobs
        public async Task<List<Job>> GetJobs(
            Expression<Func<Job, bool>> filter = null,
            Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null)
        {
            return await _jobService.GetJobs(filter, orderBy);
        }

        // Thêm mới Job
        public async Task<Job> CreateJob(Job job)
        {
            return await _jobService.CreateJob(job);
        }

        // Cập nhật Job
        public async Task<Job> UpdateJob(string id, Job updatedJob)
        {
            return await _jobService.UpdateJob(id, updatedJob);
        }

        // Xóa Job
        public async Task<Job> DeleteJob(string id)
        {
            return await _jobService.DeleteJob(id);
        }
    }
}
