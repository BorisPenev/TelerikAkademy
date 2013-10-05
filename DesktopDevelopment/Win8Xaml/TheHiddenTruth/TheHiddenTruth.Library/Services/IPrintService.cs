using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HiddenTruth.Library.ViewModel;

namespace HiddenTruth.Library.Services
{
    public interface IPrintService
    {
        void RegisterForPrinting(object sourcePage, Type printPageType, object viewModel);

        void Print();
    }
}
