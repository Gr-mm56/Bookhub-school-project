namespace WebMVC.Extensions;

public static class PaymentStatusExtension
{
    public static string PaymentTypeToText(this bool status)
    {
        return status ? "Completed" : "Pending";
    }
}
