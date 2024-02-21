using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    public class CommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EfCommentDal());
        private readonly UserManager<AppUser> _userManager;

        public CommentController(CommentManager commentManager, UserManager<AppUser> userManager)
        {
            this.commentManager = commentManager;
            _userManager = userManager;
        }

        [HttpGet]
        public  PartialViewResult AddComment()
        {
            //var values = _userManager.FindByNameAsync(User.Identity.Name);
            //ViewBag.desıd = id;
            //ViewBag.userıd = values.Id;
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddComment(Comment p)
        {

            p.CommentDate =Convert.ToDateTime( DateTime.Now.ToShortDateString());
            p.CommentState = true;
 
            commentManager.TAdd(p);
            return RedirectToAction("Index", "Destination");
        }



        //[HttpPost]
        //public PartialViewResult AddComment(Comment p)
        //{
        //    p.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //    p.CommentState = true;
        //    commentManager.TAdd(p);
        //     return PartialView();
        //}
    }
}
