using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class JobCategoryBLL
    {
        private readonly JobCategoryService _jobCategoryService;

        public JobCategoryBLL()
        {
            _jobCategoryService = new JobCategoryService();
        }

        // Lấy danh sách Job Categories
        public async Task<List<JobCategory>> GetJobCategories(
            Expression<Func<JobCategory, bool>> filter = null,
            Func<IQueryable<JobCategory>, IOrderedQueryable<JobCategory>> orderBy = null)
        {
            return await _jobCategoryService.GetJobCategories(filter, orderBy);
        }

        // Thêm mới Job Category
        public async Task<JobCategory> CreateJobCategory(JobCategory jobCategory)
        {
            return await _jobCategoryService.CreateJobCategory(jobCategory);
        }

        // Cập nhật Job Category
        public async Task<JobCategory> UpdateJobCategory(string id, JobCategory updatedJobCategory)
        {
            return await _jobCategoryService.UpdateJobCategory(id, updatedJobCategory);
        }

        // Xóa Job Category
        public async Task<JobCategory> DeleteJobCategory(string id)
        {
            return await _jobCategoryService.DeleteJobCategory(id);
        }
    }
}
