using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Persistence.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Persistence.Services
{
    public class CreateTransHeaderStageService: ICreateTransHeaderStageService
    {
        private readonly PersistenceContext db;

        public CreateTransHeaderStageService(PersistenceContext db)
        {  
            this.db = db; 
        }

        public async Task<ViewAPIResponse<CreateTransHeaderStageDto>> CreateTransHeaderStage(CreateTransHeaderStageDto request)
        {
            ViewAPIResponse<CreateTransHeaderStageDto> response = null;

            var existingUser = await FindTransHeaderStageById(request.CreditID, request.Workflow, request.Workflowlevel);

            if (existingUser.ResponseResult != null)
            {
                return response = new ViewAPIResponse<CreateTransHeaderStageDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            var user = new TransHeaderStage
            {
                CreditID = request.CreditID,
                Workflow = request.Workflow,
                Workflowlevel = request.Workflowlevel,
                TransComment = request.TransComment,
                TransCode = request.TransStatus,
                UserId = request.UserId,
                TransDesc = request.TransDesc,
                AddedDate = DateTime.Now,
                ActionCode = request.ActionCode
            };

            await db.AddAsync<TransHeaderStage>(user);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<CreateTransHeaderStageDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreateTransHeaderStageDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }

        public async Task<ViewAPIResponse<TransHeaderStageResultDto>> FindTransHeaderStageById(string creditId, string workflow, string workflowLevel)
        {
            ViewAPIResponse<TransHeaderStageResultDto> response = null;

            var existingRecord = await db.TransHeaderStages.FirstOrDefaultAsync(x => x.CreditID == creditId && x.Workflow == workflow && x.Workflowlevel == workflowLevel);

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<TransHeaderStageResultDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }
             
            var result = new TransHeaderStageResultDto
            {
                CreditID = existingRecord.CreditID,
                Workflow = existingRecord.Workflow,
                Workflowlevel = existingRecord.Workflowlevel,                
                TransDesc = existingRecord.TransDesc,
                TransCode = existingRecord.TransCode,
                TransStatus = GetTransStatus(existingRecord.TransCode),                 
                TransComment = existingRecord.TransComment,
                ActionCode = existingRecord.ActionCode,
                ActionStatus = GetActionStatus(existingRecord.ActionCode),
                UserId = existingRecord.UserId             
            };

            return response = new ViewAPIResponse<TransHeaderStageResultDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }

        public async Task<ViewAPIResponse<CreateTransHeaderStageDto>> EditTransHeaderStage(CreateTransHeaderStageDto request)
        {
            ViewAPIResponse<CreateTransHeaderStageDto> response = null;

            var existingUser = await db.TransHeaderStages.FirstOrDefaultAsync(x => x.CreditID == request.CreditID && x.Workflow == request.Workflow && x.Workflowlevel == request.Workflowlevel);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<CreateTransHeaderStageDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            existingUser.TransCode = request.TransStatus;
            existingUser.TransComment = request.TransComment;
            existingUser.TransDesc = request.TransDesc;
            existingUser.ActionCode = request.ActionCode;

            db.Update<TransHeaderStage>(existingUser);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<CreateTransHeaderStageDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreateTransHeaderStageDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }

        public async Task<ViewAPIResponse<string>> DeleteTransHeaderStage(string creditId, string workflow, string workflowLevel)
        {
            ViewAPIResponse<string> response = null;

            var existingRecord = await db.TransHeaderStages.FirstOrDefaultAsync(x => x.CreditID == creditId && x.Workflow == workflow && x.Workflowlevel == workflowLevel);

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            db.Remove<TransHeaderStage>(existingRecord);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success"
                };
            }
            else
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error"
                };
            }

        }

        public async Task<ViewAPIResponse<List<TransHeaderStageResultDto>>> FindTransHeaderStageByUserId(string userId)
        {
            ViewAPIResponse<List<TransHeaderStageResultDto>> response = null;

            List<TransHeaderStageResultDto> transList = new List<TransHeaderStageResultDto>();

            try
            {
                var existingRecord = await db.TransHeaderStages.Where(x => x.UserId == userId).ToListAsync();

                if (existingRecord.Count == 0)
                {
                    return response = new ViewAPIResponse<List<TransHeaderStageResultDto>>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "RecordNotExists",
                    };

                }

                foreach (var record in existingRecord)
                {
                    var TransStatus = GetTransStatus(record.TransCode);
                    var ActionStatus = GetActionStatus(record.ActionCode);

                    transList.Add(new TransHeaderStageResultDto
                    {
                        CreditID = record.CreditID,
                        Workflow = record.Workflow,
                        Workflowlevel = record.Workflowlevel,
                        TransDesc = record.TransDesc,
                        TransCode = record.TransCode,
                        TransStatus = TransStatus,
                        TransComment = record.TransComment,
                        ActionCode = record.ActionCode,
                        ActionStatus = ActionStatus,
                        UserId = record.UserId
                    });
                }



                return response = new ViewAPIResponse<List<TransHeaderStageResultDto>>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = transList
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public  string GetTransStatus(string statusCode)
        {
            try
            {
                if (string.IsNullOrEmpty(statusCode))
                {
                    return string.Empty;
                }

                var respons = db.TransHeaderStatus.FirstOrDefault(x => x.StatusCode == statusCode);

                if (respons == null) { return string.Empty; }

                return respons.StatusMessage;
            }
            catch(Exception ex) { throw ex; }
        }


        public string GetActionStatus(string actionCode)
        {
            try
            {
                if (string.IsNullOrEmpty(actionCode))
                {
                    return string.Empty;
                }

                var respons = db.ActionTakenStatus.FirstOrDefault(x => x.ActionCode == actionCode);

                if (respons == null) { return string.Empty; }

                return respons.ActionStatus;

            }
            catch(Exception ex) {throw ex; }
        }
    }
}
