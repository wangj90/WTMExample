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
    public partial class MajorSearcher : BaseSearcher
    {
        [Display(Name = "专业编码")]
        public String MajorCode { get; set; }
        [Display(Name = "专业名称")]
        public String MajorName { get; set; }
        public List<ComboSelectListItem> AllSchools { get; set; }
        [Display(Name = "所属学校")]
        public Guid? SchoolId { get; set; }
        public List<ComboSelectListItem> AllStudentMajorss { get; set; }
        [Display(Name = "学生")]
        public List<Guid> SelectedStudentMajorsIDs { get; set; }

        protected override void InitVM()
        {
            AllSchools = DC.Set<School>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.SchoolName);
            AllStudentMajorss = DC.Set<Student>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }
}
