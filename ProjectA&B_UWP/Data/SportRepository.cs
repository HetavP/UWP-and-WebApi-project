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
    public class SportRepository : ISportRepository
    {
        private readonly HttpClient client = new HttpClient();

        public SportRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Sport>> GetSports()
        {
            HttpResponseMessage response = await client.GetAsync("api/Sport");
            if (response.IsSuccessStatusCode)
            {
                List<Sport> Sports = await response.Content.ReadAsAsync<List<Sport>>();
                return Sports;
            }
            else
            {
                throw new Exception("Could not access the list of Sports.");
            }
        }
        public async Task<Sport> GetSport(int SportID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Sport/{SportID}");
            if (response.IsSuccessStatusCode)
            {
                Sport Sport = await response.Content.ReadAsAsync<Sport>();
                return Sport;
            }
            else
            {
                throw new Exception("Could not access that Sport.");
            }
        }

        public async Task AddSport(Sport sportToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/Sport", sportToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateSport(Sport sportToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/Sport/{sportToUpdate.ID}", sportToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteSport(Sport sportToDelete)
        {
            var response = await client.DeleteAsync($"/api/Sport/{sportToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

    }
}
