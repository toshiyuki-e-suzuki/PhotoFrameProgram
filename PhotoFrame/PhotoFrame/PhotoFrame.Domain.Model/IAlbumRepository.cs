using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    /// <summary>
    /// Albumをドメインオブジェクトとするコレクションインターフェース
    /// </summary>
    public interface IAlbumRepository : IRepository<Album>
    {
        
    }
}
