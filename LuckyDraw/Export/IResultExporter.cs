using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Export
{
    public interface IResultExporter
    {
        void Export(List<ViewResult> data, string filePath);
    }
}
