using ProjectA_B_UWP.Models;
using ProjectA_B_UWP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA_B_UWP.Data
{
    public class ContingentRepository : IContingentRepository
    {
        private readonly HttpClient client = new HttpClient();

        public ContingentRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Contingent>> GetContingents()
        {
            HttpResponseMessage response = await client.GetAsync("api/Contingent");
            if (response.IsSuccessStatusCode)
            {
                List<Contingent> Contingents = await response.Content.ReadAsAsync<List<Contingent>>();
                return Contingents;
            }
            else
            {
                throw new Exception("Could not access the list of Contingents.");
            }
        }
        public async Task<Contingent> GetContingent(int ContingentID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Contingent/{ContingentID}");
            if (response.IsSuccessStatusCode)
            {
                Contingent Contingent = await response.Content.ReadAsAsync<Contingent>();
                return Contingent;
            }
            else
            {
                throw new Exception("Could not access that Contingent.");
            }
        }

        public async Task AddContingent(Contingent contingentToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/Contingent", contingentToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateContingent(Contingent contingentToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/Contingent/{contingentToUpdate.ID}", contingentToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteContingent(Contingent contingentToDelete)
        {
            var response = await client.DeleteAsync($"/api/Contingent/{contingentToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

    }
}
