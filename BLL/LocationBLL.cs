using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class LocationBLL
    {
        private readonly LocationService _locationService;

        public LocationBLL()
        {
            _locationService = new LocationService();
        }

        // Lấy danh sách Locations
        public async Task<List<Location>> GetLocations(
            Expression<Func<Location, bool>> filter = null,
            Func<IQueryable<Location>, IOrderedQueryable<Location>> orderBy = null)
        {
            return await _locationService.GetLocations(filter, orderBy);
        }

        // Thêm mới Location
        public async Task<Location> CreateLocation(Location location)
        {
            return await _locationService.CreateLocation(location);
        }

        // Cập nhật Location
        public async Task<Location> UpdateLocation(string id, Location updatedLocation)
        {
            return await _locationService.UpdateLocation(id, updatedLocation);
        }

        // Xóa Location
        public async Task<Location> DeleteLocation(string id)
        {
            return await _locationService.DeleteLocation(id);
        }
    }
}
