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
    using Fanex.Data;
    using Fanex.Data.Repository;
    using LuckyDraw.Models.Department;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Repositories.Interfaces;

    public class StaffRepository : IStaffRepository
    {
        public StaffModel GetStaffByCode(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<StaffModel, DepartmentModel, StaffModel>(
                    (staff, dept) =>
                    {
                        staff.Department = dept;

                        return staff;
                    },
                    "DepartmentId",
                    criteria.GetSpParams());
            }
        }
    }
}