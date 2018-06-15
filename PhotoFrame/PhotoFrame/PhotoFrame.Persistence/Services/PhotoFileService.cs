using PhotoFrame.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PhotoFrame.Persistence
{
    /// <summary>
    /// <see cref="IPhotoFileService">の実装クラス
    /// </summary>
    class PhotoFileService : IPhotoFileService
    {
        public IEnumerable<Domain.Model.File> FindAllPhotoFilesFromDirectory(string directory)
        {
            // TODO: コレクション講座で実装予定
            List<Domain.Model.File> file_list = new List<Domain.Model.File>();

            // directoryが存在する場合
            if (Directory.Exists(directory))
            {
                List<string> path_list = Enumerate(directory);

                foreach (string filePath in path_list)
                {
                    Domain.Model.File file = new Domain.Model.File(filePath);

                    if(file.IsPhoto)
                    {
                        file_list.Add(file);
                    }
                }

            }
            //else
            //{
            //    throw new IOException("指定したディレクトリが存在しません");
            //}

            return file_list;
            
        }

        public List<string> Enumerate(string dir)
        {
            string[] files = Directory.GetFiles(dir);
            List<string> file_list = new List<string>();

            foreach (string s in files)
            {
                file_list.Add(s);
            }

            string[] dirs = Directory.GetDirectories(dir);

            foreach(string s in dirs)
            {
                List<string> temp_list = Enumerate(s);

                foreach(string t in temp_list)
                {
                    file_list.Add(t);
                }
            }

            return file_list;
        }
    }
}
