﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.Application.Converters
{
    public interface IConverter
    {
        string[] FileExtensions { get; }
        Task<T> ParseText<T>(string text);
        Task<string> Convert<T>(T source);
    }
}
