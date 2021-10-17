using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Domain.Tests")]
namespace Domain
{
    internal class RotaManager : IRotaManager
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INumberOfDaysCalculator _numberOfDaysCalculator;

        public RotaManager(IDateTimeProvider dateTimeProvider, INumberOfDaysCalculator numberOfDaysCalculator)
        {
            _dateTimeProvider = dateTimeProvider;
            _numberOfDaysCalculator = numberOfDaysCalculator;
        }

        public TeamMember WhoIsOnCall(List<TeamMember> teamMembers, DateTime requestedDateTimeInUtc)
        {
            if (!teamMembers.Any())
            {
                return null;
            }
            var orderedTeamMembersByOnCall = OrderTeamMembersFromCurrentOnCall(teamMembers);

            var totalDaysFromToday = _numberOfDaysCalculator.NumberOfWeekDaysBetween(_dateTimeProvider.UtcNow.Date, requestedDateTimeInUtc.Date);

            return orderedTeamMembersByOnCall[totalDaysFromToday % teamMembers.Count];
        }
        
        private List<TeamMember> OrderTeamMembersFromCurrentOnCall(List<TeamMember> teamMembers)
        {
            var orderedTeamMembersList = new List<TeamMember>();
            var onCallMember = teamMembers.Single(tm => tm.IsOnCall);
            orderedTeamMembersList.Add(onCallMember);
            orderedTeamMembersList.AddRange(teamMembers.Where(tm => tm.Order > onCallMember.Order));
            orderedTeamMembersList.AddRange(teamMembers.Where(tm => tm.Order < onCallMember.Order));
            return orderedTeamMembersList;
        }
    }
}
