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
    public class JobTypeService
    {
        private readonly IMongoCollection<JobType> _jobTypes;
        private readonly MongoDbContext _dbContext;

        public JobTypeService()
        {
            _dbContext = new MongoDbContext();
            _jobTypes = _dbContext.JobTypes;
        }

        #region CRUD Methods for JobType

        // Lấy danh sách Job Types
        public async Task<List<JobType>> GetJobTypes(
            Expression<Func<JobType, bool>> filter = null,
            Func<IQueryable<JobType>, IOrderedQueryable<JobType>> orderBy = null)
        {
            var query = _jobTypes.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<JobType>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        // Lấy JobType theo filter
        public async Task<JobType> GetJobType(Expression<Func<JobType, bool>> filter = null)
        {
            var query = _jobTypes.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        // Thêm mới Job Type
        public async Task<JobType> CreateJobType(JobType jobType)
        {
            await _jobTypes.InsertOneAsync(jobType);
            return jobType;
        }

        // Cập nhật Job Type
        public async Task<JobType> UpdateJobType(string id, JobType updatedJobType)
        {
            var existingJobType = await _jobTypes.Find(j => j.Id == id).FirstOrDefaultAsync();

            if (existingJobType != null)
            {
                updatedJobType.Id = id;
                await _jobTypes.ReplaceOneAsync(j => j.Id == id, updatedJobType);
                return updatedJobType;
            }
            else
            {
                return null;
            }
        }

        // Xóa Job Type
        public async Task<JobType> DeleteJobType(string id)
        {
            var jobTypeToDelete = await _jobTypes.Find(j => j.Id == id).FirstOrDefaultAsync();

            if (jobTypeToDelete != null)
            {
                await _jobTypes.DeleteOneAsync(j => j.Id == id);
                return jobTypeToDelete;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
