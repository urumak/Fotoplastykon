using Fotoplastykon.BLL.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Shared
{
    public class LinkedItemDTO
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public FileInfo Photo { get; set; }
        public ItemType Type { get; set; }
    }
}
