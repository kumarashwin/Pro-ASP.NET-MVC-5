using System.Collections.Generic;
using System.Linq;

namespace WebServices.Models {
    public class ReservationRepository {
        private static ReservationRepository _repo = new ReservationRepository();

        public static ReservationRepository Current {
            get { return _repo; }
        }

        private List<Reservation> _data = new List<Reservation> {
            new Reservation { Id = 1, ClientName = "Adam", Location = "Board Room" },
            new Reservation { Id = 2, ClientName = "Jacqui", Location = "Lecture Hall" },
            new Reservation { Id = 3, ClientName = "Russell", Location = "Meeting Room 1" }
        };

        public IEnumerable<Reservation> GetAll() => _data;

        public Reservation Get(int id) => _data.Where(r => r.Id == id).FirstOrDefault();

        public Reservation Add(Reservation item) {
            item.Id = _data.Count + 1;
            _data.Add(item);
            return item;
        }

        public void Remove(int id) {
            Reservation item = Get(id);
            if (item != null)
                _data.Remove(item);
        }

        public bool Update(Reservation item) {
            Reservation storedItem = Get(item.Id);
            if(storedItem != null) {
                storedItem.ClientName = item.ClientName;
                storedItem.Location = item.Location;
                return true;
            }
            return false;
        }
    }
}