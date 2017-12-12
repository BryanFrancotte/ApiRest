using System.Threading.Tasks;
using FirebaseNet.Messaging;
namespace ApiRest.Service
{
    public class FirebaseService
    {
        private const string FIREBASE_KEY = "AIzaSyC4EgyOto41jWWJpBGmgaIeiDoKO6DqPh4";
        private FCMClient _client;
        public FirebaseService (){
            _client = new FCMClient(FIREBASE_KEY);
        }
        public async Task<IFCMResponse> sendFireBaseNotification(string androidKey, string notificationTitle, string notificationBody){
            var message = new Message(){
                To = androidKey,
                Notification = new AndroidNotification(){
                    Body = notificationBody,
                    Title =notificationTitle,
                    Sound = "default",
                    Color = "#df8001",
                }
            };
            var result = await _client.SendMessageAsync(message);
            return result;
        }
    }
}