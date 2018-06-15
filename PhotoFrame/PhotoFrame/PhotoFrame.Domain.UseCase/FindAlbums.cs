using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoFrame.Domain.Model;

namespace PhotoFrame.Domain.UseCase
{
    public class FindAlbums
    {

        private readonly IAlbumRepository repository;

        public FindAlbums(IAlbumRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(Func<IQueryable<Album>, IQueryable<Album>> query)
        {
            repository.Find(query);
        }
    }
}
