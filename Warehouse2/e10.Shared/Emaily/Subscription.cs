using e10.Shared.Emaily.Config;

namespace e10.Shared.Emaily
{
    public class Subscription : Emaily
    {
        public Subscription()
        {
        }

        /// <summary>
        /// <para>Calls Emaily API to subscribe user</para>
        /// <para>API POST URL: https://emaily.xyz/subscribe </para>
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <param name="plainTextResponse"></param>
        public bool Subscribe(string listId, string email, bool plainTextResponse)
        {
            return Subscribe(listId, email, "", plainTextResponse);
        }

        /// <summary>
        /// <para>Calls Emaily API to subscribe user</para>
        /// <para>API POST URL: https://emaily.xyz/subscribe </para>
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="plainTextResponse"></param>
        public bool Subscribe(string listId, string email, string name, bool plainTextResponse)
        {
            // api url
            var result = false;
            var apiUrl = config.InstallationUrl + "/subscribe";

            if (string.IsNullOrWhiteSpace(listId)) listId = config.SubscriptionListId;

            // set the parameters to post
            this.Parameters = string.Format("list={0}&email={1}&boolean={2}{3}", 
                listId, 
                email, 
                plainTextResponse ? "true" : "false", 
                string.IsNullOrEmpty(name) ? "" : string.Format("&name={0}", name));

            // post info to Emaily api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // parse the response
            if (!bool.TryParse(this.Response, out result))
            {
                this.ErrorStatus = this.GetSubscriptionStatus(this.Response);
            }

            return result;

        }

        /// <summary>
        /// <para>Calls Emaily API to unscubscribe user</para>
        /// <para>API POST URL: https://emaily.xyz/unsubscribe </para>
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <param name="plainTextResponse"></param>
        public bool Ubsubscribe(string listId, string email, bool plainTextResponse)
        {

            // api url
            var result = false;
            var apiUrl = config.InstallationUrl + "/unsubscribe";

            // set the parameters to post
            this.Parameters = string.Format("list={0}&email={1}&boolean={2}", 
                listId, 
                email, 
                plainTextResponse ? "true" : "false");
           
            // post to Emaily api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // parse the response
            if (!bool.TryParse(this.Response, out result))
            {
                this.ErrorStatus = this.GetSubscriptionStatus(this.Response);
            }

            return result;

        }

        /// <summary>
        /// <para>Calls Emaily API to get the current status of a subscriber</para>
        /// <para>API POST URL: https://emaily.xyz/api/subscribers/subscription-status.php </para>
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public StatusCode Status(string listId, string email)
        {

            // api url
            var apiUrl = config.InstallationUrl + "/api/subscribers/subscription-status.php";
            // api key
            var apiKey = config.ApiKey;

            // set the parameters to post
            this.Parameters = string.Format("list_id={0}&email={1}&api_key={2}", 
                listId, 
                email, 
                apiKey);
            
            // post to Emaily api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            return GetSubscriptionStatus(this.Response);

        }

        /// <summary>
        /// <para>Calls Emaily API to get the total active subscriber count.</para>
        /// <para>API POST URL: https://emaily.xyz/api/subscribers/active-subscriber-count.php </para>
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public int ActiveSubscriberCount(string listId)
        {

            // api url
            var apiUrl = config.InstallationUrl + "/api/subscribers/active-subscriber-count.php";
            // api key
            var apiKey = config.ApiKey;

            int result = 0;   
         
            // set the parameters to post
            this.Parameters = string.Format("list_id={0}", listId);

            // post to Emaily api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // attempt to parse result to integer
            if (!int.TryParse(this.Response, out result))
            {
                this.ErrorStatus = GetSubscriptionStatus(this.Response);
            }

            return result;
            
        }
       
    }

}
