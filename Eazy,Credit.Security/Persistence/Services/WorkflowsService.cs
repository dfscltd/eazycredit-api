using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace Eazy.Credit.Security.Persistence.Services
{
    public class WorkflowsService: IWorkflowsService
    {
        private readonly PersistenceContext db;

        public WorkflowsService(PersistenceContext db)
        { 
            this.db = db; 
        }

        public async Task<ViewAPIResponse<CreateWorkflowsDto>> CreateWorkflow(CreateWorkflowsDto request)
        {
            ViewAPIResponse<CreateWorkflowsDto> response = null;

            var existingUser = await FindWorkflowsById(request.WorkflowID);

            if (existingUser.ResponseResult != null)
            {
                return response = new ViewAPIResponse<CreateWorkflowsDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            var user = new Workflows
            {
                WorkflowID = request.WorkflowID,
                WorkflowTitle = request.WorkflowTitle,
                WorkflowNotes = request.WorkflowNotes,
                AddedBy = request.AddedBy,
                DateAdded = DateTime.Now
            };

            await db.AddAsync<Workflows>(user);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<CreateWorkflowsDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreateWorkflowsDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }
        public async Task<ViewAPIResponse<ResultWorkflowsDto>> FindWorkflowsById(string workflowId)
        {
            ViewAPIResponse<ResultWorkflowsDto> response = null;

            var existingRecord = await db.Workflows.FirstOrDefaultAsync(x => x.WorkflowID == workflowId);

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<ResultWorkflowsDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            var result = new ResultWorkflowsDto
            {
                WorkflowID = existingRecord.WorkflowID,
                WorkflowNotes = existingRecord.WorkflowNotes,
                WorkflowTitle = existingRecord.WorkflowTitle,
                AddedBy = existingRecord.AddedBy,
                DateAdded = existingRecord.DateAdded
            };

            return response = new ViewAPIResponse<ResultWorkflowsDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }
        public async Task<ViewAPIResponse<CreateWorkflowsDto>> EditWorkflow(CreateWorkflowsDto request)
        {
            ViewAPIResponse<CreateWorkflowsDto> response = null;

            var existingUser = await db.Workflows.FirstOrDefaultAsync(x => x.WorkflowID == request.WorkflowID);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<CreateWorkflowsDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            existingUser.WorkflowTitle = request.WorkflowTitle;
            existingUser.WorkflowNotes = request.WorkflowNotes;
            existingUser.LastModifiedBy = request.AddedBy;
            existingUser.DateLastModified = DateTime.Now;

             db.Update<Workflows>(existingUser);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<CreateWorkflowsDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreateWorkflowsDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }
        public async Task<ViewAPIResponse<string>> DeleteWorkflow(string workflowId)
        {
            ViewAPIResponse<string> response = null;

            var existingUser = await db.Workflows.FirstOrDefaultAsync(x => x.WorkflowID == workflowId);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists"
                };
            }

            db.Remove<Workflows>(existingUser);
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
        public async Task<ViewAPIResponse<List<ResultWorkflowsDto>>> FindAllWorkflows()
        {
            ViewAPIResponse<List<ResultWorkflowsDto>> response = null;
            List<ResultWorkflowsDto> resultList = new List<ResultWorkflowsDto>();

            var existingRecord = await db.Workflows.ToListAsync();

            if (existingRecord.Count == 0)
            {
                return response = new ViewAPIResponse<List<ResultWorkflowsDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }
            foreach(var record in existingRecord) {
                resultList.Add( new ResultWorkflowsDto
                {
                    WorkflowID = record.WorkflowID,
                    WorkflowNotes = record.WorkflowNotes,
                    WorkflowTitle = record.WorkflowTitle,
                    AddedBy = record.AddedBy,
                    DateAdded = record.DateAdded
                });
            }
            

            return response = new ViewAPIResponse<List<ResultWorkflowsDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = resultList
            };

        }

    }
}
