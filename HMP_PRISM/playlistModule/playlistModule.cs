using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMP.Desktop.PlaylistModule
{
    public class playlistModule : IModule
    {
        private readonly IRegionManager regionManager;

        public playlistModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("playlist", typeof(Views.playlistView));
        }
    }
}
