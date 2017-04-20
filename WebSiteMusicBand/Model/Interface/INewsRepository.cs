using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMusicBand.Model
{
    public interface INewsRepository
    {
        IEnumerable<News> SelectedNews { get; }
        void CreateNews(News news);
        void DeleteNews(int newsId);
        void EditNews(News news);
        News GetNewsById(int newsId);
        IEnumerable<News> GetNewsPage(PagingInfo pageInfo, bool down = true);
        IEnumerable<NewsComments> GetNewsComments(int newsId);
        int GetTotalNumberOfNews();
        void AddComment(NewsComments comment);

        NewsComments GetCommentById(int commentId);
        void EditComment(NewsComments comment);
        void DeleteComment(int commentId);

        void AddLike(int likeId);
        void DeleteLike(int likeId);

        void Dispose();
    }
}
