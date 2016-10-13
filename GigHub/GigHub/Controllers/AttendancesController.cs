using GigHub.Models;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Linq;
using GigHub.Dtos;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext context;
        public AttendancesController()
        {
            this.context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDataTransferObject dto)
        {
            var userId = User.Identity.GetUserId();

            var exists = context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exists)
            {
                return BadRequest("Attendence already exists");
            }
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            context.Attendances.Add(attendance);
            context.SaveChanges();

            return Ok();
        }

    }
}
