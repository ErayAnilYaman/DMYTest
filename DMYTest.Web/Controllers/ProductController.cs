namespace DMYTest.Web.Controllers
{
    #region Usings
    using DMYTest.Data.Concrete;
    using DMYTest.Data.Context;
    using DMYTest.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    #endregion
    public class ProductController : Controller
    {
        ProductRepository repository = new ProductRepository();
        DMYDBContext context = new DMYDBContext();
        // GET: Product
        public PartialViewResult Popular(int pageNumber = 1)
        {
            var product = repository.GetPopularList();
            ViewBag.popular = product;
            return PartialView(product);
        }
        public ActionResult ProductDetails(int id)
        {
            
            var details = repository.GetById(id);
            var comments = context.Comments.Where(c => c.ProductID == id).ToList();
            var commentLikes = new Dictionary<int, string>();
            
            ViewBag.IsLiked = "unliked";
            if (User.Identity.IsAuthenticated)
            {
                
                foreach (var comment in comments)
                {
                    bool isLiked = context.CommentLikes.Any(like => like.Comment.ID == comment.ID && like.User.Email == User.Identity.Name);
                    commentLikes[comment.ID] = "unliked";
                    if (isLiked)
                    {
                        commentLikes[comment.ID] = "liked";
                    }
                }
                ViewBag.Comments = comments;
                ViewBag.CommentLikes = commentLikes;
                return View(details);
            }
            else
            {
                foreach (var comment in comments)
                {
                    commentLikes[comment.ID] = "unliked";
                }
                ViewBag.CommentLikes = commentLikes;
                ViewBag.Comments = comments;
                return View(details);
            }
            
        }

        [HttpPost]
        public JsonResult AddComment(int productID, string content)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                var product = context.Products.FirstOrDefault(P => P.ProductID == productID);
                var likeCount = 0;
                var comment = new Comment
                {
                    Contents = content,
                    Date = DateTime.Now,
                    ProductID = productID,
                    UserID = user.ID,
                    User = user,
                    Product = product,
                    
                };
                context.Comments.Add(comment);
                context.SaveChanges();
                return Json(new { success = true, message = "Comment added.", productID = comment.ProductID, content = comment.Contents, username = user.UserName, date = DateTime.Now.ToString(), commentID = comment.ID , likeCount =likeCount});
            }
            return Json(new { success = false, message = "You need to login if u want to do comment" });
        }
        [HttpPost]
        public JsonResult LikeComment(int commentID)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = context.Users.FirstOrDefault(U => U.Email == User.Identity.Name);
                    var comment = context.Comments.Find(commentID);
                    
                    if (comment!=null)
                    {
                        var isCommentLiked = user.Likes.FirstOrDefault(L => L.Comment.ID == commentID);
                        if (isCommentLiked!=null)
                        {
                            context.CommentLikes.Remove(isCommentLiked);
                            context.SaveChanges();
                            var likeCount = comment.Likes.Count;

                            return Json(new { success = true, message = "Comment unliked!", likeCount = comment.Likes.Count, progress = "unliked"});
                        }
                        else
                        {
                            context.CommentLikes.Add(new CommentLikeModel
                            {
                                Comment = comment,
                                User = user
                            });
                            context.SaveChanges();
                            var likeCount = comment.Likes.Count;
                            return Json(new { success = true, message = "Comment Liked!" ,likeCount = comment.Likes.Count, progress ="liked"});
                        }
                    }
                    
                    return Json(new { success = false, message = "Comment Couldn't find" });
                    
                }
                return Json(new { success = false , message = "you need login to like comment"});
            }
            catch (Exception e)
            {

                return Json(new { success = false , message = e.Message});
            }
        }
        public JsonResult ListLikes(int commentID)
        {
            try
            {
                var comment = context.Comments.Find(commentID);
                if (comment!=null)
                {
                    List<SelectListItem> likes;
                    likes = (from l in context.CommentLikes.Where(L => L.Comment.ID == comment.ID).ToList()
                             select new SelectListItem
                             {
                                 Text = l.User.UserName,
                                 Value = l.User.ID.ToString()
                             }).ToList();
                    ViewBag.LikeList = likes;
                    return Json(new { success = true, message = comment.User.UserName + " in yorumunu begenenler" , likes = likes});
                }
                return Json(new { success = false, message = "Comment not found!" });
            }
            catch (Exception e)
            {

                return Json(new { success = false , message = e.Message});
            }
        }
    }
}
