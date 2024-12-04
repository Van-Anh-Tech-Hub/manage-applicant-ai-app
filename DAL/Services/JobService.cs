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
    public class JobService
    {
        private readonly IMongoCollection<Job> _jobs;
        private readonly MongoDbContext _dbContext;

        public JobService()
        {
            _dbContext = new MongoDbContext();
            _jobs = _dbContext.Jobs;
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

        // Xóa Job
        public async Task<Job> DeleteJob(string id)
        {
            var jobToDelete = await _jobs.Find(j => j.Id == id).FirstOrDefaultAsync();

            if (jobToDelete != null)
            {
                await _jobs.DeleteOneAsync(j => j.Id == id);
                return jobToDelete;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
