using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Eazy.Credit.Security.Persistence.Services
{
    public class PrmRoleLimitService: IPrmRoleLimitService
    {
        private readonly PersistenceContext db;
        private readonly RoleManager<AppRoles> _roleManager;

        public PrmRoleLimitService(PersistenceContext db, RoleManager<AppRoles> roleManager)
        {
            this.db = db;
            _roleManager = roleManager;
        }

        public async Task<ViewAPIResponse<CreatePrmRoleLimitDto>> CreatePmrRuleNumber(CreatePrmRoleLimitDto request)
        {
            ViewAPIResponse<CreatePrmRoleLimitDto> response = null;

            var existingUser = await FindPmrRuleNumberByParamId(request.LimitId);

            if (existingUser.ResponseResult != null)
            {
                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordExists",
                    ResponseResult = request
                };
            }

            var user = new PrmRoleLimit
            {
                LimitId = request.LimitId,
                LimitDesc = request.LimitDesc,
                LowerLimit = request.LowerLimit,
                UpperLimit = request.UpperLimit,    
                CummulativeLimit = request.CummulativeLimit
            };

            await db.AddAsync<PrmRoleLimit>(user);
            var result = await db.SaveChangesAsync();
            if(result > 0)
            {
                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "error",
                    ResponseResult = request
                };
            }
        }
        public async Task<ViewAPIResponse<CreatePrmRoleLimitDto>> FindPmrRuleNumberByParamId(string request)
        {
            ViewAPIResponse<CreatePrmRoleLimitDto> response = null;

            var existingRecord = await db.PrmRoleLimts.FirstOrDefaultAsync(x => x.LimitId == request);

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            var result = new CreatePrmRoleLimitDto
            {
                LimitId = existingRecord.LimitId,
                LimitDesc = existingRecord.LimitDesc,
                LowerLimit = existingRecord.LowerLimit,
                UpperLimit = existingRecord.UpperLimit, 
                CummulativeLimit = existingRecord.CummulativeLimit
            };

            return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }
        public async Task<ViewAPIResponse<CreatePrmRoleLimitDto>> EditPmrRuleNumber(CreatePrmRoleLimitDto request)
        {
            ViewAPIResponse<CreatePrmRoleLimitDto> response = null;

            var existingUser = await db.PrmRoleLimts.FirstOrDefaultAsync(x => x.LimitId.Equals(request.LimitId));

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "Norecord",
                    ResponseResult = request
                };
            }

            existingUser.LimitDesc = request.LimitDesc;
            existingUser.LowerLimit = request.LowerLimit;
            existingUser.UpperLimit = request.UpperLimit;
            existingUser.CummulativeLimit = request.CummulativeLimit;

             db.Update<PrmRoleLimit>(existingUser);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                var data = await FindPmrRuleNumberByParamId(request.LimitId);

                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = data.ResponseResult
                };
            }
            else
            {
                return response = new ViewAPIResponse<CreatePrmRoleLimitDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "errorUpdating",
                    ResponseResult = request
                };
            }
        }
        public async Task<ViewAPIResponse<string>> DeletePmrRuleNumber(string limitId)
        {
            ViewAPIResponse<string> response = null;

            var existingUser = await db.PrmRoleLimts.FirstOrDefaultAsync(x => x.LimitId.Equals(limitId));

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordNotExists"
                };
            }

           

            db.Remove<PrmRoleLimit>(existingUser);
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
                    ResponseMessage = "errorDeleting"
                };
            }
        }
        public async Task<ViewAPIResponse<string>> AssignRolesToRuleNumber(AssignRoleToRuleNumberDto request)
        {
            ViewAPIResponse<string> response = null;

            var existingUser = await FindPmrRuleNumberByParamId(request.LimitId);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "numberrulesnotExists"
                };
            }

            var existingRole = await _roleManager.FindByIdAsync(request.UserRoleID);

            if (existingRole == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "rolenotExists"
                };
            }

            var user = new AssignRoleToLimit
            {
                LimitId = request.LimitId,
                UserRoleID = request.UserRoleID
            };

            await db.AddAsync<AssignRoleToLimit>(user);
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
        public async Task<ViewAPIResponse<string>> RemoveRolesFromRuleNumber(AssignRoleToRuleNumberDto request)
        {
            ViewAPIResponse<string> response = null;

            var existingUser = IsUserRoleToRuleNumberExist(request.LimitId, request.UserRoleID);

            if (!existingUser)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "recordnotexist"
                };
            }


            var user = new AssignRoleToLimit
            {
                LimitId = request.LimitId,
                UserRoleID = request.UserRoleID
            };

            db.Remove<AssignRoleToLimit>(user);
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
        public async Task<ViewAPIResponse<List<CreatePrmRoleLimitDto>>> FindAllPmrRuleNumbers()
        {
            ViewAPIResponse<List<CreatePrmRoleLimitDto>> response = null;
            List<CreatePrmRoleLimitDto> numberRulesList = new List<CreatePrmRoleLimitDto>();

            var existingRecord = await db.PrmRoleLimts.ToListAsync();

            if (existingRecord == null)
            {
                return response = new ViewAPIResponse<List<CreatePrmRoleLimitDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RecordNotExists",
                };

            }

            foreach (var numberRule in existingRecord)
            {
                numberRulesList.Add(new CreatePrmRoleLimitDto
                {
                    LimitId = numberRule.LimitId,
                    LimitDesc = numberRule.LimitDesc,
                    LowerLimit = numberRule.LowerLimit,
                    UpperLimit = numberRule.UpperLimit,
                    CummulativeLimit = numberRule.CummulativeLimit
                });
            }

            return response = new ViewAPIResponse<List<CreatePrmRoleLimitDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = numberRulesList
            };

        }

        public async Task<ViewAPIResponse<List<RolesPrmLimitDto>>> FindNumberRulesByRoleId(string UserRoleID)
        {
            ViewAPIResponse<List<RolesPrmLimitDto>> response = null;
            List<RolesPrmLimitDto> resultList = new List<RolesPrmLimitDto>();

            var existingRole = await _roleManager.FindByIdAsync(UserRoleID);

            if (existingRole == null)
            {
                return response = new ViewAPIResponse<List<RolesPrmLimitDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "rolenotExists"
                };
            }


            var result = db.AssignRoleToLimits.Where(x => x.UserRoleID == UserRoleID).ToList();

            foreach(var record in result) {

                var data = await FindPmrRuleNumberByParamId(record.LimitId);

                resultList.Add(new RolesPrmLimitDto
                {
                    RoleID = existingRole.Id,
                    RoleName = existingRole.Name,
                    LimitId = data.ResponseResult.LimitId,
                    LimitDesc = data.ResponseResult.LimitDesc,
                    LowerLimit = data.ResponseResult.LowerLimit,
                    UpperLimit = data.ResponseResult.UpperLimit,
                    CummulativeLimit = data.ResponseResult.CummulativeLimit
                });
            }

            

            return response = new ViewAPIResponse<List<RolesPrmLimitDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = resultList
            };

        }

        public bool IsUserRoleToRuleNumberExist(string parameter, string roleId)
        {

            var existingUser =  db.AssignRoleToLimits.Any(x => x.LimitId == parameter && x.UserRoleID == roleId);
            return existingUser;
        }

    }
}
