﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.Domain
{
    public class Status
    {
        [Key]
        public string Started { get; set; }
        public string Server { get; set; }
        public string OsVersion { get; set; }
        public string AssemblyVersion { get; set; }
        public int ProcessorCount { get; set; }
        public int ElapsedTime { get; set; }
    }
}
