﻿using ELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface ICategoryService
    {
       
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();
    }
}
