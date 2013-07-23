using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChART.Domain.Entities;

namespace ChART.DataAccess.Abstract
{
    public interface IStationRepository
    {
        IQueryable<Station> Stations { get; }
    }
}
