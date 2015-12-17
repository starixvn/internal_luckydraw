// ----------------------------------------------------------------------
// <copyright file="IStaffRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using LuckyDraw.Models.Staff;

    public interface IStaffRepository
    {
        IList<StaffModel> GetStaff(ICriteria criteria);

        StaffModel GetStaffByCode(ICriteria criteria);

        IList<StaffModel> GetWinnerByPrizeId(ICriteria criteria);
    }
}