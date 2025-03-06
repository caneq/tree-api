using TreesApi.BusinessLogic.Models;

namespace TreesApi.BusinessLogic.Factories;

public interface IErrorDetailsFactory
{
    ErrorDetails GetErrorDetails(Exception exception);
}
