using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using ChART.DataAccess.Abstract;
using ChART.Domain.Entities;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChART.DataAccess.Concrete
{
    public class WebStationRepository: IStationRepository
    {
        private static readonly string ChARTServiceUrl = "https://chart.apphb.com/";
        private static readonly string StationsEndpoint = ChARTServiceUrl + "api/routes";  
		private static readonly string RoutesFile = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "routes.json");

        public IQueryable<Station> Stations { 
            get{
				if (File.Exists (RoutesFile) && File.GetLastWriteTime (RoutesFile) >= DateTime.Now.AddDays (-7)) {
					return FromFile ();
				} else {
					return FromWeb ();
				}
            }
        }

		public static IQueryable<Station> FromFile(){
			var jsonString = File.ReadAllText (RoutesFile);
			List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(jsonString);
			return stations.AsQueryable<Station>();
		}

		public static IQueryable<Station> FromWeb()
		{
			var client = new HttpClient();
			try{
				var task = client.GetStringAsync(StationsEndpoint);
				task.Wait();
				if(task.Status == TaskStatus.RanToCompletion)
				{                    
					File.WriteAllText (RoutesFile, task.Result);
					List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(task.Result);
					return stations.AsQueryable<Station>();
				}else{
					return new List<Station>().AsQueryable<Station>();
				}
			}catch(AggregateException ex){
				Console.WriteLine (ex.Message);
				if (File.Exists (RoutesFile)) {
					return FromFile ();
				} else {
					return new List<Station>().AsQueryable<Station>();
				}
			}
		}

        public Station Station(String stationId)
        {
            return Stations.First(s => s.Id == stationId);
        }

        public void Save(Station station)
        {            
        }
    }
}
