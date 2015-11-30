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
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using LuckyDraw.Core.Enumerations;
    using LuckyDraw.Core.Utilities;
    using LuckyDraw.Criteria.Prize;
    using LuckyDraw.Criteria.Staff;
    using LuckyDraw.Models.Prize;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Services.Implements;
    using LuckyDraw.Services.Interfaces;

    public class CommonController : ApiController
    {
        private readonly IStaffService _staffService;
        private readonly IPrizeService _prizeService;

        public CommonController()
            : this(null, null)
        {
        }

        public CommonController(
            IStaffService staffService,
            IPrizeService prizeService)
        {
            _staffService = staffService ?? new StaffService();
            _prizeService = prizeService ?? new PrizeService();
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

        [HttpGet]
        public PrizeSettingModel GetPrizeSettingByPrizeType(int prizeTypeId)
        {
            GetPrizeSettingByPrizeTypeCriteria criteria = new GetPrizeSettingByPrizeTypeCriteria()
            {
                PrizeTypeId = prizeTypeId
            };

            return _prizeService.GetPrizeSettingByPrizeType(criteria);
        }

        [HttpGet]
        public IEnumerable<PrizeModel> GetPrizeByPrizeType(int prizeTypeId)
        {
            GetPrizeByPrizeTypeCriteria criteria = new GetPrizeByPrizeTypeCriteria()
            {
                PrizeTypeId = prizeTypeId
            };

            return _prizeService.GetPrizeByPrizeType(criteria);
        }

        [HttpGet]
        public IList<object> GetPrizeType()
        {
            IList<object> prizeTypes = new List<object>();

            foreach (PrizeType prizeTypeEnum in (PrizeType[])Enum.GetValues(typeof(PrizeType)))
            {
                var priceType = new
                {
                    PrizeTypeId = (int)prizeTypeEnum,
                    Name = Enumerations.GetDescription(prizeTypeEnum)
                };

                prizeTypes.Add(priceType);
            }

            return prizeTypes;
        }

        [HttpGet]
        public IList<object> GetGrade()
        {
            IList<object> grades = new List<object>();

            foreach (Grade gradeEnum in (Grade[])Enum.GetValues(typeof(Grade)))
            {
                var grade = new
                {
                    GradeId = (int)gradeEnum,
                    Name = Enumerations.GetDescription(gradeEnum)
                };

                grades.Add(grade);
            }

            return grades;
        }
    }
}