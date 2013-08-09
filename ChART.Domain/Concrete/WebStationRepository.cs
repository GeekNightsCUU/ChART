using System;
using System.Linq;
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
        public IQueryable<Station> Stations { 
            get{
                var client = new HttpClient();                
                var task = client.GetStringAsync(StationsEndpoint);
                task.Wait();
                if(task.Status == TaskStatus.RanToCompletion)
                {                    
                    List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(task.Result);
                    return stations.AsQueryable<Station>();
                }else{
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
