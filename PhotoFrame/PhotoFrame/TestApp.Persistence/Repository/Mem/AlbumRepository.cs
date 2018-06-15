using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoFrame.Domain.Model;

namespace TestApp.Persistence.Repository.Mem
{
    class AlbumRepository : IAlbumRepository
    {
        List<Album> albums;

        public AlbumRepository()
        {
            albums = new List<Album>();
        }

        public bool Exists(Album entity)
        {
            throw new NotImplementedException();
        }

        public bool ExistsBy(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> Find(Func<IQueryable<Album>, IQueryable<Album>> query)
        {
            throw new NotImplementedException();
        }

        public Album Find(Func<IQueryable<Album>, Album> query)
        {
            throw new NotImplementedException();
        }

        public Album FindBy(string id)
        {
            throw new NotImplementedException();
        }

        public Album Store(Album entity)
        {
            albums.Add(entity);
            return albums[albums.Count - 1];
        }
    }
}
