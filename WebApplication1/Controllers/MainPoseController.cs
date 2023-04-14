using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class MainPoseController : Controller
    {
        private readonly IMainPoseRepository _mainPoseRepository;


        public MainPoseController(IMainPoseRepository mainPoseRepository)
        {
            _mainPoseRepository = mainPoseRepository;


        }
        public async Task<IActionResult> Index() // อันนี้ใช้ return view() ในหน้า club -----> controller 
        {
            IEnumerable<MainPose> mainPoses = await _mainPoseRepository.GetAll(); // เป็นการดึง database แล้วมาใส่ในโปรแกรมเรา แล้ว Tolist เพื่อดำเนินการ sql แล่้วดึงข้อมูลมา return 
                                                                                  // ให้ clubs โดยตารางที่ดึงมาจะเป็นของ Club 
                                                                                  // List<Club> clubs; อันนี้คือ model
            return View(mainPoses);                  // แล้วเอาข้อมูลมาใช้กับ View ->>อันนี้คือ view
        }
        public async Task<IActionResult> Detail(int id)
        {
            MainPose mainPose = await _mainPoseRepository.GetByIdAsync(id);
            IEnumerable<Comment> comments = await _mainPoseRepository.GetCommentsByMainPoseId(id);
            return View("Detail", (mainPose, comments));

        }
    }
}
