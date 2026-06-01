using System.Text;

namespace Lab39.Services;

public interface ICsvExportService
{
    Task<string> ExportOrdersToCsvAsync(IEnumerable<Order> orders);
}

public class CsvExportService : ICsvExportService
{
    public async Task<string> ExportOrdersToCsvAsync(IEnumerable<Order> orders)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Date,Total,Status");

        foreach (var order in orders)
        {
            sb.AppendLine(string.Join(",", order.Id, order.Date.ToString("yyyy-MM-dd"), order.Total, order.Status));
        }

        return await Task.FromResult(sb.ToString());
    }
}
