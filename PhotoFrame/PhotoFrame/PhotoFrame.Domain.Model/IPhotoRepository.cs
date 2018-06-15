using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    /// <summary>
    /// Photoをドメインオブジェクトとするコレクションインターフェース
    /// </summary>
    public interface IPhotoRepository : IRepository<Photo>
    {
        /// <summary>
        /// 存在しなければ追加する
        /// </summary>
        /// <param name="photos">Photoのコレクション</param>
        void StoreIfNotExists(IEnumerable<Photo> photos);
    }
}
