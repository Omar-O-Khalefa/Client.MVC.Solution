using System.Threading.Tasks;

namespace Client.PL.services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string form,string recipents , string subject , string body);
	}
}
