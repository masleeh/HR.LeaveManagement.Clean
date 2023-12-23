using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Model {
    public class CustomValidationProblemDetails : ProblemDetails {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
