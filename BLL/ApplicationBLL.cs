using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class ApplicationBLL
    {
        private readonly ApplicationService _applicationService;

        public ApplicationBLL()
        {
            _applicationService = new ApplicationService();
        }

        #region CRUD Methods for Application

        // Lấy danh sách ứng tuyển với filter và order
        public async Task<List<Application>> GetApplications(
            Expression<Func<Application, bool>> filter = null,
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = null)
        {
            return await _applicationService.GetApplications(filter, orderBy);
        }

        // Lấy một ứng tuyển theo filter
        public async Task<Application> GetApplication(Expression<Func<Application, bool>> filter = null)
        {
            return await _applicationService.GetApplication(filter);
        }

        // Thêm mới một ứng tuyển
        public async Task<Application> CreateApplication(Application application)
        {
            return await _applicationService.CreateApplication(application);
        }

        // Cập nhật thông tin ứng tuyển
        public async Task<Application> UpdateApplication(string id, Application updatedApplication)
        {
            return await _applicationService.UpdateApplication(id, updatedApplication);
        }

        // Xóa ứng tuyển theo id
        public async Task<Application> DeleteApplication(string id)
        {
            return await _applicationService.DeleteApplication(id);
        }

        #endregion
    }
}
