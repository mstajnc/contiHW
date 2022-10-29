using Backend_Homework.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Homework.Application.Services
{
    public interface IDocumentService
    {
        Task<string> FormatText(string text, FormatType resultFormatType);
    }
}
