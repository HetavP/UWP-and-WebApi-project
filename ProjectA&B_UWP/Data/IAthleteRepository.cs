using ProjectA_B_UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA_B_UWP.Data
{
    public interface IAthleteRepository
    {
        Task<List<Athlete>> GetAthletes();
        Task<Athlete> GetAthlete(int ID);
        Task<List<Athlete>> GetAthletesBySport(int SportID);
        Task<List<Athlete>> GetAthletesByContingent(int ContingentID);
        Task AddAthlete(Athlete athleteToAdd);
        Task UpdateAthlete(Athlete athleteToUpdate);
        Task DeleteAthlete(Athlete athleteToDelete);

    }
}
