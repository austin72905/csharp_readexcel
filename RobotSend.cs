using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace readExcel
{
    public class RobotSend
    {

        //send msg to robot chat room
        public void RobotApiNew(string text)
        {
            Task.Run(async () =>
            {

                var httpClient = new HttpClient();
                string token = "836063686:AAF7t-eWtFLnQwOR_WO3GLFg4f_5VtGhq3E";//新機器人TOKEN: "836063686:AAF7t-eWtFLnQwOR_WO3GLFg4f_5VtGhq3E";//"713787636:AAGb5TWye4SLsESjevZoOWexOhGe302kkCY";

                

                var postData = new Dictionary<string, string>();
                postData["chat_id"] = 1143269186.ToString();
                postData["text"] = text;
                postData["parse_mode"] = "HTML";
                string url = "https://api.telegram.org/bot" + token + "/sendMessage";

                var content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                //response.EnsureSuccessStatusCode();
                //string resultStr = await response.Content.ReadAsStringAsync();

            });
        }
    }
}
