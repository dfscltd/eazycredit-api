using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Persistence.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/workflow")]
    [ApiController]
    public class CreditAPIServices : ControllerBase
    {
        private readonly ICreditServices creditServices;
        public CreditAPIServices(ICreditServices creditServices) { 
            this.creditServices = creditServices;
        }

        [HttpPost("AddCreditAsync")]
        public async Task<IActionResult> LoanBooking([FromBody] LoanApplicationRequestDto request)
        {
            var response = await creditServices.LoanBooking(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddCreditWithDepenciesAsync")]
        public async Task<IActionResult> LoanBookingWithDepencies([FromBody] CreateCreditMaintHistWithDependenciesDto request)
        {
            var response = await creditServices.LoanBookingWithDepencies(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetLookupAsync")]
        public async Task<IActionResult> Lookup()
        {
            var response = await creditServices.Lookup();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetCreditScheduleByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetCreditSchedulesLines(string creditId)
        {
            var response = await creditServices.GetCreditSchedulesLines(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        /// <summary>
        /// AccountNo = 0,
        /// Name = 1,
        /// Bvn = 2
        /// </summary>
        /// <param name="searchToken"></param>
        /// <param name="searchFlag"></param>
        /// <returns></returns>
        [HttpGet("GetAccountEnquiryAsync/{searchToken}/{searchFlag}")]
        public async Task<IActionResult> AccountEnquiry(string searchToken, SearchFlag searchFlag)
        {
            var response = await creditServices.AccountEnquiry(searchToken, searchFlag);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddGuarantorAsync")]
        public async Task<IActionResult> CreditGuarantorMaint([FromBody] CreateCreditGuarantorsDto request)
        {
            var response = await creditServices.CreditGuarantorMaint(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddGuarantorListAsync")]
        public async Task<IActionResult> CreditGuarantorMaint([FromBody] List<CreateCreditGuarantorsDto> request)
        {
            var response = await creditServices.CreditGuarantorMaint(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddCreditSecurityAsync")]
        public async Task<IActionResult> CreditSecurityMaint([FromBody] CreateCreditSecuritiesDto request)
        {
            var response = await creditServices.CreditSecurityMaint(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddCreditSecurityListAsync")]
        public async Task<IActionResult> CreditSecurityMaint([FromBody] List<CreateCreditSecuritiesDto> request)
        {
            var response = await creditServices.CreditSecurityMaint(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddUpfrontChargeAsync")]
        public async Task<IActionResult> CreditUpfrontChargesMaint([FromBody] CreateCreditChargeDto request)
        {
            var response = await creditServices.CreditUpfrontChargesMaint(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddUpfrontChargeListAsync")]
        public async Task<IActionResult> CreditUpfrontChargesMaint([FromBody] List<CreateCreditChargeDto> request)
        {
            var response = await creditServices.CreditUpfrontChargesMaint(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateGuarantorAsync")]
        public async Task<IActionResult> EditCreditGuarantor([FromBody] CreateCreditGuarantorsDto request)
        {
            var response = await creditServices.EditCreditGuarantor(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateGuarantorListAsync")]
        public async Task<IActionResult> EditCreditGuarantor([FromBody] List<CreateCreditGuarantorsDto> request)
        {
            var response = await creditServices.EditCreditGuarantor(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateCreditSecurityAsync")]
        public async Task<IActionResult> EditCreditSecurity([FromBody] CreateCreditSecuritiesDto request)
        {
            var response = await creditServices.EditCreditSecurity(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateCreditSecurityListAsync")]
        public async Task<IActionResult> EditCreditSecurity([FromBody] List<CreateCreditSecuritiesDto> request)
        {
            var response = await creditServices.EditCreditSecurity(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateUpfrontChargeAsync")]
        public async Task<IActionResult> EditCreditCharge([FromBody] CreateCreditChargeDto request)
        {
            var response = await creditServices.EditCreditCharge(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateUpfrontChargeListAsync")]
        public async Task<IActionResult> EditCreditCharge([FromBody] List<CreateCreditChargeDto> request)
        {
            var response = await creditServices.EditCreditCharge(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteUpfrontChargeAsync/{seqNo}")]
        public async Task<IActionResult> RemoveCreditCharge(long seqNo)
        {
            var response = await creditServices.RemoveCreditCharge(seqNo);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteUpfrontChargeAsync/{creditId}")]
        public async Task<IActionResult> RemoveCreditChargeByCreditId(string creditId)
        {
            var response = await creditServices.RemoveCreditChargeByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }


        [HttpDelete("DeleteCreditGuarantorAsync/{seqNo}")]
        public async Task<IActionResult> RemoveCreditGuarantor(long seqNo)
        {
            var response = await creditServices.RemoveCreditGuarantor(seqNo);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteCreditGuarantorAsync/{creditId}")]
        public async Task<IActionResult> RemoveCreditGuarantorByCreditId(string creditId)
        {
            var response = await creditServices.RemoveCreditGuarantorByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteCreditSecurityAsync/{seqNo}")]
        public async Task<IActionResult> RemoveCreditSecurity(long seqNo)
        {
            var response = await creditServices.RemoveCreditSecurity(seqNo);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteCreditSecurityAsync/{creditId}")]
        public async Task<IActionResult> RemoveCreditSecurityByCreditId(string creditId)
        {
            var response = await creditServices.RemoveCreditSecurityByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAllLoansAsync/{addedBy}")]
        public async Task<IActionResult> GetAllLoansByUserAsync(string addedBy)
        {
            var response = await creditServices.GetAllLoansByUserAsync(addedBy);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAllLoansWithDepenciesByUserAsync/{addedBy}")]
        public async Task<IActionResult> GetAllLoansWithDepenciesByUserAsync(string addedBy)
        {
            var response = await creditServices.GetAllLoansWithDepenciesByUserAsync(addedBy);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetLoanWithDepenciesByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetLoanWithDepenciesByCreditIdAsync(string creditId)
        {
            var response = await creditServices.GetLoanWithDepenciesByCreditIdAsync(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetLoanByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetLoansByCreditIdAsync(string creditId)
        {
            var response = await creditServices.GetLoansByCreditIdAsync(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetCreditGuarantorsByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetCreditGuarantorsByCreditId(string creditId)
        {
            var response = await creditServices.GetCreditGuarantorsByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetCreditChargesByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetCreditChargesByCreditId(string creditId)
        {
            var response = await creditServices.GetCreditChargesByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetCreditSecuritiesByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetCreditSecuritiesByCreditId(string creditId)
        {
            var response = await creditServices.GetCreditSecuritiesByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetCreditScheduleHeaderByCreditIdAsync/{creditId}")]
        public async Task<IActionResult> GetCreditScheduleHeaderByCreditId(string creditId)
        {
            var response = await creditServices.GetCreditScheduleHeaderByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteCreditSecurityAsync")]
        public async Task<IActionResult> RemoveCreditSecurity(List<long> seqNo)
        {
            var response = await creditServices.RemoveCreditSecurity(seqNo);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        [HttpDelete("DeleteCreditGuarantorAsync")]
        public async Task<IActionResult> RemoveCreditGuarantor(List<long> seqNo)
        {
            var response = await creditServices.RemoveCreditGuarantor(seqNo);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteUpfrontChargeAsync")]
        public async Task<IActionResult> RemoveCreditCharge(List<long> seqNo)
        {
            var response = await creditServices.RemoveCreditCharge(seqNo);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteCreditAsync/{creditId}")]
        public async Task<IActionResult> RemoveCreditMaintHistByCreditId(string creditId)
        {
            var response = await creditServices.RemoveCreditMaintHistByCreditId(creditId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }


        [HttpPut("UpdateCreditAsync")]
        public async Task<IActionResult> EditCreditMaintHist([FromBody] CreditUpdateRequestDto request)
        {
            var response = await creditServices.EditCreditMaintHist(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetGuarantorLookupAsync")]
        public async Task<IActionResult> GuarantorLookup()
        {
            var response = await creditServices.GuarantorLookup();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetSecurityLookupAsync")]
        public async Task<IActionResult> SecurityLookup()
        {
            var response = await creditServices.SecurityLookup();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetUpfrontFeeLookupAsync")]
        public async Task<IActionResult> UpfrontFeeLookup()
        {
            var response = await creditServices.UpfrontFeeLookup();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
