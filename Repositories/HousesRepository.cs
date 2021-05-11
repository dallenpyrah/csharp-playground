using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using csharp_playground.Models;
using Dapper;

namespace csharp_playground.Repositories
{
    public class HousesRepository
    {

        private readonly IDbConnection _db;

        public HousesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<House> getAllHouses()
        {
            string sql = @"
            SELECT 
            house.*,
            profile.*
            FROM houses house
            JOIN profiles profile WHERE house.creatorId = profile.id;";
            return _db.Query<House, Profile, House>(sql, (house, profile) =>
            {
                house.Creator = profile;
                return house;
            }, splitOn: "id");
        }

        internal House getHouseById(int id)
        {
            string sql = @"
            SELECT
            house.*,
            profile.*
            FROM houses house
            JOIN profiles profile WHERE house.creatorId = profile.id
            WHERE id = @id;";
            return _db.Query<House, Profile, House>(sql, (house, profile) =>
            {
                house.Creator = profile;
                return house;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal House createHouse(House newHouse)
        {
            string sql = @"
            INSERT INTO houses
            (pricePerNight, squareFeet, location, bedrooms, bathrooms, guestLimit, image, reviews, dateAvaliable, superHost, creatorId)
            VAULES
            (@PricePerNightm, @SquareFeet, @Location, @Bedrooms, @Bathrooms, @GuestLimit, @Image, @Reviews, @DateAvaliable, @SuperHoust, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newHouse);
            newHouse.Id = id;
            return newHouse;
        }

        internal House editHouse(House editHouse)
        {
            string sql = @"
            UPDATE houses
                SET
                    pricePerNight = @PricePerNight, 
                    squareFeet = @SquareFeet,
                    location = @Location,
                    bedrooms = @Bedrooms, 
                    bathrooms = @Bathrooms, 
                    guestLimit = @GuestLimit,
                    image = @Image, 
                    reviews = @Reviews, 
                    dateAvaliable = @DateAvaliable,
                    superHost = @SuperHost
                    WHERE id = @id;";
            return _db.QueryFirstOrDefault<House>(sql, editHouse);
        }

        internal House deleteHouse(int id)
        {
            string sql = "DELETE FROM houses WHERE id = @id LIMIT 1;";
            return _db.QueryFirstOrDefault<House>(sql, new { id });
        }
    }
}