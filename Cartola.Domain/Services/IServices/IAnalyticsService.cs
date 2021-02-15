using Cartola.Domain.Entities;

namespace Cartola.Domain.Services.IServices
{
    public interface IAnalyticsService
    {
        Analysis Analyze();
        object sql();
    }
}
