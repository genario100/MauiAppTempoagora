using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MauiAppTempoagora;
using MauiAppTempoagora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoagora.Services
{
    public class DataService
    {
        // Simulate data fetching
        public  static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

           string chave = "5bc49166876eef6abd6b28b60d93de45";   

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={chave}&units=metric&lang=pt";

           using (HttpClient client = new HttpClient())
           {
               HttpResponseMessage resp = await client.GetAsync(url);

               if (resp.IsSuccessStatusCode)
               {
                   var json = await resp.Content.ReadAsStringAsync();
                    // Desserializar o JSON para o objeto Tempo
                  var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)(int?)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)(int?)rascunho["sys"]["sunset"]).ToLocalTime();


                    t = new Tempo
                    {
                          lon = (double?)rascunho["coord"]["lon"],
                          lat = (double?)rascunho["coord"]["lat"],
                          temp_min = (double?)rascunho["main"]["temp_min"],
                          temp_max = (double?)rascunho["main"]["temp_max"],
                          visibility = (int?)rascunho["visibility"],
                          sunrise = sunrise.ToString(),
                          sunset = sunset.ToString(),
                          description = (string?)rascunho["weather"][0]["description"],
                          main = (string?)rascunho["weather"][0]["main"],
                          speed = (double?)rascunho["wind"]["speed"]
                     };// Fim do objeto Tempo

                }// Fim do if

                return t;

            }// Fim do using

        }// Fim do GetPrevisao

    }// Fim da classe DataService

}// Fim do namespace MauiAppTempoagora.Services
