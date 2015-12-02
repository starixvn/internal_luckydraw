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
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using LuckyDraw.Core.Services;
    using LuckyDraw.Criteria.Staff;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Repositories.Implements;
    using LuckyDraw.Repositories.Interfaces;
    using LuckyDraw.Services.Interfaces;

    public class StaffService : BaseService, IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService()
            : this(null)
        {
        }

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository ?? new StaffRepository();
        }

        public StaffModel GetStaffByCode(GetStaffByCodeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            StaffModel staff = new StaffModel();

            if (criteria.IsValid())
            {
                staff = _staffRepository.GetStaffByCode(criteria);
            }

            return staff;
        }

        public IList<StaffModel> GetWinnerByPrizeId(GetWinnerByPrizeIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IList<StaffModel> winners = new List<StaffModel>();

            if (criteria.IsValid())
            {
                winners = _staffRepository.GetWinnerByPrizeId(criteria);
            }

            return winners;
        }
    }
}