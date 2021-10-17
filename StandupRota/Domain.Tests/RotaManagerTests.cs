using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Tests
{
    public class Tests
    {
        private IDateTimeProvider _dateTimeProvider;
        private RotaManager _rotaManager;

        private List<TeamMember> _teamMembers;

        [SetUp]
        public void Setup()
        {
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _rotaManager = new RotaManager(_dateTimeProvider, new NumberOfDaysCalculator());
            var teamMember1 = TeamMember.CreateTeamMember(1, "Ariana", "TheAwesome", "atheawesome@nonexistant.com", 1);
            var teamMember2 = TeamMember.CreateTeamMember(2, "Cleopatra", "TheBerisome", "ctheberisome@nonexistant.com", 2);
            var teamMember3 = TeamMember.CreateTeamMember(3, "Edward", "Gawesome", "egawesome@nonexistant.com", 3);
            var teamMember4 = TeamMember.CreateTeamMember(4, "Kyle", "Stawsome", "kstawsome@nonexistant.com", 4);
            var teamMember5 = TeamMember.CreateTeamMember(5, "Malcolm", "Wosome", "mwosome@nonexistant.com", 5);
            var teamMember6 = TeamMember.CreateTeamMember(6, "Patrick", "Wawesome", "pwawesome@nonexistant.com", 6);
            _teamMembers = new List<TeamMember>
            {
                teamMember1, teamMember2, teamMember3, teamMember4, teamMember5, teamMember6
            };
        }

        [TestCaseSource(nameof(_whoIsOnCallTestData))]
        public int WhoIsOnCall(int orderOfCurrentOnCall, DateTime todaysDate, DateTime requestedDate)
        {
            _dateTimeProvider.UtcNow.Returns(todaysDate);
            _teamMembers.First(tm => tm.Order == orderOfCurrentOnCall).IsOnCall = true;
            return _rotaManager.WhoIsOnCall(_teamMembers, requestedDate).Order;
        }

        static object[] _whoIsOnCallTestData =
        {
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 11))
                .Returns(1)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_BothStartAndEndDDateIsSame_Return1"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 12))
                .Returns(2)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_1DayBetweenStartAndEnd_Return2"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 13))
                .Returns(3)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_2DaysBetweenStartAndEnd_Return3"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 14))
                .Returns(4)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_3DaysBetweenStartAndEnd_Return4"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 15))
                .Returns(5)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_4DaysBetweenStartAndEnd_Return5"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 16))
                .Returns(5)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_4DaysBetweenStartAndEnd_EndDateIsSaturday_Return5"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 17))
                .Returns(5)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_4DaysBetweenStartAndEnd_EndDateIsSunday_Return5"),

            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 18))
                .Returns(6)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_5DaysBetweenStartAndEnd_EndDateInSubsequentWeek_Return6"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 19))
                .Returns(1)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_6DaysBetweenStartAndEnd_EndDateInSubsequentWeek_Return1"),
            new TestCaseData(1, new DateTime(2021,10,11), new DateTime(2021, 10, 20))
                .Returns(2)
                .SetName("_CurrentlyOnCallIs1_TeamSizeIs6_7DaysBetweenStartAndEnd_EndDateInSubsequentWeek_Return2"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 11))
                .Returns(5)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_BothStartAndEndDDateIsSame_Return5"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 12))
                .Returns(6)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_1DayBetweenStartAndEnd_Return6"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 13))
                .Returns(1)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_2DaysBetweenStartAndEnd_Return1"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 14))
                .Returns(2)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_3DaysBetweenStartAndEnd_Return2"),
            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 15))
                .Returns(3)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_4DaysBetweenStartAndEnd_Return3"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 16))
                .Returns(3)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_4DaysBetweenStartAndEnd_Return3"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 17))
                .Returns(3)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_4DaysBetweenStartAndEnd_Return3"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 18))
                .Returns(4)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_5DaysBetweenStartAndEnd_Return4"),
            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 19))
                .Returns(5)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_6DaysBetweenStartAndEnd_Return5"),

            new TestCaseData(5, new DateTime(2021,10,11), new DateTime(2021, 10, 20))
                .Returns(6)
                .SetName("_CurrentlyOnCallIs5_TeamSizeIs6_7DaysBetweenStartAndEnd_Return6"),
        };
    }
}