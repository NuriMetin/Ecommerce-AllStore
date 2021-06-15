using DataAccess.Models;
using Services.AdoNet.Helpers.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IProducerService : IRepository<Producer>
    {
    }
}
