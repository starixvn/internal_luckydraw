// ----------------------------------------------------------------------
// <copyright file="IPrizeRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using LuckyDraw.Models.Prize;

    public interface IPrizeRepository
    {
        PrizeSettingModel GetPrizeSettingByPrizeType(ICriteria criteria);

        IEnumerable<PrizeModel> GetPrizeByPrizeType(ICriteria criteria);
    }
}