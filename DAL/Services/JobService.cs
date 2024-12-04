using MongoDB.Driver;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using static System.Net.Mime.MediaTypeNames;
using MongoDB.Bson;

namespace DAL.Services
{
    public class JobService
    {
        private readonly IMongoCollection<Job> _jobs;
        private readonly MongoDbContext _dbContext;

        public JobService()
        {
            _dbContext = new MongoDbContext();
            _jobs = _dbContext.Jobs;
        }

        public async Task<List<Job>> GetJobs(string title, string locationId, string categoryId)
        {
            var filterBuilder = Builders<Job>.Filter;
            var filter = filterBuilder.Eq(j => j.IsDel, false);

            if (!string.IsNullOrEmpty(title))
                filter &= filterBuilder.Regex(j => j.Title, new BsonRegularExpression(title, "i"));

            if (!string.IsNullOrEmpty(locationId))
                filter &= filterBuilder.Eq(j => j.LocationId, locationId);

            if (!string.IsNullOrEmpty(categoryId))
                filter &= filterBuilder.Eq(j => j.CategoryId, categoryId);

            return await _jobs.Find(filter).ToListAsync();
        }

        public async Task<Job> GetJobById(string jobId)
        {
            try
            {
                // Tìm công việc theo ID
                var job = await _jobs.Find(j => j.Id == jobId && !j.IsDel).FirstOrDefaultAsync();
                return job;
            }
            catch (Exception ex)
            {
                // Log lỗi và xử lý
                Console.WriteLine($"Error fetching job by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<Job> DeleteJob(string id)
        {
            var jobToDelete = await _jobs.Find(j => j.Id == id && !j.IsDel).FirstOrDefaultAsync();

            if (jobToDelete != null)
            {
                // Cập nhật trường IsDel thành true thay vì xóa công việc
                var update = Builders<Job>.Update.Set(j => j.IsDel, true);
                await _jobs.UpdateOneAsync(j => j.Id == id, update);
                return jobToDelete;
            }
            else
            {
                return null;
            }
        }


        #region CRUD Methods for Job

        // Lấy danh sách Jobs
        public async Task<List<Job>> GetJobs(
            Expression<Func<Job, bool>> filter = null,
            Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null)
        {
            var query = _jobs.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<Job>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        // Lấy Job theo filter
        public async Task<Job> GetJob(Expression<Func<Job, bool>> filter = null)
        {
            var query = _jobs.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        // Thêm mới Job
        public async Task<Job> CreateJob(Job job)
        {
            await _jobs.InsertOneAsync(job);
            return job;
        }

        // Cập nhật Job
        public async Task<Job> UpdateJob(string id, Job updatedJob)
        {
            var existingJob = await _jobs.Find(j => j.Id == id).FirstOrDefaultAsync();

            if (existingJob != null)
            {
                updatedJob.Id = id;
                await _jobs.ReplaceOneAsync(j => j.Id == id, updatedJob);
                return updatedJob;
            }
            else
            {
                return null;
            }
        }

     

        #endregion
    }
}
