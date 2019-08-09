using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SchoolManagement.Models;


namespace SchoolManagement.ViewModels.MajorVMs
{
    public partial class MajorTemplateVM : BaseTemplateVM
    {
        [Display(Name = "专业编码")]
        public ExcelPropety MajorCode_Excel = ExcelPropety.CreateProperty<Major>(x => x.MajorCode);
        [Display(Name = "专业名称")]
        public ExcelPropety MajorName_Excel = ExcelPropety.CreateProperty<Major>(x => x.MajorName);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Major>(x => x.Remark);
        [Display(Name = "所属学校")]
        public ExcelPropety School_Excel = ExcelPropety.CreateProperty<Major>(x => x.SchoolId);
        [Display(Name = "学生")]
        public ExcelPropety StudentMajors_Excel = ExcelPropety.CreateProperty<Major>(x => x.StudentMajors);

	    protected override void InitVM()
        {
            School_Excel.DataType = ColumnDataType.ComboBox;
            School_Excel.ListItems = DC.Set<School>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.SchoolName);
            StudentMajors_Excel.DataType = ColumnDataType.ComboBox;
            StudentMajors_Excel.ListItems = DC.Set<Student>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class MajorImportVM : BaseImportVM<MajorTemplateVM, Major>
    {

    }

}
