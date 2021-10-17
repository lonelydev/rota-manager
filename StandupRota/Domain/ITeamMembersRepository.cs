using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface ITeamMembersRepository
    {
        Task<List<TeamMember>> GetAllTeamMembers();
        Task<TeamMember> GetMostRecentlyOnCall(DateTime dateTime);
    }
}
