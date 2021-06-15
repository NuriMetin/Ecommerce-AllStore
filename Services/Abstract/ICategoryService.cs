using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface ICategoryService : IRepository<Category>
    {
    }
}
