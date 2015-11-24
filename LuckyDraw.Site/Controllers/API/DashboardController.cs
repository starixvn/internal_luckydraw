// ----------------------------------------------------------------------
// <copyright file="DashboardController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Site.Controllers.API
{
    using System.Web.Http;
    using LuckyDraw.Criteria.Staff;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Services.Implements;
    using LuckyDraw.Services.Interfaces;

    public class DashboardController : ApiController
    {
        private readonly IStaffService _staffService;

        public DashboardController()
            : this(null)
        {
        }

        public DashboardController(IStaffService staffService)
        {
            _staffService = staffService ?? new StaffService();
        }

        [HttpGet]
        public StaffModel GetStaffByCode(string code)
        {
            var criteria = new GetStaffByCodeCriteria()
            {
                Code = code
            };

            return _staffService.GetStaffByCode(criteria);
        }
    }
}