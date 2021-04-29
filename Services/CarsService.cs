using System;
using System.Collections.Generic;
using csharp_playground.Models;
using csharp_playground.Repositories;

namespace csharp_playground.Services
{
    public class CarsService
    {

        private readonly CarsRepository _crepo;

        public CarsService(CarsRepository crepo)
        {
            _crepo = crepo;
        }

        internal IEnumerable<Car> GetAllCars()
        {
            return _crepo.GetAllCars();
        }

        internal Car GetOneCar(int id)
        {
            Car car = _crepo.GetOneCar(id);
            if(car == null){
                throw new SystemException("Invalid Id or Car does not exist.");
            }
            return car;
        }

        internal Car CreateOneCar(Car newCar)
        {
            return _crepo.CreateOneCar(newCar);
        }

        internal Car EditOneCar(Car editedCar)
        {
            Car current = GetOneCar(editedCar.Id);
            if(current.CreatorId != editedCar.CreatorId){
                throw new SystemException("You are not the owner of this car you can't edit this.");
            }
            if(current == null){
                throw new SystemException("Invalid Id or Car does not exist");
            }
            editedCar.Brand = editedCar.Brand != null ? editedCar.Brand : current.Brand;
            editedCar.Color = editedCar.Color != null ? editedCar.Color : current.Color;
            editedCar.Model = editedCar.Model != null ? editedCar.Model : current.Model;
            editedCar.Price = editedCar.Price > 0 ? editedCar.Price : current.Price;
            editedCar.TopSpeed = editedCar.TopSpeed > 0 ? editedCar.TopSpeed : current.TopSpeed;
            return _crepo.EditOneCar(editedCar);
        }

        internal Car DeleteOneCar(int id, string userInfoId)
        {
            Car current = GetOneCar(id);
            if(current == null){
                throw new SystemException("Invalid Id or Car does not exist.");
            }
            if(current.CreatorId != userInfoId){
                throw new SystemException("You are not the creator of this you can not delete this.");
            }
            return _crepo.DeleteOneCar(id);
        }
    }
}