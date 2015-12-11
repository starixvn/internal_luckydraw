// ----------------------------------------------------------------------
// <copyright file="IStaffRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.23">Created.</change>
// </history>

namespace LuckyDraw.Repositories.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using LuckyDraw.Models.Department;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Repositories.Interfaces;
    using LuckyDraw.Models.Prize;

    public class StaffRepository : IStaffRepository
    {
        public StaffModel GetStaffByCode(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<StaffModel, DepartmentModel, PrizeModel, StaffModel>(
                    (staff, dept, prize) =>
                    {
                        staff.Department = dept;
                        staff.Prize = prize;

                        return staff;
                    },
                    "DepartmentId, PrizeId",
                    criteria.GetSpParams());
            }
        }

        public IList<StaffModel> GetWinnerByPrizeId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<StaffModel, DepartmentModel, PrizeModel, StaffModel>(
                    (winner, dept, prize) =>
                    {
                        winner.Department = dept;
                        winner.Prize = prize;

                        return winner;
                    },
                    "DepartmentId, PrizeId",
                    criteria.GetSpParams()).ToList();
            }
        }
    }
}