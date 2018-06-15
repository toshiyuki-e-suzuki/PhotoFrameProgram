using PhotoFrame.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PhotoFrame.Persistence.Csv
{
    /// <summary>
    /// <see cref="IPhotoRepository">の実装クラス
    /// </summary>
    public class PhotoRepository : IPhotoRepository
    {
        /// <summary>
        /// 永続化ストアとして利用するCSVファイルパス
        /// </summary>
        private string CsvFilePath { get; }
        private IAlbumRepository albumRepository;

        public PhotoRepository(string databaseName, IAlbumRepository albumRepository)
        {
            this.CsvFilePath = $"{databaseName}_Photo.csv"; // $"{...}" : 文字列展開
            this.albumRepository = albumRepository;
        }

        public bool Exists(Photo entity)
        {
            // TODO: ファイルIO講座以降で実装可能
            throw new NotImplementedException();
        }

        public bool ExistsBy(string id)
        {
            // TODO: ファイルIO講座以降で実装可能
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> Find(Func<IQueryable<Photo>, IQueryable<Photo>> query)
        {

            // TODO: イベント・デリゲート講座で実装予定
            List<Photo> photos = new List<Photo>();

            if (System.IO.File.Exists(CsvFilePath))
            {
                using (StreamReader sr = new StreamReader(CsvFilePath, Encoding.UTF8))
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        string[] photoData = line.Split(',');

                        Domain.Model.File file = new Domain.Model.File(photoData[1]);
                        Domain.Model.Album album = albumRepository.FindBy(photoData[3]);

                        photos.Add(new Photo(photoData[0], file, Convert.ToBoolean(photoData[2]), photoData[3], album));

                    }
                }

                return query(photos.AsQueryable<Photo>());

            }
            else
            {
                // なかったよ
                return null;
            }
        }

        public Photo Find(Func<IQueryable<Photo>, Photo> query)
        {
            // TODO: イベント・デリゲート講座で実装予定
            List<Photo> photos = new List<Photo>();

            if (System.IO.File.Exists(CsvFilePath))
            {
                using (StreamReader sr = new StreamReader(CsvFilePath, Encoding.UTF8))
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        string[] photoData = line.Split(',');
                           
                        Domain.Model.File file = new Domain.Model.File(photoData[1]);
                        Domain.Model.Album album = albumRepository.FindBy(photoData[3]);

                        photos.Add(new Photo(photoData[0], file, Convert.ToBoolean(photoData[2]), photoData[3], album));
                        
                    }
                }

                return query(photos.AsQueryable<Photo>());

            }
            else
            {
                // なかったよ
                return null;
            }

            
        }

        public Photo FindBy(string id)
        {
            // TODO: ファイルIO講座で実装
            // 保存したcsvからidを検索
            if (System.IO.File.Exists(CsvFilePath))
            {
                using (StreamReader sr = new StreamReader(CsvFilePath, Encoding.UTF8))
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        string[] photoData = line.Split(',');
                        if (photoData[0] == id)
                        {
                            // あったよ
                            Domain.Model.File file = new Domain.Model.File(photoData[1]);

                            Domain.Model.Album album = albumRepository.FindBy(photoData[3]);

                            return new Photo(photoData[0], file, Convert.ToBoolean(photoData[2]), photoData[3], album);
                        }
                    }
                }

            }

            // なかったよ
            return null;
        }

        public IEnumerable<Photo> FindByIsFavorite()
        {
            List<Photo> photo_list = new List<Photo>();

            if (System.IO.File.Exists(CsvFilePath))
            {
                using (StreamReader sr = new StreamReader(CsvFilePath, Encoding.UTF8))
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        string[] photoData = line.Split(',');
                        bool isFavorite = Convert.ToBoolean(photoData[2]);

                        if (isFavorite == true)
                        {
                            // あったよ
                            Domain.Model.File file = new Domain.Model.File(photoData[1]);

                            Domain.Model.Album album = albumRepository.FindBy(photoData[3]);

                            photo_list.Add(new Photo(photoData[0], file, isFavorite, photoData[3], album));
                        }
                    }
                }

            }

            // なかったよ
            return photo_list;
        }

        public Photo Store(Photo entity)
        {
            // TODO: ファイルIO講座で実装
            // entityをcsvファイルに1行出力して保存する
            List<string> temp_list = new List<string>();

            // ファイルあった場合
            if (System.IO.File.Exists(this.CsvFilePath))
            {
                // 新規フォトとIDが合致しないフォトデータだけtemp_listに避難
                using (StreamReader sr = new StreamReader(this.CsvFilePath))
                {
                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine();
                        string[] value = line.Split(',');

                        if (value[0] != entity.Id)
                        {
                            temp_list.Add(line);
                        }
                    }
                }

                // csvファイルを空っぽにする
                System.IO.File.Delete(this.CsvFilePath);
                //System.IO.File.Create(this.CsvFilePath);


                using (StreamWriter sw = new StreamWriter(this.CsvFilePath))
                {
                    // temp_list内のフォトデータを書き込み
                    foreach (string data in temp_list)
                    {
                        sw.WriteLine(data);
                    }

                }


            }
            // ファイルなかった場合
            else
            {
                //System.IO.File.Create(this.CsvFilePath);
            }

            // 新規フォトデータを書き込み
            using (StreamWriter sw = new StreamWriter(this.CsvFilePath, true))
            {
                List<string> photoData = new List<string>();
                photoData.Add(entity.Id);
                photoData.Add(entity.File.FilePath);
                photoData.Add(entity.IsFavorite.ToString());
                photoData.Add(entity.AlbumId);
               
                for (int i = 0; i < photoData.Count - 1; i++)
                {
                    sw.Write(photoData[i]);
                    sw.Write(",");
                }
                sw.WriteLine(photoData[photoData.Count - 1]);
                
            }

            return entity;
            //throw new NotImplementedException();
        }

        public void StoreIfNotExists(IEnumerable<Photo> photos)
        {
            // TODO: ファイルIO講座以降で実装可能
            throw new NotImplementedException();
        }
    }
}
