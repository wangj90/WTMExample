using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SchoolManagement.Models;


namespace SchoolManagement.ViewModels.StudentVMs
{
    public partial class StudentTemplateVM : BaseTemplateVM
    {
        [Display(Name = "姓名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Student>(x => x.Name);
        [Display(Name = "手机")]
        public ExcelPropety CellPhone_Excel = ExcelPropety.CreateProperty<Student>(x => x.CellPhone);
        [Display(Name = "邮编")]
        public ExcelPropety ZipCode_Excel = ExcelPropety.CreateProperty<Student>(x => x.ZipCode);
        [Display(Name = "日期")]
        public ExcelPropety EnRollDate_Excel = ExcelPropety.CreateProperty<Student>(x => x.EnRollDate);
        [Display(Name = "专业")]
        public ExcelPropety StudentMajor_Excel = ExcelPropety.CreateProperty<Student>(x => x.StudentMajor);

	    protected override void InitVM()
        {
            StudentMajor_Excel.DataType = ColumnDataType.ComboBox;
            StudentMajor_Excel.ListItems = DC.Set<Major>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.MajorName);
        }

    }

    public class StudentImportVM : BaseImportVM<StudentTemplateVM, Student>
    {

    }

}
