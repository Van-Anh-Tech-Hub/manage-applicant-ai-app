using MongoDB.Driver;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DAL.Services
{
    public class JobCategoryService
    {
        private readonly IMongoCollection<JobCategory> _jobCategories;
        private readonly MongoDbContext _dbContext;

        public JobCategoryService()
        {
            _dbContext = new MongoDbContext();
            _jobCategories = _dbContext.JobCategories;
        }

        #region CRUD Methods for JobCategory

        // Lấy danh sách Job Categories
        public async Task<List<JobCategory>> GetJobCategories(
            Expression<Func<JobCategory, bool>> filter = null,
            Func<IQueryable<JobCategory>, IOrderedQueryable<JobCategory>> orderBy = null)
        {
            var query = _jobCategories.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<JobCategory>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        // Lấy Job Category theo filter
        public async Task<JobCategory> GetJobCategory(Expression<Func<JobCategory, bool>> filter = null)
        {
            var query = _jobCategories.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        // Thêm mới Job Category
        public async Task<JobCategory> CreateJobCategory(JobCategory jobCategory)
        {
            await _jobCategories.InsertOneAsync(jobCategory);
            return jobCategory;
        }

        // Cập nhật Job Category
        public async Task<JobCategory> UpdateJobCategory(string id, JobCategory updatedJobCategory)
        {
            var existingCategory = await _jobCategories.Find(j => j.Id == id).FirstOrDefaultAsync();

            if (existingCategory != null)
            {
                updatedJobCategory.Id = id;
                await _jobCategories.ReplaceOneAsync(c => c.Id == id, updatedJobCategory);
                return updatedJobCategory;
            }
            else
            {
                return null;
            }
        }

        // Xóa Job Category
        public async Task<JobCategory> DeleteJobCategory(string id)
        {
            var categoryToDelete = await _jobCategories.Find(j => j.Id == id).FirstOrDefaultAsync();

            if (categoryToDelete != null)
            {
                await _jobCategories.DeleteOneAsync(c => c.Id == id);
                return categoryToDelete;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
