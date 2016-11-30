using System.Collections.Generic;
using System.Web.Http;
using WebServices.Models;

namespace WebServices.Controllers {
    public class WebController : ApiController {
        private ReservationRepository _repo = ReservationRepository.Current;
        
        public IEnumerable<Reservation> GetAll() => _repo.GetAll();

        public Reservation Get(int id) => _repo.Get(id);

        public Reservation Post(Reservation item) => _repo.Add(item);

        public void Delete(int id) => _repo.Remove(id);

        public bool Put(Reservation item) => _repo.Update(item);
    }
}
