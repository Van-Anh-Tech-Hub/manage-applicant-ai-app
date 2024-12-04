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
    public class CandidateProfileService
    {
        private readonly IMongoCollection<CandidateProfile> _candidateProfiles;
        private readonly MongoDbContext _dbContext;

        public CandidateProfileService()
        {
            _dbContext = new MongoDbContext();
            _candidateProfiles = _dbContext.CandidateProfiles;
        }

        public async Task<List<CandidateProfile>> GetCandidateProfiles(
            Expression<Func<CandidateProfile, bool>> filter = null,
            Func<IMongoQueryable<CandidateProfile>, IOrderedQueryable<CandidateProfile>> orderBy = null)
        {
            var query = _candidateProfiles.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<CandidateProfile>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<CandidateProfile> GetCandidateProfile(Expression<Func<CandidateProfile, bool>> filter = null)
        {
            var query = _candidateProfiles.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<CandidateProfile> CreateCandidateProfile(CandidateProfile profile)
        {
            await _candidateProfiles.InsertOneAsync(profile);
            return profile;
        }

        public async Task<CandidateProfile> UpdateCandidateProfile(string id, CandidateProfile updatedProfile)
        {
            var existingProfile = await _candidateProfiles.Find(p => p.id == id).FirstOrDefaultAsync();

            if (existingProfile != null)
            {
                updatedProfile.id = id;
                await _candidateProfiles.ReplaceOneAsync(p => p.id == id, updatedProfile);
                return updatedProfile;
            }
            else
            {
                return null;
            }
        }

        public async Task<CandidateProfile> DeleteCandidateProfile(string id)
        {
            var profileToDelete = await _candidateProfiles.Find(p => p.id == id).FirstOrDefaultAsync();

            if (profileToDelete != null)
            {
                await _candidateProfiles.DeleteOneAsync(p => p.id == id);
                return profileToDelete;
            }
            else
            {
                return null;
            }
        }

    }
}
