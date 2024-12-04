using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class CompanyBLL
    {
        private readonly CompanyService _companyService;

        public CompanyBLL()
        {
            _companyService = new CompanyService();
        }

        public async Task<List<Company>> GetCompanies(
            Expression<Func<Company, bool>> filter = null,
            Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null)
        {
            return await _companyService.GetCompanies(filter, orderBy);
        }

        public async Task<Company> GetCompany(Expression<Func<Company, bool>> filter = null)
        {
            return await _companyService.GetCompany(filter);
        }

        public async Task<Company> CreateCompany(Company company)
        {
            return await _companyService.CreateCompany(company);
        }

        public async Task<Company> UpdateCompany(string id, Company updatedCompany)
        {
            return await _companyService.UpdateCompany(id, updatedCompany);
        }

        public async Task<Company> DeleteCompany(string id)
        {
            return await _companyService.DeleteCompany(id);
        }

    }
}
