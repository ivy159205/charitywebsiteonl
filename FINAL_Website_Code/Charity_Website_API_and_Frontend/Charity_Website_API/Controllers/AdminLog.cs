using Charity_Website_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;



namespace Charity_Website_API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AdminLog : ControllerBase
    {
        DBCNhom1 dbc;

        public AdminLog(DBCNhom1 dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.TblAdminLogs.ToList());
        }

        string TaoLogID()
        {
            string id = "";
            var cmd = dbc.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_tbl_admin_logs_getID";
            cmd.Connection.Open();
            id = cmd.ExecuteScalar().ToString();

            //var kq = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(kq);
            //id = dt.Rows[0][0].ToString();
            return id;
        }
        string TaoAdminID()
        {
            string id = "U001";

            return id;
        }
        [HttpPost]
        [Route("Insert")]

        public IActionResult AddAdminLog(String a_log_id, String a_admin_id, String a_action)
        {
            TblAdminLog adminLog = new TblAdminLog();
            // tao log_id khi ko goi them truc  => ""
            if (a_log_id == "") a_log_id = TaoLogID();
            if (a_admin_id == "") a_admin_id = TaoAdminID();
          
            adminLog.ALogId = a_log_id;
            adminLog.AAdminId = a_admin_id;
            adminLog.AAction = a_action;
            adminLog.AActionDate = DateTime.Now;

            dbc.TblAdminLogs.Add(adminLog);
            dbc.SaveChanges();

            return Ok("Insert AdminLog ID: " + a_log_id + " successfully!");
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateAdminLog(String a_log_id, String a_admin_id, String a_action)
        {
            var existingAdminLog = dbc.TblAdminLogs.Find(a_log_id);

            existingAdminLog.ALogId = a_log_id;
            existingAdminLog.AAdminId = a_admin_id;
            existingAdminLog.AAction = a_action;
            existingAdminLog.AActionDate = DateTime.Now;

            dbc.TblAdminLogs.Update(existingAdminLog);
            dbc.SaveChanges();

            return Ok("Update AdminLog ID " + a_log_id + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteAdminLog(String a_log_id)
        {
            var adminLog = dbc.TblAdminLogs.Find(a_log_id);

            dbc.TblAdminLogs.Remove(adminLog);
            dbc.SaveChanges();

            return Ok("Delete AdminLog ID " + a_log_id + " successfully!");
        }
    }
}
