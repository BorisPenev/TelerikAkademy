using System.Runtime.Serialization;

namespace BloggingSystem.WebApi.Models
{
    [DataContract]
    public class LoggedUserModel
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }
    }
}