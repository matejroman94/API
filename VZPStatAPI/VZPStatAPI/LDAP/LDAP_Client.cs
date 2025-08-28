using System.DirectoryServices.Protocols;
using System.Net;

namespace VZPStatAPI.LDAP
{
    public class LDAP_Client
    {
        public LDAP_Client(string username, string password, string url)
        {
            var credentials = new NetworkCredential(username, password);
            var serverId = new LdapDirectoryIdentifier(url, 389);

            connection = new LdapConnection(serverId, credentials);
            connection.SessionOptions.ProtocolVersion = 3;
            connection.AuthType = AuthType.Basic;
            connection.Bind();
        }

        /// <summary>
        /// Performs a search in the LDAP server. This method is generic in its return value to show the power
        /// of searches. A less generic search method could be implemented to only search for users, for instance.
        /// </summary>
        /// <param name="baseDn">The distinguished name of the base node at which to start the search</param>
        /// <param name="ldapFilter">An LDAP filter as defined by RFC4515</param>
        /// <returns>A flat list of dictionaries which in turn include attributes and the distinguished name (DN)</returns>
        public List<Dictionary<string, string>> search(string baseDn, string ldapFilter)
        {
            var request = new SearchRequest(baseDn, ldapFilter, SearchScope.Subtree, null);
            var response = (SearchResponse)connection.SendRequest(request);

            var result = new List<Dictionary<string, string>>();

            foreach (SearchResultEntry entry in response.Entries)
            {
                var dic = new Dictionary<string, string>();
                dic["DN"] = entry.DistinguishedName;

                foreach (string attrName in entry.Attributes.AttributeNames)
                {
                    //For simplicity, we ignore multi-value attributes
                    dic[attrName] = string.Join(",", entry.Attributes[attrName].GetValues(typeof(string)));
                }

                result.Add(dic);
            }

            return result;
        }

        private LdapConnection connection;
    }
}
