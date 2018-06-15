using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFrame.Domain.Model
{
    public class File
    {
        // 写真として扱う拡張子の定義
        private static IList<string> photoFileExtensions = new List<string>()
        {
            ".bmp", ".png", ".jpg", ".jpeg"
        };

        /// <summary>
        /// パス
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// 写真かどうか（拡張子による判別）
        /// </summary>
        public bool IsPhoto { get; }





        public File(string filePath)
        {
            FilePath = filePath;
            IsPhoto = HasPhotoFileExtension(filePath);
        }





        private File() { }

        private bool HasPhotoFileExtension(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return photoFileExtensions.Any(x => x == ext);
        }

        public override bool Equals(object obj)
        {
            if ((object)this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            return FilePath == ((File)obj).FilePath;
        }

        public override int GetHashCode() => FilePath.GetHashCode();

        public static bool operator ==(File File1, File File2)
        {
            if (ReferenceEquals(File1, File2)) return true;
            if ((object)File1 == null || (object)File2 == null) return false;
            return File1.Equals(File2);
        }

        public static bool operator !=(File File1, File File2) => !(File1 == File2);
    }
}
