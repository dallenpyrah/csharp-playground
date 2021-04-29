using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using csharp_playground.Models;
using Dapper;

namespace csharp_playground.Repositories
{
    public class CarsRepository
    {

        private readonly IDbConnection _db;

        public CarsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Car> GetAllCars()
        {
            string sql = @"
            SELECT
            car.*,
            profile.*
            FROM cars car
            JOIN profiles profile ON car.creatorId = profile.id;";
            return _db.Query<Car, Profile, Car>(sql, (car, profile) =>
            {
                car.Creator = profile;
                return car;
            }, splitOn: "id");
        }

        internal Car GetOneCar(int id)
        {
            string sql = @"
            SELECT
            car.*,
            profile.*
            FROM cars car 
            JOIN profiles profile ON car.creatorId = profile.id
            WHERE car.id = @id;";
            return _db.Query<Car, Profile, Car>(sql, (c, p) =>
            {
                c.Creator = p;
                return c;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Car CreateOneCar(Car newCar)
        {
            string sql = @"
            INSERT INTO cars
            (brand, model, color, price, topSpeed, year, creatorId)
            VAULES
            (@Brand, @Model, @Color, @Price, @TopSpeed, @Year, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newCar);
            newCar.Id = id;
            return newCar;
        }

        internal Car EditOneCar(Car editedCar)
        {
            string sql = @"
            UPDATE cars
                SET
                brand = @Brand, 
                model = @Model,
                color = @Color,
                price = @Price,
                topSpeed = @TopSpeed,
                year = @Year
                WHERE id = @id;";
            return _db.QueryFirstOrDefault<Car>(sql, editedCar);
        }

        internal Car DeleteOneCar(int id)
        {
            string sql = "DELETE FROM cars WHERE id = @id LIMIT 1;";
            return _db.QueryFirstOrDefault<Car>(sql, new { id });
        }
    }
}