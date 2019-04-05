using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Wiz.Template.API.Swagger
{
    public static class MyApiConventions
    {
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(200)]
        public static void List([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request) { }

        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request) { }

        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request) { }

        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public static void Put([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request) { }

        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public static void Patch([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request) { }

        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request) { }
    }
}
