using System.Threading.Tasks;

namespace Client.PL.services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from,string recipents , string subject , string body);
	}
}
