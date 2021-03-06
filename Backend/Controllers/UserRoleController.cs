﻿using System.Threading.Tasks;
using BackendFramework.Interfaces;
using BackendFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BackendFramework.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("v1/projects/{projectId}/userroles")]
    [EnableCors("AllowAll")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IProjectService _projectService;
        private readonly IPermissionService _permissionService;

        public UserRoleController(IUserRoleService userRoleService, IProjectService projectService,
            IPermissionService permissionService)
        {
            _userRoleService = userRoleService;
            _projectService = projectService;
            _permissionService = permissionService;
        }

        /// <summary> Returns all <see cref="UserRole"/>s for specified <see cref="Project"/></summary>
        /// <remarks> GET: v1/projects/{projectId}/userroles </remarks>
        [HttpGet]
        public async Task<IActionResult> Get(string projectId)
        {
            if (!_permissionService.HasProjectPermission(Permission.WordEntry, HttpContext))
            {
                return new ForbidResult();
            }

            // Ensure project exists
            var proj = _projectService.GetProject(projectId);
            if (proj == null)
            {
                return new NotFoundObjectResult(projectId);
            }

            return new ObjectResult(await _userRoleService.GetAllUserRoles(projectId));
        }

        /// <summary> Deletes all <see cref="UserRole"/>s for specified <see cref="Project"/></summary>
        /// <remarks> DELETE: v1/projects/{projectId}/userroles </remarks>
        /// <returns> true: if success, false: if there were no UserRoles </returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string projectId)
        {
            if (!_permissionService.HasProjectPermission(Permission.DatabaseAdmin, HttpContext))
            {
                return new ForbidResult();
            }

            // Ensure project exists
            var proj = _projectService.GetProject(projectId);
            if (proj == null)
            {
                return new NotFoundObjectResult(projectId);
            }

            return new ObjectResult(await _userRoleService.DeleteAllUserRoles(projectId));
        }

        /// <summary> Returns <see cref="UserRole"/> with specified id </summary>
        /// <remarks> GET: v1/projects/{projectId}/userroles/{userRoleId} </remarks>
        [HttpGet("{userRoleId}")]
        public async Task<IActionResult> Get(string projectId, string userRoleId)
        {
            if (!_permissionService.HasProjectPermission(Permission.WordEntry, HttpContext))
            {
                return new ForbidResult();
            }

            // Ensure project exists
            var proj = _projectService.GetProject(projectId);
            if (proj == null)
            {
                return new NotFoundObjectResult(projectId);
            }

            var userRole = await _userRoleService.GetUserRole(projectId, userRoleId);
            if (userRole == null)
            {
                return new NotFoundObjectResult(userRoleId);
            }

            return new ObjectResult(userRole);
        }

        /// <summary> Creates a <see cref="UserRole"/> </summary>
        /// <remarks> POST: v1/projects/{projectId}/userroles </remarks>
        /// <returns> Id of updated UserRole </returns>
        [HttpPost]
        public async Task<IActionResult> Post(string projectId, [FromBody]UserRole userRole)
        {
            if (!_permissionService.HasProjectPermission(Permission.DeleteEditSettingsAndUsers, HttpContext))
            {
                return new ForbidResult();
            }

            userRole.ProjectId = projectId;

            // Ensure project exists
            var proj = _projectService.GetProject(projectId);
            if (proj == null)
            {
                return new NotFoundObjectResult(projectId);
            }

            var returnUserRole = await _userRoleService.Create(userRole);
            if (returnUserRole == null)
            {
                return BadRequest();
            }

            return new OkObjectResult(userRole.Id);
        }

        /// <summary> Deletes <see cref="UserRole"/> with specified id </summary>
        /// <remarks> DELETE: v1/projects/{projectId}/userroles/{userRoleId} </remarks>
        [HttpDelete("{userRoleId}")]
        public async Task<IActionResult> Delete(string projectId, string userRoleId)
        {
            if (!_permissionService.HasProjectPermission(Permission.DatabaseAdmin, HttpContext))
            {
                return new ForbidResult();
            }

            // Ensure project exists
            var proj = _projectService.GetProject(projectId);
            if (proj == null)
            {
                return new NotFoundObjectResult(projectId);
            }

            if (await _userRoleService.Delete(projectId, userRoleId))
            {
                return new OkResult();
            }
            return new NotFoundObjectResult(userRoleId);
        }

        /// <summary> Updates <see cref="UserRole"/> with specified id </summary>
        /// <remarks> PUT: v1/projects/{projectId}/userroles/{userRoleId} </remarks>
        /// <returns> Id of updated UserRole </returns>
        [HttpPut("{userRoleId}")]
        public async Task<IActionResult> Put(string projectId, string userRoleId, [FromBody] UserRole userRole)
        {
            if (!_permissionService.HasProjectPermission(Permission.DeleteEditSettingsAndUsers, HttpContext))
            {
                return new ForbidResult();
            }

            // Ensure project exists
            var proj = _projectService.GetProject(projectId);
            if (proj == null)
            {
                return new NotFoundObjectResult(projectId);
            }

            var result = await _userRoleService.Update(userRoleId, userRole);
            if (result == ResultOfUpdate.NotFound)
            {
                return new NotFoundObjectResult(userRoleId);
            }
            else if (result == ResultOfUpdate.Updated)
            {
                return new OkObjectResult(userRoleId);
            }
            else
            {
                return new StatusCodeResult(304);
            }
        }
    }
}
