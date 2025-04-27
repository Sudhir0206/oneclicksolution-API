using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using OneClickSolution.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OneClickSolution.Controllers
{
    [Route("api/Quotation")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public QuotationController(AppDbContext appdbContext)
        {
            appDbContext = appdbContext;
        }
        // GET: api/<ValuesController>
        [HttpGet("GetQuotation")]
        public async Task<ActionResult<IEnumerable<Quotation>>> GetQuatation()
        {
            if (appDbContext.quotation == null)
            {
                return NotFound();
            }
            return await appDbContext.quotation.ToListAsync();
        }

        // GET api/<GetQuotation>/5
        [HttpGet("GetById")]
        public async Task<ActionResult<Quotation>> GetQuotation(int id)
        {
            try
            {
                if (appDbContext.quotation == null)
                {
                    return NotFound();
                }
                var quotation = await appDbContext.quotation.FindAsync(id);
                if (quotation == null)
                {
                    return NotFound();
                }
                return quotation;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred:{ex.Message}");
            }
        }

        // POST api/<AddQuotation>
        [HttpPost("AddQuotation")]
        public async Task<ActionResult<Quotation>> AddQuotation(Quotation quotation)
        {
            try
            {
                if (appDbContext.quotation == null)
                {
                    return Problem("Entity set 'Qutation' is null");
                }
                // var data = new Quotation();
                var data = appDbContext.quotation.Add(quotation).Entity;
                await appDbContext.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        // PUT api/<Update>/5
        [HttpPut("UpdateQuotation")]
        public async Task<ActionResult<Quotation>> Update(int id, Quotation quotation)
        {
            if (id != quotation.Id)
            {
                return BadRequest();
            }
            appDbContext.Entry(quotation).State = EntityState.Modified;
            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!QutationExist(id))
                {
                    NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool QutationExist(int Id)
        {
            return appDbContext.quotation?.Any(q => q.Id == Id) ?? false;
        }


        // DELETE api/<DeleteQuotation>/5
        [HttpDelete("Delete")]
        public async Task<ActionResult<Quotation>> Delete(int id)
        {
            try
            {


                if (appDbContext.quotation == null)
                {
                    return NotFound();
                }
                var quoatationDetails = await appDbContext.quotation.FindAsync(id);
                if (quoatationDetails == null)
                {
                    return NotFound();
                }
                appDbContext.quotation.Remove(quoatationDetails);
                await appDbContext.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
