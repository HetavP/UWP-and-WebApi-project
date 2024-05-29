using ProjectA_B_UWP.Models;
using ProjectA_B_UWP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA_B_UWP.Data
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly HttpClient client = new HttpClient();
        public AthleteRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Athlete>> GetAthletes()
        {
            HttpResponseMessage response = await client.GetAsync("api/Athlete");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> Athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return Athletes;
            }
            else
            {
                throw new Exception("Could not access the list of Athletes.");
            }
        }

        public async Task<List<Athlete>> GetAthletesBySport(int SportID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athlete/BySport/{SportID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> Athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return Athletes;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Cannot find any Athletes for that Sport.");
                }
                else
                {
                    throw new Exception("Could not access the list of Athletes by Sport.");
                }
            }
        }

        public async Task<List<Athlete>> GetAthletesByContingent(int ContingentID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athlete/ByContingent/{ContingentID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> Athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return Athletes;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Cannot find any Athletes for that Contingent.");
                }
                else
                {
                    throw new Exception("Could not access the list of Athletes by Contingent.");
                }
            }
        }


        public async Task<Athlete> GetAthlete(int ID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athlete/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Athlete Athlete = await response.Content.ReadAsAsync<Athlete>();
                return Athlete;
            }
            else
            {
                throw new Exception("Could not access that Athlete.");
            }
        }

        public async Task AddAthlete(Athlete athleteToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/Athlete", athleteToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateAthlete(Athlete athleteToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/Athlete/{athleteToUpdate.ID}", athleteToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteAthlete(Athlete athleteToDelete)
        {
            var response = await client.DeleteAsync($"/api/Athlete/{athleteToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

    }
}
