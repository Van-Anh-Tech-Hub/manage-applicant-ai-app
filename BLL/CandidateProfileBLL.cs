using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class CandidateProfileBLL
    {
        private readonly CandidateProfileService _candidateProfileService;

        public CandidateProfileBLL()
        {
            _candidateProfileService = new CandidateProfileService();
        }

        public async Task<List<CandidateProfile>> GetCandidateProfiles(
            Expression<Func<CandidateProfile, bool>> filter = null,
            Func<IQueryable<CandidateProfile>, IOrderedQueryable<CandidateProfile>> orderBy = null)
        {
            return await _candidateProfileService.GetCandidateProfiles(filter, orderBy);
        }

        public async Task<CandidateProfile> GetCandidateProfile(Expression<Func<CandidateProfile, bool>> filter = null)
        {
            return await _candidateProfileService.GetCandidateProfile(filter);
        }

        public async Task<CandidateProfile> CreateCandidateProfile(CandidateProfile profile)
        {
            return await _candidateProfileService.CreateCandidateProfile(profile);
        }

        public async Task<CandidateProfile> UpdateCandidateProfile(string id, CandidateProfile updatedProfile)
        {
            return await _candidateProfileService.UpdateCandidateProfile(id, updatedProfile);
        }

        public async Task<CandidateProfile> DeleteCandidateProfile(string id)
        {
            return await _candidateProfileService.DeleteCandidateProfile(id);
        }
    }
}
