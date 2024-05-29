using ProjectA_B_UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA_B_UWP.Data
{
    public interface IContingentRepository
    {
        Task<List<Contingent>> GetContingents();
        Task<Contingent> GetContingent(int ID);
        Task AddContingent(Contingent contingentToAdd);
        Task UpdateContingent(Contingent contingentToUpdate);
        Task DeleteContingent(Contingent contingentToDelete);
    }
}
