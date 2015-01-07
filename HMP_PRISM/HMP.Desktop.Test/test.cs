using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMP.Desktop.Test
{
    public class test : IModule
    {
        private readonly IRegionManager regionManager;

        public test(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("SubRegion", typeof(Views.view));
        }
    }
}
