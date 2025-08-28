namespace VZPStatAPI.LDAP
{
    public class LDAP_response
    {
        public bool Success { get; set; } = false;

        public string Error { get; set; } = string.Empty;

        public List<LDAP_Role>  RoleIds { get; set; } = new List<LDAP_Role>();
    }
}
