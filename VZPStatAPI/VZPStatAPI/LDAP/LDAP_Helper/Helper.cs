using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Repository.Interfaces;
using Repository.Repository;
using Serilog;

namespace VZPStatAPI.LDAP.LDAP_Helper
{
    public class Helper
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        private LDAP_Helper_Client _client { get; set; }

        private string _login { get; set; }

        public Helper(IConfiguration configuration,
            IUnitOfWork unitOfWork,
            string login)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _login = login;

            string username = _configuration["LDAP:User"];
            string password = _configuration["LDAP:Pwd"];
            string ldap_url = _configuration["LDAP:URL"];
            _client = new LDAP_Helper_Client(ldap_url, username, password);
        }

        public async Task<LDAP_response> Get_LDAP_ResponseAsync()
        {
            LDAP_response lDAP_Response = new LDAP_response();

            string baseDN = _configuration["LDAP:baseDN"];
            string ldapFilter = _configuration["LDAP:ldapFilter"];
            ldapFilter = ldapFilter.Replace("$", _login);

            var searchResult = _client.GetSearch(baseDN, ldapFilter);
            if(searchResult == null) return lDAP_Response;

            int LDAP_id = -1;
            string LDAP_vzpCode = string.Empty;
            foreach (Dictionary<string, string> d in searchResult)
            {             
                var result = await _client.Get_LDAP_ID_AND_VZPCODE(_unitOfWork, d);
                LDAP_id = result.LDAP_ID;
                LDAP_vzpCode = result.LDAP_vzpCode;

                if (Check_LDAP_ID_VZPcode_If_Empty(LDAP_id, LDAP_vzpCode))
                {
                    lDAP_Response = _client.Set_LDAP_Response(_unitOfWork, lDAP_Response, LDAP_id, LDAP_vzpCode);
                }
            }
            if (lDAP_Response.RoleIds.Count > 0)
            {
                lDAP_Response.RoleIds = lDAP_Response.RoleIds.OrderBy(x => x.Role_id).ToList();
            }
            return lDAP_Response;
        }

        private bool Check_LDAP_ID_VZPcode_If_Empty(int ldapID,string vzpCode)
        {
            return (ldapID > 0 && !string.IsNullOrWhiteSpace(vzpCode));
        }
    }
}
