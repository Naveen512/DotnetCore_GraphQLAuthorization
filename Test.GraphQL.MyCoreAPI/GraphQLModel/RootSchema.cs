using GraphQL;
using GraphQL.Types;

namespace Test.GraphQL.MyCoreAPI.GraphQLModel
{
    public class RootSchema : Schema, ISchema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQueryType>();
        }
    }
}
