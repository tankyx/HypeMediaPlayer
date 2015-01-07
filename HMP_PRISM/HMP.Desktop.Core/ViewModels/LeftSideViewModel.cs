using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace HMP.Desktop.Core.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LeftSideViewModel
    {
        public LeftSideViewModel() { }
    }
}