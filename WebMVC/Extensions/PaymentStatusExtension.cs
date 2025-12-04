using Microsoft.VisualBasic;

namespace WebMVC.Extensions;

public static class PaymentStatusExtension
{
    private static readonly Dictionary<int, string> Values = new()
    {
        { 0, "Pending" },
        { 1, "Completed" },
    };

    public static string PaymentTypeToText(this int status)
    {
        return Values.TryGetValue(status, out var text)
            ? text
            : "Unknown";
    }
}
