using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Subject
    {
        DAO_Subject _daoSubject = new DAO_Subject();
        public int CountSubject() => _daoSubject.Count();
        public List<Subject> getAllSubject() => _daoSubject.GetAll();
        public List<Subject> GetAllSubject() => _daoSubject.GetAll();
    }
}
