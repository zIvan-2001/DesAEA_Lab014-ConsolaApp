using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(() => GetPerson());
            Task.Run(() => PostInsertPerson());

            Console.WriteLine("Laboratorio14");
            Console.Read();
        }

        private static async Task<List<PersonResponse>> GetPerson()
        {

            HttpClient client = new HttpClient();
            var person = new List<PersonResponse>();
            var urlBase = "https://localhost:44360";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/api/Persons");

            var response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                person = JsonConvert.DeserializeObject<List<PersonResponse>>(result);

            }
            return person;
        }

        private static void PostInsertPerson()
        {
            HttpClient client = new HttpClient();
            var urlBase = "https://localhost:44360";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/api/Persons");

            var model = new PersonRequest
            {

                FirstName = "Ivan",
                LastName = "Oscco"
            };

            //Qué te voy a enviar
            var request = JsonConvert.SerializeObject(model);
            //Cómo te lo voy a enviar
            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {

                Console.WriteLine("Exitoso");
            }
            else
            {
                Console.WriteLine("Error Del Servicio");
            }
        }



        private static async Task<List<Obras>> GetObrasAsync()
        {

            HttpClient client = new HttpClient();
            var obras = new List<Obras>();
            var urlBase = "http://sitra.emape.gob.pe:8082";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/api/expedientes/obtener_proyectos/0");

            var response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                obras = JsonConvert.DeserializeObject<List<Obras>>(result);

            }
            return obras;
        }


        private static async Task PostInsertAppoitmentAsync()
        {
            HttpClient client = new HttpClient();
            var urlBase = "http://18.232.226.120/WICAPIDev";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/api/UserClass/InsertAppoitment");


            var model = new AppoitmentRequest
            {
                appointmentId = 7,
                appointmentNumber = "number",
                appointmentURL = "URL",
                classCode = "code",
                //wicId = "1123132",
                wicId = "235",
                registeredAppointmentDateTime = DateTime.Today
            };
            //Qué te voy a enviar
            var request = JsonConvert.SerializeObject(model);
            //Cómo te lo voy a enviar
            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, content).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {

                var result = await response.Content.ReadAsStringAsync();
                var appoitment = JsonConvert.DeserializeObject<AppoitmentResponse>(result);
                Console.WriteLine(appoitment.message);
                Console.WriteLine(appoitment.messageEN);
            }
            else
            {
                Console.WriteLine("Error Del Servicio");
            }

        }


        private static async Task ShowObrasAsync()
        {

            HttpClient client = new HttpClient();
            var obras = new List<Obras>();
            var urlBase = "http://sitra.emape.gob.pe:8082";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/api/expedientes/obtener_proyectos/2022");

            var response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                obras = JsonConvert.DeserializeObject<List<Obras>>(result);

            }
            foreach (var item in obras)
            {
                Console.WriteLine(item.codigoUnico);
                break;
            }

        }

        static async void WriteLineAsync(string message, int miliseconds)
        {
            StringBuilder stringToWrite = new StringBuilder(message);
            stringToWrite.AppendLine();
            Thread.Sleep(miliseconds);
            using (StringWriter writer = new StringWriter(stringToWrite))
            {
                UnicodeEncoding ue = new UnicodeEncoding();
                char[] charsToAdd = ue.GetChars(ue.GetBytes(""));
                foreach (char c in charsToAdd)
                {
                    await writer.WriteLineAsync(c);
                }
                Console.WriteLine(stringToWrite.ToString());
            }
        }



    }
}
