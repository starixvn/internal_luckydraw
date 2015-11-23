// ----------------------------------------------------------------------
// <copyright file="StaffService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Services.Implements
{
    using Fanex.Common.Helper;
    using LuckyDraw.Core.Services;
    using LuckyDraw.Criteria.Staff;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Services.Interfaces;

    public class StaffService : BaseService, IStaffService
    {
        public StaffModel GetEmployeeByCode(GetStaffByCodeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            StaffModel staff = new StaffModel();

            if (criteria.IsValid())
            {
                staff = GetDataEntity<StaffModel>(criteria);
            }

            return staff;
        }
    }
}