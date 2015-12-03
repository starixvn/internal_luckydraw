// ----------------------------------------------------------------------
// <copyright file="IStaffService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Services.Interfaces
{
    using System.Collections.Generic;
    using LuckyDraw.Core.Model;
    using LuckyDraw.Criteria.Staff;
    using LuckyDraw.Models.Staff;

    public interface IStaffService
    {
        StaffModel GetStaffByCode(GetStaffByCodeCriteria criteria);

        IList<StaffModel> GetWinnerByPrizeId(GetWinnerByPrizeIdCriteria criteria);

        CommandResult DeleteWinner(DeleteWinnerCriteria criteria);
    }
}