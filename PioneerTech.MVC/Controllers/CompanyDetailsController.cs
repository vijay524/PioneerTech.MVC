using PioneerTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PioneerTech.MVC.Controllers
{
    public class CompanyDetailsController : Controller
    {
        string connectionstring = @"Data Source=DESKTOP-GQMFKE5\SQLEXPRESS;Initial Catalog=PioneerEmployeeDB;Integrated Security=True";
        // GET: Company
        public ActionResult Index()
        {
            DataTable dtblcompany = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Company_Details", sqlcon);
                sqlda.Fill(dtblcompany);
            }
            return View(dtblcompany);
        }
        // GET: Company/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CompanyDetailsModel());
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(CompanyDetailsModel cmpmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "INSERT INTO Company_Details VALUES(@Employer_Name,@Contact_Number,@Location,@Website)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@Employer_Name", cmpmodel.Employer_Name);
                cmd.Parameters.AddWithValue("@Contact_Number", cmpmodel.Contact_Number);
                cmd.Parameters.AddWithValue("@Location", cmpmodel.Location);
                cmd.Parameters.AddWithValue("@Website", cmpmodel.Website);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        // GET: Company/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CompanyDetailsModel cmmod = new CompanyDetailsModel();
            DataTable dbcompany = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string editquery = "SELECT * FROM Company_Details WHERE EmployeeID=@EmployeeID";
                SqlDataAdapter sqladp = new SqlDataAdapter(editquery, sqlcon);
                sqladp.SelectCommand.Parameters.AddWithValue("@EmployeeID", id);
                sqladp.Fill(dbcompany);
            }
            if (dbcompany.Rows.Count == 1)
            {
                cmmod.EmployeeID = Convert.ToInt32(dbcompany.Rows[0][0].ToString());
                cmmod.Employer_Name = dbcompany.Rows[0][1].ToString();
                cmmod.Contact_Number = dbcompany.Rows[0][2].ToString();
                cmmod.Location = dbcompany.Rows[0][3].ToString();
                cmmod.Website = dbcompany.Rows[0][4].ToString();
                return View(cmmod);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(CompanyDetailsModel cmpmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string edquery = "UPDATE Company_Details SET Employer_Name=@Employer_Name,Contact_Number=@Contact_Number,Location=@Location,Website=@Website WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(edquery, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", cmpmodel.EmployeeID);
                cmd.Parameters.AddWithValue("@Employer_Name", cmpmodel.Employer_Name);
                cmd.Parameters.AddWithValue("@Contact_Number", cmpmodel.Contact_Number);
                cmd.Parameters.AddWithValue("@Location", cmpmodel.Location);
                cmd.Parameters.AddWithValue("@Website", cmpmodel.Website);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE FROM Company_Details WHERE EmployeeID=@EmployeeID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}