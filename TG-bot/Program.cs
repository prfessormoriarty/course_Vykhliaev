using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;

namespace TG_bot
{
    class Program
    {
        static ITelegramBotClient botClient;
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {

            botClient = new TelegramBotClient("1878444953:AAF9LCdgvwFlorDsXDX51rsW4ROHG6pFx8Q");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.Read();
            }
            catch(Exception ex)
            {
                Console.Write("Exception:"+ex);
            }
            finally
            { 
                botClient.StopReceiving();
            }
        }

        static void SendImage(MessageEventArgs e, string url)
        {
            InputOnlineFile inputOnlineFile = new InputOnlineFile(url);

            botClient.SendPhotoAsync(
                chatId: e.Message.Chat,
                photo: inputOnlineFile  //,
                // caption: "car"
            );
        }

        [Obsolete]
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text.Contains("/"))
                {
                    try
                    {

                        var words = e.Message.Text.Trim().Split(' ');
                        var command = words[0].Split('/')[1];
                        switch (command)
                        {
                            case "":
                                break;
                            case "help":
                                string[] commands = { "/show {options: maufacturers, <empty>}", "/getCars {filter1: manufacturer(honda), year(2000;2000), <empty>} {filter2: ...}", "/getCarImage id(100)", "/getNumberCars", "/getYears", "/getManufacturers" };

                                string helpmessage = "Command list:\n";

                                foreach (var item in commands)
                                {
                                    helpmessage += item+"\n";
                                }
                                await botClient.SendTextMessageAsync(
                                    chatId: e.Message.Chat,
                                    text: helpmessage
                                );

                                break;
                            case "getManufacturers":
                                dynamic resp = JsonConvert.DeserializeObject(ReguestController.GetManufacturers());

                                var datam = resp.data;
                                string textm = "Manufacturers:\n";
                                foreach (var item in datam)
                                {
                                    textm += "\t" + item + "\n";
                                }

                                await botClient.SendTextMessageAsync(
                                    chatId: e.Message.Chat,
                                    text: textm
                                );
                                break;
                            case "getCars":
                                string filters = "";

                                for (int i = 1; i < words.Length; i++)
                                {
                                    if (filters == "")
                                        filters = "?";
                                    var tmp = words[i];
                                    var filterType = tmp.Split('(')[0];
                                    var range = tmp.Split('(')[1].Split(')')[0].Split(';');

                                    switch (filterType)
                                    {
                                        case "manufacturer":
                                            if (filters != "?")
                                                filters += "&";
                                            filters += filterType + "=" + range[0];
                                            break;
                                        case "year":
                                            if (filters != "?")
                                                filters += "&";
                                            string years = "";
                                            int c = 0;
                                            foreach (var y in range)
                                            {
                                                years += y + ";";
                                                c++;
                                                if (c == 2)
                                                    break;
                                            }

                                            StringBuilder sb1 = new StringBuilder(years);
                                            sb1.Remove(years.Length - 1, 1);
                                            years = sb1.ToString();

                                            filters += filterType + "=" + years;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                dynamic response = JsonConvert.DeserializeObject(ReguestController.GetAutos(filters));

                                var datac = response.data;
                                string text = "Cars:";
                                await botClient.SendTextMessageAsync(
                                    chatId: e.Message.Chat,
                                    text: text
                                );

                                int counter = 0;

                                foreach (var item in datac)
                                {
                                    text = "Model: \t" + item.model + "\n";
                                    text += "ID: \t" + item.aid + "\n";
                                    text += "Manufacturer: \t" + item.manufacturer + "\n";
                                    text += "Year: \t" + item.year + "\n";
                                    counter++;
                                    if (counter > 20)
                                    {
                                        await botClient.SendTextMessageAsync(
                                            chatId: e.Message.Chat,
                                            text: "Too many cars. Use more strict filters"
                                        );
                                        break;
                                    }
                                    await botClient.SendTextMessageAsync(
                                        chatId: e.Message.Chat,
                                        text: text
                                    );
                                }
                                break;
                            case "getCarImage":
                                filters = "";
                                for (int i = 1; i < words.Length; i++)
                                {
                                    filters = "?";
                                    var tmp = words[i];
                                    var filterType = tmp.Split('(')[0];
                                    var range = tmp.Split('(')[1].Split(')')[0].Split(';');

                                    switch (filterType)
                                    {
                                        case "id":
                                            filters += "aid=" + range[0];
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                dynamic responseu = JsonConvert.DeserializeObject(ReguestController.GetAutoImage(filters));

                                var data = responseu.data;

                                foreach (var item in data)
                                {
                                    string s = item.img_url;
                                    SendImage(e, s);
                                    break;
                                }
                                break;
                            case "getYears":
                                dynamic responsey = JsonConvert.DeserializeObject(ReguestController.GetYears());

                                var datay = responsey.data;
                                string texty = "Years:\n";

                                foreach (var item in datay)
                                {
                                    texty += item+"\n";
                                }

                                await botClient.SendTextMessageAsync(
                                    chatId: e.Message.Chat,
                                    text: texty
                                );
                                break;
                            case "getNumberCars":
                                dynamic responsen = JsonConvert.DeserializeObject(ReguestController.GetNumber());

                                var number = responsen.data;
                                string textn = "Total number of accesible cars: "+number;

                                await botClient.SendTextMessageAsync(
                                    chatId: e.Message.Chat,
                                    text: textn
                                );
                                break;
                            default:
                                break;
                        }
                    }
                    catch(Exception ex)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "There is an exception: " + ex
                        );
                    }
                }
                else
                {
                    Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "I don't understand you~ (UwU)"
                    );
                }
            }
        }
    }
}
