using HasatPiyasa.Core.Utilities.Results;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Business.Abstract
{
    public interface IUserClaimService
    {
        Task<NIslemSonuc<UserClaims>> CreateUserClaim(UserClaims sube);
        NIslemSonuc<List<UserClaims>> ListUserClaims();
        NIslemSonuc<UserClaims> GetUserClaim(int id);
        Task<NIslemSonuc<UserClaims>> UpdateUserClaim(UserClaims sube);
        Task<NIslemSonuc<UserClaims>> GetUserClaimTable(int value);
    }
}
