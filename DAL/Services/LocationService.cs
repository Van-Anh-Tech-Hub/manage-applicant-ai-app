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
    public class LocationService
    {
        private readonly IMongoCollection<Location> _locations;
        private readonly MongoDbContext _dbContext;

        public LocationService()
        {
            _dbContext = new MongoDbContext();
            _locations = _dbContext.Locations;
        }

        #region CRUD Methods for Location

        // Lấy danh sách Locations
        public async Task<List<Location>> GetLocations(
            Expression<Func<Location, bool>> filter = null,
            Func<IQueryable<Location>, IOrderedQueryable<Location>> orderBy = null)
        {
            var query = _locations.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<Location>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        // Lấy Location theo filter
        public async Task<Location> GetLocation(Expression<Func<Location, bool>> filter = null)
        {
            var query = _locations.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        // Thêm mới Location
        public async Task<Location> CreateLocation(Location location)
        {
            await _locations.InsertOneAsync(location);
            return location;
        }

        // Cập nhật Location
        public async Task<Location> UpdateLocation(string id, Location updatedLocation)
        {
            var existingLocation = await _locations.Find(l => l.Id == id).FirstOrDefaultAsync();

            if (existingLocation != null)
            {
                updatedLocation.Id = id;
                await _locations.ReplaceOneAsync(l => l.Id == id, updatedLocation);
                return updatedLocation;
            }
            else
            {
                return null;
            }
        }

        // Xóa Location
        public async Task<Location> DeleteLocation(string id)
        {
            var locationToDelete = await _locations.Find(l => l.Id == id).FirstOrDefaultAsync();

            if (locationToDelete != null)
            {
                await _locations.DeleteOneAsync(l => l.Id == id);
                return locationToDelete;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
