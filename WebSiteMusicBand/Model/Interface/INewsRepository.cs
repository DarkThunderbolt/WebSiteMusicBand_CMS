using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMusicBand.Model
{
    interface INewsRepository
    {
        IEnumerable<News> SelectedNews { get; }

        void CreateNews(News news, int user);

        void DeleteNews(int newsId);

        void EditNews(News news);

        News GetNewsById(int? newsId);

        IEnumerable<News> GetNewsPage(PagingInfo pageInfo, bool down = true);

        IEnumerable<NewsComment> GetNewsComments(int newsId);

        int GetTotalNumberOfNews();

        void Dispose();
    }
}
