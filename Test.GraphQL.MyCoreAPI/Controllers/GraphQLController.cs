using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.GraphQL.MyCoreAPI.Models;

namespace Test.GraphQL.MyCoreAPI.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IValidationRule _validationRule;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GraphQLController(
            ISchema schema, 
            IDocumentExecuter executer,
            IValidationRule validationRule,
            IHttpContextAccessor httpContextAccessor)
        {
            _schema = schema;
            _executer = executer;
            _validationRule = validationRule;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQueryDto query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.ValidationRules = new List<IValidationRule> { _validationRule };
                _.UserContext = _httpContextAccessor.HttpContext.User;
               
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return Problem(detail: result.Errors.Select(_ => _.Message).FirstOrDefault(), statusCode: 500);
            }
            return Ok(result.Data);
        }
    }
}
