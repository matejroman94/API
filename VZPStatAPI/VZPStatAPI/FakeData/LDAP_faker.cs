using System.Collections.Generic;

namespace VZPStatAPI.FakeData
{
    public class LDAP_faker
    {
        public List<Dictionary<string,string>> GetFakeRolesAndWorkPlaces(List<string> VZPCodes)
        {
            var random = new Bogus.Randomizer();
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            int count = random.Int(1,100);

            string[] roles = new string[] 
            { 
                "Administrator",
                "Manazer",
                "Marketingovy_manazer",
                "Frontoffice_pracovnik "
            };
            string role = string.Empty;
            string vzpcode = string.Empty;  
            for (int i = 0; i < count; i++)
            {
                Dictionary<string, string> res = new Dictionary<string, string>();

                role = roles[random.Int(0,3)];
                res.Add("applicationright", role);

                vzpcode = VZPCodes[random.Int(0, VZPCodes.Count-1)];
                res.Add("workplace", vzpcode);

                if(!results.Contains(res)) { results.Add(res); }
            }    
            return results;
        }
    }
}
