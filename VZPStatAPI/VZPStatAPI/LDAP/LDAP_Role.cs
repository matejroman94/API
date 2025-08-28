namespace VZPStatAPI.LDAP
{
    public class LDAP_Role
    {
        public int Role_id { get; set; } = -1;

        public List<string> VzpCodes { get; set; } = new List<string>();
    }
}
