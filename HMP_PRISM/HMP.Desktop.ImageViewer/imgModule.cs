using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMP.Desktop.ImageViewer
{
    public class imgModule : IModule
    {
        private readonly IRegionManager regionManager;

        public imgModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("ImgView", typeof(Views.View));
        }
    }
}
