using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoFrame.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PhotoFrame.Persistence.Test
{
    [TestClass]
    public class EFRepositoryTest
    {
        private static IPhotoRepository photoRepository;
        private static IAlbumRepository albumRepository;

        private TransactionScope scope;

        [ClassInitialize]
        public static void SetUpTestCase(TestContext context)
        {
            // リポジトリ生成
            var repos = new RepositoryFactory(Type.EF);
            photoRepository = repos.PhotoRepository;
            albumRepository = repos.AlbumRepository;

            // テスト用データベースが存在していない場合は作りにいくが、
            // Transaction中ではテーブルを作成できないため、ここで作成させる
            photoRepository.FindBy("dummy");
            albumRepository.FindBy("dummy");
        }

        [TestInitialize]
        public void SetUp()
        {
            scope = new TransactionScope();
        }

        [TestCleanup]
        public void TearDown()
        {
            scope.Dispose();
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
    }
}
