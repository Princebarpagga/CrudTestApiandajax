namespace CrudTestApi.Models
{
    public class StudentVM
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int DepId { get; set; }

        public int QualifId { get; set; }

        public decimal Percentage { get; set; }
        public string DepartmentName { get; set; }
        public string QualificationsName { get; set; }
    }
    public class ServiceResponse
    {
        public object data { get; set; }

        public object  dataInfo { get; set; }

        public bool isSuccess { get; set; }
        
    }
}
