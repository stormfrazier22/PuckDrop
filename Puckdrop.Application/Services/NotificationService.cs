using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PuckDrop.Services
{
    public class NotificationService : INotificationService
    {
        private readonly string _twilioAccountSid; //The sid provided by Twilio
        private readonly string _twilioAuthToken; //The auth token provided by Twilio
        private readonly string _twilioPhoneNumber; //The phone number provided by Twilio

        public NotificationService(string twilioAccountSid, string twilioAuthToken, string twilioPhoneNumber)
        {
            _twilioAccountSid = twilioAccountSid;
            _twilioAuthToken = twilioAuthToken;
            _twilioPhoneNumber = twilioPhoneNumber;
        }

        public async Task SendSMSAsync(string textMessage, string phoneNumber)
        {
            TwilioClient.Init(_twilioAccountSid, _twilioAuthToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(phoneNumber) //To
            );

            messageOptions.From = new PhoneNumber(_twilioPhoneNumber); //From
            messageOptions.Body = textMessage; //Message body

            await MessageResource.CreateAsync(messageOptions);
        }
    }
}
