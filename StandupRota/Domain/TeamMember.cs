namespace Domain
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Order { get; set; }
        public bool IsOnCall {get;set; }

        private TeamMember()
        {

        }

        public static TeamMember CreateTeamMember(int id, string firstName, string lastName, string emailAddress, int order, bool isOnCall = false)
        {
            return new TeamMember
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Order = order,
                IsOnCall = isOnCall
            };
        }

        private static TeamMember _nullTeamMember = new TeamMember
                {
                    FirstName = null,
                    LastName = null,
                    EmailAddress = null,
                    Order = 0,
                    Id = 0
                };

        public static TeamMember NullTeamMember {
            get
            {
                return _nullTeamMember;
            }
        }
    }
}
