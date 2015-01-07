using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace HMP.Desktop.Core
{
    [Module(ModuleName = "RootModule")]
    [ModuleExport(typeof(RootModule))]
    public class RootModule : IModule
    {
        private IRegionManager _RegionManager;
        [ImportingConstructor]
        public RootModule(IRegionManager regionManager)
        {
            this._RegionManager = regionManager;
        }
        #region IModule Members
        public void Initialize()
        {
            _RegionManager.RegisterViewWithRegion("LeftRegion", typeof(Views.LeftSideViews));
            _RegionManager.RegisterViewWithRegion("RightRegion", typeof(Views.RightSideViews));
        }
        #endregion
    }
}