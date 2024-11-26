namespace PeopleManager.Dto.Results
{
    public class OrganizationResult
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfMembers { get; set; }
    }
}
