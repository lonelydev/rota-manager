using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IRotaManager
    {
        /// <summary>
        /// Given a list of TeamMembers and a certain date, returns who is on call on that date.
        /// By default, takes in to account weekends as Saturdays and Sundays
        /// </summary>
        /// <param name="teamMembers"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        TeamMember WhoIsOnCall(List<TeamMember> teamMembers, DateTime dateTime);
    }
}
