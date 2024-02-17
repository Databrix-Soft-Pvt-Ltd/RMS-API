using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;

namespace Management.Services.Masters
{
    public class ProjectMasterDomain : IProjectMasterDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        public ProjectMasterDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
        }


        public async Task<IEnumerable<GetAllProjectREsponseModel>> GetAll()
        {
            var query = _unitOfWork.ProjectMasterRepository
                          .AsQueryable()
                             .Select(c => new GetAllProjectREsponseModel
                             {
                                 Id = c.Id,
                                 ProjectName = c.ProjectName,
                                 LoginPageLogo = c.LoginPageLogo,
                                 HeaderPageLogo = c.LoginPageLogo

                             }).ToList();

            return query;
        }



        public async Task<GetAllProjectREsponseModel> GetProjectById(GetProjectByIdRequestModel request)
        {
            var user = _unitOfWork.ProjectMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllProjectREsponseModel
                {
                    Id = c.Id,
                    ProjectName = c.ProjectName,
                    LoginPageLogo = c.LoginPageLogo,
                    HeaderPageLogo = c.LoginPageLogo

                })
                .FirstOrDefault();

            return user;
        }


        public async Task<HashSet<string>> AddValidation(AddProjectReuestModel request)
        {
            bool isBranchExists = await _unitOfWork.ProjectMasterRepository.Any(x => x.ProjectName.ToLower().Trim() == request.ProjectName.ToLower().Trim());
            if (isBranchExists)
            {
                validationMessage.Add("Branch Already Exists");
            }
            return validationMessage;
        }
        public async Task<ProjectMaster> Add(AddProjectReuestModel request)
        {
            var project = new ProjectMaster();
            project.ProjectName = request.ProjectName;
            project.LoginPageLogo = request.LoginPageLogo;
            project.HeaderPageLogo = request.HeaderPageLogo;
            var response = await _unitOfWork.ProjectMasterRepository.Add(project);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateProjectRequestModel request)
        {
            bool isProjectExists = await _unitOfWork.ProjectMasterRepository.Any(x => x.Id != request.Id && x.ProjectName.ToLower().Trim() == request.ProjectName.ToLower().Trim());
            if (isProjectExists)
            {
                validationMessage.Add("Project doesnot exits Exists");
            }
            return validationMessage;
        }



        public async Task<ProjectMaster> Update(UpdateProjectRequestModel request)
        {
            var projectMaster = await _unitOfWork.ProjectMasterRepository.GetById(request.Id);
            if (projectMaster == null)
            {
                throw new Exception("Project not found");
            }
            projectMaster.ProjectName = request.ProjectName;
            projectMaster.LoginPageLogo = request.LoginPageLogo;
            projectMaster.HeaderPageLogo = request.HeaderPageLogo;
            //  projectMaster.UpdatedAt = DateTime.Now;
            var response = await _unitOfWork.ProjectMasterRepository.Update(projectMaster);
            await _unitOfWork.Commit();

            return response;
        }


        public async Task<HashSet<string>> DeleteValidation(GetProjectByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.ProjectMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetProjectByIdRequestModel id)
        {
            var projectToDelete = await _unitOfWork.ProjectMasterRepository.GetById(id.Id);

            if (projectToDelete != null)
            {
                _unitOfWork.ProjectMasterRepository.Delete(projectToDelete);
                await _unitOfWork.Commit();
            }
        }


    }
    public interface IProjectMasterDomain
    {
        Task<IEnumerable<GetAllProjectREsponseModel>> GetAll();
        Task<GetAllProjectREsponseModel> GetProjectById(GetProjectByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddProjectReuestModel request);
        Task<ProjectMaster> Add(AddProjectReuestModel request);
        Task<ProjectMaster> Update(UpdateProjectRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateProjectRequestModel request);
        Task<HashSet<string>> DeleteValidation(GetProjectByIdRequestModel request);
        Task Delete(GetProjectByIdRequestModel id);
    }


}
