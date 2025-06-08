namespace EmployeeAdminPortal.Model
{
    public class AddEmployeeDto
    {
        public required string Name { get; set; } //green line asking us that is this property is a required feild? if it is so it should be existing from the constructor as a non nullable property, so we want to make some properties required and some optional so name is a required one
        public required string Email { get; set; }
        public string? Phone { get; set; } // if i want to make phone a nullable property so ill add a ? to make it nullable property
        public decimal Salary { get; set; }
    }
}
