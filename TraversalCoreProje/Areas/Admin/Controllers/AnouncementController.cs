using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer.DTOs.AnouncementDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IMapper _mapper;

        public AnouncementController(IAnnouncementService announcementService, IMapper mapper)
        {
            _announcementService = announcementService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            var values = _mapper.Map<List<AnouncementListDTO>>(_announcementService.TGetList());

            return View(values);
            //List<Announcement> announcements = _announcementService.TGetList();
            //List<AnouncementListViewsModel> model = new List<AnouncementListViewsModel>();
            //foreach (var item in model)
            //{
            //    AnouncementListViewsModel anouncementListViewsModel = new AnouncementListViewsModel();
            //    anouncementListViewsModel.ID = item.ID;
            //    anouncementListViewsModel.Title = item.Title;
            //    anouncementListViewsModel.Content = item.Content;

            //    model.Add(anouncementListViewsModel);
            //}
            //return View(model);
        }

        [HttpGet]
        public IActionResult AddAnouncement()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddAnouncement(AnouncementAddDTOs model)
        {
            if (ModelState.IsValid)
            {
                _announcementService.TAdd(new Announcement()
                {
                    Content = model.Content,
                    Title = model.Title,
                    Date = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult DeleteAnouncement(int id)
        {
            var values = _announcementService.TGetByID(id);
            _announcementService.TDelete(values);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateAnouncement(int id)
        {
            var values = _mapper.Map<AnouncementUpdateDto>(_announcementService.TGetByID(id));
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateAnouncement(AnouncementUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                _announcementService.TUpdate(new Announcement
                {
                    AnnouncementID = model.AnnouncementID,
                    Content = model.Content,
                    Date = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    Title = model.Title,
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
