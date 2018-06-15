using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    /// <summary>
    /// ドメインオブジェクトに共通するCRU(D)操作を用意したコレクションインターフェース
    /// </summary>
    /// <typeparam name="E">オブジェクト（エンティティ）</typeparam>
    public interface IRepository<E> where E : IEntity // where句で「EはIEntity型」と制約
    {
        /// <summary>
        /// クエリをもとにオブジェクトを検索する
        /// </summary>
        /// <param name="query">検索クエリ（デリゲード）</param>
        /// <returns>検索結果（コレクション）</returns>
        IEnumerable<E> Find(Func<IQueryable<E>, IQueryable<E>> query);

        /// <summary>
        /// クエリをもとにオブジェクトを検索する
        /// </summary>
        /// <param name="query">検索クエリ（デリゲード）</param>
        /// <returns>検索結果</returns>
        E Find(Func<IQueryable<E>, E> query);

        /// <summary>
        /// IDをもとにオブジェクトを検索する
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>検索結果</returns>
        E FindBy(string id);

        /// <summary>
        /// すでに存在するオブジェクトかどうか
        /// </summary>
        /// <param name="entity">オブジェクト</param>
        /// <returns>true 存在する, false 存在しない</returns>
        bool Exists(E entity);

        /// <summary>
        /// すでに存在するオブジェクトかどうか
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true 存在する, false 存在しない</returns>
        bool ExistsBy(string id);

        /// <summary>
        /// オブジェクトを保存する
        /// </summary>
        /// <param name="entity">オブジェクト</param>
        /// <returns>保存したオブジェクト</returns>
        E Store(E entity);

        
    }
}
