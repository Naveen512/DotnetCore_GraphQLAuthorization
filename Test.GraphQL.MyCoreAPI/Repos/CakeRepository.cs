using System.Collections.Generic;
using System.Linq;
using Test.GraphQL.MyCoreAPI.Data;
using Test.GraphQL.MyCoreAPI.Models;

namespace Test.GraphQL.MyCoreAPI.Repos
{
    public class CakeRepository : ICakeRepository
    {
        private readonly BakeryContext _bakeryContext;
        public CakeRepository(BakeryContext bakeryContext)
        {
            _bakeryContext = bakeryContext;
        }

        public List<Cake> GetCakes()
        {
            return _bakeryContext.Cake.ToList();
        }
    }
}
