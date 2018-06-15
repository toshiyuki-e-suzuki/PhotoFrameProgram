using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    /// <summary>
    /// ファイルディレクトリ中の写真ファイルを扱うサービスインターフェース
    /// </summary>
    public interface IPhotoFileService
    {
        IEnumerable<File> FindAllPhotoFilesFromDirectory(string directory);
    }
}
