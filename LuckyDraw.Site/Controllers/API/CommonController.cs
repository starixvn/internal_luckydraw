// ----------------------------------------------------------------------
// <copyright file="CommonController.cs" company="Nexcel Solutions Viet Nam">
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

    public class CommonController : ApiController
    {
        private readonly IStaffService _staffService;

        public CommonController()
            : this(null)
        {
        }

        public CommonController(IStaffService staffService)
        {
            _staffService = staffService ?? new StaffService();
        }

        [HttpGet]
        public StaffModel GetStaffByCode(string staffCode)
        {
            var criteria = new GetStaffByCodeCriteria()
            {
                StaffCode = staffCode
            };

            return _staffService.GetStaffByCode(criteria);
        }
    }
}