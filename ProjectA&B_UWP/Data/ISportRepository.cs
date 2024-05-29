using ProjectA_B_UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA_B_UWP.Data
{
    public interface ISportRepository
    {
        Task<List<Sport>> GetSports();
        Task<Sport> GetSport(int ID);
        Task AddSport(Sport sportToAdd);
        Task UpdateSport(Sport sportToUpdate);
        Task DeleteSport(Sport sportToDelete);
    }
}
