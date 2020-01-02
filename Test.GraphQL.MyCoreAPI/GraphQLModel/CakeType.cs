using GraphQL.Types;
using Test.GraphQL.MyCoreAPI.Models;

namespace Test.GraphQL.MyCoreAPI.GraphQLModel
{
    public class CakeType : ObjectGraphType<Cake>
    {
        public CakeType()
        {
            Field(_ => _.Id);
            Field(_ => _.Name);
            Field(_ => _.Cost);
        }
    }
}
