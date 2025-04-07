using Charity_Website_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charity_Website_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class Donation : ControllerBase
    {
        DBCNhom1 dbc;

        public Donation(DBCNhom1 dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.TblDonations.ToList());
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult AddDonation(String d_donation_id, String d_user_id, String d_campaign_id, String d_name, decimal d_amount, decimal d_total_value)
        {
            TblDonation donation = new TblDonation();
            donation.DDonationId = d_donation_id;
            donation.DUserId = d_user_id;
            donation.DCampaignId = d_campaign_id;
            donation.DName = d_name;
            donation.DAmount = d_amount;
            donation.DTotalValue = d_total_value;
            donation.DDonationDate = DateTime.Now;

            dbc.TblDonations.Add(donation);
            dbc.SaveChanges();

            dbc.Database.ExecuteSqlRaw("EXEC UpdateCollectedAmount;");

            return Ok("Insert donation ID: " + d_donation_id + " successfully!");
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateDonation(String d_donation_id, String d_user_id, String d_campaign_id, String d_name, decimal d_amount, decimal d_total_value)
        {
            var existingDonation = dbc.TblDonations.Find(d_donation_id);

            existingDonation.DDonationId = d_donation_id;
            existingDonation.DUserId = d_user_id;
            existingDonation.DCampaignId = d_campaign_id;
            existingDonation.DName = d_name;
            existingDonation.DAmount = d_amount;
            existingDonation.DTotalValue = d_total_value;
            existingDonation.DDonationDate = DateTime.Now;

            dbc.TblDonations.Update(existingDonation);

            dbc.SaveChanges();

            dbc.Database.ExecuteSqlRaw("EXEC UpdateCollectedAmount;");

            return Ok("Update donation ID " + d_donation_id + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteDonation(String d_donation_id)
        {
            var donation = dbc.TblDonations.Find(d_donation_id);

            dbc.TblDonations.Remove(donation);
            dbc.SaveChanges();

            dbc.Database.ExecuteSqlRaw("EXEC UpdateCollectedAmount;");

            return Ok("Delete donation ID " + d_donation_id + " successfully!");
        }
    }
}
