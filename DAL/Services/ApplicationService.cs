using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ApplicationService
    {
        private readonly IMongoCollection<Application> _applications;
        private readonly MongoDbContext _dbContext;

        public ApplicationService()
        {
            _dbContext = new MongoDbContext();
            _applications = _dbContext.Applications;
        }

        #region CRUD Methods for Application

        // Lấy danh sách ứng tuyển với filter và order
        public async Task<List<Application>> GetApplications(
            Expression<Func<Application, bool>> filter = null,
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = null)
        {
            var query = _applications.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<Application>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        // Lấy một ứng tuyển theo filter
        public async Task<Application> GetApplication(Expression<Func<Application, bool>> filter = null)
        {
            var query = _applications.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        // Thêm mới một ứng tuyển
        public async Task<Application> CreateApplication(Application application)
        {
            await _applications.InsertOneAsync(application);
            return application;
        }

        // Cập nhật thông tin ứng tuyển
        public async Task<Application> UpdateApplication(string id, Application updatedApplication)
        {
            var existingApplication = await _applications.Find(a => a.Id == id).FirstOrDefaultAsync();

            if (existingApplication != null)
            {
                updatedApplication.Id = id;
                await _applications.ReplaceOneAsync(a => a.Id == id, updatedApplication);
                return updatedApplication;
            }
            else
            {
                return null;
            }
        }

        // Xóa ứng tuyển theo id
        public async Task<Application> DeleteApplication(string id)
        {
            var applicationToDelete = await _applications.Find(a => a.Id == id).FirstOrDefaultAsync();

            if (applicationToDelete != null)
            {
                await _applications.DeleteOneAsync(a => a.Id == id);
                return applicationToDelete;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
