using System;
using csharp_playground.Models;
using csharp_playground.Repositorys;

namespace csharp_playground.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;
        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }
        internal Profile GetOrCreateProfile(Profile userInfo)
        {
            Profile profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

        internal Profile GetProfileById(string id)
        {
            Profile profile = _repo.GetById(id);
            if (profile == null)
            {
                throw new Exception("Invalid Id");
            }
            return profile;
        }

    }
}