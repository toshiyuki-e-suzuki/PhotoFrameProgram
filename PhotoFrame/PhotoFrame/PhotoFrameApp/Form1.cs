using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhotoFrame.Application;
using PhotoFrame.Domain;
using PhotoFrame.Domain.Model;
using PhotoFrame.Domain.UseCase;
using PhotoFrame.Persistence;
using PhotoFrame.Persistence.Csv;
using System.IO;

namespace PhotoFrameApp
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// /
        /// </summary>
        private IPhotoRepository photoRepository;
        private IAlbumRepository albumRepository;
        private IEnumerable<Photo> searchedPhotos; // リストビュー上のフォトのリスト

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // 各テストごとにデータベースファイルを削除
            // (35-42をコメントアウトしても動きます)
            if (System.IO.File.Exists("_Photo.csv"))
            {
                System.IO.File.Delete("_Photo.csv");
            }
            if (System.IO.File.Exists("_Album.csv"))
            {
                System.IO.File.Delete("_Album.csv");
            }

            // リポジトリ生成
            var repos = new RepositoryFactory(PhotoFrame.Persistence.Type.Csv);
            photoRepository = repos.PhotoRepository;
            albumRepository = repos.AlbumRepository;

            // 全アルバム名を取得し、アルバム変更リストをセット
            IEnumerable<Album> allAlbums = albumRepository.Find((IQueryable<Album> albums) => albums);

            if(allAlbums != null)
            {
                foreach (Album album in allAlbums)
                {
                    comboBox_ChangeAlbum.Items.Add(album.Name);
                }
            }
            
        }

        /// <summary>
        /// アルバム名でフォトを検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SearchAlbum_Click(object sender, EventArgs e)
        {
            // this.searchedPhotos = photoRepository.Find(photos => (from p in photos where p.Album.Name == textBox_SearchAlbum.Text select p).ToList().AsQueryable());
            // ここから　(67-73は↑に書き換え可)
            Func<IQueryable<Photo>, IQueryable<Photo>> query = allPhotos =>
            {
                var q = (from p in allPhotos where p.Album.Name == textBox_SearchAlbum.Text select p).ToList();
                return q.AsQueryable();
            };

            this.searchedPhotos = photoRepository.Find(query);
            // ここまで

            listView_PhotoList.Items.Clear();

            if(searchedPhotos != null)
            {
                foreach (Photo photo in searchedPhotos)
                {
                    if (photo.IsFavorite)
                    {
                        string[] item = { photo.File.FilePath, photo.Album.Name, "★" };
                        listView_PhotoList.Items.Add(new ListViewItem(item));
                    }
                    else
                    {
                        string[] item = { photo.File.FilePath, photo.Album.Name, "" };
                        listView_PhotoList.Items.Add(new ListViewItem(item));
                    }

                }
            }
            

        }

        /// <summary>
        /// アルバム新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CreateAlbum_Click(object sender, EventArgs e)
        {
            string albumName = textBox_SearchAlbum.Text;

            if (Directory.Exists(albumName))
            {
                IEnumerable<Album> result = albumRepository.Find((IQueryable<Album> albums) => (from p in albums where p.Name == albumName select p));

                // 登録済みのアルバム名でない場合はアルバムを作成
                if (result == null || result.Count() == 0)
                {
                    Album album = Album.Create(albumName);
                    albumRepository.Store(album);

                    string[] path_list = Directory.GetFiles(albumName, "*.*", SearchOption.AllDirectories);

                    foreach (string filePath in path_list)
                    {
                        PhotoFrame.Domain.Model.File file = new PhotoFrame.Domain.Model.File(filePath);

                        if (file.IsPhoto)
                        {
                            Photo photo = Photo.CreateFromFile(file);
                            photo.IsAssignedTo(album);

                            photoRepository.Store(photo);
                        }
                    }

                    // アルバム変更用のアルバム名リストの更新
                    comboBox_ChangeAlbum.Items.Add(album.Name);
                    
                }
            }

            
        }

        /// <summary>
        /// お気に入り切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ToggleFavorite_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < listView_PhotoList.SelectedItems.Count; i++)
            {
                int index = listView_PhotoList.SelectedItems[i].Index;

                if (searchedPhotos.ElementAt(index).IsFavorite)
                {
                    searchedPhotos.ElementAt(index).MarkAsUnFavorite();
                    listView_PhotoList.SelectedItems[i].SubItems[2].Text = "";

                }
                else
                {
                    searchedPhotos.ElementAt(index).MarkAsFavorite();
                    listView_PhotoList.SelectedItems[i].SubItems[2].Text = "★";
                }

                photoRepository.Store(searchedPhotos.ElementAt(index));

            }
        }

        /// <summary>
        /// 選択中のフォトの所属アルバムを変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ChangeAlbum_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView_PhotoList.SelectedItems.Count; i++)
            {
                Func<IQueryable<Album>, Album> query = allAlbums =>
                {
                    foreach(Album album in allAlbums)
                    {
                        if(album.Name == comboBox_ChangeAlbum.SelectedItem.ToString())
                        {
                            return album;
                        }
                    }

                    return null;
                };

                int index = listView_PhotoList.SelectedItems[i].Index;
                Album newAlbum = albumRepository.Find(query);

                if(newAlbum != null)
                {
                    this.searchedPhotos.ElementAt(index).IsAssignedTo(newAlbum);
                    photoRepository.Store(searchedPhotos.ElementAt(index));
                    listView_PhotoList.SelectedItems[i].SubItems[1].Text = newAlbum.Name;
                }
                
            }
        }

        /// <summary>
        /// リストビュー上のフォトのスライドショーを実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SlideShow_Click(object sender, EventArgs e)
        {
            if(this.searchedPhotos.Count() > 0)
            {
                SlideShow slideShowForm = new SlideShow(this.searchedPhotos);
                slideShowForm.ShowDialog();
            }
            
        }
    }
}
