using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDB;

namespace WebApiWithDB.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<WebApiDB.Employee> Get()
        {
            using(WebApiDB.EmployeesDBEntities entities = new WebApiDB.EmployeesDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public WebApiDB.Employee Get(int id)
        {
            using (WebApiDB.EmployeesDBEntities entities = new WebApiDB.EmployeesDBEntities())
            {
                return entities.Employees.FirstOrDefault(e => e.PersonID == id);
            }
        }

        // POST api/employees
        public void Post([FromBody]Employee employee)
        {
            using (WebApiDB.EmployeesDBEntities entities = new WebApiDB.EmployeesDBEntities())
            {
                entities.Employees.Add(employee);
            }
        }

        // PUT api/employees/5
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            using (WebApiDB.EmployeesDBEntities entities = new WebApiDB.EmployeesDBEntities())
            {
                try
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.PersonID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id + " does not found.");
                    }
                    else
                    {

                        entity.LastName = employee.LastName;
                        entity.FirstName = employee.FirstName;
                        entity.Address = employee.Address;
                        entity.City = employee.City;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

                }
            }
        }

        // DELETE api/employees/5
        public HttpResponseMessage Delete(int id)
        {

            using (WebApiDB.EmployeesDBEntities entities = new WebApiDB.EmployeesDBEntities())
            {
                try
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.PersonID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id + " does not found.");
                    }
                    else
                    {
                        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.PersonID == id));
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

                }
            }

        }
    }
}
