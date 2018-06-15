using PhotoFrame.Domain.Model;
using PhotoFrame.Domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Application
{
    /// <summary>
    /// PhotoFrameのUIの指示にしたがってドメインのユースケースを起動する
    /// </summary>
    // TODO: 仮実装
    public class PhotoFrameApplication
    {
        private readonly CreateAlbum createAlbum;

        public PhotoFrameApplication(IAlbumRepository albumRepository)
        {
            this.createAlbum = new CreateAlbum(albumRepository);
        }

        public void CreateAlbum(string albumTitle)
        {
            createAlbum.Execute(albumTitle);
        }
    }
}
