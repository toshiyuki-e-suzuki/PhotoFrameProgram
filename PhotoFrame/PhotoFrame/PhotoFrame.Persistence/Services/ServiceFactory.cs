using PhotoFrame.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Persistence
{
    public class ServiceFactory : IServiceFactory
    {
        public IPhotoFileService PhotoFileService { get; }

        public ServiceFactory()
        {
            PhotoFileService = new PhotoFileService();
        }
    }
}
