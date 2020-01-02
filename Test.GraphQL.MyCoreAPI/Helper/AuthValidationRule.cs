using System.Linq;
using System.Security.Claims;
using GraphQL.Language.AST;
using GraphQL.Validation;
using Test.GraphQL.MyCoreAPI.Extens;

namespace Test.GraphQL.MyCoreAPI.Helper
{
    public class AuthValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            var userContext = context.UserContext as ClaimsPrincipal;
            var authenticated = userContext?.Identity?.IsAuthenticated ?? false;


            return new EnterLeaveListener(_ =>
            {
                _.Match<Operation>(op =>
                {
                    if (op.OperationType == OperationType.Query && !authenticated)
                    {
                        context.ReportError(new ValidationError(
                            context.OriginalQuery,
                            "auth-required",
                            $"Authorization is required to access {op.Name}.",
                            op));
                    }
                });

                // this could leak info about hidden fields in error messages
                // it would be better to implement a filter on the schema so it
                // acts as if they just don't exist vs. an auth denied error
                // - filtering the schema is not currently supported
                _.Match<Field>(fieldAst =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();
                    var claims = userContext.Claims.Select(_ => _.Value).ToList();
                    if (fieldDef.RequiresPermission() &&
                        (!authenticated || !fieldDef.CanAccess(claims)))
                    {
                        context.ReportError(new ValidationError(
                            context.OriginalQuery,
                            "auth-required",
                            $"You are not authorized to run this query.",
                            fieldAst));
                    }
                });
            });
        }
    }
}
