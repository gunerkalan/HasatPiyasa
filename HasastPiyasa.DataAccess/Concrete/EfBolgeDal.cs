using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Entity.DataAccess.EntityFrameworkCore;
using HasatPiyasa.Entity.Entity;

namespace HasastPiyasa.DataAccess.Concrete
{
    public class EfBolgeDal : EfEntityRepositoryBase<Bolges>, IBolgeDal
    {
    }
}
