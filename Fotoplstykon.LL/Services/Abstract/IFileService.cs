﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFileService
    {
        FileInfo Get(long id);
        FileInfo Add(IFormFile file, string relativePath = null);
        void Remove(long id);
    }
}
