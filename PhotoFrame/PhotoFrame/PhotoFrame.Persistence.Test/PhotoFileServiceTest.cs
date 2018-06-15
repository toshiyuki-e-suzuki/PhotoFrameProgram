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
    public class PhotoFileServiceTest
    {
        private IPhotoFileService service;

        [TestInitialize]
        public void SetUp()
        {
            service = new ServiceFactory().PhotoFileService;
        }

        [TestMethod]
        public void 指定されたフォルダの写真ファイルが取得できること()
        {
            // テストデータをどう与えるかなどはお任せします
            var result = service.FindAllPhotoFilesFromDirectory("TestDir");

            // テストデータに応じたアサーション
            string[] test = { @"TestDir\sample1.bmp" , @"TestDir\sample2.bmp", @"TestDir\SubTestDir\sample3.bmp" };
            int i = 0;

            foreach(Domain.Model.File file in result)
            {
                Assert.AreEqual(file.FilePath, test[i]);
                i++;

            }

            //Assert.AreEqual(result.Count(), 0);
        }

        // テストの観点としてはエッジケース（0枚時など）やディレクトリのネスト、存在しないディレクトリの指定やパーミッションがないなどの例外処理など
    }
}
