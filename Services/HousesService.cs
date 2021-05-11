using System;
using System.Collections.Generic;
using csharp_playground.Models;
using csharp_playground.Repositories;

namespace csharp_playground.Services
{
    public class HousesService
    {
        private readonly HousesRepository _hrepo;
        internal IEnumerable<House> getAllHouses()
        {
            return _hrepo.getAllHouses();
        }

        internal House getHouseById(int id)
        {
            House house = _hrepo.getHouseById(id);
            if (house == null)
            {
                throw new SystemException("Invalid Id: No house found");
            }
            return house;
        }

        internal House createHouse(House newHouse)
        {
            return _hrepo.createHouse(newHouse);
        }

        internal House editHouse(House editHouse)
        {
            House current = getHouseById(editHouse.Id);
            if (current.CreatorId != editHouse.CreatorId)
            {
                throw new SystemException("You are not the creator you can not edit this.");
            }
            editHouse.Bathrooms = editHouse.Bathrooms > 0 ? editHouse.Bathrooms : current.Bathrooms;
            editHouse.Bedrooms = editHouse.Bedrooms > 0 ? editHouse.Bathrooms : current.Bedrooms;
            editHouse.DateAvaliable = editHouse.DateAvaliable != null ? editHouse.DateAvaliable : current.DateAvaliable;
            editHouse.GuestLimit = editHouse.GuestLimit > 0 ? editHouse.GuestLimit : current.GuestLimit;
            editHouse.Image = editHouse.Image != null ? editHouse.Image : current.Image;
            editHouse.Location = editHouse.Location != null ? editHouse.Location : current.Location;
            editHouse.PricePerNight = editHouse.PricePerNight > 0 ? editHouse.PricePerNight : current.PricePerNight;
            editHouse.Reviews = editHouse.Reviews >= 0 ? editHouse.Reviews : current.Reviews;
            editHouse.SqaureFeet = editHouse.SqaureFeet > 0 ? editHouse.SqaureFeet : current.SqaureFeet;
            editHouse.SuperHost = editHouse.SuperHost != current.SuperHost ? editHouse.SuperHost : current.SuperHost; 
            return _hrepo.editHouse(editHouse);
        }

        internal House deleteHouse(int id, string userId)
        {
            House current = getHouseById(id);
            if(current.CreatorId != userId){
                throw new SystemException("You are not the creator you can not delete this.");
            }
            return _hrepo.deleteHouse(id);
        }
    }
}