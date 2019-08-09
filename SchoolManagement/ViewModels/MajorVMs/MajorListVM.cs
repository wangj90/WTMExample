﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Models;


namespace SchoolManagement.ViewModels.MajorVMs
{
    public partial class MajorListVM : BasePagedListVM<Major_View, MajorSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("Major", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardExportAction(null,false,ExportEnum.Excel)
            };
        }

        protected override IEnumerable<IGridColumn<Major_View>> InitGridHeader()
        {
            return new List<GridColumn<Major_View>>{
                this.MakeGridHeader(x => x.MajorCode),
                this.MakeGridHeader(x => x.MajorName),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.SchoolName_view),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Major_View> GetSearchQuery()
        {
            var query = DC.Set<Major>()
                .CheckContain(Searcher.MajorCode, x=>x.MajorCode)
                .CheckContain(Searcher.MajorName, x=>x.MajorName)
                .CheckEqual(Searcher.SchoolId, x=>x.SchoolId)
                .CheckWhere(Searcher.SelectedStudentMajorsIDs,x=>DC.Set<StudentMajor>().Where(y=>Searcher.SelectedStudentMajorsIDs.Contains(y.StudentId)).Select(z=>z.MajorId).Contains(x.ID))
                .Select(x => new Major_View
                {
				    ID = x.ID,
                    MajorCode = x.MajorCode,
                    MajorName = x.MajorName,
                    Remark = x.Remark,
                    SchoolName_view = x.School.SchoolName,
                    Name_view = DC.Set<Student>().Where(y => x.StudentMajors.Select(z => z.StudentId).Contains(y.ID)).Select(y => y.Name).ToSpratedString(null,","),
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Major_View : Major{
        [Display(Name = "学校名称")]
        public String SchoolName_view { get; set; }
        [Display(Name = "姓名")]
        public String Name_view { get; set; }

    }
}
