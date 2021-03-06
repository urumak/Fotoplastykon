﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Fotoplastykon.DAL.Storage
{
    public class Storekeeper : IStorekeeper
    {
        protected string BasePath { get; }

        public Storekeeper(IConfiguration configuration)
        {
            BasePath = configuration["Files:BasePath"];
        }

        public FileInfo Get(string fileName, string relavivePath = null)
        {
            var path = Path.Combine(BasePath, relavivePath ?? "");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return new FileInfo(Path.Combine(path, fileName));
        }

        public byte[] GetAllBytes(string fileName, string relavivePath = null)
        {
            var path = Path.Combine(BasePath, relavivePath ?? "", fileName);
            var file = new FileInfo(path);

            if(file.Exists) return File.ReadAllBytes(path);

            return new byte[0];
        }

        public List<FileInfo> Get(List<(string fileName, string relavivePath)> names)
        {
            var result = new List<FileInfo>();

            foreach (var (fileName, relavivePath) in names)
            {
                result.Add(Get(fileName, relavivePath));
            }

            return result;
        }

        public FileInfo Add(byte[] fileContent, string fileName, string relativePath = null)
        {
            var file = Get(fileName, relativePath);

            using (var stream = file.Create())
            {
                stream.Write(fileContent, 0, fileContent.Length);
            }

            return file;
        }

        public void Remove(string fileName, string relativePath = null)
        {
            var file = Get(fileName, relativePath);

            file.Delete();
        }
    }
}
