using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Agency.Models;
using System.Data.SqlClient;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var con = "data source=DESKTOP-V0KQMB2; Initial Catalog = agency; Integrated Security = True;";
            List<Vacancy> vacancies = new List<Vacancy>();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from Job_Information";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        vacancies.Add(new Vacancy((int)oReader["ID"], oReader["Name"].ToString(), oReader["Discription"].ToString(),
                           (int)oReader["Salary"]));
                    }
                    myConnection.Close();
                }
                ViewBag.Vacancies = vacancies;
                return View();
            }
        }

        [HttpGet]
        public IActionResult CreateNewVacancy()
        {
            return View();
        }
        [HttpPost]
        public RedirectResult CreateNewVacancy(string Name, string Discription, uint Salary)
        {
            var con = "data source=DESKTOP-V0KQMB2; Initial Catalog = agency; Integrated Security = True;";
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                myConnection.Open();
                string sql =
                    "insert into dbo.Job_Information (Name, Discription, Salary) values(@Name,@Discription,@Salary)";
                SqlCommand oCmd = new SqlCommand(sql, myConnection);
                oCmd.Parameters.Add("@Name", System.Data.SqlDbType.VarChar).Value = Name;
                oCmd.Parameters.Add("@Discription", System.Data.SqlDbType.VarChar).Value = Discription;
                oCmd.Parameters.Add("@Salary", System.Data.SqlDbType.Int).Value = Salary;
                oCmd.ExecuteNonQuery();
                myConnection.Close();
            }
            return Redirect("/Home");
        }
        public IActionResult VacancyInfo(int ID)
        {
            var con = "data source=DESKTOP-V0KQMB2; Initial Catalog = agency; Integrated Security = True;";
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from Job_Information WHERE ID = " + ID;
                Vacancy vacancy = new Vacancy();
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        vacancy = new Vacancy((int)oReader["ID"], oReader["Name"].ToString(), oReader["Discription"].ToString(),
                           (int)oReader["Salary"]);
                    }
                    myConnection.Close();
                }
                ViewBag.vacancy = vacancy;
                return View();
            }
        }
        public RedirectResult DeleteVacancy(int ID) {

            var con = "data source=DESKTOP-V0KQMB2; Initial Catalog = agency; Integrated Security = True;";
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                myConnection.Open();
                string sql =
                    "DELETE dbo.Job_Information WHERE Id = " + ID;
                SqlCommand oCmd = new SqlCommand(sql, myConnection);
                oCmd.ExecuteNonQuery();
                myConnection.Close();
            }
            return Redirect("/Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
