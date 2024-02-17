using Management.Model.RMSEntity;
using Management.Model.ResponseModel;
using System.Reflection.Metadata;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.DBModels;
using Management.Model.RequestModel;
using Microsoft.EntityFrameworkCore;
using static Management.Services.User.UserDomain;

namespace Management.Services.User
{
    public class UserDomain : IUserDomain
    {
        readonly IUnitOfWork _unitOfWork;


        private HashSet<string> validationMessage = new HashSet<string>();
        public UserDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
        }

        public async Task<IEnumerable<GetAllUserResponse>> GetAll()
        {

            List<GetAllUserResponse> users = _unitOfWork.UserRepository.AsQueryable().Select(u => new GetAllUserResponse
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                UserName = u.UserName,
                EmailId = u.EmailId,
                MobileNo = u.MobileNo,

            }).ToList();
            return users;


        }

        public async Task<GetAllUserResponse> GetUserById(GetUserByIdRequest request)
        {
            var user = _unitOfWork.UserRepository.AsQueryable()
                .Where(r => r.UserId == request.UserId)
                .Select(u => new GetAllUserResponse
                {
                    UserId = u.UserId,
                    RoleId = u.RoleId,
                    UserName = u.UserName,
                    EmailId = u.EmailId,
                    MobileNo = u.MobileNo,
                })
                .FirstOrDefault();

            return user;
        }





        /*  public async Task<UserMaster> Get(int id)
          {
              return await _unitOfWork.UserRepository.FirstOrDefault(x => x.UserId == id);
          }
        */
        public async Task<HashSet<string>> AddValidation(AddUserRequestModel request, string Chk)
        {
            validationMessage = new HashSet<string>();

            if (Chk == "User")
            {
                bool isUserExists = await _unitOfWork.UserRepository.Any(x =>x.EmailId.ToLower() == request.EmailId.ToLower());
                if(!isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            else
            {
                bool CheckRoleName = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (CheckRoleName)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            } 
            return validationMessage;
        }
        public async Task<UserMaster> Add(AddUserRequestModel request)
        {
            var usermaster = new UserMaster();

            usermaster.UserName = request.UserName;
            usermaster.RoleId = request.RoleId;
            usermaster.Password = request.Password;
            usermaster.EmailId = request.EmailId;
            usermaster.MobileNo = request.MobileNo;
            usermaster.UserType = request.UserType;
            usermaster.IsActive = request.IsActive;
            usermaster.PasswordExpiryDate = request.PasswordExpiryDate;
            //usermaster.LastLoginDate = request.LastLoginDate;
            usermaster.Ipaddress = request.Ipaddress;
            usermaster.CreatedDate = DateTime.Now;
            //usermaster.CreatedBy = CreatedBy  

            var response = await _unitOfWork.UserRepository.Add(usermaster);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateUserRequestModel request, string Chk)
        {

            validationMessage = new HashSet<string>();

            if (Chk == "User")
            {
                bool isUserExists = await _unitOfWork.UserRepository.Any(x => x.UserId == request.UserId);
                if (isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Chk == "email")
            {
                bool isUserExists = await _unitOfWork.UserRepository.Any(x=>x.EmailId.ToLower() == request.EmailId.ToLower());
                if (!isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Chk == "Role")
            {
                bool CheckRoleName = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (CheckRoleName)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            return validationMessage; 
             
        } 
        public async Task<UserMaster> Update(UpdateUserRequestModel request)
        {

            var usermaster = new UserMaster();

            usermaster.UserId = request.UserId;
            usermaster.UserName = request.UserName;
            usermaster.RoleId = request.RoleId;
            usermaster.Password = request.Password;
            usermaster.EmailId = request.EmailId;
            usermaster.MobileNo = request.MobileNo;
            usermaster.UserType = request.UserType;
            usermaster.IsActive = request.IsActive;
            usermaster.PasswordExpiryDate = request.PasswordExpiryDate;
            //usermaster.LastLoginDate = request.LastLoginDate;
            usermaster.Ipaddress = request.Ipaddress;
            //usermaster.IsDeleted = request.IsDeleted;
            usermaster.ModifiedDate = DateTime.Now;
            //usermaster.CreatedBy = CreatedBy  

            var response = await _unitOfWork.UserRepository.Update(usermaster);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<HashSet<string>> DeleteValidation(GetUserByIdRequest request)
        {
            bool IsRecord = await _unitOfWork.UserRepository.Any(x => x.UserId == request.UserId);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetUserByIdRequest id)
        {
            var UsertToDelete = await _unitOfWork.UserRepository.GetById(id.UserId);

            if (UsertToDelete != null)
            {
                _unitOfWork.UserRepository.Delete(UsertToDelete);
                await _unitOfWork.Commit();
            }
        }
        public interface IUserDomain
        {
            Task<IEnumerable<GetAllUserResponse>> GetAll();
            Task<GetAllUserResponse> GetUserById(GetUserByIdRequest request);
            Task<UserMaster> Add(AddUserRequestModel request);
            Task<UserMaster> Update(UpdateUserRequestModel request);
            Task<HashSet<string>> AddValidation(AddUserRequestModel request, string Chk);
            Task<HashSet<string>> UpdateValidation(UpdateUserRequestModel request, string Chk);
            Task<HashSet<string>> DeleteValidation(GetUserByIdRequest request);
            Task Delete(GetUserByIdRequest id);
            // Task Delete(int id);
        }

    }
}
