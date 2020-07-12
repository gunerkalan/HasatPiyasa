using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Entity.DataAccess.EntityFrameworkCore;
using HasatPiyasa.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace HasastPiyasa.DataAccess.Concrete
{
    public class EfBolgeDal : EfEntityRepositoryBase<Bolges>, IBolgeDal
    {
        
    }
}
