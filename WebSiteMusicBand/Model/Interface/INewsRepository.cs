using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMusicBand.Model
{
    public interface INewsRepository
    {
        /// <summary>
        /// Get news selected in section
        /// </summary>
        IEnumerable<News> SelectedNews { get; }
        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="news"></param>
        bool CreateNews(News news);
        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="newsId"></param>
        bool DeleteNews(int newsId);
        /// <summary>
        /// Edit news
        /// </summary>
        /// <param name="news"></param>
        bool EditNews(News news);
        /// <summary>
        /// Get news by newsId
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        News GetNewsById(int newsId);
        /// <summary>
        /// Get all news in section news by pages
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="down"></param>
        /// <returns></returns>
        IEnumerable<News> GetNewsPage(PagingInfo pageInfo, bool down = true);
        /// <summary>
        /// Get all comments for news(by newsId)
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        IEnumerable<NewsComments> GetNewsComments(int newsId);
        /// <summary>
        /// Get total number of news in section news
        /// </summary>
        /// <returns></returns>
        int GetTotalNumberOfNews();
        /// <summary>
        /// Get comment by his Id 
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        NewsComments GetCommentById(int commentId);
        /// <summary>
        /// Add new comment 
        /// </summary>
        /// <param name="comment"></param>
        bool AddComment(NewsComments comment);
        /// <summary>
        /// Edit comment
        /// </summary>
        /// <param name="comment"></param>
        bool EditComment(NewsComments comment);
        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="commentId"></param>
        bool DeleteComment(int commentId);
        /// <summary>
        /// Add news in current user favorites
        /// </summary>
        /// <param name="likeId"></param>
        bool AddLike(int likeId);
        /// <summary>
        /// Remove news from current user favorites
        /// </summary>
        /// <param name="likeId"></param>
        bool DeleteLike(int likeId);

        void Dispose();//?! VD Do I need it?
    }
}
