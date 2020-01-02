using System.Collections.Generic;
using Test.GraphQL.MyCoreAPI.Models;

namespace Test.GraphQL.MyCoreAPI.Repos
{
    public interface ICakeRepository
    {
        List<Cake> GetCakes();
    }
}
