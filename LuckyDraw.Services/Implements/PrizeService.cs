// ----------------------------------------------------------------------
// <copyright file="PrizeService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Services.Implements
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using LuckyDraw.Core.Model;
    using LuckyDraw.Core.Services;
    using LuckyDraw.Criteria.Prize;
    using LuckyDraw.Models.Prize;
    using LuckyDraw.Repositories.Implements;
    using LuckyDraw.Repositories.Interfaces;
    using LuckyDraw.Services.Interfaces;

    public class PrizeService : BaseService, IPrizeService
    {
        private readonly IPrizeRepository _prizeRepository;

        public PrizeService()
            : this(null)
        {
        }

        public PrizeService(IPrizeRepository prizeRepository)
        {
            _prizeRepository = prizeRepository ?? new PrizeRepository();
        }

        public PrizeSettingModel GetPrizeSettingByPrizeType(GetPrizeSettingByPrizeTypeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            PrizeSettingModel prizeSetting = new PrizeSettingModel();

            if (criteria.IsValid())
            {
                prizeSetting = _prizeRepository.GetPrizeSettingByPrizeType(criteria);
            }

            return prizeSetting;
        }

        public IEnumerable<PrizeModel> GetPrizeByPrizeType(GetPrizeByPrizeTypeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<PrizeModel> prizes = new List<PrizeModel>();

            if (criteria.IsValid())
            {
                prizes = _prizeRepository.GetPrizeByPrizeType(criteria);
            }

            return prizes;
        }

        public CommandResult UpdateCurrentPrize(UpdateCurrentPrizeCriteria criteria)
        {
            CommandResult commandResult = new CommandResult();
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }
    }
}