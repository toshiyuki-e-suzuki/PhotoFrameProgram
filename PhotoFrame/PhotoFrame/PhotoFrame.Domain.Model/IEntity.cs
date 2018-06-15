using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    /// <summary>
    /// ドメインで扱うオブジェクトのインターフェース
    /// </summary>
    public interface IEntity
    {
        string Id { get; }
    }
}
