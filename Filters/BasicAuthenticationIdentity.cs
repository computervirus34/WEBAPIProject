using System.Security.Principal;

namespace WEBAPIProject.Filters
{
    /// <summary>  
    /// Basic Authentication identity  
    /// </summary>  
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        /// <summary>  
        /// Get/Set for word  
        /// </summary>  
        public string word
        {
            get;
            set;
        }
        /// <summary>  
        /// Get/Set for UserName  
        /// </summary>  
        public string UserName
        {
            get;
            set;
        }
        /// <summary>  
        /// Get/Set for UserId  
        /// </summary>  
        public int UserId
        {
            get;
            set;
        }

        /// <summary>  
        /// Basic Authentication Identity Constructor  
        /// </summary>  
        /// <param name="userName"></param>  
        /// <param name="word"></param>  
        public BasicAuthenticationIdentity(string userName, string word) : base(userName, "Basic")
        {
            this.word = word;
            UserName = userName;
        }
    }
}
