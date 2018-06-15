using PhotoFrame.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PhotoFrame.Domain.UseCase
{
    /// <summary>
    /// アルバムを作成するユースケースを実現する
    /// </summary>
    // TODO: 仮実装
    public class CreateAlbum
    {
        private readonly IAlbumRepository albumRepository;
        private readonly IPhotoRepository photoRepository;

        public CreateAlbum(IAlbumRepository albumRepository, IPhotoRepository photoRepository)
        {
            this.albumRepository = albumRepository;
            this.photoRepository = photoRepository;
        }

        public void Execute(string albumTitle)
        {
            //変数名整理必要
            //if (Directory.Exists(albumTitle))
            //{
            //    IEnumerable<Album> result = repository.Find((IQueryable<Album> albums) => (from p in albums where p.Name == albumTitle select p));

            //    // 登録済みのアルバム名でない場合はアルバムを作成
            //    if (result == null || result.Count() == 0)
            //    {
            //        var album = Album.Create(albumTitle);
            //        repository.Store(album);
            //        string[] path_list = Directory.GetFiles(albumTitle, "*.*", SearchOption.AllDirectories);

            //        foreach (string filePath in path_list)
            //        {
            //            PhotoFrame.Domain.Model.File file = new PhotoFrame.Domain.Model.File(filePath);

            //            if (file.IsPhoto)
            //            {
            //                Photo photo = Photo.CreateFromFile(file);
            //                photo.IsAssignedTo(album);

            //                repository.Store(photo);
            //            }
            //        }


            //    }
            //}

        }
    }
}
