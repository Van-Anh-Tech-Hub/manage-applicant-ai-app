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
    public class CompanyService
    {
        private readonly MongoDbContext _dbContext;
        private readonly IMongoCollection<Company> _companies;
        private readonly IMongoCollection<Location> _locations;
        public CompanyService()
        {
            _dbContext = new MongoDbContext();
            _companies = _dbContext.Companies;
            _locations = _dbContext.Locations;
        }

        public async Task<List<Company>> GetCompanies(
                Expression<Func<Company, bool>> filter = null,
                Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null)
        {
            var query = _companies.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<Company>)orderBy(query);
            }

            var companies = await query.ToListAsync();

            foreach (var company in companies)
            {
                if (!string.IsNullOrEmpty(company.LocationId))
                {
                    var location = await _locations.Find(l => l.Id == company.LocationId).FirstOrDefaultAsync();
                    company.Location = location;
                }
            }

            return companies;
        }

        public async Task<Company> GetCompany(Expression<Func<Company, bool>> filter = null)
        {
            var query = _companies.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var company = await _companies.AsQueryable().FirstOrDefaultAsync(filter);

            if (company != null && !string.IsNullOrEmpty(company.LocationId))
            {
                var location = await _locations.Find(l => l.Id == company.LocationId).FirstOrDefaultAsync();
                company.Location = location;
            }

            return company;
        }

        public async Task<Company> CreateCompany(Company company)
        {
            await _companies.InsertOneAsync(company);
            return company;
        }

        public async Task<Company> UpdateCompany(string id, Company updatedCompany)
        {
            var existingCompany = await _companies.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (existingCompany != null)
            {
                updatedCompany.Id = id;
                await _companies.ReplaceOneAsync(c => c.Id == id, updatedCompany);
                return updatedCompany;
            }
            else
            {
                return null;
            }
        }

        public async Task<Company> DeleteCompany(string id)
        {
            var companyToDelete = await _companies.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (companyToDelete != null)
            {
                await _companies.DeleteOneAsync(c => c.Id == id);
                return companyToDelete;
            }
            else
            {
                return null;
            }
        }
    }
}