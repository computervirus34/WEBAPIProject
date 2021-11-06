using System.Threading;
using System.Web.Http.Controllers;
using BusinessServices;
using WEBAPIProject.Filters;

namespace WEBAPIProject.Filters
{
    /// <summary>  
    /// Custom Authentication Filter Extending basic Authentication  
    /// </summary>  
    public class APIAuthenticationFilter : GenericAuthenticationFilter
    {
        /// <summary>  
        /// Default Authentication Constructor  
        /// </summary>  
        public APIAuthenticationFilter() { }

        /// <summary>  
        /// AuthenticationFilter constructor with isActive parameter  
        /// </summary>  
        /// <param name="isActive"></param>  
        public APIAuthenticationFilter(bool isActive) : base(isActive) { }

        /// <summary>  
        /// Protected overriden method for authorizing user  
        /// </summary>  
        /// <param name="username"></param>  
        /// <param name="word"></param>  
        /// <param name="actionContext"></param>  
        /// <returns></returns>  
        protected override bool OnAuthorizeUser(string username, string word, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IUserServices)) as IUserServices;
            if (provider != null)
            {
                var userId = provider.Authenticate(username, word);
                if (userId > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null) basicAuthenticationIdentity.UserId = userId;
                    return true;
                }
            }
            return false;
        }
    }
}
