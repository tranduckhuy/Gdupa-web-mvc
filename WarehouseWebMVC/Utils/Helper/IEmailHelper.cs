namespace WarehouseWebMVC.Utils.Helper
{
    public interface IEmailHelper
    {
        string RenderBodyResetPassword(string resetLink);
        string RenderBodyActive(string email, string link);
    }
}
