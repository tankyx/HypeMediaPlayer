using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace HMP.Desktop.Sources
{
    public class SourceModule : IModule
    {
        private readonly IRegionManager regionManager;

        public SourceModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("Source", typeof(Views.SourceView));
        }
    }
}