using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.DAL.Storage
{
    public interface IStorekeeper
    {
        FileInfo Get(string fileName, string relavivePath = null);
        List<FileInfo> Get(List<(string fileName, string relavivePath)> names);
        FileInfo Add(byte[] fileContent, string fileName, string relativePath = null);
        void Remove(string fileName, string relativePath = null);
        byte[] GetAllBytes(string fileName, string relavivePath = null);
    }
}
