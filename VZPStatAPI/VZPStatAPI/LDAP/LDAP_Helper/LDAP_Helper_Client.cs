using Domain.Models;
using Repository.Interfaces;
using Repository.Repository;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using VZPStatAPI.Filter;
using VZPStatAPI.Helpers;

namespace VZPStatAPI.LDAP.LDAP_Helper
{
    public class LDAP_Helper_Client
    {
        public string URL { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        private LDAP_Client? _client { get; set; } 

        public LDAP_Helper_Client() { }

        public LDAP_Helper_Client(string url, string username, string password)
        {
            URL = url;
            UserName = username;
            Password = password;
            _client = new LDAP_Client(UserName, Password, URL);
        }

        public bool IsInit() { return _client != null; }

        public void Init(string url, string username, string password)
        {
            URL = url;
            UserName = username;
            Password = password;
            _client = new LDAP_Client(UserName, Password, URL);
        }

        public IEnumerable<Dictionary<string,string>>? GetSearch(string baseDN, string ldapFilter)
        {
            if (IsInit() is false) return null;
            return _client?.search(baseDN, ldapFilter);
        }

        public async Task<(int LDAP_ID, string LDAP_vzpCode)> Get_LDAP_ID_AND_VZPCODE(IUnitOfWork unitOfWork, Dictionary<string, string> search)
        {
            (int LDAP_ID, string LDAP_vzpCode) result = (-1, string.Empty);

            if (unitOfWork is null) return result;
            if (search is null) return result;

            foreach (KeyValuePair<string, string> entry in search)
            {
                if (entry.Key.Equals("applicationright"))
                {
                    Role? role = await unitOfWork.Roles.GetFirstOrDefaultAsync(x => x.Name.Equals(entry.Value));
                    if (role is not null)
                    {
                        result.LDAP_ID = role.RoleId;
                    }
                }
                else if (entry.Key.Equals("workplace"))
                {
                    result.LDAP_vzpCode = entry.Value;
                }
            }
            return result;
        }

        public LDAP_response Set_LDAP_Response(IUnitOfWork unitOfWork, LDAP_response lDAP_Response, int LDAP_id, string LDAP_vzpCode)
        { 
            if (unitOfWork is null) return lDAP_Response;

            LDAP_Role? role = lDAP_Response.RoleIds.Where(x => x.Role_id == LDAP_id).FirstOrDefault();
            if (role is null)
            {
                LDAP_Role lDAP_Role = new LDAP_Role();
                lDAP_Role.Role_id = LDAP_id;
                lDAP_Role.VzpCodes = ProcessVZPCodes(unitOfWork, lDAP_Role.VzpCodes, LDAP_vzpCode);
                lDAP_Response.RoleIds.Add(lDAP_Role);
            }
            else
            {
                var index = lDAP_Response.RoleIds.FindIndex(c => c.Role_id == role.Role_id);
                lDAP_Response.RoleIds[index].VzpCodes = ProcessVZPCodes(unitOfWork, lDAP_Response.RoleIds[index].VzpCodes, LDAP_vzpCode);
            }
            return lDAP_Response;
        }

        public List<string> ProcessVZPCodes(IUnitOfWork unitOfWork, List<string> listVzpCode, string vzpCode)
        {
            if (unitOfWork is null) return listVzpCode.OrderBy(q => q).ToList();

            int vzpCodeInt = int.TryParse(vzpCode, out int regionIdVal) ? regionIdVal : -1;
            Region? region = unitOfWork.Regions.GetFirstOrDefault(x => x.RegionId == vzpCodeInt);

            var filter = Repository.Pagination.Filter.AllRecords();
            if (region == null)
            {
                Branch? parentBranch = unitOfWork.Branches.GetFirstOrDefault(x => x.VZP_code.Equals(vzpCode));
                if(parentBranch is not null)
                {
                    List<string> vzpCodes = unitOfWork.Branches.GetAll(ref filter, x => x.ParentBranchId == parentBranch.BranchId).Select(x => x.VZP_code).ToList();
                    foreach(var code in vzpCodes)
                    {
                        if (!listVzpCode.Contains(code)) listVzpCode.Add(code);
                    }
                }
                if(!listVzpCode.Contains(vzpCode)) listVzpCode.Add(vzpCode);
            }
            else
            {
                IEnumerable<string> branchVzpCodes = unitOfWork.Branches.GetAll(ref filter, x => x.RegionId == vzpCodeInt).Select(x => x.VZP_code);
                foreach (var branchVzpCode in branchVzpCodes)
                {
                    if (!listVzpCode.Contains(branchVzpCode)) listVzpCode.Add(branchVzpCode);
                }
            }
            return listVzpCode.OrderBy(q => q).ToList();
        }
    }
}
