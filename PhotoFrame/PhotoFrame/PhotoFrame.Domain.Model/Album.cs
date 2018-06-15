using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    public class Album : IEntity
    {
        public string Id { get; private set; }

        /// <summary>
        /// アルバム名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 所属写真（コレクション）
        /// </summary>
        public virtual ICollection<Photo> Photos { get; private set; }

        public static Album Create(string name, string description = null)
            => new Album(Guid.NewGuid().ToString(), name, description);

        public Album(string albumId, string name, string description)
        {
            Id = albumId;
            Name = name;
            Description = description;
        }

        private Album() { }

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }

        public override bool Equals(object obj)
        {
            if ((object)this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            return Id == ((Album)obj).Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Album album1, Album album2)
        {
            if (ReferenceEquals(album1, album2)) return true;
            if ((object)album1 == null || (object)album2 == null) return false;
            return album1.Equals(album2);
        }

        public static bool operator !=(Album album1, Album album2) => !(album1 == album2);
    }
}
