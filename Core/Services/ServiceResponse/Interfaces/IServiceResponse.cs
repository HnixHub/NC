using api.multitracks.com.Models;

namespace api.multitracks.com.Interfaces
{
    public interface IServiceResponse
    {
        ServiceError Error { get; set; }

        bool Status { get; }

        string Message { get; set; }
    }
}
