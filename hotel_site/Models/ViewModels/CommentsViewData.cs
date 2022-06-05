using System.Collections.Generic;

namespace hotel_site.Models.ViewModels
{
    public class CommentsViewData
    {
        public IEnumerable<Comment> Comments { get; set; }
        public bool AddCommentEnabled { get; set; }
    }
}
