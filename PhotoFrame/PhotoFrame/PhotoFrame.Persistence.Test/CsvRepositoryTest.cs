using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoFrame.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Persistence.Test
{
    [TestClass]
    public class CsvRepositoryTest
    {
        private IPhotoRepository photoRepository;
        private IAlbumRepository albumRepository;

        [TestInitialize]
        public void SetUp()
        {
            // 各テストごとにデータベースファイルを削除
            // TODO: 実装によってCSVファイル名が違うと思うので適宜修正を
            if (System.IO.File.Exists("PhotoFrameDatabaseForTest_Photo.csv"))
            {
                System.IO.File.Delete("PhotoFrameDatabaseForTest_Photo.csv");
            }
            if (System.IO.File.Exists("PhotoFrameDatabaseForTest_Album.csv"))
            {
                System.IO.File.Delete("PhotoFrameDatabaseForTest_Album.csv");
            }

            // リポジトリ生成
            var repos = new RepositoryFactory(Type.Csv);
            photoRepository = repos.PhotoRepository;
            albumRepository = repos.AlbumRepository;
        }

        [TestMethod]
        public void 写真を追加できること()
        {
            var photo = Photo.CreateFromFile(new File("dummy.bmp"));

            photoRepository.Store(photo);

            var result = photoRepository.FindBy(photo.Id);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void 写真を更新できること()
        {
            var photo = Photo.CreateFromFile(new File("dummy.bmp"));
            photoRepository.Store(photo);

            photo.MarkAsFavorite();
            photoRepository.Store(photo);

            var result = photoRepository.FindBy(photo.Id);
            Assert.AreEqual(true, result.IsFavorite);
        }

        [TestMethod]
        public void 既存の写真をアルバムに追加できること()
        {
            var album = Album.Create("Album1");
            albumRepository.Store(album);
            var photo = Photo.CreateFromFile(new File("dummy.bmp"));
            photoRepository.Store(photo);

            photo.IsAssignedTo(album);
            photoRepository.Store(photo);

            var result = photoRepository.FindBy(photo.Id);
            Assert.AreEqual(album.Id, result.Album.Id);
        }

        [TestMethod]
        public void アルバムの検索ができること()
        {
            for(int i = 0; i < 5; i++)
            {
                Album album = Album.Create("Album" + i);
            }

            //Album result = albumRepository.Find(albums => from a in albums where a.Name.Contains("3") select a);
        }

        [TestMethod]
        public void 写真の検索ができること()
        {
            for (int i = 0; i < 5; i++)
            {
                Photo photo = Photo.CreateFromFile(new File("dummy.bmp"));


                if (i % 2 == 1)
                {
                    photo.MarkAsFavorite();
                }

                photoRepository.Store(photo);
            }


            Func<IQueryable<Photo>, IQueryable<Photo>> query = (IQueryable<Photo> photos) =>
            {
                List<Photo> new_photos = new List<Photo>();

                //var q = from p in photos where p.IsFavorite == true select p;
                //new_photos = q.ToList();

                foreach (Photo photo in photos)
                {
                    if (photo.IsFavorite)
                    {
                        new_photos.Add(photo);
                    }
                }

                return new_photos.AsQueryable<Photo>();
            };


            IEnumerable<Photo> result0 = photoRepository.Find(photos => from p in photos where p.IsFavorite == true select p);


            IEnumerable<Photo> result = photoRepository.Find(query);

            int photo_num = 0;

            foreach (Photo photo in result0)
            {
                Assert.IsTrue(photo.IsFavorite);
                photo_num++;
            }
            Assert.AreEqual(photo_num, 2);
        }

    }
}
