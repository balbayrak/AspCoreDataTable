using AspCoreDataTable.Core.DataTable.ModelBinder;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.General;
using AspCoreDataTable.Test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreDataTable.Test.Controllers
{
    public class HomeController : Controller
    {
        public static List<Person> personList = new List<Person>
                {
                    new Person() {
                        id = Guid.NewGuid(),name="Linda",surname="Estrada",status = 1,
                        PersonAdress = new PersonAdress
                        {
                            city="ankara",
                            country="Turkey"
                        }

                    },
                    new Person() {id = Guid.NewGuid(),name="George",surname="Davis",status = 0,
                     PersonAdress = new PersonAdress
                        {
                            city="istanbul",
                            country="Turkey"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Marilyn",surname="Shaw",status = 0,
                      PersonAdress = new PersonAdress
                        {
                            city="berlin",
                            country="almanya"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Terry",surname="Perez",status = 1,
                     PersonAdress = new PersonAdress
                        {
                            city="liverpool",
                            country="ingiltere"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Henry",surname="Freeman",status = -1,
                     PersonAdress = new PersonAdress
                     {
                            city="paris",
                            country="fransa"
                        }},
                    new Person() {id = Guid.NewGuid(),name="John",surname="Kerr",status = 1,
                     PersonAdress = new PersonAdress
                     {
                            city="brüksel",
                            country="belçika"
                        }}
                };


        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadTable(JQueryDataTablesModel jQueryDataTablesModel)
        {
            try
            {
                return Json(jQueryDataTablesModel.ToJqueryDataTablesResponse(personList));
            }
            catch
            {
                // ignored
            }

            return null;

        }

        [HttpPost]
        public JsonResult LoadTable2([JQueryDataTablesModelBinder] JQueryDataTablesModel jQueryDataTablesModel)
        {
            try
            {
                return Json(jQueryDataTablesModel.ToJqueryDataTablesResponse(personList));
            }
            catch
            {
                // ignored
            }

            return null;
        }

        [HttpGet]
        public IActionResult AddOrEdit(string id)
        {
            Person person = new Person();
            if (id != "-1")
            {
                Guid Uid = new Guid(id);
                person = personList.FirstOrDefault(t => t.id == Uid);
            }

            return PartialView("AddOrEdit", person);
        }

        [HttpPost]
        public string AddOrEdit(Person person)
        {
            AjaxResult result = new AjaxResult();

            Person per = personList.FirstOrDefault(t => t.id == person.id);
            if (per == null)
            {
                personList.Add(person);
                result.ResultText = "Person Added";
            }
            else
            {
                per.name = person.name;
                per.surname = person.surname;
                per.PersonAdress.city = person.PersonAdress.city;
                per.PersonAdress.country = person.PersonAdress.country;
                result.ResultText = "Person Updated";
            }

            result.Result = AjaxResultEnum.Success;
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public void Delete(Person person)
        {
            if (person != null)
            {
                person = personList.FirstOrDefault(t => t.id == person.id);
                personList.Remove(person);
            }
        }
    }
}
