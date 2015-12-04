// ----------------------------------------------------------------------
// <copyright file="InsertWinnerCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.04">Created.</change>
// </history>

namespace LuckyDraw.Criteria.Staff
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.Data.Repository;
    using LuckyDraw.Models.Staff;

    public class InsertWinnerCriteria : CriteriaBase
    {
        private const string WinnerXmlFormat = "<Staff StaffId='{0}' PrizeId='{1}'></Staff>";
        private const string BeginRootTag = "<ROOT>";
        private const string EndRootTag = "</ROOT>";

        public string WinnerDetails
        {
            get
            {
                var result = new StringBuilder(BeginRootTag);

                foreach (var winner in Winners)
                {
                    result.AppendFormat(WinnerXmlFormat, winner.StaffId, winner.Prize.PrizeId);
                }

                result.Append(EndRootTag);

                return result.ToString();
            }
        }

        [SpParam(Ignored = true)]
        public IEnumerable<StaffModel> Winners { get; set; }

        public override string GetSettingKey()
        {
            return "Winner_Insert";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}