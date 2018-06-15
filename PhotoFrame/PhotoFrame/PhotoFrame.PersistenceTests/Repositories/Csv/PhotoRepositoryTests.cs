using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoFrame.Persistence.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoFrame.Domain.Model;


namespace PhotoFrame.Persistence.Csv.Tests
{
    //[TestClass()]
    //public class PhotoRepositoryTests
    //{
    //    private PhotoRepository photoRepository;
    //    private IAlbumRepository albumRepository;

    //    [TestInitialize]
    //    public void SetUp()
    //    {
    //        // 各テストごとにデータベースファイルを削除
    //        // TODO: 実装によってCSVファイル名が違うと思うので適宜修正を
    //        if (System.IO.File.Exists("PhotoFrameDatabaseForTest_Photo.csv"))
    //        {
    //            System.IO.File.Delete("PhotoFrameDatabaseForTest_Photo.csv");
    //        }
    //        if (System.IO.File.Exists("PhotoFrameDatabaseForTest_Album.csv"))
    //        {
    //            System.IO.File.Delete("PhotoFrameDatabaseForTest_Album.csv");
    //        }

    //        // リポジトリ生成
    //        var repos = new RepositoryFactory(Type.Csv);
    //        photoRepository = repos.PhotoRepository;
    //        albumRepository = repos.AlbumRepository;
    //    }

    //    [TestMethod]
    //    public void お気に入り検索ができること()
    //    {
            
    //        List<Photo> photo_list = new List<Photo>();

    //        for (int i = 0; i < 5; i++)
    //        {
    //            photo_list.Add(Photo.CreateFromFile(new File("dummy.bmp")));

    //            if (i % 2 == 1)
    //            {
    //                photo_list[i].MarkAsFavorite();

    //            }
    //        }

    //        foreach (Photo photo in photo_list)
    //        {
    //            photoRepository.Store(photo);
    //        }

    //        var favorite_list = photoRepository.FindByIsFavorite();

    //        int num = 0;

    //        foreach (Photo photo in favorite_list)
    //        {
    //            if (num % 2 == 1)
    //            {
    //                Assert.IsTrue(photo.IsFavorite);
    //            }
    //            else
    //            {
    //                Assert.IsFalse(photo.IsFavorite);
    //            }

    //        }
    //    }

    //     //10万件突っ込むなどの意地悪なテストがあっても面白いと思います。

    //}
}