using GraphQL.Types;
using Test.GraphQL.MyCoreAPI.Extens;
using Test.GraphQL.MyCoreAPI.Repos;

namespace Test.GraphQL.MyCoreAPI.GraphQLModel
{
    public class RootQueryType : ObjectGraphType
    {
        public RootQueryType(ICakeRepository cakeRepository)
        {
            Field<ListGraphType<CakeType>>("allCakes", resolve: context =>
             {
                 return cakeRepository.GetCakes();
             }).AddPermissions("user");

            Field<ListGraphType<CakeType>>("cakesList", resolve: context =>
            {
                return cakeRepository.GetCakes();
            }).AddPermissions("super admin");
        }
    }
}
