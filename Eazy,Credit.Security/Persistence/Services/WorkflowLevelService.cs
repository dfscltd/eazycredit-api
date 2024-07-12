
using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Eazy.Credit.Security.Persistence.Services
{
    public class WorkflowLevelService: IWorkflowLevelService
    {
        private readonly PersistenceContext db;
        private readonly IWorkflowsService workflowsService;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly UserManager<AppUsers> _userManager;

        public WorkflowLevelService(PersistenceContext db, IWorkflowsService workflowsService, RoleManager<AppRoles> roleManager, UserManager<AppUsers> userManager)
        {
            this.db = db;
            this.workflowsService = workflowsService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ViewAPIResponse<CreateWorkflowLevelDto>> CreateWorkflowLevel(CreateWorkflowLevelDto request)
        {
            ViewAPIResponse<CreateWorkflowLevelDto> response = null;

            var existingUser = await FindWorkflowsLevelById(request.WorkflowID, request.LevelID);

            if (existingUser.ResponseResult != null)
            {
                return response = new ViewAPIResponse<CreateWorkflowLevelDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            var user = new WorkflowLevel
            {
                LevelID = request.LevelID,
                WorkflowID = request.WorkflowID,
                WorkflowLevelTitle = request.WorkflowLevelTitle,
                AllowDocUpload = request.AllowDocUpload,
                LevelOrder = request.LevelOrder,
                FinalLevel = request.FinalLevel,
                AddedBy = request.AddedBy,
                DateAdded = DateTime.Now
            };

            await db.AddAsync<WorkflowLevel>(user);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<CreateWorkflowLevelDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreateWorkflowLevelDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }
        public async Task<ViewAPIResponse<ResulyWorkflowLevelDto>> FindWorkflowsLevelById(string workflowId, string levelId)
        {
            ViewAPIResponse<ResulyWorkflowLevelDto> response = null;

            var existingRecord = await db.WorkflowLevels.FirstOrDefaultAsync(x => x.WorkflowID == workflowId && x.LevelID == levelId);

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<ResulyWorkflowLevelDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            var workflow = await workflowsService.FindWorkflowsById(workflowId);

            var result = new ResulyWorkflowLevelDto
            {
                WorkflowID = existingRecord.WorkflowID,
                WorkflowTitle = workflow.ResponseResult.WorkflowTitle,
                LevelID = levelId,
                WorkflowLevelTitle = existingRecord.WorkflowLevelTitle,
                AllowDocUpload = existingRecord.AllowDocUpload,
                FinalLevel = existingRecord.FinalLevel,
                LevelOrder = existingRecord.LevelOrder,
                Previous = existingRecord.LevelOrder - 1,
                Next = existingRecord.LevelOrder + 1,
                AddedBy = existingRecord.AddedBy,
                DateAdded = existingRecord.DateAdded
            };

            return response = new ViewAPIResponse<ResulyWorkflowLevelDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }
        public async Task<ViewAPIResponse<CreateWorkflowLevelDto>> EditWorkflowLevel(CreateWorkflowLevelDto request)
        {
            ViewAPIResponse<CreateWorkflowLevelDto> response = null;

            var existingUser = await db.WorkflowLevels.FirstOrDefaultAsync(x => x.WorkflowID == request.WorkflowID && x.LevelID == request.LevelID);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<CreateWorkflowLevelDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            existingUser.AllowDocUpload = request.AllowDocUpload;
            existingUser.FinalLevel = request.FinalLevel;
            existingUser.LevelOrder = request.LevelOrder;
            existingUser.LastModifiedBy = request.AddedBy;
            existingUser.DateLastModified = DateTime.Now;

            db.Update<WorkflowLevel>(existingUser);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return response = new ViewAPIResponse<CreateWorkflowLevelDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreateWorkflowLevelDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }
        public async Task<ViewAPIResponse<string>> RemoveWorkflowLevel(string workflowId, string levelId)
        {
            ViewAPIResponse<string> response = null;

            var existingUser = await db.WorkflowLevels.FirstOrDefaultAsync(x => x.WorkflowID == workflowId && x.LevelID == levelId);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists"
                };
            }

           
            db.Remove<WorkflowLevel>(existingUser);
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
        public async Task<ViewAPIResponse<List<ResulyWorkflowLevelDto>>> FindWorkflowsLevelByWorkflow(string workflowId)
        {
            ViewAPIResponse<List<ResulyWorkflowLevelDto>> response = null;

            List<ResulyWorkflowLevelDto> resultList = new List<ResulyWorkflowLevelDto>();

            var workflow = await workflowsService.FindWorkflowsById(workflowId);


            if (workflow == null)
            {
                return response = new ViewAPIResponse<List<ResulyWorkflowLevelDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "WorkflowNotExists",
                };

            }

            var existingRecord = await db.WorkflowLevels.Where(x => x.WorkflowID == workflowId).ToListAsync();

            if (existingRecord.Count ==0)
            {
                return response = new ViewAPIResponse<List<ResulyWorkflowLevelDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            foreach (var record in existingRecord)
            {

                resultList.Add(new ResulyWorkflowLevelDto
                {
                    WorkflowID = record.WorkflowID,
                    WorkflowTitle = workflow.ResponseResult.WorkflowTitle,
                    LevelID = record.LevelID,
                    WorkflowLevelTitle = record.WorkflowLevelTitle,
                    AllowDocUpload = record.AllowDocUpload,
                    FinalLevel = record.FinalLevel,
                    LevelOrder = record.LevelOrder,
                    //Previous = record.LevelOrder - 1,
                    //Next = record.LevelOrder + 1,
                    Previous = record.LevelOrder == 1 ? -1 : record.LevelOrder - 1,
                    Next = record.FinalLevel ? 0 : record.LevelOrder + 1,
                    AddedBy = record.AddedBy,
                    DateAdded = record.DateAdded
                });
            }
            

            return response = new ViewAPIResponse<List<ResulyWorkflowLevelDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = resultList.OrderBy(x => x.LevelOrder).ToList()
            };

        }
        public async Task<ViewAPIResponse<string>> AssignRolesToWorkflowLevel(AssignRoleToWorkflowLevelDto request)
        {
            ViewAPIResponse<string> response = null;

            var existWorkflow = await workflowsService.FindWorkflowsById(request.WorkflowID);

            if (existWorkflow == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "workflownotExists"
                };
            }

            var existRole = await _roleManager.FindByIdAsync(request.RoleID);

            if (existRole == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "rolenotExists"
                };
            }


            var existworkflowlevel = await FindWorkflowsLevelById(request.WorkflowID, request.LevelID);

            if (existworkflowlevel == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "workflowlevelNotExists"
                };
            }

            var user = new AssignRoleToWorkflowLevel
            {
                WorkflowID = request.WorkflowID,
                LevelID = request.LevelID,
                RoleID = request.RoleID,
            };

            await db.AddAsync<AssignRoleToWorkflowLevel>(user);
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
        public async Task<ViewAPIResponse<string>> RemoveRolesFromWorkflowLevel(AssignRoleToWorkflowLevelDto request)
        {
            ViewAPIResponse<string> response = null;

            var existWorkflow = await db.AssignRoleToWorkflowLevels.FirstOrDefaultAsync(x => x.WorkflowID == request.WorkflowID && x.LevelID == request.LevelID && x.RoleID == request.RoleID);

            if (existWorkflow == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "workflownotExists"
                };
            }            

            db.Remove<AssignRoleToWorkflowLevel>(existWorkflow);
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

        public async Task<ViewAPIResponse<List<ResultRoleToWorkflowLevelDto>>> FindAssignedRoleToWorkflowsLevel(string roleId)
        {
            ViewAPIResponse<List<ResultRoleToWorkflowLevelDto>> response = null;

            List<ResultRoleToWorkflowLevelDto> resultList = new List<ResultRoleToWorkflowLevelDto>();


            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return response = new ViewAPIResponse<List<ResultRoleToWorkflowLevelDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "roleNotExists",
                };

            }

            var workflow = await db.AssignRoleToWorkflowLevels.Where(x => x.RoleID == roleId).ToListAsync();


            if (workflow.Count == 0)
            {
                return response = new ViewAPIResponse<List<ResultRoleToWorkflowLevelDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordNotExists",
                };

            }

            foreach (var record in workflow)
            {
                var workflowRecord = await workflowsService.FindWorkflowsById(record.WorkflowID);

                var workflowlevel = await FindWorkflowsLevelById(record.WorkflowID, record.LevelID);

                resultList.Add(new ResultRoleToWorkflowLevelDto
                {
                    WorkflowID = record.WorkflowID,
                    WorkflowTitle = workflowRecord.ResponseResult.WorkflowTitle,
                    LevelID = record.LevelID,
                    WorkflowLevelTitle = workflowlevel.ResponseResult.WorkflowLevelTitle,
                    LevelOrder = workflowlevel.ResponseResult.LevelOrder,
                    Previous = workflowlevel.ResponseResult.LevelOrder == 1 ? - 1 : workflowlevel.ResponseResult.LevelOrder - 1,
                    Next = workflowlevel.ResponseResult.FinalLevel ? 0 : workflowlevel.ResponseResult.LevelOrder + 1,
                    RoleID = role.Id,
                    RoleName = role.Name,
                    AddedBy = record.AddedBy,
                    DateAdded = record.DateAdded
                });
            }


            return response = new ViewAPIResponse<List<ResultRoleToWorkflowLevelDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = resultList.OrderBy(x => x.LevelOrder).ToList()
            };

        }

        public async Task<ViewAPIResponse<List<ApprovingUsersDto>>> FindApprovingUsersInWorkflowsLevelById(ApprovingUsersRequestDto request)
        {
            ViewAPIResponse<List<ApprovingUsersDto>> response = null;
            List<ApprovingUsersDto> approvingList = new List<ApprovingUsersDto>();

            var existingRecord = await db.AssignRoleToWorkflowLevels.Where(x => x.WorkflowID == request.WorkflowId && x.LevelID == request.LevelId).ToListAsync();

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<List<ApprovingUsersDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            var creditRecord = await db.CreditMaintHists.FirstOrDefaultAsync(x => x.CreditId == request.CreditId);

            foreach (var record in existingRecord)
            {


                var role = await _roleManager.FindByIdAsync(record.RoleID);
                var userList = await _userManager.GetUsersInRoleAsync(role.Name);

                if (userList.Count > 0) {
                    foreach (var user in userList)
                    {
                        var userRecord = await _userManager.FindByIdAsync(user.Id);
                        approvingList.Add(new ApprovingUsersDto
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            FullName = $"{user.FirstName}{" "}{user.LastName}",
                            RoleId = role.Id,
                            RoleName = role.Name,
                            Photo = Convert.ToBase64String(user.UserPhoto)
                        });
                    }
                    
                }
            }

            return response = new ViewAPIResponse<List<ApprovingUsersDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = approvingList
            };

        }

        public async Task<ViewAPIResponse<List<RolesInWorkflowsLevelResultDto>>> FindRolesInWorkflowsLevelById(string workflowId, string levelId)
        {
            ViewAPIResponse<List<RolesInWorkflowsLevelResultDto>> response = null;
            List<RolesInWorkflowsLevelResultDto> levelResultDtos = new List<RolesInWorkflowsLevelResultDto>();

            var existingRecord = await db.AssignRoleToWorkflowLevels.Where(x => x.WorkflowID == workflowId && x.LevelID == levelId).ToListAsync();

            if (existingRecord.Count == 0)
            {
                return response = new ViewAPIResponse<List<RolesInWorkflowsLevelResultDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            foreach (var record in existingRecord)
            {
                var workflow = await workflowsService.FindWorkflowsById(record.WorkflowID);
                var role = await _roleManager.FindByIdAsync(record.RoleID);
                var workflowLevel = await db.WorkflowLevels.FirstOrDefaultAsync(x => x.WorkflowID == record.WorkflowID && x.LevelID == record.LevelID);

                levelResultDtos.Add(new RolesInWorkflowsLevelResultDto
                {
                    WorkflowId = record.WorkflowID,
                    WorkflowName = workflow.ResponseResult.WorkflowTitle,
                    RoleId = record.RoleID,
                    RoleName = role.Name,
                    WorkflowLevelId = record.LevelID,
                    WorkflowsLevelDesc = workflowLevel.WorkflowLevelTitle
                });
            }


            return response = new ViewAPIResponse<List<RolesInWorkflowsLevelResultDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = levelResultDtos
            };

        }

    }

}
