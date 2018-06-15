using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    public class Photo : IEntity
    {
        public string Id { get; private set; }

        /// <summary>
        /// ファイル実体
        /// </summary>
        public File File { get; private set; }

        /// <summary>
        /// お気に入りかどうか
        /// </summary>
        public bool IsFavorite { get; private set; }

        /// <summary>
        /// 所属アルバム
        /// </summary>
        public virtual Album Album { get; private set; }

        /// <summary>
        /// 所属アルバムID
        /// </summary>
        public string AlbumId { get; set; }

        public Photo(string photoId, File file, bool isFavorite = false, string albumId = null, Album album = null)
        {
            Id = photoId;
            File = file;
            IsFavorite = isFavorite;
            AlbumId = albumId;
            Album = album;
        }

        public static Photo CreateFromFile(File file)
        {
            if (!file.IsPhoto)
            {
                throw new ArgumentException("The specified file is not a photo.");
            }
            return new Photo(Guid.NewGuid().ToString(), file);
        }

        private Photo() { }

        public void IsAssignedTo(Album album)
        {
            Album = album;
            AlbumId = album.Id;
        }

        public void MarkAsFavorite()
        {
            IsFavorite = true;
        }

        public void MarkAsUnFavorite()
        {
            IsFavorite = false;
        }

        public override bool Equals(object obj)
        {
            if ((object)this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            return Id == ((Photo)obj).Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Photo photo1, Photo photo2)
        {
            if (ReferenceEquals(photo1, photo2)) return true;
            if ((object)photo1 == null || (object)photo2 == null) return false;
            return photo1.Equals(photo2);
        }

        public static bool operator !=(Photo photo1, Photo photo2) => !(photo1 == photo2);
    }
}
